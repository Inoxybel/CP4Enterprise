using ConsoleTools;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise
{
    public class App
    {
        private readonly IMenuService _menuService;

        public App(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public static void Run(string[] args)
        {
            new ConsoleMenu()
              .Add("Exibir Fúncionarios CLT", () =>
              {
                  Console.WriteLine("\nOne");
                  Console.ReadLine();
              })
             .Add("Exibir Fúncionarios CLT", () => Console.WriteLine("Two"))
             .Add("Soma de custo Total de Fúncionarios", () => Console.WriteLine("Two"))
             .Add("Aumentar salário de fúncionario CLT", () => Console.WriteLine("Two"))
             .Add("Aumentar salário de fúncionario PJ", () => Console.WriteLine("Two"))
             .Add("Pesquisar Fúncionario", () => Console.WriteLine("Two"))
             .Add("Pesquisar Custo de Fúncionario", () => Console.WriteLine("Two"))
             .Add("Sair", ConsoleMenu.Close)
             .Configure(config =>
             {
                 config.WriteHeaderAction = () => Console.WriteLine("escolha uma opção:");
                 config.Title = "MENU EMPRESARIAL\n";
                 config.EnableWriteTitle = true;
             })
             .Show();
        }
    }
}
