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

        public async Task<List<ClientDTO>> GetClients()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS";
            command.Connection = connection;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ClientDTO> clients = new List<ClientDTO>();
                while (reader.Read())
                {
                    ClientDTO client = new ClientDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["CPF"],
                                       (string)reader["EMAIL"],
                                       (string)reader["RG"],
                                       (string)reader["PHONE"],
                                       (DateTime)reader["DATEBIRTH"],
                                       (bool)reader["ISACTIVE"]);

                    clients.Add(client);
                }
                return clients;
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                return null;
            }
            finally
            {
                await connection.DisposeAsync();
            }
        }

        public async Task<ClientDTO> GetClientsByCPF(string cpf)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE CPF LIKE @CPF";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            if (await reader.ReadAsync())
            {
                ClientDTO client = new ClientDTO(Convert.ToInt32(reader["ID"]),
                      (string)reader["NAME"],
                      (string)reader["CPF"],
                      (string)reader["EMAIL"],
                      (string)reader["RG"],
                      (string)reader["PHONE"],
                      (DateTime)reader["DATEBIRTH"],
                      (bool)reader["ISACTIVE"]);

               return client;
            }
            else
            {
                return null;
            }
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

        public async Task Update(ClientDTO client)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE CLIENTS SET NOME = @NAME, EMAIL = EMAIL, CPF = @CPF, RG = @RG, PHONE = @PHONE, DATEBIRTH = @DATEBIRTH, PASSWORD = @PASSWORD WHERE ID = @ID";
            command.Parameters.AddWithValue(@"NAME", client.Name);
            command.Parameters.AddWithValue(@"EMAIL", client.Email);
            command.Parameters.AddWithValue(@"CPF", client.CPF);
            command.Parameters.AddWithValue(@"RG", client.RG);
            command.Parameters.AddWithValue(@"PHONE", client.Phone);
            command.Parameters.AddWithValue(@"DATEBIRTH", client.DateBirth);
            command.Parameters.AddWithValue(@"PASSWORD", Password.HashPassword(client.Password));

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

        Task<List<ClientDTO>> IClientRepository.GetClientsPage(int page, int size)
        {
            throw new NotImplementedException();
        }
    } 
}

