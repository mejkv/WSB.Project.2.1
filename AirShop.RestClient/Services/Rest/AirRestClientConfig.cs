namespace AirShop.ExternalServices.Services.Rest
{
    public class AirRestClientConfig : IAirRestClientConfig
    {
        public string BaseUrl { get => SetBaseUrl(); }

        private string SetBaseUrl()
        {
            return $"http://localhost:5000/";
        }

    }
}