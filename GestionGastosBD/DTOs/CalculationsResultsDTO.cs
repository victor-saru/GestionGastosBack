using GestionGastosBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.DTOs
{
    public class CalculationsResultsDTO
    {
        public decimal totalAnnualExpenses { get; set; }
       
        public decimal totalMonthlyExpenses { get; set; }

        public List<ResultsDTO> participantsAnnualIncome { get; set; }
        public List<ResultsDTO> participantsMonthlyIncome { get; set; }
        
        public decimal  annualIncome { get; set; }
       
        public decimal  monthlyIncome { get; set; }

        public decimal  monthlyIncomeSamePayments { get; set; }

        public decimal annualCelarMoney { get; set; }
        
        public decimal monthlyCelarMoney { get; set; }

        public List<ResultsDTO> annualEqualParticipation { get; set; }
        
        public List<ResultsDTO> monthlyEqualParticipation { get; set; }

        public List<ResultsDTO> annualEqualParticipationLeftover { get; set; }

        public List<ResultsDTO> monthlyEqualParticipationLeftover { get; set; }

        public List<ResultsDTO> annualPercentageParticipation { get; set; }
       
        public List<ResultsDTO> monthlyPercentageParticipation { get; set; }

        public List<ResultsDTO> annualPercentageParticipationLeftover { get; set; }

        public List<ResultsDTO> monthlyPercentageParticipationLeftover { get; set; }

    }
}
