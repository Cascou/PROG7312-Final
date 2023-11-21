//------------------------------Start of LearnIdentifyingAreas From---------------------------------
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
    //Start of LearnIdentifyingAreas Form Class Method Header
    public partial class LearnIdentifyingAreasForm : Form
    {
        //Initializes Path for Wav Files
        public string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Initializes the Form
        public LearnIdentifyingAreasForm()
        {
            InitializeComponent();
        }

        //Loads the form
        private void LearnIdentifyingAreasForm_Load(object sender, EventArgs e)
        {
            //prevents users to resize the window
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //creating relevant tooltips for the buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnLearnIdentifyingAreasOK, "You understand, and want to continue");
        }

        //----------------------------------------------------------------------
        //Event Triggers 
        //----------------------------------------------------------------------

        /// <summary>
        /// This event is triggered when the mouse enters the LearnIdentifyingAreasOK Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyingAreasOK_MouseEnter(object sender, EventArgs e)
        {
            this.btnLearnIdentifyingAreasOK.Width = 113;
            this.btnLearnIdentifyingAreasOK.Height = 46;
            this.btnLearnIdentifyingAreasOK.BackColor = Color.LimeGreen;
        }

        /// <summary>
        /// This event is triggered when the mouse leaves the LearnIdentifyingAreasOk Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyingAreasOK_MouseLeave(object sender, EventArgs e)
        {
            this.btnLearnIdentifyingAreasOK.Width = 112;
            this.btnLearnIdentifyingAreasOK.Height = 45;
            this.btnLearnIdentifyingAreasOK.BackColor = Color.PaleGreen;
        }

        /// <summary>
        /// This event is triggered when the LearnIdentifyingAreasOk Button is clicked, closing the popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLearnIdentifyingAreasOK_Click(object sender, EventArgs e)
        {
            var soundpath = Path.Combine(executableDirectory, "PageSound.wav");
            var sound = new SoundPlayer(soundpath);
            sound.Play();

            this.Close();
        }
    }
}
//---------------------------------------End of LearnIdentifyingAreas From---------------------------------