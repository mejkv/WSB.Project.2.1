using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Entities;
using AirShop.WebApp.Pages;
using AirShop.WebApp.ShopContext;
using Document = AirShop.ExternalServices.Entities.Document;
using DocumentPosition = AirShop.ExternalServices.Entities.DocumentPosition;

namespace AirShop.WebApp.Tools
{
    public class DocumentHelper
    {
        private readonly ShopMainContext _context;
        public DocumentHelper(ShopMainContext context)
        {
            _context = context;
        }
        public Document GenerateInvoice(CheckoutInputModel checkoutInput, List<Product> orderedProducts)
        {
            if (checkoutInput == null || orderedProducts == null)
                throw new ArgumentNullException(nameof(checkoutInput));

            var document = new Document
            {
                User = _context.LoggedInUser,
                CreationTime = DateTime.Now,
                DocumentPositions = GenerateDocumentPositions(orderedProducts, checkoutInput.GenerateInvoice),
            };

            //document.DocumentPositions.ForEach(dp => dp.Document = document);
            document.Customer = GetCustomer(checkoutInput);

            if (checkoutInput.IsCompany)
                document.Customer.Nip = checkoutInput.NIP;

            return document;
        }

        private List<DocumentPosition> GenerateDocumentPositions(List<Product> orderedProducts, bool generateInvoice)
        {
            var productsGroupById = orderedProducts.GroupBy(p => p.ProductId);
            var documentPostionList = new List<DocumentPosition>();

            foreach (var productsGroup in productsGroupById)
            {
                documentPostionList.Add(CreateDocumentPosition(productsGroup));   
            }

            return documentPostionList;
        }

        private DocumentPosition CreateDocumentPosition(IGrouping<int, Product> productsGroup)
        {
            return new DocumentPosition()
            {
                ProductId = productsGroup.Key,
                Product = productsGroup.FirstOrDefault(),
                Quantity = productsGroup.Count(),
                UnitPrice = productsGroup.FirstOrDefault().Price,
                TotalPrice = productsGroup.Sum(p => p.Price),
            };
        }

        public Customer GetCustomer(CheckoutInputModel checkoutInput)
        {
            return new Customer() 
            {
                FirstName = checkoutInput.FirstName,
                LastName = checkoutInput.LastName,
                ApartmentNumber = checkoutInput.ApartmentNumber,
                City = checkoutInput.City,
                HouseNumber = checkoutInput.HouseNumber,
                //Name = checkoutInput.Name,
                PostalCode = checkoutInput.ZipCode,
                Street = checkoutInput.Street,
            };
        }
    }
}
