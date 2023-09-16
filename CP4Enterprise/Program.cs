using CP4Enterprise.Domain.Interfaces;
using CP4Enterprise.Services;

namespace CP4Enterprise
{
    public class Program
    {
        private readonly IMenuService service;

        public Program()
        {
            service = new MenuService();    
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Uhul");
        }
    }
}