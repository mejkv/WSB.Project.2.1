using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;

namespace AirShop.ExternalServices.Services
{
    public class InvoiceTemplateService
    {
        private readonly IConfiguration _configuration;

        public InvoiceTemplateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string LoadInvoiceTemplate()
        {
            string templatePath = _configuration.GetValue<string>("TemplatePath") ?? throw new ConfigurationErrorsException("TemplatePath configuration value is missing.");

            templatePath = Path.Combine(Directory.GetCurrentDirectory(), templatePath);

            return File.Exists(templatePath) ? File.ReadAllText(templatePath) : throw new FileNotFoundException($"Template file not found: {templatePath}");
        }
    }
}
