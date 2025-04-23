using MySql.Data.MySqlClient;
using System.Configuration;

namespace LOJA_RN_VENDA_001.Data
{
    public class ConexaoMySQL
    {
        private string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;

        public void ExecuteQuery()
        {
            using (MySqlConnection mysql = new MySqlConnection(conexao))
            {
                mysql.Open();

                string sql = "SELECT * FROM tbprodutos;";

                using (MySqlCommand cmd = new MySqlCommand(sql, mysql))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
