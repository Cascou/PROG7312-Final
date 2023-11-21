//------------------------------Start of Home User Control---------------------------------
//Importing Libraries
using PROG7312_Part1.Forms;
using PROG7312_Part1.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Configuration;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1
{
    //------------------------------------------------------------------------------
    //Start of Home User Control Class
    public partial class HomeUC : UserControl
    {
        //Setting up config file
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Initializes the user control
        public HomeUC()
        {
            InitializeComponent();
        }

        //Loads the user Control
        private void HomeUC_Load(object sender, EventArgs e)
        {
            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnReplaceBooks, "Click to Launch Game");
            toolTip.SetToolTip(btnLearnReplaceBooks, "Learn more about Replace Books Game");
            toolTip.SetToolTip(btnCloseApplication, "Click to Exit Application");
            toolTip.SetToolTip(btnIdentifyAreas, "Click to Launch Game");
            toolTip.SetToolTip(btnLearnIdentifyAreas, "Learn more about Identifying Areas Game");
            toolTip.SetToolTip(btnFindingCallNumbers, "Click to Launch Game");
            toolTip.SetToolTip(btnLearnFindingCallNumbers, "Learn more about Finding Call Numbers Game");
        }

        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event is triggered when the mouse enter the ReplaceBooks Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceBooks_MouseEnter(object sender, EventArgs e)
        {
            this.btnReplaceBooks.Width = 212;
            this.btnReplaceBooks.Height = 83;
            this.btnReplaceBooks.BackColor = Color.FromArgb(64, 64, 64);
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the ReplaceBooks Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceBooks_MouseLeave(object sender, EventArgs e)
        {
            this.btnReplaceBooks.Width = 211;
            this.btnReplaceBooks.Height = 82;
            this.btnReplaceBooks.BackColor = Color.Black;
        }

        /// <summary>
        /// This event is triggered when the mouse enters the IdentifyingAreas Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyAreas_MouseEnter(object sender, EventArgs e)
        {
            this.btnIdentifyAreas.Width = 212;
            this.btnIdentifyAreas.Height = 83;
            this.btnIdentifyAreas.BackColor = Color.FromArgb(64, 64, 64);
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the IdentifyingAreas Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyAreas_MouseLeave(object sender, EventArgs e)
        {
            this.btnIdentifyAreas.Width = 211;
            this.btnIdentifyAreas.Height = 82;
            this.btnIdentifyAreas.BackColor = Color.Black;
        }

        /// <summary>
        /// This event is triggered when the mouse enters the LearnReplaceBooks Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooks_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnReplaceBooks.Width = 116;
            this.btnLearnReplaceBooks.Height = 46;
            this.btnLearnReplaceBooks.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the LearnReplaceBooks Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooks_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnReplaceBooks.Width = 115;
            this.btnLearnReplaceBooks.Height = 45;
            this.btnLearnReplaceBooks.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse enters the FindingCallNumbers Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindingCallNumbers_MouseEnter(object sender, EventArgs e)
        {
            this.btnFindingCallNumbers.Width = 212;
            this.btnFindingCallNumbers.Height = 83;
            this.btnFindingCallNumbers.BackColor = Color.FromArgb(64, 64, 64);
        }

        /// <summary>
        /// This event is triggered when the mouse enters the FindingCallNumbers Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindingCallNumbers_MouseLeave(object sender, EventArgs e)
        {
            this.btnFindingCallNumbers.Width = 211;
            this.btnFindingCallNumbers.Height = 82;
            this.btnFindingCallNumbers.BackColor = Color.Black;
        }

        /// <summary>
        /// This event is triggered when the mouse enters the LearnFindingCallNumbers Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbers_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnFindingCallNumbers.Width = 116;
            this.btnLearnFindingCallNumbers.Height = 46;
            this.btnLearnFindingCallNumbers.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the LearnFindingCallNumbers Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbers_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnFindingCallNumbers.Width = 115;
            this.btnLearnFindingCallNumbers.Height = 45;
            this.btnLearnFindingCallNumbers.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse enters the CloseApplication Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApplication_MouseEnter(object sender, EventArgs e)
        {
            this.btnCloseApplication.Width = 212;
            this.btnCloseApplication.Height = 83;
            this.btnCloseApplication.BackColor = Color.DarkRed;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the CloseApplication Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApplication_MouseLeave(object sender, EventArgs e)
        {
            this.btnCloseApplication.Width = 211;
            this.btnCloseApplication.Height = 82;
            this.btnCloseApplication.BackColor = Color.FromArgb(192,0,0);
        }

        /// <summary>
        /// This event is triggered when the mouse enters the LearnIdentifyingAreas Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyAreas_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnIdentifyAreas.Width = 116;
            this.btnLearnIdentifyAreas.Height = 46;
            this.btnLearnIdentifyAreas.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the LearnIdentifyingAreas Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyAreas_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnIdentifyAreas.Width = 115;
            this.btnLearnIdentifyAreas.Height = 45;
            this.btnLearnIdentifyAreas.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the Close Application Button is clicked, to exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApplication_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            Application.Exit();
        }

        /// <summary>
        /// This event is triggered when a user clicks LearnReplaceBooks Button, which will open a popup form for players to learn how to play 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooks_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myLearnReplaceBooksForm = new LearnReplaceBooksForm();
            myLearnReplaceBooksForm.ShowDialog();
        }

        /// <summary>
        /// This event is triggered when a user clicks the LearnIdentifyingAreas Button, which will open a pop up form for players to learn how to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyAreas_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myLearnIdentifyingAreasForm = new LearnIdentifyingAreasForm();
            myLearnIdentifyingAreasForm.ShowDialog();
        }

        /// <summary>
        /// This event is triggered when a user clicks the LearnFindingCallNumbers Button, which will open a pop up form for players to learn how to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbers_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myLearnFindCallNumbersForm = new LearnFindingCallNumbersForm();
            myLearnFindCallNumbersForm.ShowDialog();
        }

        /// <summary>
        /// This event is triggered when a user clicks the ReplaceBooks Button, this will launch the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceBooks_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myReplaceBooksUC = new ReplaceBooksUC();
            this.Controls.Clear();
            this.Controls.Add(myReplaceBooksUC);
            this.Dock = DockStyle.Fill;
            myReplaceBooksUC.BringToFront();
        }

        /// <summary>
        /// This event is triggered when a user clicks the IdentifyAreas Button, this will launch the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyAreas_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myIdentifyAreasUC = new IdentifyAreasUC();
            this.Controls.Clear();
            this.Controls.Add(myIdentifyAreasUC);
            this.Dock = DockStyle.Fill;
            myIdentifyAreasUC.BringToFront();
        }

        /// <summary>
        /// This event is triggered when a user clicks the FindingCallNumbers Button, this will launch the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindingCallNumbers_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            var myFindingCallNumbers = new FindingCallNumbersUC();
            this.Controls.Clear();
            this.Controls.Add(myFindingCallNumbers);
            this.Dock = DockStyle.Fill;
            myFindingCallNumbers.BringToFront();
        }
    }
}
//-----------------------------End of Home User Control-----------------------------------