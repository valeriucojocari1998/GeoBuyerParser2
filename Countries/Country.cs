namespace GeoBuyerParser2.Countries;

public static class Country
{
    public static IDictionary<string, string> Config = new Dictionary<string, string>()
    {
        {"SK", "[out:json][timeout:600];\r\narea['ISO3166-1'='SK']->.boundary;\r\nnode(area.boundary)['shop'];\r\nout;" },
        {"SI", "[out:json][timeout:600];\r\narea['ISO3166-1'='SI']->.boundary;\r\nnode(area.boundary)['shop'];\r\nout;" },
        {"PL", "[out:json][timeout:600];\r\narea['ISO3166-1'='PL']->.boundary;\r\nnode(area.boundary)['shop'];\r\nout;" },
    };
}


