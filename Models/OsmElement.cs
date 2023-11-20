
namespace GeoBuyerParser2.Models;

public class OsmElement
{
    public string type { get; set; }
    public long id { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public Dictionary<string, string> tags { get; set; }
}
