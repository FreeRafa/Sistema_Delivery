using SistemaDelivery.Presentation.Menu.MenuGestao;
using SistemaDelivery.Presentation.Menu.MenuFluxo;
using System;
using System.Threading.Tasks;

namespace SistemaDelivery.Presentation.Menu
{
    public class MenuPrincipal
    {
        private readonly MenuCliente _menuCliente;
        private readonly MenuRestaurante _menuRestaurante;
        private readonly MenuPrato _menuPrato;
        private readonly MenuCompra _menuCompra;

        public MenuPrincipal(
            MenuCliente menuCliente,
            MenuRestaurante menuRestaurante,
            MenuPrato menuPrato,
            MenuCompra menuCompra)
        {
            _menuCliente = menuCliente;
            _menuRestaurante = menuRestaurante;
            _menuPrato = menuPrato;
            _menuCompra = menuCompra;
        }

        public async Task ExibirMenuPrincipalAsync()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== SistemaDelivery — Menu de Testes ===");
                Console.WriteLine("1. Gerir Clientes");
                Console.WriteLine("2. Gerir Restaurantes");
                Console.WriteLine("3. Gerir Pratos");
                Console.WriteLine("4. Fazer Pedido");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        await _menuCliente.ExibirMenuClienteAsync();
                        break;

                    case "2":
                        await _menuRestaurante.ExibirMenuRestauranteAsync();
                        break;

                    case "3":
                        await _menuPrato.ExibirMenuPratoAsync();
                        break;

                    case "4":
                        await _menuCompra.ExibirMenuCompraAsync();
                        break;

                    case "0":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}