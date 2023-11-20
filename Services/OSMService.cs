#nullable enable
using GeoBuyerParser2.Countries;
using GeoBuyerParser2.Models;
using GeoBuyerParser2.Repositories;
using Newtonsoft.Json;

namespace GeoBuyerParser2.Services;

public class OSMService
{
    public Repository _repository;

    public OSMService(Repository repository)
    {
        _repository = repository;
    }

    public async Task<List<Location>> ParseData(string country)
    {
        var query = Country.Config.TryGetValue(country, out var info) ? info : null;
        if (query == null)
        {
            throw new Exception("wrong country name");
        }
            var json = await GetDataFromOverpass(query);
            var parsedData = ParseJsonData(json, country);
            _repository.InsertLocations(parsedData);
            var data = _repository.GetLocations();
            return parsedData;
    }

    public List<Location> GetData()
    {
        return _repository.GetLocations().ToList();
    }

    private async Task<string> GetDataFromOverpass(string query)
    {
        try
        {
            var overpassEndpoint = "https://overpass-api.de/api/interpreter";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent("data=" + Uri.EscapeDataString(query));

                var response = await httpClient.PostAsync(overpassEndpoint, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("error at overpass api request", ex);
        }
    }

    private List<Location> ParseJsonData(string jsonData, string country)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<OsmResponse>(jsonData);

            // Extract and convert the elements into Location objects
            var locationData = response.elements.Select(x => Location.FromOsmElement(x, country)).ToList();
            var validLocations = locationData.Where(x => x.Valid()).ToList();

            return locationData;
        }
        catch (Exception ex)
        {
            throw new Exception("error at parsing the json data", ex);
        }
    }
}

public class OsmResponse
{
    public List<OsmElement> elements { get; set; } = new List<OsmElement>();
}