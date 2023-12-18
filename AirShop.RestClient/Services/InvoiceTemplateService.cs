namespace AirShop.ExternalServices.Services
{
    public class InvoiceTemplateService
    {
        public string LoadInvoiceTemplate()
        {
            var templatePath = "C:\\Users\\fujit\\WSB.Project.2.1\\AirShop.RestClient\\Templates\\ReceiptTemplate.jrxml";

            if (File.Exists(templatePath))
            {
                return File.ReadAllText(templatePath);
            }
            else
            {
                throw new FileNotFoundException($"Template file not found: {templatePath}");
            }
        }
    }
}
