using DAL.Interfaces;
using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class CategoryRepositoryADO : ICategoryRepository
    {
        private readonly DbOptionsADO _options;
        public CategoryRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }
        public Task<List<CategoryDTO>> GetCategories()
        {
            //Objeto que se conecta a bases SQLSERVER
            SqlConnection connection = new SqlConnection();

            //Vincula a connection string ao objeto que gerencia conexões 
            connection.ConnectionString = _options.ConnectionString;

            //Objeto que realiza comandos em bases SQLSERVR
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM CATEGORIES";
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<CategoryDTO> categories = new List<CategoryDTO>();

                //Enquanto houver registros, leia!
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    CategoryDTO c = new CategoryDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NOME"],


                    //Adicionando o gênero na lista criada acima.
                    categories.Add(c);
                }
                DataResponse<Cliente> response = new DataResponse<Cliente>();
                response.Sucesso = true;
                response.Data = clientes;
                return response;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<Cliente> response = new DataResponse<Cliente>();
                response.Sucesso = false;
                response.Erros.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response;
            }
            finally
            {
                connection.Dispose();
            }
        }

        public async Task Insert(CategoryDTO category)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO CATEGORIES (NAME) VALUES (@NAME); select scope_identity()";
            command.Parameters.AddWithValue(@"NAME", category.Name);
            command.Connection = connection;
            Response response = new Response();
            try
            {
                await connection.OpenAsync();
                int idGerado = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                response.Errors.Add("Erro no banco de dados, contate o administrador!");
                File.WriteAllText("log.txt", ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
