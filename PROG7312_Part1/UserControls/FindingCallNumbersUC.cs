//------------------------------Start of FindingCallNumbers User Control---------------------------------
//Importing Libraries
using PROG7312_Part1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1.UserControls
{
    //------------------------------------------------------------------------------
    //Start of FindingCallNumbers User Control Class Method Header
    public partial class FindingCallNumbersUC : UserControl
    {
        //Setting up config file
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Setting up FindingCallNumbers Object
        private FindingCallNumbers  myFindingCallNumbers = new FindingCallNumbers();

        //Setting up Global variables for counters
        public int QuizErrorCounter = 0;
        public int QuizCorrectCounter = 0;
        public int QuizCounter = 0;

        //Initializes the user control
        public FindingCallNumbersUC()
        {
            InitializeComponent();
        }

        //Loads the user Control
        private void FindingCallNumbersUC_Load(object sender, EventArgs e)
        {
            //Starting background music to the game
            var soundpath = Path.Combine(executableDirectory, "BackgroundMusic.wav");
            var sound = new SoundPlayer(soundpath);
            sound.PlayLooping();

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBackToMenuIdentify, "You are done with game, and wish to go back to the home screen");
            toolTip.SetToolTip(btnIdentifyMoreQuestions, "You would like to play again or start new");

            //call method that creates and builds the tree
            myFindingCallNumbers.CreateTree();
            lblChosenLevelThree.Text = myFindingCallNumbers.PickRandomThirdLevelCallNumber();//calls the method to randomly assign a third level entry description

            //Rounding down number to nearest 100
            var roundedDownCategory = myFindingCallNumbers.RoundDownToNearestHundred(myFindingCallNumbers.myCode);
            //finding index in array where index matches call number
            var myIndex = myFindingCallNumbers.FindIndexBySubstring(myFindingCallNumbers.mainOptions, roundedDownCategory);
            //creates array with 3 random indexes and 1 correct index
            myFindingCallNumbers.CreateRandomArrayWithGivenIndex(myFindingCallNumbers.mainOptions, myIndex);

            //adds array to combobox
            cbOptions.Items.Add(myFindingCallNumbers.randomOptions[0]);
            cbOptions.Items.Add(myFindingCallNumbers.randomOptions[1]);
            cbOptions.Items.Add(myFindingCallNumbers.randomOptions[2]);
            cbOptions.Items.Add(myFindingCallNumbers.randomOptions[3]);
        }


        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event trigger, goes through the logic of the quiz every time the SubmitAnswer button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            //ensures that the combobox has an option selected before submitting an answer
            if(cbOptions.SelectedItem != null)
            {
                //Rounds down the code
                var roundedDownCategory = myFindingCallNumbers.RoundDownToNearestHundred(myFindingCallNumbers.myCode);
                
                //If counter = 0, first question in the quiz.
                if (QuizCounter == 0)
                {
                    if (cbOptions.SelectedItem.ToString().Contains(roundedDownCategory))
                    {
                        //plays the winning sound
                        var soundpath = Path.Combine(executableDirectory, "WonSound.wav");
                        var sound = new SoundPlayer(soundpath);
                        sound.Play();

                        //show's messagebox of user got the answer correct
                        MessageBox.Show(null, "That is the correct answer.", "Well done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        QuizCounter++;
                        QuizCorrectCounter++;
                        
                        //assings user progess
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //Starting background music to the game
                        var soundpath2 = Path.Combine(executableDirectory, "BackgroundMusic.wav");
                        var sound2 = new SoundPlayer(soundpath2);
                        sound2.PlayLooping();

                        //gets list from tree of all first entry subcategories
                        List<string> firstLevelCategories = myFindingCallNumbers.GetFirstLevelSubcategories(roundedDownCategory);
                        cbOptions.SelectedIndex = -1;
                        cbOptions.Items.Clear();//clears the current combo box

                        //Assigns new list to the combo box
                        cbOptions.Items.AddRange(firstLevelCategories.ToArray());
                    }
                    else//if answer is incorrect
                    {
                        //plays the losing sound
                        string soundpath = Path.Combine(executableDirectory, "Error.wav");
                        SoundPlayer sound = new SoundPlayer(soundpath);
                        sound.Play();

                        //show's messagebox of user got the answer incorrect
                        MessageBox.Show(null, "That is the incorrect answer.", "Bad Luck", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        QuizCounter++;
                        QuizErrorCounter++;

                        //asisngs user progess
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //Starting background music to the game
                        var soundpath2 = Path.Combine(executableDirectory, "BackgroundMusic.wav");
                        var sound2 = new SoundPlayer(soundpath2);
                        sound2.PlayLooping();

                        //gets list from tree of all first entry subcategories
                        List<string> firstLevelCategories = myFindingCallNumbers.GetFirstLevelSubcategories(roundedDownCategory);
                        cbOptions.SelectedIndex = -1;
                        cbOptions.Items.Clear();//clears current combo box

                        //Assigns new list to the combo box
                        cbOptions.Items.AddRange(firstLevelCategories.ToArray());
                    }
                }
                else if (QuizCounter == 1)
                {
                    //Check if the first 2 characters match
                    bool isFirstTwoCharactersMatch = cbOptions.SelectedItem.ToString().Substring(0, 2) == myFindingCallNumbers.myCode.Substring(0, 2);
                    
                    if (isFirstTwoCharactersMatch)
                    {
                        //plays the winning sound
                        var soundpath = Path.Combine(executableDirectory, "WonSound.wav");
                        var sound = new SoundPlayer(soundpath);
                        sound.Play();

                        //show's messagebox of user got the answer correct
                        MessageBox.Show(null, "That is the correct answer.", "Well done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        QuizCounter++;
                        QuizCorrectCounter++;
                    
                        //Assigns user porgress
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //Starting background music to the game
                        var soundpath2 = Path.Combine(executableDirectory, "BackgroundMusic.wav");
                        var sound2 = new SoundPlayer(soundpath2);
                        sound2.PlayLooping();

                        //gets list from tree of all second entry subcategories
                        List<string> secondLevelMainCategories = myFindingCallNumbers.GetSecondLevelMainCategories(roundedDownCategory);
                        //filters previous list in new list where it contains first two characters
                        List<string> filteredList = myFindingCallNumbers.FilterList(secondLevelMainCategories, myFindingCallNumbers.myCode.Substring(0, 2));
                        cbOptions.SelectedIndex = -1;
                        cbOptions.Items.Clear();//clears old combo box

                        //Assings filtered list to combo box
                        cbOptions.Items.AddRange(filteredList.ToArray());
                    }
                    else
                    {
                        //plays the losing sound
                        string soundpath = Path.Combine(executableDirectory, "Error.wav");
                        SoundPlayer sound = new SoundPlayer(soundpath);
                        sound.Play();

                        //show's messagebox of user got the answer incorrect
                        MessageBox.Show(null, "That is the incorrect answer.", "Bad Luck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        QuizCounter++;
                        QuizErrorCounter++;

                        //assigns user progress
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //Starting background music to the game
                        var soundpath2 = Path.Combine(executableDirectory, "BackgroundMusic.wav");
                        var sound2 = new SoundPlayer(soundpath2);
                        sound2.PlayLooping();

                        //gets list from tree of all second entry subcategories
                        List<string> secondLevelMainCategories = myFindingCallNumbers.GetSecondLevelMainCategories(roundedDownCategory);
                        //filters previous list in new list where it contains first two characters
                        List<string> filteredList = myFindingCallNumbers.FilterList(secondLevelMainCategories, myFindingCallNumbers.myCode.Substring(0, 2));
                        cbOptions.SelectedIndex = -1;
                        cbOptions.Items.Clear();//clears old combo box

                        //Assings filtered list to combo box
                        cbOptions.Items.AddRange(filteredList.ToArray());
                    }
                }
                else if (QuizCounter == 2)
                {
                    //checks that user selected item equals code from third level entry
                    if (cbOptions.SelectedItem.ToString() == myFindingCallNumbers.myCode)
                    {
                        //plays the winning sound
                        var soundpath = Path.Combine(executableDirectory, "WonSound.wav");
                        var sound = new SoundPlayer(soundpath);
                        sound.Play();

                        
                        QuizCorrectCounter++;

                        //assigns user progress
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //show's messagebox of user got the answer correct
                        MessageBox.Show(null, "Correct Answer, you got " + QuizCorrectCounter.ToString() +  " out of 3 for this quiz", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        //reset the counter of incorrect answers
                        QuizCorrectCounter = 0;
                        QuizCounter = 0;
                        QuizErrorCounter = 0;

                        //checks if user wants to play again or not.
                        DialogResult result = MessageBox.Show(null, "Do you want to play again? ", "How to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        //Checks if user said yes or no, and follows the following logic
                        if (result == DialogResult.Yes)
                        {
                            var myFindingCallNumbers = new FindingCallNumbersUC();
                            this.Controls.Add(myFindingCallNumbers);
                            this.Dock = DockStyle.Fill;
                            myFindingCallNumbers.BringToFront();
                        }
                        else if (result == DialogResult.No)
                        {
                            var myHomeUC = new HomeUC();
                            this.Controls.Add(myHomeUC);
                            this.Dock = DockStyle.Fill;
                            myHomeUC.BringToFront();
                        }
                    }
                    else
                    {
                        //plays the losing sound
                        string soundpath = Path.Combine(executableDirectory, "Error.wav");
                        SoundPlayer sound = new SoundPlayer(soundpath);
                        sound.Play();

                        QuizErrorCounter++;

                        //assigns user progress
                        lblCorrectAnswer.Text = QuizCorrectCounter.ToString() + "/3";
                        lblIncorrectAnswers.Text = QuizErrorCounter.ToString() + "/3";

                        //show's messagebox of user got the answer incorrect
                        MessageBox.Show(null, "Incorrect answer, you got " + QuizCorrectCounter.ToString() + " out of 3 for this quiz", "Bad Luck", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //reset the counter of incorrect answers
                        QuizCorrectCounter = 0;
                        QuizCounter = 0;
                        QuizErrorCounter = 0;

                        //checks if user wants to play again or not.
                        DialogResult result = MessageBox.Show(null, "Do you want to play again? ", "How to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        //Checks if user said yes or no, and follows the following logic
                        if (result == DialogResult.Yes)
                        {
                            var myFindingCallNumbers = new FindingCallNumbersUC();
                            this.Controls.Add(myFindingCallNumbers);
                            this.Dock = DockStyle.Fill;
                            myFindingCallNumbers.BringToFront();
                        }
                        else if (result == DialogResult.No)
                        {
                            var myHomeUC = new HomeUC();
                            this.Controls.Add(myHomeUC);
                            this.Dock = DockStyle.Fill;
                            myHomeUC.BringToFront();
                        }
                    }
                }
            }
            else
            {
                //Show's messagebox that user must select an option
                MessageBox.Show(null, "Must select an option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This event is triggered if the IdentifyMoreQuestions button is clicked, restarting the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyMoreQuestions_Click(object sender, EventArgs e)
        {
            var myFindingCallNumbers = new FindingCallNumbersUC();
            this.Controls.Clear();
            this.Controls.Add(myFindingCallNumbers);
            this.Dock = DockStyle.Fill;
            myFindingCallNumbers.BringToFront();
        }

        /// <summary>
        /// This event is triggered if the BackToMenuIdentify button is clicked, going back to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMenuIdentify_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myHomeUC = new HomeUC();
            this.Controls.Add(myHomeUC);
            this.Dock = DockStyle.Fill;
            myHomeUC.BringToFront();
        }
    }
}
//-----------------------------End of FindingCallNumbers User Control-----------------------------------