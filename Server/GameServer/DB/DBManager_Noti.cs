using MySql.Data.MySqlClient;

namespace GameServer
{
    public partial class DBManager : JobSerializer
    {

        public static void TestDB()
        {
            long accountDbId = new Random().Next(0, 100);
            DateTime createDate = DateTime.UtcNow;

            using (MySqlConnection connection = new MySqlConnection(ConfigManager.Config.connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = @"INSERT INTO Hero (account_db_id, created_at) 
                                   VALUES (@account_db_id, @created_at);
                                   SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@account_db_id", accountDbId);
                        command.Parameters.AddWithValue("@created_at", createDate);

                        long heroDbId = Convert.ToInt64(command.ExecuteScalar());

                        Console.WriteLine($"New hero created with ID: {heroDbId}");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error inserting hero: {ex.Message}");
                }
            }
        }
    }
}
