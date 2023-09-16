using CP4Enterprise.CrossCutting.Enums;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Domain.Entities
{
    public class CLT : Employee
    {
        public float Salary { get; set; }

        public bool Trusted { get; set; }

        public CLT(int register,string name, Gender gender ,float salary, bool trusted) 
            : base(register, name, gender)
        {
            Salary = salary;
            Trusted = trusted;
        }
    }
}
