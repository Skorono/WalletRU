using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WalletRU.DAL.Models;

public sealed class Message
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("message_body")]
    [MaxLength(125)]
    public string MessageBody { get; set; } = null!;

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}