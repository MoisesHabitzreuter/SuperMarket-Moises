using Commom.Security;
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
    public class EmployeeRepositoryADO : IEmployeeRepository
    {
        private readonly DbOptionsADO _options;
        public EmployeeRepositoryADO(DbOptionsADO options)
        {
            this._options = options;
        }
        public async Task<EmployeeDTO> GetEmployeeByCPF(string cpf)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE CPF LIKE @CPF";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            if (await reader.ReadAsync())
            {
                EmployeeDTO client = new EmployeeDTO(Convert.ToInt32(reader["ID"]),
                      (string)reader["NAME"],
                      (string)reader["CPF"],
                      (string)reader["EMAIL"],
                      (string)reader["RG"],
                      (string)reader["PHONE"],
                      (DateTime)reader["DATEBIRTH"],
                      (Function)reader["FUNCTION"],
                      (bool)reader["ISACTIVE"]);
                return client;
            }
            else
            {
                return null;
            }
        }

        public async Task<EmployeeDTO> GetEmployeeByEmail(string email)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE EMAIL LIKE @EMAIL";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            if (await reader.ReadAsync())
            {
                EmployeeDTO client = new EmployeeDTO(Convert.ToInt32(reader["ID"]),
                      (string)reader["NAME"],
                      (string)reader["CPF"],
                      (string)reader["EMAIL"],
                      (string)reader["RG"],
                      (string)reader["PHONE"],
                      (DateTime)reader["DATEBIRTH"],
                      (Function)reader["FUNCTION"],
                      (bool)reader["ISACTIVE"]);
                return client;
            }
            else
            {
                return null;
            }
        }

        public async Task<EmployeeDTO> GetEmployeeByRG(string rg)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE RG LIKE @RG";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            if (await reader.ReadAsync())
            {
                EmployeeDTO client = new EmployeeDTO(Convert.ToInt32(reader["ID"]),
                      (string)reader["NAME"],
                      (string)reader["CPF"],
                      (string)reader["EMAIL"],
                      (string)reader["RG"],
                      (string)reader["PHONE"],
                      (DateTime)reader["DATEBIRTH"],
                      (Function)reader["FUNCTION"],
                      (bool)reader["ISACTIVE"]);
                return client;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM EMPLOYEES";
            command.Connection = connection;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();
                List<EmployeeDTO> employees = new List<EmployeeDTO>();
                while (reader.Read())
                {
                    EmployeeDTO employee = new EmployeeDTO(Convert.ToInt32(reader["ID"]),
                                       (string)reader["NAME"],
                                       (string)reader["EMAIL"],
                                       (string)reader["CPF"],
                                       (string)reader["RG"],
                                       (string)reader["PHONE"],
                                       (DateTime)reader["DATEBIRTH"],
                                       (Function)reader["FUNCTION"],
                                       (bool)reader["ISACTIVE"]);
                    employees.Add(employee);
                }
                return employees;
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
    
        public async Task Insert(EmployeeDTO employee)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO EMPLOYEES (NAME,EMAIL,CPF,RG,PHONE,DATEBIRTH,PASSWORD,FUNCTION) VALUES (@NAME,@EMAIL,@CPG,@RG,@PHONE,@DATEBIRTH,@PASSWORD,@FUNCTION); select scope_identity()";
            command.Parameters.AddWithValue(@"NAME", employee.Name);
            command.Parameters.AddWithValue(@"EMAIL", employee.Email);
            command.Parameters.AddWithValue(@"CPF", employee.CPF);
            command.Parameters.AddWithValue(@"RG", employee.RG);
            command.Parameters.AddWithValue(@"PHONE", employee.Phone);
            command.Parameters.AddWithValue(@"DATEBIRTH", employee.DateBirth);
            command.Parameters.AddWithValue(@"PASSWORD", employee.Password);
            command.Parameters.AddWithValue(@"FUNCTION", employee.Function);
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

        public async Task Update(EmployeeDTO employee)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _options.ConnectionString;
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE EMPLOYEES SET NOME = @NAME, EMAIL = @EMAIL, CPF = @CPF, RG = @RG, PHONE = @PHONE, DATEBIRTH = @DATEBIRTH, PASSWORD = @PASSWORD, ISACTIVE = @ISACTIVE WHERE ID = @ID";
            command.Parameters.AddWithValue(@"NAME", employee.Name);
            command.Parameters.AddWithValue(@"EMAIL", employee.Email);
            command.Parameters.AddWithValue(@"CPF", employee.CPF);
            command.Parameters.AddWithValue(@"RG", employee.RG);
            command.Parameters.AddWithValue(@"PHONE", employee.Phone);
            command.Parameters.AddWithValue(@"DATEBIRTH", employee.DateBirth);
            command.Parameters.AddWithValue(@"PASSWORD", Password.HashPassword(employee.Password));


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
