using System.Data.SqlClient;

//ConnectionString usada para acessar o banco e as tabelas via ADO.NET
string ConnectionString = "Data Source=FABIO-PC;Initial Catalog=ByteShopDB;User ID=sa;Password=@dm1n";

#region Variáveis

string Nome = "";
string Cpf = "";
string Telefone = "";
string DataNascimento = "";
string ValorCompra = "";
string DataCompra = "";
string Cliente = "";
string Sql = "";
int Controle = 1;
int ModoOperacao = 1;
int id = 0;
int VendaId = 0;

#endregion Variáveis

Console.WriteLine("Selecione a opção:\n 1 - Se deseja o modo de Clientes\n 2 - Se deseja o modo de Vendas\n");
ModoOperacao = Convert.ToInt32(Console.ReadLine());

#region Clientes

if (ModoOperacao == 1)
{
    while (Controle != 0)
    {
        Console.WriteLine("\nSelecione a opção:\n 1 - Se deseja consultar todos os clientes\n 2 - Se deseja cadastrar um cliente. \n 3 - Se deseja atualizar um cliente. \n 4 - Se deseja deletar um cliente.\n 0 - Se deseja finalizar a execução.\n");
        Controle = Convert.ToInt32(Console.ReadLine());
        try
        {
            if (Controle != 1 && Controle != 2 && Controle != 3 && Controle != 4 && Controle != 0) throw new Exception("Opção Inválida!");

            switch (Controle)
            {
                #region Buscar Todos os clientes

                case 1:

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                        try
                        {
                            {
                                connection.Open();

                                Sql = "SELECT * FROM CLIENTES";

                                using (SqlCommand command = new SqlCommand(Sql, connection))
                                {
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            id = Convert.ToInt32(reader["CLIENTEID"]);
                                            Nome = reader["Nome"].ToString();
                                            Cpf = reader["Cpf"].ToString();
                                            Telefone = reader["Telefone"].ToString();
                                            DataNascimento = ((DateTime)reader["DATANASCIMENTO"]).ToString("dd/MM/yyyy");
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

                #endregion Buscar todos os clientes

                #region Cadastrar Novo Cliente

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

                        Sql = string.Format("INSERT INTO CLIENTES (NOME, CPF, TELEFONE, DATANASCIMENTO) VALUES ('{0}', '{1}', '{2}', '{3}')", Nome, Cpf, Telefone, DataNascimento);
                        try
                        {
                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader()) ;
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

                #endregion Cadastrar Novo Cliente

                #region Editar um Cliente

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

                #endregion Editar um Cliente

                #region Deletar um cliente

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

                    #endregion Deletar um Cliente
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
#endregion Clientes

#region Vendas

else if (ModoOperacao == 2)
{
    while (Controle != 0)
    {
        Console.WriteLine("\nSelecione a opção:\n 1 - Se deseja consultar todas as vendas\n 2 - Se deseja consultar todas as vendas de um cliente\n 3 - Se deseja realizar uma venda. \n 4 - Se deseja atualizar uma venda. \n 5 - Se deseja deletar uma venda.\n 0 - Se deseja finalizar a execução.\n");
        Controle = Convert.ToInt32(Console.ReadLine());
        using (SqlConnection connection = new SqlConnection(ConnectionString))

            try
            {
                if (Controle != 1 && Controle != 2 && Controle != 3 && Controle != 4 && Controle != 5 && Controle != 0) throw new Exception("Opção Inválida!");

                switch (Controle)
                {
                    #region Busca Todas as Compras
                    case 1:
                        try
                        {
                            {
                                connection.Open();

                                Sql = "SELECT V.VENDAID, V.VALOR, V.DATACOMPRA, C.NOME " +
                                " FROM VENDAS V" +
                                " INNER JOIN CLIENTES C ON V.CLIENTEID = C.CLIENTEID";

                                using (SqlCommand command = new SqlCommand(Sql, connection))
                                {
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            VendaId = Convert.ToInt32(reader["VENDAID"]);
                                            ValorCompra = reader["VALOR"].ToString();
                                            DataCompra = ((DateTime)reader["DATACOMPRA"]).ToString("dd/MM/yyyy");
                                            Cliente = reader["NOME"].ToString();

                                            Console.WriteLine($"\n Id da Compra: {VendaId}\n Valor da Compra: R$ {ValorCompra}\n Data da Compra: {DataCompra}\n Cliente: {Cliente}");
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
                    #endregion Busca Todas as Compras

                    #region Busca venda Específica

                    case 2:
                        try
                        {
                            Console.WriteLine("Informe o Id do Cliente: ");
                            id = Convert.ToInt32(Console.ReadLine());

                            connection.Open();

                            Sql = string.Format("SELECT V.VENDAID, V.VALOR, V.DATACOMPRA, C.NOME " +
                            " FROM VENDAS V" +
                            " INNER JOIN CLIENTES C ON V.CLIENTEID = C.CLIENTEID" +
                            " WHERE V.CLIENTEID = {0} ", id);

                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        VendaId = Convert.ToInt32(reader["VENDAID"]);
                                        ValorCompra = reader["VALOR"].ToString();
                                        DataCompra = ((DateTime)reader["DATACOMPRA"]).ToString("dd/MM/yyyy");
                                        Cliente = reader["NOME"].ToString();

                                        Console.WriteLine($"\n Id da Compra: {VendaId}\n Valor da Compra: R$ {ValorCompra}\n Data da Compra: {DataCompra}\n Cliente: {Cliente}");
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

                    #endregion Busca Venda Específica

                    #region Cadastrar Nova Venda

                    case 3:

                        Console.WriteLine("Digite o valor da compra");
                        ValorCompra = Console.ReadLine();

                        Console.WriteLine("Digite a data da compra");
                        DataCompra = Console.ReadLine();
                        if(DataCompra == "Hoje")
                        {
                            DateTime hoje = DateTime.Today;
                            DataCompra = hoje.ToString("yyyy-MM-dd");
                        }

                        Console.WriteLine("Digite o Id do Cliente");
                        id = Convert.ToInt32(Console.ReadLine());

                        connection.Open();

                        Sql = string.Format("INSERT INTO VENDAS (VALOR, DATACOMPRA, CLIENTEID) VALUES ('{0}', '{1}', '{2}')", ValorCompra, DataCompra, id);
                        try
                        {
                            using (SqlCommand command = new SqlCommand(Sql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader()) ;
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

                        break;

                    #endregion Cadastrar Nova Venda

                    #region Atualizar uma venda

                    case 4:
                        connection.Open();

                        Console.WriteLine("Digite o número do ID da venda que deseja editar: ");
                        VendaId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Digite os novos dados da venda:\n Valor da Venda: ");
                        ValorCompra = Console.ReadLine();

                        Console.WriteLine("Data da Venda:");
                        DataCompra = Console.ReadLine();

                        Sql = string.Format("UPDATE VENDAS SET(VALOR = {0}, DATACOMPRA = '{1}')", ValorCompra, DataCompra);

                        using (SqlCommand command = new SqlCommand(Sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader()) ;
                        };

                        Console.WriteLine("Venda Atualizada com sucesso!");

                        break;

                    #endregion Atualizar uma venda

                    #region Atualizar uma venda

                    case 5:

                        connection.Open();

                        Console.WriteLine("Digite o número do ID da venda que deseja deletar: ");
                        VendaId = Convert.ToInt32(Console.ReadLine());

                        Sql = string.Format("DELETE VENDAS WHERE VENDAID = {0}", VendaId);

                        using (SqlCommand command = new SqlCommand(Sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader()) ;
                        };

                        Console.WriteLine("Venda Deletada com sucesso!");

                        break;

                        #endregion Atualizar uma venda
                }

            }
            catch (Exception)
            {
                throw;
            }
    }
}
#endregion Vendas