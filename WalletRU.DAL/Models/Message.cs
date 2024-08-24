using System.ComponentModel.DataAnnotations;

namespace WalletRU.DAL.Models;

public sealed class Message
{
    public int Id { get; set; }
    
    [MaxLength(125)]
    public string MessageBody { get; set; } = null!;

    public DateTime PublishedAt { get; set; } = DateTime.Now;
}