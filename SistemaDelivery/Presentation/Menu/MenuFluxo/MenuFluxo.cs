using System;
using System.Threading.Tasks;

namespace SistemaDelivery.Presentation.Menu.MenuGestao
{
    public class MenuFluxo
    {
        private readonly MenuCliente _menuCliente;
        private readonly MenuRestaurante _menuRestaurante;
        private readonly MenuPrato _menuPrato;

        public MenuFluxo(MenuCliente menuCliente, MenuRestaurante menuRestaurante, MenuPrato menuPrato)
        {
            _menuCliente = menuCliente;
            _menuRestaurante = menuRestaurante;
            _menuPrato = menuPrato;
        }

        public async Task ExibirMenuFluxoAsync()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== SistemaDelivery — Menu de Testes ===");
                Console.WriteLine("1. Gerir Clientes");
                Console.WriteLine("2. Gerir Restaurantes");
                Console.WriteLine("3. Gerir Pratos");
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