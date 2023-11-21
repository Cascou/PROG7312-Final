//------------------------------Start of ReplaceBooks User Control---------------------------------
//Importing Libraries
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
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Media;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1
{
    //------------------------------------------------------------------------------
    //Start of ReplaceBooks User Control Class Method Header
    public partial class ReplaceBooksUC : UserControl
    {
        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Setting up connection string for database
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFilename=|DataDirectory|\App_Data\ReplaceBooksDatabase.mdf;Integrated Security=true;";
       
        //Setting up Dewey Decimal Object
        private DeweyDecimal myDeweyDecimal = new DeweyDecimal();

        //Setting up Lists for sorting
        List<string> sortedList;
        LinkedList<string> sortedCallNumbers;

        //Setting up Point
        private Point labelOffset;
        
        //Setting up Global Variables
        private int elapsedSeconds = 0;

        //Initializes the user control
        public ReplaceBooksUC()
        {
            InitializeComponent();          
        }

        //Loads the user Control
        private void ReplaceBooksUC_Load(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "BackgroundMusic.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            //Fetch Leaderboard data
            SelectLeaderBoards();

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBackToMenuReplace, "You are done with game, and wish to go back to the home screen");

            //generate random 10 call numbers
            myDeweyDecimal.GenerateRandomTenCallNumbers();

            //Setting up Sorted LinkedList with the data
            sortedList = new List<string>(myDeweyDecimal.myCallNumbers);
            sortedList.Sort(new DeweyDecimalComparer());
            sortedCallNumbers = new LinkedList<string>(sortedList);
                      
            //creating variables for labels
            var labels = new Label[] { lblpb1, lblpb2, lblpb3, lblpb4, lblpb5, lblpb6, lblpb7, lblpb8, lblpb9, lblpb10 };
            int labelCounter = 0;

            //assigning the various call numbers to the labels
            foreach(string item in myDeweyDecimal.myCallNumbers)
            {
                if(labelCounter < labels.Length)
                {
                    labels[labelCounter].Text = item;
                    labelCounter++;
                }
            }

            //setting tooltips for each label
            toolTip.SetToolTip(lblpb1, lblpb1.Text);
            toolTip.SetToolTip(lblpb2, lblpb2.Text);
            toolTip.SetToolTip(lblpb3, lblpb3.Text);
            toolTip.SetToolTip(lblpb4, lblpb4.Text);
            toolTip.SetToolTip(lblpb5, lblpb5.Text);
            toolTip.SetToolTip(lblpb6, lblpb6.Text);
            toolTip.SetToolTip(lblpb7, lblpb7.Text);
            toolTip.SetToolTip(lblpb8, lblpb8.Text);
            toolTip.SetToolTip(lblpb9, lblpb9.Text);
            toolTip.SetToolTip(lblpb10, lblpb10.Text);


            MessageBox.Show("Click ok when you are ready to start", "Ready?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //start timer
            ReplaceBooksTimer.Start();
        }

        //--------------------------------------------------------------------------
        //Method responsible for timer ticker and updating label field
        private void ReplaceBooksTimer_Tick(object sender, System.EventArgs e)
        {
            elapsedSeconds++;

            string elapsedTime = TimeSpan.FromSeconds(elapsedSeconds).ToString(@"hh\:mm\:ss");
            lblTimeElapsed.Text = "Time: " + elapsedTime;
        }

        //--------------------------------------------------------------------------
        //Method responsible for for checking if user dragged call number is in order for progress bar
        private void ProcessDeweyDecimal(int index, string input, Label label)
        {
            string currentCallNumber = sortedCallNumbers.ElementAt(index);

            if (currentCallNumber == input)
            {
                int step = 10;
                pbReplaceBooks.Value += step;
                label.MouseDown -= label_MouseDown;

                if(pbReplaceBooks.Value == 100)
                {
                    var soundpath = Path.Combine(executableDirectory, "WonSound.wav");
                    var sound = new SoundPlayer(soundpath);
                    sound.Play();

                    //Stops timer
                    ReplaceBooksTimer.Stop();
                    string completedGameTime = lblTimeElapsed.Text;//assigns it to a value

                    //show's messagebox of user completed game
                    MessageBox.Show(null, "Welldone on completing the game in " + completedGameTime, "Congratulations - Winner Winner Chicken Dinner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    //Inserts data into database
                    InsertQuery(completedGameTime);
                    //Checks if user wants to play again
                    
                    var result = MessageBox.Show(null, "Do you want to play again? ", "How to Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //Checks if user said yes or no, and follows the following logic
                    if (result == DialogResult.Yes)
                    {
                        var soundpath2 = Path.Combine(executableDirectory, "BackgroundMusic.wav");
                        var sound2 = new SoundPlayer(soundpath2);
                        sound2.Play();

                        var myReplaceBooksUC = new ReplaceBooksUC();
                        this.Controls.Add(myReplaceBooksUC);
                        this.Dock = DockStyle.Fill;
                        myReplaceBooksUC.BringToFront();
                    }
                    else if (result == DialogResult.No)
                    {
                        var soundpath3 = Path.Combine(executableDirectory, "PageSound.wav");
                        var sound3 = new SoundPlayer(soundpath3);
                        sound3.Play();

                        var myHomeUC = new HomeUC();
                        this.Controls.Add(myHomeUC);
                        this.Dock = DockStyle.Fill;
                        myHomeUC.BringToFront();
                    }   
                }
            }
        }

        //---------------------------------------------------------------------
        //Method that is responsible for selecting the data from the database for the leaderboards
        private void SelectLeaderBoards()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT (Time) FROM [dbo].[Leaderboards]";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        string[] timeArray = new string[dataTable.Rows.Count];
                        //inserting query data into timeArray 
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            timeArray[i] = dataTable.Rows[i]["Time"].ToString();
                        }

                        TimeSpan[] timedArray = new TimeSpan[timeArray.Length];
                        //inserting string array into timespan array for comparison
                        for (int i = 0; i < timeArray.Length; i++)
                        {
                            timedArray[i] = TimeSpan.Parse(timeArray[i]);
                        }

                        //sorts the array
                        Array.Sort(timedArray);

                        //Validation check, if there are less than 5 entries in the db
                        //Note: the array has already been sorted by this point
                        if (timedArray.Length > 0 && timedArray.Length < 2)
                        {
                            lblTime1.Text = timedArray[0].ToString();
                        }else if(timedArray.Length > 1 && timedArray.Length < 3)
                        {
                            lblTime1.Text = timedArray[0].ToString();
                            lblTime2.Text = timedArray[1].ToString();
                        }
                        else if(timedArray.Length > 2 && timedArray.Length < 4)
                        {
                            lblTime1.Text = timedArray[0].ToString();
                            lblTime2.Text = timedArray[1].ToString();
                            lblTime3.Text = timedArray[2].ToString();
                        }
                        else if(timedArray.Length > 3 && timedArray.Length < 5)
                        {
                            lblTime1.Text = timedArray[0].ToString();
                            lblTime2.Text = timedArray[1].ToString();
                            lblTime3.Text = timedArray[2].ToString();
                            lblTime4.Text = timedArray[3].ToString();
                        }else if(timedArray.Length > 4)
                        {
                            lblTime1.Text = timedArray[0].ToString();
                            lblTime2.Text = timedArray[1].ToString();
                            lblTime3.Text = timedArray[2].ToString();
                            lblTime4.Text = timedArray[3].ToString();
                            lblTime5.Text = timedArray[4].ToString();
                        }      
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        //---------------------------------------------------------------------
        //Method that is responsible for inserting data into the database
        private void InsertQuery(string time)
        {
            //splitting string for time format
            string[] timeParts = time.Split(' ');
            
            //Splitting variable to be formated in hh:/mm:/ss
            TimeSpan formattedTime = TimeSpan.Parse(timeParts[1]);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO [dbo].[Leaderboards] ([Time]) VALUES (@Time)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Time", formattedTime);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            
        }

        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event is triggered when the back to menu button is clicked, returing the user to the home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMenuReplace_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            sortedList.Clear();
            sortedCallNumbers.Clear();

            var myHomeUC = new HomeUC();
            this.Controls.Add(myHomeUC);
            this.Dock = DockStyle.Fill;
            myHomeUC.BringToFront();
        }

        /// <summary>
        /// Checks if label is present
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private bool IsLabelPresent(Panel panel)
        {
            return panel.Controls.OfType<Label>().Any();
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
        /// This event is triggered when the label is dropped in the PanelTop1 panel
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
                draggedLabel.Location = new Point(-1, 0);
            }
            
        }

        /// <summary>
        /// This event is triggered when the label is dropped in the PanelBottom1 panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelBottom_DragDrop(object sender, DragEventArgs e)
        {
            Panel targetPanel = (Panel)sender;

            if (!IsLabelPresent(targetPanel))
            {
                
                Label draggedLabel = (Label)e.Data.GetData(typeof(Label));
                int result = 0;

                targetPanel.Controls.Add(draggedLabel);
                draggedLabel.Location = new Point(-1, 0);

                // Use a regular expression to match numeric values
                string pattern = @"\d+(\.\d+)?";
                MatchCollection matches = Regex.Matches(targetPanel.Name, pattern);

                foreach (Match match in matches)
                {
                    result += Convert.ToInt32(match.Value);
                }

                string labelText = draggedLabel.Text;

                ProcessDeweyDecimal(result-1, labelText, draggedLabel);
            }
        }
    }
}
//-----------------------------End of ReplaceBooks User Control-----------------------------------