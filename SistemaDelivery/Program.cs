using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaDelivery.Infrastructure.Data;
using SistemaDelivery.Infrastructure.Repositorio;
using SistemaDelivery.Modelo.Interfaces;
using SistemaDelivery.Presentation.Menu.MenuFluxo;
using SistemaDelivery.Presentation.Menu.MenuGestao;
using SistemaDelivery.Servico;


IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("SistemaDelivery")!;

var services = new ServiceCollection();

services.AddDbContext<SistemaDeliveryContext>(options =>
    options.UseSqlServer(connectionString));

services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
services.AddScoped<ClienteServico>();

services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
services.AddScoped<PedidoServico>();

services.AddScoped<IItemPedidoRepositorio, ItemPedidoRepositorio>();
services.AddScoped<ItemPedidoServico>();

services.AddScoped<IPratoRepositorio, PratoRepositorio>();
services.AddScoped<PratoServico>();

services.AddScoped<IRestauranteRepositorio, RestauranteRepositorio>();
services.AddScoped<RestauranteServico>();

services.AddScoped<MenuCliente>();
services.AddScoped<MenuRestaurante>();
services.AddScoped<MenuPrato>();
services.AddScoped<MenuCompra>();
services.AddScoped<MenuFluxo>();


using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();


try
{
    var menuFluxo = scope.ServiceProvider.GetRequiredService<MenuFluxo>();
    await menuFluxo.ExibirMenuFluxoAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex}");
}

Console.WriteLine("Fim do programa. Pressione qualquer tecla...");
Console.ReadKey();





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

