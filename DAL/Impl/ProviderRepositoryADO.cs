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
    public class ProviderRepositoryADO : IProviderRepository
    {
        private readonly DbOptionsADO _options;
        public ProviderRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }

        public async Task<List<ProviderDTO>> GetProviders()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PROVIDERS";
            command.Connection = connection;
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ProviderDTO> providers = new List<ProviderDTO>();
                while (reader.Read())
                {
                    ProviderDTO provider = new ProviderDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAMEFANTASY"],
                                       (string)reader["EMAIL"],
                                       (string)reader["CNPJ"],
                                       (string)reader["PHONE"],
                                       (bool)reader["ISACTIVE"]);
                    providers.Add(provider);
                }
                return providers;
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

        public async Task Insert(ProviderDTO provider)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO PROVIDERS (FANTASYNAME,EMAIL,CNPJ,PHONE) VALUES (@FANTASYNAME,@EMAIL,@CNPJ,@PHONE); select scope_identity()";
            command.Parameters.AddWithValue(@"FANTASYNAME", provider.FantasyName);
            command.Parameters.AddWithValue(@"EMAIL", provider.Email);
            command.Parameters.AddWithValue(@"CNPJ", provider.CNPJ);
            command.Parameters.AddWithValue(@"PHONE", provider.Phone);
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
