//------------------------------Start of Home From---------------------------------
//Importing Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//----------------------------------------------------------------------------------
//Start of Namespace
namespace PROG7312_Part1
{
    //------------------------------------------------------------------------------
    //Start of Home Form Class
    public partial class Home : Form
    {
        //Initializes the Form
        public Home()
        {
            InitializeComponent();
        }
        //Loads the form
        private void Home_Load(object sender, EventArgs e)
        {
            //prevents users to resize the window
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            var myHomeUC = new HomeUC();
            this.Controls.Add(myHomeUC);
            this.Dock = DockStyle.Fill;
            myHomeUC.BringToFront();
        }

        /// <summary>
        /// This event is triggered when a user tries to exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prompt the user to confirm before exiting
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true; // Cancel the form closing event
            }
        }
    }
}
//-----------------------------End of Home Form-----------------------------------
