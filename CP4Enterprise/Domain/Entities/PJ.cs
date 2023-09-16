using CP4Enterprise.CrossCutting.Enums;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Domain.Entities
{
    public class PJ : Employee
    {
        public float HourValue { get; set; }

        public float HourWorked { get; set; }

        public string CNPJ { get; set; }

        public PJ(int register, 
                  string name, 
                  Gender gender, 
                  float hourValue, 
                  float hourWorked,
                  string cnpj) 
            : base(register, name, gender)
        {
            HourValue = hourValue;
            HourWorked = hourWorked;
            CNPJ = cnpj;
        }
    }
}
