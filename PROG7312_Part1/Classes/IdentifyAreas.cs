//------------------------------Start of IdentifyAreas Class---------------------------------
//Importing Libraries
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1.Classes
{
    //------------------------------------------------------------------------------
    //Start of IdentifyAreas Class Method Header
    class IdentifyAreas
    {
        //Setting up configuration file
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //Declaring global objects needed in the methods below
        private Random random = new Random();
        public Dictionary<string, string> myDictionary = new Dictionary<string, string>();
      
        

        //Declaring categories for the call numbers
        private string categoryOne = "General Information";
        private string categoryTwo = "Philosophy and Psychology";
        private string categoryThree = "Religion";
        private string categoryFour = "Social Sciences";
        private string categoryFive = "Language";
        private string categorySix = "Science";
        private string categorySeven = "Technology";
        private string categoryEight = "Arts and Recreation";
        private string categoryNine = "Literature";
        private string categoryTen = "History and Geography";

        /// <summary>
        /// This method generates a random 3 digit number for the first category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryOne()
        {
            int randomNumber = random.Next(0, 100);
            var formattedRandomNumber = randomNumber.ToString("D3");//if two digits add a zero infront

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the second category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryTwo()
        {
            int randomNumber = random.Next(100, 200);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the third category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryThree()
        {
            int randomNumber = random.Next(200, 300);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the fourth category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryFour()
        {
            int randomNumber = random.Next(300, 400);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the fifth category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryFive()
        {
            int randomNumber = random.Next(400, 500);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the sixth category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategorySix()
        {
            int randomNumber = random.Next(500, 600);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the seventh category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategorySeven()
        {
            int randomNumber = random.Next(600, 700);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the eight category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryEight()
        {
            int randomNumber = random.Next(700, 800);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the ninth category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryNine()
        {
            int randomNumber = random.Next(800, 900);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method generates a random 3 digit number for the tenth category
        /// </summary>
        /// <returns></returns>
        private string GenerateCategoryTen()
        {
            int randomNumber = random.Next(900, 1000);
            var formattedRandomNumber = randomNumber.ToString("D3");

            return formattedRandomNumber;
        }

        /// <summary>
        /// This method is used to generate a dictionay of value pairs for the 10 categories
        /// </summary>
        public void GenerateRandomTenDictionary()
        {
            myDictionary.Clear();

            int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCounter"]);

            if (myCounter % 2 != 0)
            {
                myDictionary.Add(GenerateCategoryOne(), categoryOne);
                myDictionary.Add(GenerateCategoryTwo(), categoryTwo);
                myDictionary.Add(GenerateCategoryThree(), categoryThree);
                myDictionary.Add(GenerateCategoryFour(), categoryFour);
                myDictionary.Add(GenerateCategoryFive(), categoryFive);
                myDictionary.Add(GenerateCategorySix(), categorySix);
                myDictionary.Add(GenerateCategorySeven(), categorySeven);
                myDictionary.Add(GenerateCategoryEight(), categoryEight);
                myDictionary.Add(GenerateCategoryNine(), categoryNine);
                myDictionary.Add(GenerateCategoryTen(), categoryTen); 
            }
            else if (myCounter % 2 == 0)
            {
                myDictionary.Add(categoryOne, GenerateCategoryOne());
                myDictionary.Add(categoryTwo, GenerateCategoryTwo());
                myDictionary.Add(categoryThree, GenerateCategoryThree());
                myDictionary.Add(categoryFour, GenerateCategoryFour());
                myDictionary.Add(categoryFive, GenerateCategoryFive());
                myDictionary.Add(categorySix, GenerateCategorySix());
                myDictionary.Add(categorySeven, GenerateCategorySeven());
                myDictionary.Add(categoryEight, GenerateCategoryEight());
                myDictionary.Add(categoryNine, GenerateCategoryNine());
                myDictionary.Add(categoryTen, GenerateCategoryTen());
            }
        }
    }
}
//-----------------------------End of IdentifyAreas Class-----------------------------------