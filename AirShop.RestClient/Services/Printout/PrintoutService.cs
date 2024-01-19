using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services.Rest;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using Document = AirShop.ExternalServices.Entities.Document;
using iTextDocument = iText.Layout.Document;

namespace AirShop.ExternalServices.Services.Printout
{
    public class PrintoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAirRestClient _restClient;

        public PrintoutService(
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IAirRestClient restClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _restClient = restClient;
        }
        public byte[] GeneratePdf(Document invoice)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath, "WebAppPDFs", "invoice.pdf");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                using (var pdfWriter = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(pdfWriter))
                    {
                        var document = new iTextDocument(pdf);
                        var font = PdfFontFactory.CreateFont();

                        DrawText(document, "Dane Sklepu:", font, 50, 50);
                        FillShopInfo(document, font);

                        DrawText(document, "Dane Odbiorcy:", font, 350, 50);

                        if (invoice.Customer != null)
                            FillCustomer(invoice, document, font);

                        DrawText(document, "Pozycje dokumentu:", font, 50, 150);

                        int yPos = 170;
                        foreach (var position in invoice.DocumentPositions)
                        {
                            FillDocumentPosition(document, font, yPos, position);
                            yPos += 60;
                        }

                        FillDocumentFooter(invoice, document, font, yPos);
                    }
                }
                var fileBytes = File.ReadAllBytes(filePath);
                _httpContextAccessor.HttpContext.Response.Clear();
                _httpContextAccessor.HttpContext.Response.ContentType = "application/pdf";
                _httpContextAccessor.HttpContext.Response.Headers.Add("Content-Disposition", $"inline; filename=invoice.pdf");
                _httpContextAccessor.HttpContext.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
                _httpContextAccessor.HttpContext.Response.Body.Flush();
                _httpContextAccessor.HttpContext.Response.Body.Close();
                return fileBytes;
            }
        }

        private void FillDocumentFooter(Document invoice, iTextDocument document, PdfFont font, int yPos)
        {
            decimal totalAmount = invoice.DocumentPositions.Sum(position => position.TotalPrice);
            string paymentMethod = "Przelew bankowy";

            DrawText(document, "Do zapłaty:", font, 50, yPos);
            DrawText(document, $"{totalAmount:C}", font, 150, yPos);

            DrawText(document, "Metoda płatności:", font, 300, yPos);
            DrawText(document, paymentMethod, font, 450, yPos);
        }

        private void FillDocumentPosition(iTextDocument document, PdfFont font, int yPos, DocumentPosition position)
        {
            string positionInfo = $"{position.Product?.Name} - Quantity: {position.Quantity} - Unit Price: {position.UnitPrice:C} - Total Price: {position.TotalPrice:C}";
            DrawText(document, positionInfo, font, 50, yPos);

            var eancode = position.Product?.Code?.Ean;

            if (eancode == null)
                throw new ExecutionEngineException("EAN code is null");

            // Replace BarcodePrinterService with the appropriate iText barcode generation code
            // BarcodePrinterService.DrawBarcode(gfx, eancode, 50, yPos + 20);
        }

        private void FillCustomer(Document invoice, iTextDocument document, PdfFont font)
        {
            if (!string.IsNullOrWhiteSpace(invoice.Customer.Name) && !string.IsNullOrWhiteSpace(invoice.Customer.Nip))
            {
                DrawText(document, invoice.Customer.Name, font, 350, 70);
                DrawText(document, $"NIP: {invoice.Customer.Nip}", font, 350, 90);
                DrawText(document, $"{invoice.Customer.City} {invoice.Customer.PostalCode}, {invoice.Customer.Street}", font, 350, 110);
            }
            else if (!string.IsNullOrWhiteSpace(invoice.Customer.FirstName) && !string.IsNullOrWhiteSpace(invoice.Customer.LastName))
            {
                DrawText(document, $"{invoice.Customer.FirstName} {invoice.Customer.LastName}", font, 350, 70);
                DrawText(document, $"{invoice.Customer.City} {invoice.Customer.PostalCode}, {invoice.Customer.Street}", font, 350, 90);
            }
        }

        private void FillShopInfo(iTextDocument document, PdfFont font)
        {
            string shopName = "AirShop Company";
            string shopNIP = "213766699";
            string shopCity = "Poznań";
            string shopPostalCode = "61-535";
            string shopStreet = "Kremowa 21/37";

            DrawText(document, shopName, font, 50, 70);
            DrawText(document, $"NIP: {shopNIP}", font, 50, 90);
            DrawText(document, $"{shopCity} {shopPostalCode}, {shopStreet}", font, 50, 110);
        }

        private void DrawText(iTextDocument document, string text, PdfFont font, float x, float y)
        {
            document.Add(new Paragraph(text).SetFont(font).SetFixedPosition(x, y, 200));
        }
    }
}
