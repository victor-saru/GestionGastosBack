using GestionGastosBD;
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

        public decimal CalculateTotalExpenses(List<Expenses> expensesList)
        {
            return expensesList.Where(item => item.final_payment == null || item.final_payment > DateTime.Now).Sum(item =>
            {
                return item.cost * Constants.paymentMethods.SingleOrDefault(x => x.name == item.id_periodicity).periodicity;
            });
        }
    }
}
