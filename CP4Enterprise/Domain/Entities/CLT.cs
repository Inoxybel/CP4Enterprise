using CP4Enterprise.CrossCutting.Enums;

namespace CP4Enterprise.Domain.Entities
{
    public class CLT : Employee
    {
        public decimal Salary { get; set; }
        public bool Trusted { get; set; }

        public CLT(
            string name, 
            Gender gender, 
            decimal salary,
            bool trusted, 
            int register = 0) 
            : base(register, name, gender)
        {
            Salary = salary;
            Trusted = trusted;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nSálario: R${Salary}.\nConfiável: {Trusted}";
        }
    }
}
