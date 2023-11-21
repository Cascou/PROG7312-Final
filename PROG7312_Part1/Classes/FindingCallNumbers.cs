//------------------------------Start of FindingCallNumbers Class---------------------------------
//Importing Libraries
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Linq;
using System.IO;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1.Classes
{
    //------------------------------------------------------------------------------
    //Start of FindingCallNumbers Class Method Header
    public class FindingCallNumbers
    {
        //Declaring Global variables that are used in other User Controls and the method below
        private Random random = new Random();
        private Dictionary<string, TreeNode> treeRoots;
        public string myCode;
        public string[] mainOptions = {"000 Computer Science, Information, & General Works", "100 Philosophy & Psychology", "200 Religion", "300 Social Sciences", "400 Language", "500 Science", "600 Technology", "700 Arts & Recreation", "800 Literature", "900 History & Geography"};
        public string[] randomOptions;

        /// <summary>
        /// This method assigns the correct Option and other random options and puts them in a array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="givenIndex"></param>
        public void CreateRandomArrayWithGivenIndex(string[] array, int givenIndex)
        {
            var random = new Random();
            var randomIndices = Enumerable.Range(0, array.Length)
                                           .Where(i => i != givenIndex)
                                           .OrderBy(i => random.Next())
                                           .Take(3)
                                           .ToArray();

            randomOptions = new string[4];
            for (int i = 0; i < 3; i++)
            {
                randomOptions[i] = array[randomIndices[i]];
            }
            randomOptions[3] = array[givenIndex];

            // Order the randomOptions array in numerical order
            randomOptions = randomOptions.OrderBy(option => int.Parse(option.Split(' ')[0])).ToArray();
        }
       
        /// <summary>
        /// This method finds a specific index within the array that contains a specific string
        /// </summary>
        /// <param name="array"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public int FindIndexBySubstring(string[] array, string searchString)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Contains(searchString))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// This methods takes in a string input and rounds it down to the nearest hundred.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string RoundDownToNearestHundred(string input)
        {
            if (int.TryParse(input, out int number))
            {
                int roundedNumber = (number / 100) * 100;
                return roundedNumber.ToString("D3");
            }
            else
            {
                return "Invalid Input";
            }
        }

        /// <summary>
        /// This method returns a string of a third entry level call number
        /// </summary>
        /// <returns></returns>
        public string PickRandomThirdLevelCallNumber()
        {
            if (treeRoots == null || treeRoots.Count == 0)
            {
                return "No data available";
            }

            ///calls a method to search the tree
            TreeNode randomSubcategory = GetRandomThirdLevelSubcategory();
            myCode = randomSubcategory.Code;

            return $"{randomSubcategory.Title}";
        }

        /// <summary>
        /// This method uses the tree to find a random kvp that is a third entry level dewey decimal number
        /// </summary>
        /// <returns></returns>
        private TreeNode GetRandomThirdLevelSubcategory()
        {
            List<TreeNode> thirdLevelSubcategories = treeRoots.Values
                .SelectMany(root => root.Children.Values.SelectMany(child => child.Children.Values))
                .Where(sub => sub.Children.Count == 0)
                .ToList();

            if (thirdLevelSubcategories.Count > 0)
            {
                int randomChildIndex = random.Next(0, thirdLevelSubcategories.Count);
                return thirdLevelSubcategories[randomChildIndex];
            }
            else
            {
                return treeRoots.Values.First(); // Return any root if no third-level subcategories found
            }
        }

        /// <summary>
        /// This method is used to return all the firt level subcatergories within the main categories
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> GetFirstLevelSubcategories(string code)
        {
            List<string> firstLevelSubcategories = new List<string>();

            if (treeRoots.TryGetValue(code, out var rootNode))
            {
                foreach (var subcategory in rootNode.Children.Values)
                {
                    firstLevelSubcategories.Add($"{subcategory.Code} {subcategory.Title}");
                }
            }

            return firstLevelSubcategories;
        }

        /// <summary>
        /// This method is used to return all the second level subcategories found within the main categories
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> GetSecondLevelMainCategories(string code)
        {
            List<string> secondLevelMainCategories = new List<string>();

            if (treeRoots.TryGetValue(code, out var rootNode))
            {
                foreach (var childNode in rootNode.Children.Values)
                {
                    secondLevelMainCategories.AddRange(childNode.Children.Values.Select(subcategory => $"{subcategory.Code}"));
                }
            }

            return secondLevelMainCategories;
        }

        /// <summary>
        /// This method is used to filter an array to see if any of the elements contain a specific string
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public List<string> FilterList(List<string> inputList, string filterString)
        {
            return inputList.Where(item => item.Contains(filterString)).ToList();
        }

        /// <summary>
        /// Method to create the tree
        /// </summary>
        public void CreateTree()
        {
            //getting dynamic file location
            string fileName = "DeweyDecimal.json";
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName); ;
            Console.WriteLine(jsonFilePath);

            try
            {
                string jsonText = System.IO.File.ReadAllText(jsonFilePath);
                JObject jsonObject = JObject.Parse(jsonText);

                // Initializing the root of the tree
                treeRoots = new Dictionary<string, TreeNode>();

                JArray categories = (JArray)jsonObject["categories"];
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        TreeNode categoryNode = new TreeNode
                        {
                            Code = category["code"]?.ToString(),
                            Title = category["title"]?.ToString()
                        };

                        // Recursive call for nested subcategories
                        BuildTree(categoryNode, category["subcategories"]);

                        treeRoots.Add(categoryNode.Code, categoryNode);
                    }
                }
                else
                {
                    Console.WriteLine("No 'categories' array found in the JSON file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }
        }


        /// <summary>
        /// Method that is used to build the tree
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="subcategories"></param>
        public void BuildTree(TreeNode parentNode, JToken subcategories)
        {
            if (subcategories != null)
            {
                if (subcategories.Type == JTokenType.Array)
                {
                    foreach (var subcategory in subcategories.Children())
                    {
                        if (subcategory is JObject)
                        {
                            TreeNode subcategoryNode = new TreeNode
                            {
                                Code = subcategory["code"]?.ToString(),
                                Title = subcategory["title"]?.ToString()
                            };

                            // Recursive call for nested subcategories
                            BuildTree(subcategoryNode, subcategory["subcategories"]);

                            if (parentNode == null)
                            {
                                treeRoots.Add(subcategoryNode.Code, subcategoryNode);
                            }
                            else
                            {
                                parentNode.Children.Add(subcategoryNode.Code, subcategoryNode);
                            }
                        }
                        else if (subcategory.Type == JTokenType.String)
                        {
                            // Handle leaf nodes represented as strings
                            if (parentNode != null)
                            {
                                TreeNode subcategoryNode = new TreeNode
                                {
                                    Code = subcategory.Path.Split('.').Last(),
                                    Title = subcategory.ToString()
                                };

                                parentNode.Children.Add(subcategoryNode.Code, subcategoryNode);
                            }
                        }
                    }
                }
                else if (subcategories.Type == JTokenType.Object)
                {
                    foreach (var subcategory in subcategories.Children())
                    {
                        if (subcategory is JProperty jProperty)
                        {
                            TreeNode subcategoryNode = new TreeNode
                            {
                                Code = jProperty.Name,
                                Title = jProperty.Value?["title"]?.ToString()
                            };

                            // Recursive call for nested subcategories
                            BuildTree(subcategoryNode, jProperty.Value?["subcategories"]);

                            if (parentNode == null)
                            {
                                treeRoots.Add(subcategoryNode.Code, subcategoryNode);
                            }
                            else
                            {
                                parentNode.Children.Add(subcategoryNode.Code, subcategoryNode);
                            }
                        }
                    }
                }
            }
        }
    }
}
//-----------------------------End of FindingCallNumbers Class-----------------------------------


//-----------------------------------------------------------------------------------
//Creating new class called TreeNode for breaking down JSON Objects.
public class TreeNode
{
    public string Code { get; set; }
    public string Title { get; set; }
    public Dictionary<string, TreeNode> Children { get; set; }
    
    public TreeNode()
    {
        Children = new Dictionary<string, TreeNode>();
    }
}
//-----------------------------End of TreeNode Class-----------------------------------