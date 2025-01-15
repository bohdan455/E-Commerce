namespace Offers.Options;

public class ElasticSearchOptions
{
    public const string ElasticSearch = "ElasticSearch";
    
    public string Url { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
}