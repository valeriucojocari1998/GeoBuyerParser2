namespace GeoBuyerParser2.Countries;

public static class Country
{
    public static IDictionary<string, string> Config = new Dictionary<string, string>()
    {
        {"SK", "[out:json];area['ISO3166-1'='SK']->.boundary;" +
                        "(node(area.boundary)['shop'='yes'];" +
                        "way(area.boundary)['shop'='yes'];" +
                        "relation(area.boundary)['shop'='yes'];);out;" },

        {"SI", "[out:json];area['ISO3166-1'='SI']->.boundary;" +
                        "(node(area.boundary)['shop'='yes'];" +
                        "way(area.boundary)['shop'='yes'];" +
                        "relation(area.boundary)['shop'='yes'];);out;" },

        {"PL", "[out:json];area['ISO3166-1'='PL']->.boundary;" +
                        "(node(area.boundary)['shop'='yes'];" +
                        "way(area.boundary)['shop'='yes'];" +
                        "relation(area.boundary)['shop'='yes'];);out;" }
    };
}


