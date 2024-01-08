using AirShop.ExternalServices.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Receipt = AirShop.ExternalServices.Entities.Receipt;
using IHttpContextAccessor = Microsoft.AspNetCore.Http.IHttpContextAccessor;
using Microsoft.AspNetCore.Hosting;
using AirShop.DataAccess.Data.Models;

namespace AirShop.ExternalServices.Services
{
    public class ReceiptService
    {
        private readonly InvoiceTemplateService _invoiceTemplateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReceiptService(
            InvoiceTemplateService invoiceTemplateService,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _invoiceTemplateService = invoiceTemplateService;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public Receipt ReturnUserRecipt(List<Product> products)
        {
            var customer = new Customer() 
            {
                Name = "",
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            var devices = GetDevices(products);

            var receipt = CreateReceipt(customer, devices);
            
            CreatePdfDocument(receipt);

            return receipt;
        }

        private IList<Device> GetDevices(IList<Product> products)
        {
            var devices = new List<Device>();

            foreach (var p in products)
            {
                var device = new Device() 
                {
                    Name = p.Name,
                    Discount = p.Discount,
                    Price = p.Price,
                    ProductId = p.ProductId,
                    Value = p.Value,
                    Vat = p.Vat,
                };

                devices.Add(device);
            }

            return devices;
        }

        public void CreatePdfDocument(Receipt receipt)
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return;

            var invoiceTemplateContent = _invoiceTemplateService.LoadInvoiceTemplate();

            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath, "WebAppPDFs", "invoice.pdf");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var pdfWriter = new PdfWriter(stream);
                var pdf = new PdfDocument(pdfWriter);
                var document = new iText.Layout.Document(pdf);

                AddContentToPdf(document, invoiceTemplateContent, receipt);

                // Zapisz PDF w formie stringa
                pdf.Close();
            }

            var fileBytes = File.ReadAllBytes(filePath);
            _httpContextAccessor.HttpContext.Response.Clear();
            _httpContextAccessor.HttpContext.Response.ContentType = "application/pdf";
            _httpContextAccessor.HttpContext.Response.Headers.Add("Content-Disposition", $"inline; filename=invoice.pdf");
            _httpContextAccessor.HttpContext.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
            _httpContextAccessor.HttpContext.Response.Body.Flush();
            _httpContextAccessor.HttpContext.Response.Body.Close();
        }

        public Receipt CreateReceipt(Customer customer, IList<Device> devices)
        {
            return new Receipt
            {
                CustomerName = GetCustomerName(customer),
                TotalAmount = devices.Sum(d => d.Value),
                Devices = devices,
                //InvoicePdf = Convert.ToBase64String(stream.ToArray())
            };
            
        }

        private void AddContentToPdf(iText.Layout.Document document, string templateContent, Receipt invoice)
        {
            if (invoice is null)
                return;

            document.Add(new Paragraph($"Nr paragonu: {invoice.InvoiceNumber}"));
            document.Add(new Paragraph($"Klient: {invoice.CustomerName}"));
            document.Add(new Paragraph($"Suma: {invoice.TotalAmount}"));
            foreach (var device in invoice.Devices) 
            { 
                document.Add(new Paragraph($"Produkt: {device}")); 
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
