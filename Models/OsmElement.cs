
using System;
namespace GeoBuyerParser2.Models;

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
