using LOJA_RN_VENDA_001.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Configuration;

Console.WriteLine("##########################");
Console.WriteLine("Bem-vindo à Loja!!!");
Console.WriteLine("##########################");

while (true)
{
    Console.WriteLine("Por favor digite o seu nome: ");
    string nome = Console.ReadLine();
    Console.WriteLine("Seja Bem-vindo(a) " + nome);

    Console.WriteLine("Escolha uma das opções abaixo: ");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("1 - Cadastrar cliente");
    Console.WriteLine("2 - Buscar cliente");
    Console.WriteLine("3 - Cadastrar produto");
    Console.WriteLine("4 - Buscar produto");
    Console.WriteLine("5 - Adicionar produto ao carrinho");
    Console.WriteLine("6 - Consultar preço");
    Console.WriteLine("7 - Finalizar compra");
    Console.WriteLine("8 - Consultar Total");

    string opcao = Console.ReadLine();


    switch (opcao)
    {
        case "0":
            Console.WriteLine("Saindo... aperte uma tecla para finalizar o programa");
            Console.ReadKey();
            break;

        case "1":
            CadastrarCliente();
            Console.WriteLine("Cliente cadastrado com sucesso! ");
            break;

        case "2":
            buscarCliente();
            break;

        case "3":
            Console.WriteLine("Informe o nome do produto para cadastro...");
            string NomeProduto = Console.ReadLine();

            Console.WriteLine("Informe o preço do produto para cadastro...");
            string PrecoUnitario = Console.ReadLine();

            Console.WriteLine("Informe se o produto está em promoção. (Digite 1 ou 0)");
            string isPromocao = Console.ReadLine();

            Console.WriteLine("Informe o id do cliente para cadastro...");
            string tbClientes_idClientes = Console.ReadLine();

            Console.WriteLine("Informe o id da categoria do produto para cadastro...");
            string Categoria_idCategoria = Console.ReadLine();

            string sqlInsert = "INSERT INTO tbprodutos " +
           "(Nome, PrecoUnitario, isPromocao, tbClientes_idClientes, " +
           "Categoria_idCategoria) VALUES (@Nome, @PrecoUnitario, @isPromocao, " +
           "@tbClientes_idClientes,@Categoria_idCategoria )";

            string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
            MySqlConnection mysql = new MySqlConnection(conexao);
            mysql.Open();
            MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, mysql);

            cmdInsert.Parameters.AddWithValue("@Nome", NomeProduto);
            cmdInsert.Parameters.AddWithValue("@PrecoUnitario", PrecoUnitario);
            cmdInsert.Parameters.AddWithValue("@isPromocao", isPromocao);
            cmdInsert.Parameters.AddWithValue("@tbClientes_idClientes", tbClientes_idClientes);
            cmdInsert.Parameters.AddWithValue("@Categoria_idCategoria", Categoria_idCategoria);
            
            cmdInsert.ExecuteNonQuery();
            Console.WriteLine("Registro inserido com sucesso!");
            break;

        case "4":
            buscarProduto();
            break;

        case "5":
            adicionarProduto();
            break;
        case "6":
            break;
        case "7":
            break;
        case "8":
            Console.WriteLine("Informe o id do cliente para consulta...");
            string tbClientes_idClientes2 = Console.ReadLine();
            //string sqlBusca = "SELECT * FROM tbprodutos WHERE tbClientes_idClientes = @tbClientes_idClientes";
            string sqlBusca2 = "SELECT * FROM tbcarrinho WHERE tbClientes_idClientes = @tbClientes_idClientes";
            string conexao2 = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
            MySqlConnection mysql2 = new MySqlConnection(conexao2);
            mysql2.Open();
            MySqlCommand cmdBusca2 = new MySqlCommand(sqlBusca2, mysql2);
            cmdBusca2.Parameters.AddWithValue("@tbClientes_idClientes", tbClientes_idClientes2);
            MySqlDataReader readerBusca2 = cmdBusca2.ExecuteReader();
            if (readerBusca2.HasRows)
            {
                while (readerBusca2.Read())
                {
                    Console.WriteLine(readerBusca2["idCarrinho"].ToString() + " - " + readerBusca2["tbClientes_idClientes"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Carrinho vazio.");
            }
            break;
    }
}

void adicionarProduto()
{    
    Console.WriteLine("Digite o produto que deseja adicionar ao carrinho: ");
    string nomeProduto = Console.ReadLine();

    string sqlBusca = "SELECT * FROM tbprodutos WHERE nome LIKE @Nome";
    
    string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
 
    MySqlConnection mysql = new MySqlConnection(conexao);
    mysql.Open();

    MySqlCommand cmdBusca = new MySqlCommand(sqlBusca, mysql);
    cmdBusca.Parameters.AddWithValue("@Nome", "%" + nomeProduto + "%");
    MySqlDataReader readerBusca = cmdBusca.ExecuteReader();
    
    if (readerBusca.HasRows)
    {
        while (readerBusca.Read())
        {
            Console.WriteLine(readerBusca["idProdutos"].ToString() + " - " + readerBusca["nome"].ToString());
        }
    }
    else
    {
        Console.WriteLine("Produto não encontrado.");
    }
    readerBusca.Close();

    var consulta = 'S';

    Console.WriteLine("Deseja adicionar o produto ao carrinho? (S/N)");

    if (consulta == 'S')
    {
        Console.WriteLine("Qual a quantidade?");
        string quantidade = Console.ReadLine();

        Console.WriteLine("Informe o id do cliente para cadastro...");
        string tbClientes_idClientes = Console.ReadLine();

        string sqlInsert = "INSERT INTO tbcarrinho (tbClientes_idClientes, Quantidade) " +
            "VALUES (@tbClientes_idClientes, @Quantidade)";
        
        string conexao2 = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        
        MySqlConnection mysql2 = new MySqlConnection(conexao2);
        mysql2.Open();  

        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, mysql);
        cmdInsert.Parameters.AddWithValue("@tbClientes_idClientes", tbClientes_idClientes);
        cmdInsert.Parameters.AddWithValue("@Quantidade", quantidade);

        cmdInsert.ExecuteNonQuery();
        mysql.Close();
        Console.WriteLine("Produto adicionado ao carrinho com sucesso!");
    }
    else
    {
        Console.WriteLine("Produto não adicionado ao carrinho.");
    };   

    mysql.Close();
}

void CadastrarCliente()
{
    Console.WriteLine("Informe o nome do cliente para cadastro...");
    string nomeCliente = Console.ReadLine();
    Console.WriteLine("Informe o email do cliente para cadastro...");
    string emailCliente = Console.ReadLine();

    string sqlInsert = "INSERT INTO tbclientes (Nome, email) VALUES (@Nome, @Email)";

    string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
    MySqlConnection mysql = new MySqlConnection(conexao);
    mysql.Open();
    MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, mysql);

    cmdInsert.Parameters.AddWithValue("@Nome", nomeCliente);
    cmdInsert.Parameters.AddWithValue("@Email", emailCliente);

    cmdInsert.ExecuteNonQuery();
    mysql.Close();   
}

