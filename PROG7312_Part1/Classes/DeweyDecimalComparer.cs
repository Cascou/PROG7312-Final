//------------------------------Start of DeweyDecimalComparer Class---------------------------------
//Importing Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1
{
    //------------------------------------------------------------------------------
    //Start of DeweyDecimalComparer Class Method Header, inhertis IComparer
    public class DeweyDecimalComparer: IComparer<string>
    {
        /// <summary>
        /// This method is used to split the number from the letters, and compare both the numbers and the letters from each other
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            var xParts = x.Split(' ');
            var yParts = y.Split(' ');

            var numericCompare = CompareNumbers(xParts[0], yParts[0]);

            if (numericCompare != 0)
            {
                return numericCompare;
            }

            return string.Compare(xParts[1], yParts[1], StringComparison.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// This method is used to split the two numbers from each other and compare them to one another
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int CompareNumbers(string x, string y)
        {
            var xNumbers = x.Split('.');
            var yNumbers = y.Split('.');

            var minLength = Math.Min(xNumbers.Length, yNumbers.Length);

            for (var i = 0; i < minLength; i++)
            {
                var xNumber = int.Parse(xNumbers[i]);
                var yNumber = int.Parse(yNumbers[i]);

                var numberComparison = xNumber.CompareTo(yNumber);

                if (numberComparison != 0)
                {
                    return numberComparison;
                }
            }

            return xNumbers.Length.CompareTo(yNumbers.Length);
        }
    }
}
//-----------------------------End of DeweyDecimalComparer Class-----------------------------------