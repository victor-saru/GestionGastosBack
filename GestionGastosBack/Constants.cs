using GestionGastosBD.Models;

namespace GestionGastosBack
{
    public class Constants
    {
        public static readonly PaymentMethods[] paymentMethods =
        {
            new PaymentMethods { name = "Annually", periodicity = 12},
            new PaymentMethods { name = "Monthly", periodicity = 1},
            new PaymentMethods { name = "Bimonthly", periodicity = 2},
            new PaymentMethods { name = "Quarterly", periodicity = 3},
            new PaymentMethods { name = "Semiannually", periodicity = 6},
        };
    }
}
