using WalletRU.DAL.Models;

namespace WalletRU.DAL.Repositories;

public class MessageRepository: EntityRepository<Message>
{
    public MessageRepository(string connectionString) : base(connectionString)
    {
    }
}