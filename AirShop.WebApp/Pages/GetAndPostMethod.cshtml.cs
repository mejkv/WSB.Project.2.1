using AirShop.ExternalServices;
using AirShop.WebApiPostgre.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirShop.WebApp.Pages.Shared
{
    public class GetAndPostMethodModel : PageModel
    {
        public void OnGet()
        {
        }

        public IEnumerable<Product> GetProducts()
        {
            var client = new AirRestClient(new AirRestClientConfig());
            return client.GetDevices();
        }
    }
}
