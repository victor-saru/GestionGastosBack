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

        public List<(decimal, Participants)> participantsAnnualIncome { get; set; }
        public List<(decimal, Participants)> participantsMonthlyIncome { get; set; }
        
        public decimal  annualIncome { get; set; }
       
        public decimal  monthlyIncome { get; set; }

        public decimal annualExpenses { get; set; }
        
        public decimal monthlyExpenses { get; set; }

        public decimal annualCelarMoney { get; set; }
        
        public decimal monthlyCelarMoney { get; set; }

        public List<(decimal, Participants)> annualEqualParticipation { get; set; }
        
        public List<(decimal, Participants)> monthlyEqualParticipation { get; set; }

        public List<(decimal, Participants)> annualPercentageParticipation { get; set; }
       
        public List<(decimal, Participants)> monthlyPercentageParticipation { get; set; }

    }
}
