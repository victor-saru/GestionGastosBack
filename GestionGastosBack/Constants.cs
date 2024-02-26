using GestionGastosBD.Models;

namespace GestionGastosBack
{
    public class Constants
    {
        public static readonly PaymentMethods[] paymentMethods =
        {
            new PaymentMethods { name = "Annually", periodicity = 1},
            new PaymentMethods { name = "Monthly", periodicity = 12},
            new PaymentMethods { name = "Bimonthly", periodicity = 6},
            new PaymentMethods { name = "Quarterly", periodicity = 4},
            new PaymentMethods { name = "Semiannually", periodicity = 2},
        };
    }
}
