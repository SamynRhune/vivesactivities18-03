using System.Net.Http.Json;
using VivesActivities.Model;

namespace VivesActivities.Sdk
{
    public class LocationSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //Find
        public async Task<IList<Location>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("VivesActivitiesApi");
            var route = "/api/locations";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var locations = await response.Content.ReadFromJsonAsync<IList<Location>>();

            if (locations is null)
            {
                return new List<Location>();
            }

            return locations;
        }

        //Get

        //Create

        //Update

        //Delete
    }
}
