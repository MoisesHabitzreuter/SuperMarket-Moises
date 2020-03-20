using DAL.Interfaces;
using DTO;
using DTO.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class UserRepositoryADO : IUserRepository
    {
        private readonly DbOptionsADO _options;
        public UserRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }

        public async Task<UserDTO> Authenticate(string email, string passWord)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM USERS WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD ";
            command.Connection = connection;
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserDTO user = new UserDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["EMAIL"],
                                       (Permissions)reader["PERMISSIONS"],
                                       (bool)reader["ISACTIVE"]);
                    return user;
                }
                else
                {
                    return null;
                }
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

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM USERS WHERE EMAIL LIKE @EMAIL";
            command.Connection = connection;
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserDTO user = new UserDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["EMAIL"],
                                       (Permissions)reader["PERMISSIONS"],
                                       (bool)reader["ISACTIVE"]);
                    return user;
                }
                else
                {
                    return null;
                }
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

        public async Task<List<UserDTO>> GetUsers()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM USERS";
            command.Connection = connection;
            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO user = new UserDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["EMAIL"],
                                       (Permissions)reader["PERMISSIONS"],
                                       (bool)reader["ISACTIVE"]);
                    users.Add(user);
                }
                return users;
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

        public async Task Insert(UserDTO user)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO USERS (EMAIL,PASSWORD,NAME,PERMISSION) VALUES (@EMAIL,@PASSWORD,@NAME,@PERMISSION); select scope_identity()";
            command.Parameters.AddWithValue(@"EMAIL", user.Email);
            command.Parameters.AddWithValue(@"PASSWORD", user.Password);
            command.Parameters.AddWithValue(@"NAME", user.Name);
            command.Parameters.AddWithValue(@"PERMISSION", DTO.Enums.Permissions.Normal);
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

        public async Task Update(UserDTO user)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE USERS SET EMAIL = @EMAIL,PASSWORD = @PASSWORD, NAME = @NAME, ISACTIVE = @ISACTIVE WHERE ID = @ID";
            command.Parameters.AddWithValue(@"EMAIL", user.Email);
            command.Parameters.AddWithValue(@"PASSWORD", user.Password);
            command.Parameters.AddWithValue(@"NAME", user.Name);



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

