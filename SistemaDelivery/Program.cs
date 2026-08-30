using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaDelivery.Infrastructure.Data;


IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("SistemaDelivery")!;

var services = new ServiceCollection();

services.AddDbContext<SistemaDeliveryContext>(options =>
    options.UseSqlServer(connectionString));

using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();








//string connectionString = config.GetConnectionString("SistemaDelivery")!;

//using (SqlConnection conn = new SqlConnection(connectionString))
//{
//    try
//    {
//        conn.Open();
//        Console.WriteLine("Ligação feita com sucesso!");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Erro ao ligar: " + ex.Message);
//    }
//}

