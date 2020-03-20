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
    public class ProductRepositoryADO : IProductRepository
    {
        private readonly DbOptionsADO _options;
        public ProductRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }
        public async Task<List<ProductDTO>> GetProducts()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PRODUCTS";
            command.Connection = connection;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ProductDTO> products = new List<ProductDTO>();
                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["DESCRIPTION"],
                                       (int)reader["BRANDID"],
                                       (int)reader["PROVIDERID"],
                                       (double)reader["PRICE"],
                                       (bool)reader["ISACTIVE"]);
                    products.Add(product);
                }
                return products;
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

        public async Task<List<ProductDTO>> GetProductsByBrand(BrandDTO brand)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PRODUCTS WHERE BRANDID = @BRANDID";
            command.Connection = connection;
            command.Parameters.AddWithValue("@BRANDID", brand);

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ProductDTO> products = new List<ProductDTO>();
                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["DESCRIPTION"],
                                       (int)reader["BRANDID"],
                                       (int)reader["PROVIDERID"],
                                       (double)reader["PRICE"],
                                       (bool)reader["ISACTIVE"]);
                    products.Add(product);
                }
                return products;
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

        public async Task<List<ProductDTO>> GetProductsByCategory(int category)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PRODUCTS WHERE CATEGORY = @CATEGORY";
            command.Connection = connection;
            command.Parameters.AddWithValue("@CATEGORY", category);

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ProductDTO> products = new List<ProductDTO>();
                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["DESCRIPTION"],
                                       (int)reader["BRANDID"],
                                       (int)reader["PROVIDERID"],
                                       (double)reader["PRICE"],
                                       (bool)reader["ISACTIVE"]);
                    products.Add(product);
                }
                return products;
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

        public async Task<List<ProductDTO>> GetProductsByPrice(double price)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PRODUCTS WHERE PRICE <= @PRICE";
            command.Connection = connection;
            command.Parameters.AddWithValue("@PRICE", price);
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<ProductDTO> products = new List<ProductDTO>();
                while (reader.Read())
                {
                    ProductDTO product = new ProductDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["DESCRIPTION"],
                                       (int)reader["BRANDID"],
                                       (int)reader["PROVIDERID"],
                                       (double)reader["PRICE"],
                                       (bool)reader["ISACTIVE"]);
                    products.Add(product);
                }
                return products;
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

        public async Task Insert(ProductDTO product)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO PRODUCTS (DESCRIPTION,BRANDID,PRODUCTCATEGORY,PROVIDERID,PRICE) VALUES (@DESCRIPTION,@BRANDID,@PRODUCTCATEGORY,@PRICE); select scope_identity()";
            command.Parameters.AddWithValue(@"NAME", product.Description);
            command.Parameters.AddWithValue(@"EMAIL", product.BrandID);
            command.Parameters.AddWithValue(@"CPF", product.ProductCategory);
            command.Parameters.AddWithValue(@"RG", product.ProviderID);
            command.Parameters.AddWithValue(@"PHONE", product.Price);
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

        public async Task Update(ProductDTO product)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE PRODUCTS SET DESCRIPTION = @DESCRIPTION,PROVIDER = @PROVIDER,BRAND = @BRAND,PRICE = @PRICE WHERE ID = @ID";
            command.Parameters.AddWithValue(@"DESCRIPTION", product.Description);
            command.Parameters.AddWithValue(@"PROVIDER", product.Provider);
            command.Parameters.AddWithValue(@"BRAND", product.Brand);
            command.Parameters.AddWithValue(@"PRICE", product.Price);


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
