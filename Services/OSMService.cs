using System.Text;
using GeoBuyerParser2.Models;
using GeoBuyerParser2.Repositories;
using Newtonsoft.Json;
using OsmSharp.Streams;

namespace GeoBuyerParser2.Services;

public class OSMService
{
    public Repository _repository;

    public OSMService(Repository repository)
    {
        _repository = repository;
    }

    public async Task<List<Location>> GetData()
    {
        try
        {
            var overpassEndpoint = "https://overpass-api.de/api/interpreter";
            var query = "[out:json];area['ISO3166-1'='SK']->.boundary;" +
                        "(node(area.boundary)['shop'='yes'];" +
                        "way(area.boundary)['shop'='yes'];" +
                        "relation(area.boundary)['shop'='yes'];);out;";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent("data=" + Uri.EscapeDataString(query));

                var response = await httpClient.PostAsync(overpassEndpoint, content);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var parsedData = ParseJsonData(json);
                _repository.InsertLocations(parsedData);
                var data = _repository.GetLocations();
                return parsedData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error making the API request: {ex.Message}");
            return new List<Location>();
        }
    }

    private List<Location> ParseJsonData(string jsonData)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<OsmResponse>(jsonData);

            // Extract and convert the elements into Location objects
            var locationData = response.elements.Select(element => Location.FromOsmElement(element)).ToList();

            return locationData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON data: {ex.Message}");
            return new List<Location>();
        }
    }
}

public class OsmResponse
{
    public List<OsmElement> elements { get; set; }
}

public class OsmElement
{
    public string type { get; set; }
    public long id { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public Dictionary<string, string> tags { get; set; }
    public string addr_city { get; set; }
    public string addr_housenumber { get; set; }
    public string addr_postcode { get; set; }
    public string addr_street { get; set; }
    public string amenity { get; set; }
    public string brand { get; set; }
    public string brand_wikidata { get; set; }
    public string brand_wikipedia { get; set; }
    public string email { get; set; }
    public string fuel_adblue { get; set; }
    public string fuel_diesel { get; set; }
    public string fuel_diesel_plus { get; set; }
    public string fuel_lpg { get; set; }
    public string fuel_octane_100 { get; set; }
    public string fuel_octane_95 { get; set; }
    public string import_ref { get; set; }
    public string name { get; set; }
    public string opening_hours { get; set; }
    public string website { get; set; }
    public string note { get; set; }
    public string compressed_air { get; set; }
    public string wheelchair { get; set; }
        // Add more properties as needed for other fields in the JSON
}