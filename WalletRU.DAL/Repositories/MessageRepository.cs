using System.Data.SqlClient;
using System.Globalization;
using WalletRU.DAL.Helpers;
using WalletRU.DAL.Models;

namespace WalletRU.DAL.Repositories;

public class MessageRepository: EntityRepository<Message>
{
    public MessageRepository(string connectionString) : base(connectionString)
    {
    }

    //TODO: In theory, i can get around this with reflection or serialization/deserialization into dict, but for now there is no need to be so sophisticated.
    public override void Add(Message entity)
    {
        string sqlQuery = 
            $"INSERT INTO messages( message_body, published_at, updated_at ) VALUES ( @message_body, @published_at, @updated_at )";
        
        DatabaseHelper.ExecuteSqlQuery(sqlQuery, _connection,
        [
            new SqlParameter("message_body", entity.MessageBody),
            new SqlParameter("published_at", entity.PublishedAt),
            new SqlParameter("updated_at", entity.UpdatedAt)
        ]);
    }

    public override void Delete(int id)
    {
        string sqlQuery = "DELETE FROM messages WHERE id = @id";
        
        DatabaseHelper.ExecuteSqlQuery(sqlQuery, _connection, [new SqlParameter("id", id)]);    
    }

    public override void Update(Message entity)
    {
        string sqlQuery =
            "UPDATE messages SET message_body = @message_body, updated_at = @updated_at WHERE id = @id";
        
        DatabaseHelper.ExecuteSqlQuery(sqlQuery, _connection,
        [
            new SqlParameter("message_body", entity.MessageBody),
            new SqlParameter("updated_at", entity.UpdatedAt),
            new SqlParameter("id", entity.Id)
        ]);
    }
}