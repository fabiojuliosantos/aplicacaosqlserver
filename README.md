# :computer: Aplicação de Teste para o Banco de Dados Byte Shop

>Aplicação voltada para fazer operações simples de CRUD no banco utilizado para a oficina de SQL Server.

#### :question: Como rodar a aplicação 

- Baixe o código fonte, e altere a ConnectionString para o correspondente ao seu banco
  >Data Source=SEU-SERVER;Initial Catalog=SUA-DATABASE;User ID=SEU-USER;Password=SUA-SENHA";  

- Caso use outro banco de dados que não seja o SQL Server, crie sua ConnectionString [aqui](https://www.connectionstrings.com/).
- Compile o projeto usando `dotnet run`.
- Rode o projeto usando `dotnet run`.

>Obs.: Caso queira rodar a aplicação em modo Release, em um prompt próprio, rode o comando `dotnet publish -c Release -r win-x64` e rode a aplicação exportada em `aplicacaosqlserver\Aplicacao\bin\Release\net8.0\win-x64\Aplicacao.exe`

#### :globe_with_meridians: Overview da aplicação

>A aplicação faz o Cadastro de usuários e realiza compras desses usuários, fazendo a persistência dos dados em um banco SQL Server, com as tabelas de Vendas e Clientes.


#### :raising_hand: Entidades da aplicação
- #### Vendas
  
   Coluna | Tipo
  ---- | ----
  VendaID | Int
  Valor | Decimal
  DataCompra | DateTime


- #### Clientes
  Coluna | Tipo
  ---- | ----
  ClienteID | Int
  Nome | String
  Cpf | String
  Telefone | String
  DataNascimento | DateTime

#### :computer: Funções da Aplicação
* Clientes:
  1. Cadastro de Clientes;
  2. Listagem de Clientes;
  3. Exclusão de Clientes;
  4. Atualização de Clientes.
   
* Vendas:
    1. Listagem das Vendas;
    2. Consulta de Vendas pelo Cliente;
    3. Cadastro de vendas;
    4. Atualização de vendas;
    5. Exclusão de vendas.