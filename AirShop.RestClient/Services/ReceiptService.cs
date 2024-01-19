using AirShop.ExternalServices.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Receipt = AirShop.DataAccess.Data.Models.Receipt;
using IHttpContextAccessor = Microsoft.AspNetCore.Http.IHttpContextAccessor;
using Microsoft.AspNetCore.Hosting;
using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Interfaces;
using RestSharp;
using AirShop.ExternalServices.Services.Rest;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
using log4net.Core;
using Microsoft.Extensions.Logging;
using iText.Kernel.Pdf.Canvas.Draw;

namespace AirShop.ExternalServices.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly InvoiceTemplateService _invoiceTemplateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAirRestClient _restClient;
        private readonly ILogger<ReceiptService> _logger;

        public ReceiptService(
            InvoiceTemplateService invoiceTemplateService,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IAirRestClient restClient,
            ILogger<ReceiptService> logger)
        {
            _invoiceTemplateService = invoiceTemplateService;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _restClient = restClient;
            _logger = logger;
        }

        public Receipt ReturnUserReceipt(List<Product> products, Customer customer)
        {
            var receipt = CreateReceipt(customer, products);
            
            CreatePdfDocument(receipt);
            SendReceiptToDb(receipt);
            return receipt;
        }

        private void SendReceiptToDb(Receipt receipt)
        {
            try
            {
                var result = _restClient.PostReceipt(receipt);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    _logger.Log(LogLevel.Information, "Sending receipt to db completed");
            }
            catch (RestClientException e)
            {
                _logger.Log(LogLevel.Error, $"Sending receipt to db failed: {e.Message}");
            }
        }

        public void CreatePdfDocument(Receipt receipt)
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return;

            var invoiceTemplateContent = _invoiceTemplateService.LoadInvoiceTemplate();

            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var rand = new Random();
            var filePath = Path.Combine(wwwrootPath, "WebAppPDFs", $"invoice{rand.Next(1,100)}.pdf");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var pdfWriter = new PdfWriter(stream);
                var pdf = new PdfDocument(pdfWriter);
                var document = new iText.Layout.Document(pdf);

                AddContentToPdf(document, invoiceTemplateContent, receipt);

                pdf.Close();
            }

            var fileBytes = File.ReadAllBytes(filePath);
            //_httpContextAccessor.HttpContext.Response.Clear();
            //_httpContextAccessor.HttpContext.Response.ContentType = "application/pdf";
            //_httpContextAccessor.HttpContext.Response.Headers.Add("Content-Disposition", $"inline; filename=invoice.pdf");
            //_httpContextAccessor.HttpContext.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
            //_httpContextAccessor.HttpContext.Response.Body.Flush();
            //_httpContextAccessor.HttpContext.Response.Body.Close();
        }

        public Receipt CreateReceipt(Customer customer, IList<Product> products)
        {
            var receipt = new Receipt
            {
                IsSimplifiedInvoice = true,
                Vat = 23,
                Name = GetCustomerName(customer),
                Value = products.Sum(p => p.Value),
                Discount = 0,
                Price = products.Sum(p => p.Price),
            };

            receipt.ReceiptPositions = CreateReceiptPositions(receipt, products);
            return receipt;
        }

        private IList<ReceiptPosition> CreateReceiptPositions(Receipt receipt, IList<Product> products)
        {
            var receiptPositions = new List<ReceiptPosition>();
            var productsGroup = products.GroupBy(p => p.ProductId);
            foreach (var productGroup in productsGroup)
            {
                var receiptPosition = new ReceiptPosition()
                {
                    Quantity = productGroup.Count(),
                    Product = productGroup.FirstOrDefault(),
                    //Receipt = receipt,
                    TotalPrice = productGroup.Sum(p => p.Price),
                };

                receiptPositions.Add(receiptPosition);
            }
            return receiptPositions;
        }

        private void AddContentToPdf(iText.Layout.Document document, string templateContent, Receipt invoice)
        {
            if (invoice is null)
                return;
            document.Add(new Paragraph($"AirShop Company"));
            document.Add(new Paragraph($"NIP: 213766699"));
            document.Add(new Paragraph($"ul. Kremowa 21/37, Poznań 61-535"));

            document.Add(new LineSeparator(new SolidLine()));
            document.Add(new Paragraph());

            var rand = new Random();
            document.Add(new Paragraph($"Nr paragonu: {rand.Next(1000,9999)}"));
            document.Add(new Paragraph($"Klient: {invoice.Name}"));
            document.Add(new Paragraph($"Suma: {invoice.Price}"));

            document.Add(new LineSeparator(new SolidLine()));
            document.Add(new Paragraph());
            foreach (var position in invoice.ReceiptPositions) 
            { 
                document.Add(new Paragraph($"{position.Product.Name} | {position.Product.Code.Ean} | {position.Product.Vat} | {position.Product.Price}")); 
            }
        }

        private string GetCustomerName(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(customer.Name))
                return customer.Name;

            return customer.FirstName + " " + customer.LastName;
        }
    }
}
