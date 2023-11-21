//------------------------------Start of DeweyDecimal Class---------------------------------
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
    //Start of DeweyDecimal Class Method Header
    class DeweyDecimal
    {
        //Declaring global objects needed in the methods below
        public LinkedList<string> myCallNumbers = new LinkedList<string>();
        private Random random = new Random();

        /// <summary>
        /// This method generates a random 3 digit number
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomNumber()
        {
            int randomNumber = random.Next(0, 1000);
            var formattedRandomNumber = randomNumber.ToString("D3");//if two digits add a zero infront

            return formattedRandomNumber;   
        }

        /// <summary>
        /// This method generates a random 3 letter Author, with consonant + vowel + consonant
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomAuthor()
        {
            //depicting the vowels and consonants for letter randomization
            string vowels = "AEIOU";
            string consonants = "BCDFGHJKLMNPQRSTVWXYZ";

            var randomString = new char[3];

            //generate first letter - consonants
            randomString[0] = consonants[random.Next(consonants.Length)];

            //generate second letter - vowel
            randomString[1] = vowels[random.Next(vowels.Length)];

            //generate third letter - consonants
            randomString[2] = consonants[random.Next(consonants.Length)];

            return new string(randomString);
        }

        /// <summary>
        /// This method generates a random call number by combining the random digits and letters together
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCallNumber()
        {
            var myFinalCallNumber = GenerateRandomNumber() + "." + GenerateRandomNumber() + " " + GenerateRandomAuthor();

            return myFinalCallNumber;
        }

        /// <summary>
        /// This method is used to generate 10 random call numbers
        /// </summary>
        public void GenerateRandomTenCallNumbers()
        {
            myCallNumbers.Clear();

            for(var i = 0; i <10; i++)
            {
                string potentialCallNumber;

                LinkedListNode<string> node;
                
                //ensures that there are not two of the same call numbers
                do
                {
                    potentialCallNumber = GenerateRandomCallNumber();
                    node = myCallNumbers.Find(potentialCallNumber);
                }
                while (node != null);

                myCallNumbers.AddLast(potentialCallNumber);                
            }
        }
    }
}
//-----------------------------End of DeweyDecimal Class-----------------------------------