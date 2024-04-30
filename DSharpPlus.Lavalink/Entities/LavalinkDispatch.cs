namespace DSharpPlus.Lavalink.Entities;
using Newtonsoft.Json;

internal sealed class VoiceDispatch
{
    [JsonProperty("op")]
    public int OpCode { get; set; }

    [JsonProperty("d")]
    public object Payload { get; set; }

    [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
    public int? Sequence { get; set; }

    [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
    public string EventName { get; set; }
}
