using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Interfaces;
using AirShop.ExternalServices.Services.Printout;
using AirShop.ExternalServices.Services.Rest;
using AirShop.ExternalServices.Services.Rest.RestExceptions;
using iText.Kernel.Pdf;
using log4net.Core;
using Microsoft.Extensions.Logging;
using TextDocument = iText.Layout.Document;

namespace AirShop.WebApp.Pages
{
    public class DocumentInvoiceService : IDocumentService
    {
        private readonly PrintoutService _printoutService;
        private readonly IAirRestClient _restClient;
        private readonly ILogger<DocumentInvoiceService> _logger;

        public DocumentInvoiceService(
            PrintoutService printoutService,
            IAirRestClient restClient,
            ILogger<DocumentInvoiceService> logger)
        {
            _printoutService = printoutService;
            _restClient = restClient;
            _logger = logger;
        }

        public void SaveDocument(Document invoice)
        {
            byte[] documentContent = _printoutService.GeneratePdf(invoice);
            SendInvoiceToDb(invoice);
            SaveDocumentToFile(documentContent, "Invoice.pdf");
        }

        private void SendInvoiceToDb(Document invoice)
        {
            try
            {
                var saveInvoice = _restClient.PostDocument(invoice);
                _logger.Log(LogLevel.Information, $"Saving document completed: {saveInvoice.StatusCode} {saveInvoice.Content}");
            }
            catch (RestClientException e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }
        }

        private void SaveDocumentToFile(byte[] documentContent, string fileName)
        {
            File.WriteAllBytes(fileName, documentContent);
        }
    }
}