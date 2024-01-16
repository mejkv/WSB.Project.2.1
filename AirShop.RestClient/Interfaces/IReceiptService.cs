using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Entities;
using Receipt = AirShop.ExternalServices.Entities.Receipt;

namespace AirShop.ExternalServices.Interfaces
{
    public interface IReceiptService
    {
        Receipt ReturnUserReceipt(List<Product> products, Customer customer);
    }
}
