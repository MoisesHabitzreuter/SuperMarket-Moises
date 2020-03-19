using DAL.Interfaces;
using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class BrandRepositoryADO : IBrandRepository
    {
        private readonly DbOptionsADO _options;
        public BrandRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }
        public async Task<List<BrandDTO>> GetBrands()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM BRANDS";
            command.Connection = connection;
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<BrandDTO> brands = new List<BrandDTO>();
                while (reader.Read())
                {
                    //Exemplo utilizando um cast, veloz, porém perigoso
                    //em caso de migração de base
                    //string nome = (string)reader["NAME"];
                    //Criando um gênero pra representar o registro no banco
                    BrandDTO brand = new BrandDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (bool)reader["ISACTIVE"]);

                    //Adicionando o gênero na lista criada acima.
                    brands.Add(brand);
                }
                return brands;
            }
            catch (Exception ex)
            {
                //Logar o erro pro adm do sistema ter acesso.
                File.WriteAllText("log.txt", ex.Message);
                return null;
            }
            finally
            {
                connection.Dispose();
            }
        }
        public async Task Insert(BrandDTO brand)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO BRANDS (NAME, IsActive) VALUES (@NAME, @IsActive); select scope_identity()";
            command.Parameters.AddWithValue(@"NAME", brand.Name);
            command.Parameters.AddWithValue(@"IsActive", brand.IsActive);

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

        public async Task Update(BrandDTO brand)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE BRANDS SET NAME = @NAME, ISACTIVE = @ISACTIVE WHERE ID = @ID";
            command.Parameters.AddWithValue(@"NAME", brand.Name);
            command.Parameters.AddWithValue(@"ISACTIVE", brand.IsActive);


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
