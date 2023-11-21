//------------------------------Start of IdentifyAreas User Control---------------------------------
//Importing Libraries
using PROG7312_Part1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Media;
using System.IO;
using System.Configuration;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1.UserControls
{
    //------------------------------------------------------------------------------
    //Start of IdentifyAreas User Control Class Method Header
    public partial class IdentifyAreasUC : UserControl
    {
        //Setting up config file
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public int myCounter = 0;

        //Setting up IdentifyAreas Object
        private IdentifyAreas myIdentifyAreas = new IdentifyAreas();
        private Dictionary<string, string> myRandomizedDictionary = new Dictionary<string, string>();

        //Setting up Point
        private Point labelOffset;

        //Initializes the user control
        public IdentifyAreasUC()
        {
            InitializeComponent();
        }

        //Loads the user Control
        private void IdentifyAreasUC_Load(object sender, EventArgs e)
        {
            //Starting background music to the game
            var soundpath = Path.Combine(executableDirectory, "BackgroundMusic.wav");
            var sound = new SoundPlayer(soundpath);
            sound.PlayLooping();

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBackToMenuIdentify, "You are done with game, and wish to go back to the home screen");
            toolTip.SetToolTip(btnIdentifyMoreQuestions, "You would like to play again or start new");
            myCounter++;
            //generate random 10 dictionary
            myIdentifyAreas.GenerateRandomTenDictionary();

            //creating variables for labels
            var terms = new Label[] { lblTerm1, lblTerm2, lblTerm3, lblTerm4};
            var options = new Label[] { lblOption1, lblOption2, lblOption3, lblOption4, lblOption5, lblOption6, lblOption7 };
            var termCounter = 0;
            var optionCounter = 0;

            this.RandomizedDictionary();

            //assigning the Key Value Pairs to the term labels
            foreach (var kvp in myRandomizedDictionary)
            {
                if (termCounter < terms.Length)
                {
                    terms[termCounter].Text = kvp.Key.ToString();
                    termCounter++;
                }
            }

            //assigning the various call numbers to the option labels
            foreach (var kvp in myRandomizedDictionary)
            {
                if (optionCounter < options.Length)
                {
                    options[optionCounter].Text = kvp.Value.ToString();
                    optionCounter++;
                }
            }
        }

        //----------------------------------------------------------------------------------
        //General Methods
        //----------------------------------------------------------------------------------

        /// <summary>
        /// This method will randomize the dictionary
        /// </summary>
        public void RandomizedDictionary()
        {
            
            this.myIdentifyAreas.GenerateRandomTenDictionary();
            List<KeyValuePair<string, string>> myKeyValueList = myIdentifyAreas.myDictionary.ToList();

            int length = myKeyValueList.Count;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int j = random.Next(i, length);
                KeyValuePair<string, string> tempPair = myKeyValueList[i];
                myKeyValueList[i] = myKeyValueList[j];
                myKeyValueList[j] = tempPair;
            }

            myRandomizedDictionary = myKeyValueList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Method Responsible for processing the columns to check if they match and update the Progress Bar.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="label"></param>
        /// <param name="panel"></param>
        private void ProcessColumn(string input, Label label, Panel panel)
        {
            //Setting local string variables to the terms.
            string term1 = lblTerm1.Text;
            string term2 = lblTerm2.Text;
            string term3 = lblTerm3.Text;
            string term4 = lblTerm4.Text;

            //Getting the key of the value user is dragging
            string foundKey = FindKeyByValue(myRandomizedDictionary, input);

            //If statement to check if the user's answer is correct
            if(foundKey == term1 && panel.Name == "pnlDescription1")
            {
                int step = 25;
                pbIdentifyAreas.Value += step;
                label.MouseDown -= label_MouseDown;
                pnlDescription1.BackColor = Color.PaleGreen;
                pnlTerm1.BackColor = Color.PaleGreen;
            }
            else if (foundKey == term2 && panel.Name == "pnlDescription2")
            {
                int step = 25;
                pbIdentifyAreas.Value += step;
                label.MouseDown -= label_MouseDown;
                pnlDescription2.BackColor = Color.PaleGreen;
                pnlTerm2.BackColor = Color.PaleGreen;
            }
            else if (foundKey == term3 && panel.Name == "pnlDescription3")
            {
                int step = 25;
                pbIdentifyAreas.Value += step;
                label.MouseDown -= label_MouseDown;
                pnlDescription3.BackColor = Color.PaleGreen;
                pnlTerm3.BackColor = Color.PaleGreen;
            }
            else if (foundKey == term4 && panel.Name == "pnlDescription4")
            {
                int step = 25;
                pbIdentifyAreas.Value += step;
                label.MouseDown -= label_MouseDown;
                pnlDescription4.BackColor = Color.PaleGreen;
                pnlTerm4.BackColor = Color.PaleGreen;
            }
            //If the answer is wrong
            else if (panel.Name.Contains('1'))
            {
                pnlDescription1.BackColor = Color.Salmon;
                pnlTerm1.BackColor = Color.Salmon;

                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalIncorrectCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                lblIncorrectAnswers.Text = myCounter.ToString() + "/3";
            }
            else if (panel.Name.Contains('2'))
            {
                pnlDescription2.BackColor = Color.Salmon;
                pnlTerm2.BackColor = Color.Salmon;

                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalIncorrectCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                lblIncorrectAnswers.Text = myCounter.ToString() + "/3";

            }
            else if (panel.Name.Contains('3'))
            {
                pnlDescription3.BackColor = Color.Salmon;
                pnlTerm3.BackColor = Color.Salmon;

                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalIncorrectCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                lblIncorrectAnswers.Text = myCounter.ToString() + "/3";
            }
            else if (panel.Name.Contains('4'))
            {
                pnlDescription4.BackColor = Color.Salmon;
                pnlTerm4.BackColor = Color.Salmon;

                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalIncorrectCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                lblIncorrectAnswers.Text = myCounter.ToString() + "/3";
            }

            if(lblIncorrectAnswers.Text == "4/3")
            {
                //plays the losing sound
                string soundpath = Path.Combine(executableDirectory, "Error.wav");
                SoundPlayer sound = new SoundPlayer(soundpath);
                sound.Play();

                //reset the counter of incorrect answers
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = "0";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //increase the counter
                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //show's messagebox of user completed game
                MessageBox.Show(null, "You have gotten more than three answers wrong.", "Bad Luck", MessageBoxButtons.OK, MessageBoxIcon.Error);


                DialogResult result = MessageBox.Show(null, "Do you want to play again? ", "How to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Checks if user said yes or no, and follows the following logic
                if (result == DialogResult.Yes)
                {
                    var myIdentifyAreasUC = new IdentifyAreasUC();
                    this.Controls.Add(myIdentifyAreasUC);
                    this.Dock = DockStyle.Fill;
                    myIdentifyAreasUC.BringToFront();
                }
                else if (result == DialogResult.No)
                {
                    var myHomeUC = new HomeUC();
                    this.Controls.Add(myHomeUC);
                    this.Dock = DockStyle.Fill;
                    myHomeUC.BringToFront();
                }
            }

            //Checks if user has completed the game
            if (pbIdentifyAreas.Value == 100)
            {
                //plays the winning sound
                var soundpath = Path.Combine(executableDirectory, "WonSound.wav");
                var sound = new SoundPlayer(soundpath);
                sound.Play();

                //reset the counter of incorrect answers
                config.AppSettings.Settings["GlobalIncorrectCounter"].Value = "0";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //increase the counter
                int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCounter"]);
                myCounter++;
                config.AppSettings.Settings["GlobalCounter"].Value = myCounter.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                //show's messagebox of user completed game
                MessageBox.Show(null, "Welldone on completing the game.", "Congratulations - Winner Winner Chicken Dinner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                var result = MessageBox.Show(null, "Do you want to play again? ", "How to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Checks if user said yes or no, and follows the following logic
                if (result == DialogResult.Yes)
                {
                    var myIdentifyAreasUC = new IdentifyAreasUC();
                    this.Controls.Add(myIdentifyAreasUC);
                    this.Dock = DockStyle.Fill;
                    myIdentifyAreasUC.BringToFront();
                }
                else if (result == DialogResult.No)
                {
                    var soundpath3 = Path.Combine(executableDirectory, "PageSound.wav");
                    var sound3 = new SoundPlayer(soundpath3);
                    sound3.Play();

                    HomeUC myHomeUC = new HomeUC();
                    this.Controls.Add(myHomeUC);
                    this.Dock = DockStyle.Fill;
                    myHomeUC.BringToFront();
                }
            }
        }

        /// <summary>
        /// Method Responsible for processing the description panels to check if they are empty, and make them white.
        /// </summary>
        /// <param name="description1"></param>
        /// <param name="description2"></param>
        /// <param name="description3"></param>
        /// <param name="description4"></param>
        private void ProcessLabelDescriptions(Panel description1, Panel description2, Panel description3, Panel description4)
        {
            if (!IsLabelPresent(description1))
            {
                pnlDescription1.BackColor = Color.White;
                pnlTerm1.BackColor = Color.White;
            }
            if (!IsLabelPresent(description2))
            {
                pnlDescription2.BackColor = Color.White;
                pnlTerm2.BackColor = Color.White;
            }
            if (!IsLabelPresent(description3))
            {
                pnlDescription3.BackColor = Color.White;
                pnlTerm3.BackColor = Color.White;
            }
            if (!IsLabelPresent(description4))
            {
                pnlDescription4.BackColor = Color.White;
                pnlTerm4.BackColor = Color.White;
            }
        }

        //--------------------------------------------------------------------------
        //Method responsible for finding key value for the value
        public string FindKeyByValue(Dictionary<string, string> dictionary, string targetValue)
        {
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == targetValue)
                {
                    return kvp.Key; // Return the key if the value matches
                }
            }
            return null; 
        }

        //----------------------------------------------------------------------
        //Draggable Event Triggers 
        //----------------------------------------------------------------------

        //----------------------------------------------------------------------
        //General Mouse Down Event Trigger 

        /// <summary>
        /// This event is triggered when someone's mouse is down on the lblpb1 label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label draggedLabel = (Label)sender;
                labelOffset = new Point(e.X, e.Y);
                draggedLabel.DoDragDrop(draggedLabel, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Checks if a label is present
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private bool IsLabelPresent(Panel panel)
        {
            return panel.Controls.OfType<Label>().Any();
        }

        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        //----------------------------------------------------------------------
        //General Panel DragEnter Event Trigger 
        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        //----------------------------------------------------------------------
        //Panel DragDrop Event Triggers

        /// <summary>
        /// This event is triggered when the label is dropped in the options panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            Panel targetPanel = (Panel)sender;

            if (!IsLabelPresent(targetPanel))
            {
                Label draggedLabel = (Label)e.Data.GetData(typeof(Label));
                targetPanel.Controls.Add(draggedLabel);
                
                
                int x = 0;
                int y = (targetPanel.Height - draggedLabel.Height) / 2;

                draggedLabel.Location = new Point(x, y);

                ProcessLabelDescriptions(pnlDescription1, pnlDescription2, pnlDescription3, pnlDescription4);
            }
        }

        /// <summary>
        /// This event is triggered when the label is dropped in the Description panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelOption_DragDrop(object sender, DragEventArgs e)
        {
            Panel targetPanel = (Panel)sender;

            if (!IsLabelPresent(targetPanel))
            {
                
                Label draggedLabel = (Label)e.Data.GetData(typeof(Label));
                
                targetPanel.Controls.Add(draggedLabel);

                int x = 0;
                int y = (targetPanel.Height - draggedLabel.Height) / 2;

                draggedLabel.Location = new Point(x, y);

                string labelText = draggedLabel.Text;
                
                ProcessColumn(labelText, draggedLabel, targetPanel);
                ProcessLabelDescriptions(pnlDescription1, pnlDescription2, pnlDescription3, pnlDescription4);
            }
        }


        /// <summary>
        /// This event is triggered when the back to menu button is clicked, returing the user to the home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMenuIdentify_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            //reset the counter of incorrect answers
            config.AppSettings.Settings["GlobalIncorrectCounter"].Value = "0";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCounter"]);
            myCounter++;
            config.AppSettings.Settings["GlobalCounter"].Value = myCounter.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            var myHomeUC = new HomeUC();
            this.Controls.Add(myHomeUC);
            this.Dock = DockStyle.Fill;
            myHomeUC.BringToFront();
        }

        /// <summary>
        /// This event is triggered when the IdentifyMoreQuestions button is clicked, to play the game again or reset the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyMoreQuestions_Click(object sender, EventArgs e)
        {
            //reset the counter of incorrect answers
            config.AppSettings.Settings["GlobalIncorrectCounter"].Value = "0";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            int myCounter = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCounter"]);
            myCounter++;
            config.AppSettings.Settings["GlobalCounter"].Value = myCounter.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            

            var myIdentifyAreasUC = new IdentifyAreasUC();
            this.Controls.Clear();
            this.Controls.Add(myIdentifyAreasUC);
            this.Dock = DockStyle.Fill;
            myIdentifyAreasUC.BringToFront();
        }
    }
}
//-----------------------------End of IdentifyAreas User Control-----------------------------------