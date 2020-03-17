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

        public Task<List<BrandDTO>> GetBrands()
        {
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
    }

}
