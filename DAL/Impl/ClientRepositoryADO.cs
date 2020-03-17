using Commom.Security;
using DAL.Interfaces;
using DTO;
using DTO.Responses;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class ClientRepositoryADO : IClientRepository
    {
        private readonly DbOptionsADO _options;
        public ClientRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }
        public Task<List<ClientDTO>> GetClient()
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientDTO>> GetClients(int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientDTO>> GetClients()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM BRANDS";
            command.Connection = connection;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<ClientDTO> clients = new List<ClientDTO>();
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    ClientDTO client = new ClientDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["CPF"],
                                       (string)reader["EMAIL"],
                                       (string)reader["RG"],
                                       (string)reader["PHONE"],
                                       (DateTime)reader["DATEBIRTH"],
                                       (bool)reader["ISACTIVE"]);

                    //Adicionando o gênero na lista criada acima.
                    clients.Add(client);
                }
                return clients;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                DataResponse<ClientDTO> response = new DataResponse<ClientDTO>();
                response.Success = false;
                response.Errors.Add("Falha ao acessar o banco de dados, contate o suporte.");
                return response.Data;
            }
            finally
            {
                connection.Dispose();
            }
        }



        public Task<List<ClientDTO>> GetClientsPage(int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(ClientDTO client)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO CLIENTS (NAME, EMAIL, CPF, RG, PHONE, DATEBIRTH, PASSWORD) VALUES (@NAME, @EMAIL, @CPF, @RG, @PHONE, @DATEBIRTH, @PASSWORD); select scope_identity()";
            command.Parameters.AddWithValue(@"NAME", client.Name);
            command.Parameters.AddWithValue(@"EMAIL", client.Email);
            command.Parameters.AddWithValue(@"CPF", client.CPF);
            command.Parameters.AddWithValue(@"RG", client.RG);
            command.Parameters.AddWithValue(@"PHONE", client.Phone);
            command.Parameters.AddWithValue(@"DATEBIRTH", client.DateBirth);
            command.Parameters.AddWithValue(@"PASSWORD", Password.HashPassword(client.Password));
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

