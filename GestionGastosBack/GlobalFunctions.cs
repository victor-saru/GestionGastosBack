using GestionGastosBD;
using GestionGastosBD.DTOs;
using GestionGastosBD.Models;

namespace GestionGastosBack
{
    public class GlobalFunctions
    {
        private readonly GestionGastosBDContext _context;

        public GlobalFunctions(GestionGastosBDContext context)
        {
            _context = context;

        }

        /*
        Esta función permite insertar elementos de una lista en una base de datos utilizando Entity Framework Core.
        Toma como parámetros una expresión lambda que indica la propiedad que se utilizará como clave primaria y una lista de elementos a insertar.
        Los elementos se insertarán solo si no existen en la base de datos según su valor de clave primaria.
        La función utiliza el contexto de la base de datos proporcionado y guarda los cambios en la base de datos si se insertan nuevos elementos.
        Se asume que los elementos de la lista y los objetos de la base de datos son del mismo tipo.
        */
        public void InsertItems<T>(Func<T, object> keySelector, List<T> items) where T : class
        {
            try
            {
                var keyValuesBD = _context.Set<T>()
                .Select(keySelector)
                .ToList();

                var itemsToAdd = items
                    .Where(item => !keyValuesBD.Contains(keySelector(item)))
                    .ToList();

                if (itemsToAdd.Count != 0)
                {
                    _context.AddRange(itemsToAdd);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public decimal CalculateTotalAnnualExpenses(List<Expenses> expensesList)
        {
            return expensesList.Where(item => item.final_payment == null || item.final_payment > DateTime.Now).Sum(item =>
            {
                return item.cost * Constants.paymentMethods.SingleOrDefault(x => x.name == item.id_periodicity).periodicity;
            });
        }

        public decimal CalculateTotalAnnualIncome(List<Participants> participantsList)
        {
            return participantsList.Sum(item =>
            {
                return item.net_monthly_salary * item.paymanets;
            });
        }

        public decimal CalculateTotalMonthlyIncome(List<Participants> participantsList)
        {
            return participantsList.Sum(item =>
            {
                return item.net_monthly_salary;
            });
        }

        internal List<ResultsDTO> CalculateParticipantsAnnualIncome(List<Participants> participantsList)
        {
            List<ResultsDTO> income = new List<ResultsDTO>();

            
            participantsList.ForEach(item =>
            {
                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = item.net_monthly_salary * item.paymanets;
                resultDto.participant = item;
                income.Add(resultDto);
            });

            return income;
        }

        internal List<ResultsDTO> CalculateParticipantsMonthlyIncome(List<Participants> participantsList)
        {
            List<ResultsDTO> income = new List<ResultsDTO>();

            participantsList.ForEach(item =>
            {
                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = item.net_monthly_salary;
                resultDto.participant = item;
                income.Add(resultDto);
            });

            return income;
        }

        internal (List<ResultsDTO>, List<ResultsDTO>) CalculateEqualAnnualParticipation(decimal totalAnnualExpenses, List<Participants> participantsList)
        {
            List<ResultsDTO> contribution = new List<ResultsDTO>();
            List<ResultsDTO> leftover = new List<ResultsDTO>();

            totalAnnualExpenses = 27508.77m;

            participantsList.ForEach(item =>
            {
                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = (totalAnnualExpenses / participantsList.Count);
                resultDto.participant = item;

                contribution.Add(resultDto);

                resultDto.value = (item.net_monthly_salary * item.paymanets) - (totalAnnualExpenses / participantsList.Count);

                leftover.Add(resultDto);
            });

            return (contribution, leftover);
        }

        internal (List<ResultsDTO>, List<ResultsDTO>) CalculateEqualMonthlyParticipation(decimal totalAnnualExpenses, List<Participants> participantsList)
        {
            List<ResultsDTO> contribution = new List<ResultsDTO>();
            List<ResultsDTO> leftover = new List<ResultsDTO>();

            totalAnnualExpenses = 27508.77m;

            participantsList.ForEach(item =>
            {
                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = ((totalAnnualExpenses / 12) / participantsList.Count);
                resultDto.participant = item;
                contribution.Add(resultDto);

                resultDto.value = (item.net_monthly_salary * item.paymanets) - (totalAnnualExpenses / participantsList.Count);
                leftover.Add(resultDto);
            });

            return (contribution, leftover);
        }

        internal (List<ResultsDTO>, List<ResultsDTO>) CalculatePercentageAnnualParticipation(decimal totalAnnualExpenses, List<Participants> participantsList)
        {
            totalAnnualExpenses = 27508.77m;

            List<ResultsDTO> contribution = new List<ResultsDTO>();
            List<ResultsDTO> leftover = new List<ResultsDTO>();

            var ingresosMensualPromedio = participantsList.Sum(x => x.net_monthly_salary * x.paymanets)/12;
            var resultado = ((totalAnnualExpenses / 12) * 100) / ingresosMensualPromedio;

            participantsList.ForEach((item) => 
            {
                var annualParticipation = ((resultado * (item.net_monthly_salary * item.paymanets)/12) / 100) * 12;

                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = annualParticipation;
                resultDto.participant = item;

                contribution.Add(resultDto);

                resultDto.value = (item.net_monthly_salary * item.paymanets) - annualParticipation;
                leftover.Add(resultDto);
            });

            return (contribution, leftover);
        }

        internal (List<ResultsDTO>, List<ResultsDTO>) CalculatePercentageMonthlyParticipation(decimal totalAnnualExpenses, List<Participants> participantsList)
        {
            List<ResultsDTO> contribution = new List<ResultsDTO>();
            List<ResultsDTO> leftover = new List<ResultsDTO>();

            totalAnnualExpenses = 27508.77m;

            var ingresosMensualPromedio = participantsList.Sum(x => x.net_monthly_salary * x.paymanets) / 12;
            var resultado = ((totalAnnualExpenses / 12) * 100) / ingresosMensualPromedio;

            participantsList.ForEach((item) =>
            {
                var monthlyParticipation = ((resultado * (item.net_monthly_salary * item.paymanets) / 12) / 100);

                ResultsDTO resultDto = new ResultsDTO();
                resultDto.value = monthlyParticipation;
                resultDto.participant = item;

                contribution.Add(resultDto);

                resultDto.value = (item.net_monthly_salary * item.paymanets) / 12 - monthlyParticipation;
                leftover.Add(resultDto);
            });

            return (contribution, leftover);
        }
    }
}
