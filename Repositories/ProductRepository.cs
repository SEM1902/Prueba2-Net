using System.Data;
using Microsoft.Data.SqlClient;
using PruebaNet.Models;

namespace PruebaNet.Repositories
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<SqlConnection> GetOpenConnectionAsync()
        {
            var primaryConnection = _configuration.GetConnectionString("DefaultConnection");
            var secondaryConnection = _configuration.GetConnectionString("LocalAuthConnection");

            try
            {
                var connection = new SqlConnection(primaryConnection);
                await connection.OpenAsync();
                return connection;
            }
            catch (SqlException ex) when (ex.Number == 18456 || ex.Number == 4060)
            {
                if (!string.IsNullOrEmpty(secondaryConnection))
                {
                    var connection = new SqlConnection(secondaryConnection);
                    await connection.OpenAsync();
                    return connection;
                }
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>();
            using (var connection = await GetOpenConnectionAsync())
            {
                using (var command = new SqlCommand("sp_GetProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                                Price = reader.GetDecimal("Price"),
                                CreatedDate = reader.GetDateTime("CreatedDate")
                            });
                        }
                    }
                }
            }
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                using (var command = new SqlCommand("sp_GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Product
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                                Price = reader.GetDecimal("Price"),
                                CreatedDate = reader.GetDateTime("CreatedDate")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task CreateAsync(Product product)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                using (var command = new SqlCommand("sp_InsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", (object?)product.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Product product)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                using (var command = new SqlCommand("sp_UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", (object?)product.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                using (var command = new SqlCommand("sp_DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
