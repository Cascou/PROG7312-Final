//------------------------------Start of LearnFindingCallNumbers From---------------------------------
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
namespace PROG7312_Part1.Forms
{
    //------------------------------------------------------------------------------
    //Start of LearnFindingCallNumbers Form Class Method Header
    public partial class LearnFindingCallNumbersForm : Form
    {
        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Initializes the Form
        public LearnFindingCallNumbersForm()
        {
            InitializeComponent();
        }

        //Loads the form
        private void LearnFindingCallNumbersForm_Load(object sender, EventArgs e)
        {
            //prevents users to resize the window
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnLearnFindingCallNumbersOK, "You understand, and want to continue");
        }


        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event is triggered when the mouse enters the btnLearnFindingCallNumbersOK Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbersOK_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnFindingCallNumbersOK.Width = 113;
            this.btnLearnFindingCallNumbersOK.Height = 46;
            this.btnLearnFindingCallNumbersOK.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the btnLearnFindingCallNumbersOK Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbersOK_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnFindingCallNumbersOK.Width = 112;
            this.btnLearnFindingCallNumbersOK.Height = 45;
            this.btnLearnFindingCallNumbersOK.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the btnLearnFindingCallNumbersOK Button is clicked, closing the popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnFindingCallNumbersOK_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            this.Close();
        }
    }
}
//---------------------------------------End of LearnFindingCallNumbers From---------------------------------
