using MySql.Data.MySqlClient;
using System.Configuration;

string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;

MySqlConnection mysql = new MySqlConnection(conexao);
mysql.Open();

string sql = "SELECT * FROM tbprodutos;";

MySqlCommand cmd = new MySqlCommand( sql, mysql);

cmd.ExecuteNonQuery();

Console.WriteLine("##########################\n");
Console.WriteLine("Bem-vindo à Loja!!!\n");
Console.WriteLine("##########################\n");


while (true)
{   

    Console.WriteLine("Por favor digite o seu nome: \n");
    string nome = Console.ReadLine();
    Console.WriteLine("Seja Bem-vindo(a) " + nome);

    Console.WriteLine("Escolha uma das opções abaixo: \n");
    Console.WriteLine("0 - Sair\n");
    Console.WriteLine("1 - Cadastrar cliente\n");
    Console.WriteLine("2 - Buscar cliente\n");
    Console.WriteLine("3 - Cadastrar produto\n");
    Console.WriteLine("4 - Buscar produto\n");
    Console.WriteLine("5 - Adicionar produto ao carrinho\n");
    Console.WriteLine("6 - Consultar preço\n");
    Console.WriteLine("7 - Finalizar compra\n");

    string opcao = Console.ReadLine();

    if (opcao == "0")
        Console.WriteLine("Saindo... aperte uma tecla para finalizar o programa");
        Console.ReadKey();      

    if (opcao == "1")
        Console.WriteLine("Informe o nome do cliente para cadastro...\n");
        string nomeCliente = Console.ReadLine();
        Console.WriteLine("Informe o email do cliente para cadastro...\n");
        string emailCliente = Console.ReadLine();

        CadastrarCliente();
        Console.WriteLine("Cliente cadastrado com sucesso!\n");

    if (opcao == "2")
        buscarCliente();
    
    if(opcao == "3")
        Console.WriteLine("Informe o nome do produto para cadastro...\n");
        string NomeProduto = Console.ReadLine();
        Console.WriteLine("Informe o preço do produto para cadastro...\n");    
        string PrecoUnitario = Console.ReadLine();    
        Console.WriteLine("Informe se o produto está em promoção (true/false)...\n");    
        string isPromocao = Console.ReadLine();
        Console.WriteLine("Informe o id do cliente para cadastro...\n");
        string tbClientes_idClientes = Console.ReadLine();
        Console.WriteLine("Informe o id da categoria do produto para cadastro...\n");
        string Categoria_idCategoria = Console.ReadLine();

        string sqlInsert = "INSERT INTO tbprodutos " +
       "(Nome, PrecoUnitario, isPromocao, tbClientes_idClientes, " +
       "Categoria_idCategoria) VALUES (@Nome, @PrecoUnitario, @isPromocao, " +
       "@tbClientes_idClientes,@Categoria_idCategoria )";

        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, mysql);

        cmdInsert.Parameters.AddWithValue("@Nome", NomeProduto);
        cmdInsert.Parameters.AddWithValue("@PrecoUnitario", PrecoUnitario);
        cmdInsert.Parameters.AddWithValue("@isPromocao", isPromocao);
        cmdInsert.Parameters.AddWithValue("@tbClientes_idClientes", tbClientes_idClientes);
        cmdInsert.Parameters.AddWithValue("@Categoria_idCategoria", Categoria_idCategoria);

        Console.WriteLine("Registro inserido com sucesso!");


    if (opcao == "4")
    {
        buscarProduto();
    }


    void CadastrarCliente()
    {
        string sqlInsert = "INSERT INTO tbclientes (Nome, email) VALUES (@Nome, @Email)";

        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, mysql);

        cmdInsert.Parameters.AddWithValue("@Nome", nomeCliente);
        cmdInsert.Parameters.AddWithValue("@Email", emailCliente);

        Console.WriteLine("Registro inserido com sucesso!");
    }


    void buscarProduto()
    {
        Console.WriteLine("Digite o nome do produto que deseja buscar: ");

        string nomeProduto = Console.ReadLine();

        string sqlBusca = "SELECT * FROM tbprodutos WHERE nome LIKE @Nome";

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





    var consulta = 'S';

    Console.WriteLine("Deseja adicionar o produto ao carrinho? (S/N)");

    if (consulta == 'S')
    {
        string carrinho = Console.ReadLine();
    }
    else 
    {
        Console.WriteLine("Produto não adicionado ao carrinho.");
    };

        Console.WriteLine("Consultar preço...");

    //string sql = "UPDATE tbclientes SET Nome = @Nome WHERE idClientes = @idClientes";


    mysql.Close();
}

void buscarCliente()
{
    Console.WriteLine("Digite o nome do cliente que deseja buscar: ");

    string nomeCliente = Console.ReadLine();

    string sqlBusca = "SELECT * FROM tbclientes WHERE nome LIKE @Nome";

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