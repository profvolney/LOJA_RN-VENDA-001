using MySql.Data.MySqlClient;
using System.Configuration;

string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;

MySqlConnection mysql = new MySqlConnection(conexao);
mysql.Open();

string sql = "INSERT INTO tbclientes (Nome, email) VALUES (@Nome, @Email)";

MySqlCommand cmd = new MySqlCommand( sql, mysql);
cmd.Parameters.AddWithValue("@Nome", "João da Silva");
cmd.Parameters.AddWithValue("@Email", "joao@email.com.br");

cmd.ExecuteNonQuery();


// comentado para não dar erro de duplicidade
Console.WriteLine("Registro inserido com sucesso!");



MySqlDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(reader["id"].ToString() + " - " + reader["nome"].ToString());
}

mysql.Close();