using Dapper;
using Infrastructure.DataModels;
using Microsoft.Data.SqlClient;
using SampleProduct.ViewModels.Products;
using System.Data;

namespace SampleProduct.Respositories
{
    public class ProductRepository
    {
        private readonly string _connection = "Data Source=DESKTOP-ONNOJQJ;Initial Catalog=ProductStore;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;";
        //private readonly IConfiguration _configuration;

        public IDbConnection CreateConnection() => new SqlConnection(_connection);
        public async Task<IEnumerable<view_product>> GetAllProducts()
        {
            var sql = $@"SELECT [product_id],
                               [product_name],
                               [price],
                               [description],
                               [date_added]
                            FROM
                               [product]
                            ORDER BY date_added DESC";

            using var connection = CreateConnection();
            return await connection.QueryAsync<view_product>(sql);
        }
        public async Task<view_product> FindProductById(Guid id)
        {
            var sql = $@"SELECT [product_id],
                               [product_name],
                               [price],
                               [description],
                               [date_added]
                            FROM
                               [product]
                            WHERE
                              [product_id]=@id";

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<view_product>(sql, new { id });
        }
        public async Task<product> Add(ProductEntryModel model)
        {
            model.Product.product_id = Guid.NewGuid().ToString();
            model.Product.date_added = DateTime.Now;
            var sql = $@"INSERT INTO [dbo].[product]
                                ([product_id],
                                 [product_name],
                                 [price],
                                 [description],
                                 [date_added])
                                VALUES
                                (@product_id,
                                 @product_name,
                                 @price,
                                 @description,
                                 @date_added)";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, model.Product);
            return model.Product;
        }
        public async Task<product> Update(product model)
        {
            var sql = $@"UPDATE[dbo].[product]
                           SET [product_id] = @product_id,
                               [product_name] = @product_name,
                               [price] = @price,
                               [description] = @description
                          WHERE
                              product_id=@product_id";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<view_product> Remove(view_product model)
        {
            var sql = $@"
                        DELETE FROM
                            [dbo].[product]
                        WHERE
                            [product_id]=@product_id";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
