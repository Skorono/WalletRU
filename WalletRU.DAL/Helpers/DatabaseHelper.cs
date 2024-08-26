using System.Data.SqlClient;
using Npgsql;

namespace WalletRU.DAL.Helpers;

public static class DatabaseHelper
{
    public static IEnumerable<TResult> ExecuteSqlQuery<TResult>(string query, NpgsqlConnection connection,
        params SqlParameter[]? parameters)
    {
        connection.Open();
        List<TResult> result = new();

        using (var command = new NpgsqlCommand(query, connection))
        {
            if (parameters != null)
                _parseParameters(command, parameters);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    result.Add(reader.Deserialize<TResult>()!);
            }
        }

        return result;
    }

    public static void ExecuteSqlQuery(string query, NpgsqlConnection connection,
        params SqlParameter[]? parameters)
    {
        connection.Open();
        using (var command = new NpgsqlCommand(query, connection))
        {
            if (parameters != null)
                _parseParameters(command, parameters);
            command.ExecuteNonQuery();
        }
    }

    private static void _parseParameters(NpgsqlCommand command, params SqlParameter[] parameters)
    {
        foreach (var parameter in parameters)
            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
    }
}