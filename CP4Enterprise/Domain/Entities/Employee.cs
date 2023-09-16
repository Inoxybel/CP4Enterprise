using CP4Enterprise.CrossCutting.Enums;

namespace CP4Enterprise.Domain.Entities
{
    public abstract class Employee 
    {
        public int Register { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public Employee(int register, string name, Gender gender) 
        {
            Register = register;
            Name = name;
            Gender = gender;
        }
    }
}
