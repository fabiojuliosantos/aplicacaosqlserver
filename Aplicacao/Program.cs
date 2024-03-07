using System.Data.SqlClient;

string ConnectionString = "Data Source=FABIO-PC;Initial Catalog=ByteShopDB;User ID=sa;Password=@dm1n";

#region Variáveis

string Nome = "";
string Cpf = "";
string Telefone = "";
string DataNascimento = "";
int id = 0;

#endregion Variáveis

Console.WriteLine("Selecione a opção:\n 1 - Se deseja fazer a leitura dos dados\n 2 - Se deseja cadastrar um cliente. \n 3 - Se deseja atualizar um cliente. \n 4 - Se deseja deletar um cliente.");
int Controle = Convert.ToInt32(Console.ReadLine());

if (Controle != 1 && Controle != 2 && Controle != 3 && Controle != 4)
{
    Console.WriteLine("Valor inválido!");
}

switch (Controle)
{
    case 1:
        using (SqlConnection connection = new SqlConnection(ConnectionString))
            try
            {
                {
                    connection.Open();

                    string Sql = "SELECT * FROM CLIENTES";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = Convert.ToInt32( reader["CLIENTEID"]);
                                Nome = reader["Nome"].ToString();
                                Cpf = reader["Cpf"].ToString();
                                Telefone = reader["Telefone"].ToString();
                                DataNascimento = reader["DATANASCIMENTO"].ToString();
                                Console.WriteLine($"Cliente: {Nome}, Data de Nascimento: {DataNascimento}, Cpf: {Cpf}, Telefone: {Telefone}");
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível fazer a leitura!");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        break;

    case 2:

        Console.WriteLine("Digite o Nome do cliente");
        Nome = Console.ReadLine();

        Console.WriteLine("Digite o CPF do cliente");
        Cpf = Console.ReadLine();

        Console.WriteLine("Digite o Telefone do cliente");
        Telefone = Console.ReadLine();

        Console.WriteLine("Digite a Data de Nascimento do cliente");
        DataNascimento = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            string Sql = string.Format("INSERT INTO CLIENTES (NOME, CPF, TELEFONE, DATANASCIMENTO) VALUES ('{0}', '{1}', '{2}', '{3}')", Nome, Cpf, Telefone, DataNascimento);
            try
            {
                using (SqlCommand command = new SqlCommand(Sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader());
                };
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível inserir os dados!");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        break;

    case 3:

        Console.WriteLine("Digite o número do ID do cliente que deseja editar: ");
        id = Convert.ToInt32(Console.ReadLine());

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            string SqlLeitura = string.Format("SELECT NOME FROM CLIENTES WHERE CLIENTEID = {0}", id);

            string SqlAtualizacao = string.Format("UPDATE CLIENTES SET NOME = '{0}' " +
                                                                       "CPF = '{1}' " +
                                                                       "TELEFONE = '{2}'" +
                                                                       "DATANASCIMENTO = '{3}'", Nome, Cpf, Telefone, DataNascimento);
            try
            {
                using (SqlCommand command = new SqlCommand(SqlLeitura, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nome = reader["Nome"].ToString();
                            Console.WriteLine($"O usuário selecionado é: {Nome}");
                        }
                    };

                };

                Console.WriteLine("Deseja alterar o usuário? S/N");
                string escolha = Console.ReadLine();

                if (escolha.Equals("S"))
                {
                    Console.WriteLine("Digite os novos dados do cliente:\n Nome do Cliente: ");
                    Nome = Console.ReadLine();

                    Console.WriteLine("CPF do Cliente:");
                    Cpf = Console.ReadLine();

                    Console.WriteLine("Telefone do Cliente:");
                    Telefone = Console.ReadLine();

                    Console.WriteLine("Data de Nascimento do Cliente:");
                    DataNascimento = Console.ReadLine();

                    using (SqlCommand command = new SqlCommand(SqlAtualizacao, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) ;
                    };

                    Console.WriteLine("Cliente Atualizado com sucesso!");
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        break;

    case 4:

        Console.WriteLine("Digite o número do ID do cliente que deseja deletar: ");
        id = Convert.ToInt32(Console.ReadLine());

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            string SqlLeitura = string.Format("SELECT NOME FROM CLIENTES WHERE CLIENTEID = {0}", id);

            string SqlDeletar = string.Format("DELETE CLIENTES WHERE CLIENTEID = {0}", id);

            try
            {
                using (SqlCommand command = new SqlCommand(SqlLeitura, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nome = reader["Nome"].ToString();
                            Console.WriteLine($"O usuário selecionado é: {Nome}");
                        }
                    };
                };

                Console.WriteLine("Deseja deletar o usuário? S/N");
                string escolha = Console.ReadLine();

                if (escolha.Equals("S"))
                {
                    using (SqlCommand command = new SqlCommand(SqlDeletar, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) ;
                    };

                    Console.WriteLine("Cliente deletado com sucesso!");
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        break;
}