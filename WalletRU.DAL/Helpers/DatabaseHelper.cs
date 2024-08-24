using System.Data.SqlClient;

namespace WalletRU.DAL.Helpers;

public static class DatabaseHelper
{
    public static IEnumerable<TResult> ExecuteSqlQuery<TResult>(string query, SqlConnection connection,
        params SqlParameter[]? parameters)
    {
        
        connection.Open();
        List<TResult> result = new();
        
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            if (parameters != null)
                _parseParameters(command, parameters);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    result.Add(reader.Deserialize<TResult>()!);
            }
        }

        return result;
    }

    public static async void ExecuteSqlQuery(string query, SqlConnection connection, 
        params SqlParameter[]? parameters)
    {
        
    }

    private static void _parseParameters(SqlCommand command, params SqlParameter[] parameters)
    {
        foreach (var parameter in parameters)
            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
    }
}