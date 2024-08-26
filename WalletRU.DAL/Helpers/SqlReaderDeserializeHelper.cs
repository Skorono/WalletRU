using System.Text.Json;
using Npgsql;

namespace WalletRU.DAL.Helpers;

//TODO: strange name, i may rename it later...
public static class SqlReaderDeserializeHelper
{
    public static TEntity? Deserialize<TEntity>(this NpgsqlDataReader reader)
    {
        Dictionary<string, object> data = new();

        for (var i = 0; i < reader.FieldCount; i++) data.Add(reader.GetName(i), reader[i]);

        var serializedData = JsonSerializer.Serialize(data);
        return JsonSerializer.Deserialize<TEntity>(serializedData);
    }
}