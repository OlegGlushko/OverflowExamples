using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowDemo1
{
    public class SimpleOverflow
    {
        private long Amount = 100;

        public void ConstructAmount()
        {


            // Compilation error as compiler can check the range
            AddToAmount(long.MaxValue + 1);
            
            // No compilation error as the data types fit
            AddToAmount(long.MaxValue);

            // Unchecked keyword can be used to supress overflow-checking for integral type arithmetic operations and conversions
            unchecked
            {
                AddToAmount(long.MaxValue + 1);
            };

            checked
            {
                // execute code here that might cause an overflow

                unchecked
                {
                    // execute code here for which performance is a priority
                }
            }
        }

        public void AddToAmount(long amount)
        {
            // 100 + max value will overflow the long
            Amount += amount;
        }

    }
}
