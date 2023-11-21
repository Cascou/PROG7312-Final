//------------------------------Start of LearnReplaceBooks From---------------------------------
//Importing Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
namespace PROG7312_Part1
{
    //------------------------------------------------------------------------------
    //Start of LearnReplaceBooks Form Class Method Header
    public partial class LearnReplaceBooksForm : Form
    {
        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Initializes the Form
        public LearnReplaceBooksForm()
        {
            InitializeComponent();
        }

        //Loads the form
        private void LearnReplaceBooksForm_Load(object sender, EventArgs e)
        {
            //prevents users to resize the window
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnLearnReplaceBooksOK, "You understand, and want to continue");
        }

        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event is triggered when the mouse enters the LearnReplaceBooksOK Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooksOK_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnReplaceBooksOK.Width = 113;
            this.btnLearnReplaceBooksOK.Height = 46;
            this.btnLearnReplaceBooksOK.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the LearnReplaceBooksOk Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooksOK_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnReplaceBooksOK.Width = 112;
            this.btnLearnReplaceBooksOK.Height = 45;
            this.btnLearnReplaceBooksOK.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the LearnReplaceBooksOk Button is clicked, closing the popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnReplaceBooksOK_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            this.Close();
        }
    }
}
//-----------------------------End of LearnReplaceBooks Form-----------------------------------