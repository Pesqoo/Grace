using Microsoft.Data.SqlClient;
using System.Data;

namespace Grace.Model.DataContext;

public static class DBManager
{
    private static readonly string _connectionString = "Server=localhost;Database=Arcadia;User Id=sa;Password=Rappelz1;TrustServerCertificate=true;";

    public static async Task<DataTable> ExecuteQueryAsync(string query)
    {
        DataTable dataTable = new DataTable();
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Failed to connect to database: {ex.Message}", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error executing query: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return dataTable;
    }

    public static async Task<int> ExecuteNonQueryAsync(string query)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost;Database=Arcadia;User Id=sa;Password=Rappelz1;TrustServerCertificate=true;"))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Failed to connect to database: {ex.Message}", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
            return -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error executing query: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }
    }
}
