using System;
using Dapper;
using IniciandoComDapper.Models;
using Microsoft.Data.SqlClient;

namespace IniciandoComDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CONNECTION_STRING = @"Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";

            using var connection = new SqlConnection(CONNECTION_STRING);

            // CreateNewCategory(connection);
            // UpdateCategory(connection, "aaeac888-9711-44ec-bf66-e0c22b8ce20e");
            // DeleteCategory(connection);
            // GetAllCategories(connection);
            GetCategoryById(connection, "09ce0b7b-cfca-497b-92c0-3290ad9d5142");
        }

        public static void GetAllCategories(SqlConnection connection)
        {
            var sql = @"SELECT [Id], [Title], [Summary] FROM [Category]";

            var categorias = connection.Query<Category>(sql);

            foreach (var item in categorias)
            {
                Console.WriteLine($"{item.Id} - {item.Title} - {item.Summary}");
            }
        }

        public static void GetCategoryById(SqlConnection connection, string id)
        {
            var sql = "SELECT * FROM [Category] WHERE [Id] = @Id";

            var categoryId = connection.QueryFirstOrDefault(sql,
            new
            {
                Id = id
            });

            Console.WriteLine(categoryId);
        }

        public static void CreateNewCategory(SqlConnection connection)
        {
            Category category = new Category
            {
                // Id = Guid.NewGuid(),
                Title = "Curso de Dapper com ASP.NET",
                Url = "curso-dapper",
                Summary = "Dapper e ASP.NET e SQLServer",
                Order = 1,
                Description = "DAPPER E ASP.NET",
                Featured = true,
            };

            var sql = @"INSERT INTO [Category] VALUES(NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured)";

            var newCategory = connection.Execute(sql, new
            {
                // category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{newCategory} linha inserida em Category");
        }

        public static void UpdateCategory(SqlConnection connection, string id)
        {
            var sql = @"UPDATE [Category] SET [Title]=@title, [Url]=@url, [Summary]=@summary, [Order]=@order, [Description]=@description, [Featured]=@Featured WHERE [Id]=@id";

            var rows = connection.Execute(sql, new
            {
                Id = id,
                Title = "Curso de Dapper com SQLServer e C# ASP.NET UPDATE PART. 2",
                Url = "categoria-criada-com-dapper2",
                Summary = "Dapper e asp.net é o bixoo 2",
                Order = 3,
                Description = "DAPPER E ASP.NET 2",
                Featured = true,

            });

            Console.WriteLine($"{rows} linha atualizada com sucesso");
        }

        public static void DeleteCategory(SqlConnection connection)
        {
            var sql = "DELETE FROM [Category] WHERE [Id] = @Id";

            var rows = connection.Execute(sql, new { Id = "fba9f1c9-b167-4ecc-8563-ffca6eb2e533" });

            Console.WriteLine($"{rows} linha excluída com sucesso");
        }

    }
}