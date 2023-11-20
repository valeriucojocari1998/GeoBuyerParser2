namespace GeoBuyerParser2.Models;

public record Location(
    string id,
    string countryCode,
    long timestamp,
    long object_id,
    double latitude,
    double longitude,
    // Address fields
    string? address_housenumber = null,
    string? address_housename = null,
    string? address_flats = null,
    string? address_conscriptionnumber = null,
    string? address_street = null,
    string? address_place = null,
    string? address_postcode = null,
    string? address_city = null,
    string? address_country = null,
    string? address_postbox = null,
    string? address_full = null,
    string? address_hamlet = null,
    string? address_suburb = null,
    string? address_subdistrict = null,
    string? address_district = null,
    string? address_province = null,
    string? address_state = null,
    string? address_interpolation = null,
    string? address_inclusion = null,
    string? address_door = null,
    string? address_unit = null,
    string? address_floor = null,
    string? address_block = null,
    string? address_block_number = null,
    // Contact fields
    string? contact_phone = null,
    string? contact_fax = null,
    string? contact_website = null,
    string? contact_email = null,
    string? contact_url = null,
    string? contact_facebook = null,
    string? contact_wikipedia = null,
    // Location fields
    string? attribution = null,
    string? comment = null,
    string? description = null,
    string? fixme = null,
    string? image = null,
    string? note = null,
    string? source = null,
    string? name = null,
    string? brand = null,
    string? clothes = null,
    string? internet_access = null,
    string? opening_hours = null,
    string? location_operator = null,
    bool? toilets = false,
    bool? toilets_wheelchair = false,
    string? wheelchair = null,
    string? category = null)

{
    public bool Valid()
    {
        if (object_id == null) 
        {
            return false;
        }
        if (latitude == null)
        {
            return false;
        }
        if(longitude == null)
        {
            return false;
        }
        if(name == null || name == "")
        {
            return false;
        }
        if (category == null)
        {
            return false;
        }
        return true;
    }
    public static Location FromOsmElement(OsmElement osmElement, string country)
    {
        Location location = new Location(
            id: Guid.NewGuid().ToString(), 
            countryCode: country,
            timestamp: DateTime.UtcNow.Ticks, 
            object_id: osmElement.id, 
            latitude:  osmElement.lat, 
            longitude: osmElement.lon);

        foreach (var tag in osmElement.tags)
        {
            location = ProcessLocationTag(location, tag.Key, tag.Value);
        }

        return location;
    }

    private static Location ProcessLocationTag(Location location, string key, string value)
    {
        return key switch
        {
            // Address fields
            "addr:housenumber" => location with { address_housenumber = value },
            "addr:housename" => location with { address_housename = value },
            "addr:flats" => location with { address_flats = value },
            "addr:conscriptionnumber" => location with { address_conscriptionnumber = value },
            "addr:street" => location with { address_street = value },
            "addr:place" => location with { address_place = value },
            "addr:postcode" => location with { address_postcode = value },
            "addr:city" => location with { address_city = value },
            "addr:country" => location with { address_country = value },
            "addr:postbox" => location with { address_postbox = value },
            "addr:full" => location with { address_full = value },
            "addr:hamlet" => location with { address_hamlet = value },
            "addr:suburb" => location with { address_suburb = value },
            "addr:subdistrict" => location with { address_subdistrict = value },
            "addr:district" => location with { address_district = value },
            "addr:province" => location with { address_province = value },
            "addr:state" => location with { address_state = value },
            "addr:interpolation" => location with { address_interpolation = value },
            "addr:inclusion" => location with { address_inclusion = value },
            "addr:door" => location with { address_door = value },
            "addr:unit" => location with { address_unit = value },
            "addr:floor" => location with { address_floor = value },
            "addr:block" => location with { address_block = value },
            "addr:block_number" => location with { address_block_number = value },

            // Contact fields
            "contact:phone" => location with { contact_phone = value },
            "contact:fax" => location with { contact_fax = value },
            "contact:website" => location with { contact_website = value },
            "contact:email" => location with { contact_email = location.contact_email ?? value },
            "email" => location with { contact_email = location.contact_email ?? value },
            "contact:url" => location with { contact_url = location.contact_url ?? value },
            "url" => location with { contact_url = location.contact_url ?? value },
            "contact:facebook" => location with { contact_facebook = location.contact_facebook ?? value },
            "facebook" => location with { contact_facebook = location.contact_facebook ?? value },
            "contact:wikipedia" => location with { contact_wikipedia = location.contact_wikipedia ?? value },
            "wikipedia" => location with { contact_wikipedia = location.contact_wikipedia ?? value },

            // Location fields
            "description" => location with { description = value },
            "attribution" => location with { attribution = value },
            "comment" => location with { comment = value },
            "fixme" => location with { fixme = value },
            "image" => location with { image = value },
            "note" => location with { note = value },
            "source" => location with { source = value },
            "name" => location with { name = value },
            "brand" => location with { brand = value },
            "clothes" => location with { clothes = value },
            "internet_access" => location with { internet_access = value },
            "opening_hours" => location with { opening_hours = value },
            "operator" => location with { location_operator = value },
            "toilets" => location with { toilets = value == "yes" },
            "toilets:wheelchair" => location with { toilets_wheelchair = value == "yes" },
            "wheelchair" => location with { wheelchair = value },
            "shop" => location with { category = value != "yes" || value != "no" ? value : (location.category ?? null) },
            "amenity" => location with { category = value },

            _ => location
        };
    }
}
