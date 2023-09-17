using ConsoleTables;
using ConsoleTools;
using CP4Enterprise.CrossCutting.Enums;
using CP4Enterprise.Domain.Entities;
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

        public void createEmployee(EmployeeType type)
        {

            string name;
            Gender gender = Gender.Other;
            decimal salary;
            bool trusted = false;
            decimal hourValue;
            decimal hourWorked;
            string cnpj;
            Employee employee = null;

            var genderMenu = new ConsoleMenu()
               .Add("Feminino", (ConsoleMenu) => {
                   gender = Gender.Female;
                   ConsoleMenu.CloseMenu();
               })
               .Add("Masculino", (ConsoleMenu) => {
                   gender = Gender.Male;
                   ConsoleMenu.CloseMenu();
               })
               .Add("outro", (ConsoleMenu) => {
                   gender = Gender.Other;
                   ConsoleMenu.CloseMenu();
               })
               .Configure(config =>
               {
                   config.EnableWriteTitle = true;
                   config.Title = "DECLARE O GÊNERO\n";
                   config.WriteHeaderAction = () => Console.WriteLine("escolha uma opção:");
               });





            Console.WriteLine("Digite o Nome do Funcionário:");
            name = Console.ReadLine();
            genderMenu.Show();

            if (type == EmployeeType.CLT)
            {
                Console.Write("\nDigite o Salario do Funcionário:\nR$");
                salary = Convert.ToInt32(Console.ReadLine());

                while (true)
                {
                    Console.WriteLine("\nFuncionário é confiavel ?\n1-Sim\n0-Não");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        trusted = true;
                        break;
                    }
                    else if (choice == "0")
                    {
                        trusted = false;
                        break;
                    }
                    else { Console.WriteLine("Opção Invalida\n"); }

                }

                employee = new CLT(name, gender, salary, trusted);
            }
            else if (type == EmployeeType.PJ)
            {
                Console.Write("\nDigite o Valor da hora do Funcionário:\nR$");
                hourValue = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nDigite o quantas Horas esse Funcionário Trabalhou:");
                hourWorked = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nDigite o CNPJ do Funcionário:");
                cnpj = Console.ReadLine();


                employee = new PJ(name, gender, hourValue, hourWorked, cnpj);
            }


           var result = _menuService.CreateEmployee(employee);

 

            if(result.Data == true)
            {
                Console.WriteLine("\nUsuário Cadastrado com Sucesso\n\nAperte qualquer tecla para voltar...");
            }
            else
            {
                Console.WriteLine("algo deu errado");
            }
            Console.ReadKey();



        }


        public void Run(string[] args)
        {
            var subMenu = new ConsoleMenu()
                .Add("Criar Funcionário CLT", () => createEmployee(EmployeeType.CLT))
                .Add("Criar Funcionário PJ", () => createEmployee(EmployeeType.PJ))
                .Add("Voltar", ConsoleMenu.Close);

            new ConsoleMenu()
              .Add("Criar Funcionário", () => subMenu.Show())
              .Add("Exibir Funcionários CLT", () =>
              {
                  var employees = _menuService.GetAllCLTEmployees();

                  var table = new ConsoleTable("Register", "Name", "Gender", "Salary", "Trusted");

                  foreach (var employee in employees)
                  {

                      table.AddRow(employee.Register, employee.Name, employee.Gender, $"R${employee.Salary}", employee.Trusted);
                  }

                  Console.WriteLine(table.ToString());
                  Console.ReadKey();

              })
             .Add("Exibir Funcionários PJ", () => {

                 var employees = _menuService.GetAllPJEmployees();

                 var table = new ConsoleTable("Register", "Name", "Gender", "Hour Value", "Hours Worked", "CNPJ");

                 foreach (var employee in employees)
                 {

                     table.AddRow(employee.Register, employee.Name, employee.Gender, $"R${employee.HourValue}", employee.HourWorked, employee.CNPJ);
                 }

                 Console.WriteLine(table.ToString());
                 Console.ReadKey();


             })
             .Add("Soma de custo Total de Funcionários", () => {
                 var cost = _menuService.GetTotalMonthlyCost();
                 Console.WriteLine($"O custo total do Mês está no valor de:\nR${cost}");
                 Console.ReadKey();
              })
             .Add("Aumentar salário de Funcionário CLT", () => {
                 int register;
                 decimal percentage;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = Convert.ToInt32(Console.ReadLine());

                 Console.WriteLine("\nDigite o valor em Porcetagem para aumento:");
                 percentage = Convert.ToInt32(Console.ReadLine());

                 var increased = _menuService.IncreaseCLTSalaryByPercentage(register, percentage);

                 Console.WriteLine($"O Salário do Funcionário de Registro:{register}.\nAumentou para:{increased}");
                 Console.ReadKey();

             })
             .Add("Aumentar salário de Funcionário PJ", () => {
                 int register;
                 decimal hourRate;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = Convert.ToInt32(Console.ReadLine());

                 Console.Write("\nDigite o valor em R$ por hora para aumento:\nR$");
                 hourRate = Convert.ToInt32(Console.ReadLine());

                 var increased = _menuService.IncreasePJSalaryByHourlyRate(register, hourRate);

                 Console.WriteLine($"\nO Salário do Funcionário de Registro:{register}.\nAumentou para:{increased}");
                 Console.ReadKey();

             })
             .Add("Pesquisar Funcionário", () => {

                 int register;
                 

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = Convert.ToInt32(Console.ReadLine());

                 var employee = _menuService.GetEmployeeById(register);

                 
                 Console.WriteLine(employee.Data.ToString());
                 Console.ReadKey();


             })
             .Add("Pesquisar Custo de Funcionário", () => {

                 int register;


                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = Convert.ToInt32(Console.ReadLine());

                 var cost = _menuService.GetEmployeeMonthlyTotalCost(register);

                 Console.WriteLine($"\nO custo Mensal do Funcionário é:\nR${cost}");
                 Console.ReadKey();


             })
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
