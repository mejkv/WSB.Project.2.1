using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Entities;
using AirShop.ExternalServices.Services;
using AirShop.WebApp.ShopContext;
using AirShop.WebApp.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace AirShop.WebApp.Pages
{
    public class CheckoutPageModel : PageModel
    {
        private readonly ReceiptService _receiptService;
        private readonly DocumentInvoiceService _receiptDocumentService;
        private readonly DocumentHelper _helper;
        private readonly ShoppingCart _cart;

        public CheckoutPageModel(
            ReceiptService receiptService,
            DocumentInvoiceService receiptDocumentService,
            DocumentHelper helper,
            ShoppingCart cart)
        {
            _receiptService = receiptService;
            _receiptDocumentService = receiptDocumentService;
            _helper = helper;
            _cart = cart;
        }

        [BindProperty]
        public CheckoutInputModel CheckoutInput { get; set; }

        public IActionResult OnPost()
        {
            var listOfProducts = GetProductFromShoppingCart();
            
            if (CheckoutInput.GenerateInvoice)
            {
                var invoice = _helper.GenerateInvoice(CheckoutInput, listOfProducts);
                _receiptDocumentService.SaveInvoice(invoice);
                return RedirectToPage("/Index");
            }

            _receiptService.ReturnUserReceipt(listOfProducts, _helper.GetCustomer(CheckoutInput));
            return RedirectToPage("/Index");
        }

        private List<Product> GetProductFromShoppingCart()
        {
            return _cart.CartItems.ToList();
        }
    }

    public class CheckoutInputModel
    {
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [EmailAddress(ErrorMessage = "Nieprawid?owy format adresu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string HouseNumber { get; set; }

        public string ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Nieprawid?owy format kodu pocztowego. Wprowad? XX-XXX.")]
        public string ZipCode { get; set; }

        public bool GenerateInvoice { get; set; }

        public bool IsCompany { get; set; }

        public string NIP { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        public string PaymentMethod { get; set; }
    }
}
