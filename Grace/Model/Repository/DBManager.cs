using Grace.Config;
using System.Data;
using System.Data.SqlClient;

namespace Grace.Model.DataContext;

public class DBManager
{
    private readonly string _connectionString;

    public DBManager(ConfigManager configManager)
    {
        _connectionString = configManager.Config.ConnectionString;
    }

    public async Task<DataTable> ExecuteQueryAsync(string query)
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

    public async Task<int> ExecuteNonQueryAsync(string query)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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
