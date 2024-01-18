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
            {
                throw new ArgumentNullException(nameof(checkoutInput));
            }

            // Utwórz obiekt Document
            var document = new Document
            {
                User = _context.LoggedInUser,
                CreationTime = DateTime.Now,
                DocumentPositions = GenerateDocumentPositions(orderedProducts, checkoutInput.GenerateInvoice)
                // Dodaj inne pola związane z dokumentem zgodnie z potrzebą
            };

            // Jeśli generujemy fakturę, dodaj informacje o kontrahencie
            document.Customer = GetCustomer(checkoutInput);
            if (checkoutInput.IsCompany)
            {
                document.Customer.Nip = checkoutInput.NIP;
            }

            return document;
        }

        private List<DocumentPosition> GenerateDocumentPositions(List<Product> orderedProducts, bool generateInvoice)
        {
            // Utwórz listę pozycji dokumentu na podstawie zamówionych produktów
            var documentPositions = orderedProducts.Select(product => new DocumentPosition
            {
                ProductId = product.ProductId,
                Quantity = 1, // Zakładam, że każdy produkt ma ilość równą 1
                UnitPrice = product.Price,
                TotalPrice = product.Price // Zakładam, że każda pozycja ma cenę równą cenie jednostkowej
            }).ToList();

            return documentPositions;
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
