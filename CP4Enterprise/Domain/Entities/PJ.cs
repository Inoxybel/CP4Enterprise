using CP4Enterprise.CrossCutting.Enums;

namespace CP4Enterprise.Domain.Entities
{
    public class PJ : Employee
    {
        public decimal HourValue { get; set; }
        public decimal HourWorked { get; set; }
        public string CNPJ { get; set; }

        public PJ(string name, 
                  Gender gender, 
                  decimal hourValue, 
                  decimal hourWorked,
                  string cnpj,
                  int register = 0) 
            : base(register, name, gender)
        {
            HourValue = hourValue;
            HourWorked = hourWorked;
            CNPJ = cnpj;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nValor da Hora: R${HourValue}.\nHoras Trabalhadas: {HourWorked}.\nCNPJ: {CNPJ}.";
        }
    }
}
