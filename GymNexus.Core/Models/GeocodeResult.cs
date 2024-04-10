using Newtonsoft.Json;

namespace GymNexus.Core.Models;

public class GeocodeResult
{
    [JsonProperty("lat")]
    public string Latitude { get; set; } = string.Empty;

    [JsonProperty("lon")]
    public string Longitude { get; set; } = string.Empty;
}