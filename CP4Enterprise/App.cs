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

        public void CreateEmployee(EmployeeType type)
        {
            string name;
            Gender gender = Gender.Other;
            Employee employee = null;

            var genderMenu = new ConsoleMenu()
               .Add("Feminino", (ConsoleMenu) =>
               {
                   gender = Gender.Female;
                   ConsoleMenu.CloseMenu();
               })
               .Add("Masculino", (ConsoleMenu) =>
               {
                   gender = Gender.Male;
                   ConsoleMenu.CloseMenu();
               })
               .Add("Outro", (ConsoleMenu) =>
               {
                   gender = Gender.Other;
                   ConsoleMenu.CloseMenu();
               })
               .Configure(config =>
               {
                   config.EnableWriteTitle = true;
                   config.Title = "DECLARE O GÊNERO\n";
                   config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
               });

            Console.WriteLine("Digite o Nome do Funcionário:");
            name = GetNameInput();

            genderMenu.Show();

            switch (type)
            {
                case EmployeeType.CLT:
                    employee = GetCLTEmployeeData(name, gender);
                    break;

                case EmployeeType.PJ:
                    employee = GetPJEmployeeData(name, gender);
                    break;
            }

            var result = _menuService.CreateEmployee(employee);

            if (result.Data == true)
            {
                Console.WriteLine("\nUsuário Cadastrado com Sucesso\n\nAperte qualquer tecla para voltar...");
            }
            else
            {
                Console.WriteLine("algo deu errado");
            }

            Console.ReadKey();
        }

        private static Employee GetCLTEmployeeData(string name, Gender gender)
        {
            Console.Write("\nDigite o Salario do Funcionário:\nR$");
            var salary = GetDecimalInput();
            bool trusted;

            while (true)
            {
                Console.WriteLine("\nFuncionário possui cargo de confiança ?\n1-Sim\n0-Não");
                int choice = GetIntInput();

                if (choice == 1)
                {
                    trusted = true;
                    break;
                }
                else if (choice == 2)
                {
                    trusted = false;
                    break;
                }
                
                Console.WriteLine("Opção Invalida\n");
            }

            return new CLT(name, gender, salary, trusted);
        }

        private static Employee GetPJEmployeeData(string name, Gender gender)
        {
            Console.Write("\nDigite o Valor da hora do Funcionário:\nR$");
            var hourValue = GetIntInput();

            Console.WriteLine("\nDigite o quantas Horas esse Funcionário Trabalhou:");
            var hourWorked = GetIntInput();

            Console.WriteLine("\nDigite o CNPJ do Funcionário:");
            var cnpj = GetValidCNPJ();

            return new PJ(name, gender, hourValue, hourWorked, cnpj);
        }

        public void Run(string[] args)
        {
            var subMenu = new ConsoleMenu()
                .Add("Criar Funcionário CLT", () => CreateEmployee(EmployeeType.CLT))
                .Add("Criar Funcionário PJ", () => CreateEmployee(EmployeeType.PJ))
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
             .Add("Exibir Funcionários PJ", () =>
             {
                 var employees = _menuService.GetAllPJEmployees();

                 var table = new ConsoleTable("Register", "Name", "Gender", "Hour Value", "Hours Worked", "CNPJ");

                 foreach (var employee in employees)
                 {
                     table.AddRow(employee.Register, employee.Name, employee.Gender, $"R${employee.HourValue}", employee.HourWorked, employee.CNPJ);
                 }

                 Console.WriteLine(table.ToString());
                 Console.ReadKey();
             })
             .Add("Soma de custo Total de Funcionários", () =>
             {
                 var cost = _menuService.GetTotalMonthlyCost();
                 Console.WriteLine($"O custo total do Mês está no valor de:\n R${cost:C2}");
                 Console.ReadKey();
             })
             .Add("Aumentar salário de Funcionário CLT", () =>
             {
                 int register;
                 decimal percentage;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = GetIntInput();

                 Console.WriteLine("\nDigite o valor em Porcetagem para aumento:");
                 percentage = GetIntInput();

                 var increased = _menuService.IncreaseCLTSalaryByPercentage(register, percentage);

                 var finalMessage = increased == 0 ? "Não aumentou." : $"Aumentou para: {increased}";

                 Console.WriteLine($"O Salário do Funcionário de Registro:{register}.\n {finalMessage}");
                 Console.ReadKey();
             })
             .Add("Aumentar salário de Funcionário PJ", () =>
             {
                 int register;
                 decimal hourRate;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = GetIntOrNullInput();

                 Console.Write("\nDigite o valor em R$ por hora para aumento: \n");
                 hourRate = GetDecimalInput();

                 var increased = _menuService.IncreasePJSalaryByHourlyRate(hourRate, register);

                 if(increased)
                 {
                     Console.WriteLine($"\nO Salário aumentado com sucesso em: {hourRate}");
                 }
                 else
                 {
                     Console.WriteLine($"\nFalha ao aumentar a remuneração.");
                 }

                 Console.ReadKey();

             })
             .Add("Pesquisar Funcionário", () =>
             {
                 int register;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = GetIntInput();

                 var employee = _menuService.GetEmployeeById(register);

                 Console.WriteLine(employee.Data.ToString());
                 Console.ReadKey();
             })
             .Add("Pesquisar Custo de Funcionário", () =>
             {
                 int register;

                 Console.WriteLine("Digite o Registro do Funcionário:");
                 register = GetIntInput();

                 var cost = _menuService.GetEmployeeMonthlyTotalCost(register);

                 Console.WriteLine($"\nO custo Mensal do Funcionário é: \nR${cost:C2}");
                 Console.ReadKey();
             })
             .Add("Sair", ConsoleMenu.Close)
             .Configure(config =>
             {
                 config.WriteHeaderAction = () => Console.WriteLine("Escolha uma opção:");
                 config.Title = "MENU EMPRESARIAL\n";
                 config.EnableWriteTitle = true;
             })
             .Show();
        }

        private static string GetNameInput()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input) && input.Length >= 3)
                        return input;

                    Console.WriteLine("O nome deve ter 3 caracteres ou mais.");
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static int GetIntInput()
        {
            while (true)
            {
                try
                {
                    var input = Convert.ToInt32(Console.ReadLine());

                    if (input >= 0)
                        return input;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static int GetIntOrNullInput()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                        return -1;

                    var convertedInput = Convert.ToInt32(input);

                    if (convertedInput >= 0)
                        return convertedInput;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static decimal GetDecimalInput()
        {
            while (true)
            {
                try
                {
                    var input = Convert.ToDecimal(Console.ReadLine());

                    if (input > 0)
                        return input;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static string GetValidCNPJ()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Entrada vazia. Por favor, insira um CNPJ válido.");
                        continue;
                    }

                    var digitsOnly = string.Concat(input.Where(char.IsDigit));

                    if (digitsOnly.Length != 14)
                    {
                        Console.WriteLine("CNPJ inválido. O CNPJ deve conter 14 dígitos.");
                        continue;
                    }

                    if (IsValidCNPJ(digitsOnly))
                    {
                        return digitsOnly;
                    }
                    else
                    {
                        Console.WriteLine("CNPJ inválido. Por favor, insira um CNPJ válido.");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static bool IsValidCNPJ(string cnpj)
        {
            int[] firstMultiplier = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondMultiplier = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj[..12];
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * firstMultiplier[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj += digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * secondMultiplier[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cnpj.EndsWith(digit);
        }

    }
}