void buscarCliente()
{
    Console.WriteLine("Digite o nome do cliente que deseja buscar: ");

    string nomeCliente = Console.ReadLine();

    string sqlBusca = "SELECT * FROM tbclientes WHERE nome LIKE @Nome";

    string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
    MySqlConnection mysql = new MySqlConnection(conexao);
    mysql.Open();

    MySqlCommand cmdBusca = new MySqlCommand(sqlBusca, mysql);

    cmdBusca.Parameters.AddWithValue("@Nome", "%" + nomeCliente + "%");
    MySqlDataReader readerBusca = cmdBusca.ExecuteReader();

    if (readerBusca.HasRows)
    {
        while (readerBusca.Read())
        {
            Console.WriteLine(readerBusca["idClientes"].ToString() + " - " + readerBusca["nome"].ToString());
        }
    }
    else
    {
        Console.WriteLine("Cliente não encontrado.");
    }
}

void buscarProduto()
{
    Console.WriteLine("Digite o nome do produto que deseja buscar: ");

    string nomeProduto = Console.ReadLine();

    string sqlBusca = "SELECT * FROM tbprodutos WHERE nome LIKE @Nome";

    string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
    MySqlConnection mysql = new MySqlConnection(conexao);
    mysql.Open();

    MySqlCommand cmdBusca = new MySqlCommand(sqlBusca, mysql);

    cmdBusca.Parameters.AddWithValue("@Nome", "%" + nomeProduto + "%");
    MySqlDataReader readerBusca = cmdBusca.ExecuteReader();

    if (readerBusca.HasRows)
    {
        while (readerBusca.Read())
        {
            Console.WriteLine(readerBusca["idProdutos"].ToString() + " - " + readerBusca["nome"].ToString());
        }
    }
    else
    {
        Console.WriteLine("Produto não encontrado.");
    }
}













