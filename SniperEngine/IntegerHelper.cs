using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperEngine {
    public static class IntegerHelper {

        /// <summary>
        /// Round a given float to a number of decimal places
        /// </summary>
        /// <param name="f">Float to round</param>
        /// <param name="place">Number of decimal places</param>
        /// <returns>Rounded float</returns>
        public static float RoundToDecimalPlace(float f, int place) {

            f *= (float)Math.Pow(10, place);
            f = (float)Math.Round(f);
            f /= (float)Math.Pow(10, place);

            return f;
        }

    }
}
