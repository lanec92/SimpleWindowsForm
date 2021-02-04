using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEntryProject
{
    public partial class frmDataEntry : Form
    {
        TimeSpan elapsedTime;
        DateTime lastElapsed;

        public frmDataEntry()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close(); //closes the form
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clears the text boxes
            txtName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZip.Clear();

            //Focuses the cursor on the first txtBox
            txtName.Focus();
        }

        private void timTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime += DateTime.Now - lastElapsed; // add time between now & last tick
            lastElapsed = DateTime.Now; //set lastElapsed to current time
            txtTimer.Text = Convert.ToString(new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds)); //Display time in textBox
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            timTimer.Enabled = true;
            grbDataEntry.Enabled = true;
            txtName.Focus();
            lastElapsed = DateTime.Now;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            grbDataEntry.Enabled = false;
            timTimer.Enabled = false;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string dataEntry;

            //Checks for empty text box
            if (txtName.Text.Equals("") || txtAddress.Text.Equals("") || txtCity.Text.Equals("") || txtState.Text.Equals("") || txtZip.Text.Equals(""))
            {
                MessageBox.Show("Each box must have an input", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); // Error message displays if empty textBox
                txtName.Focus(); //Focuses on first textBox
                return; //b/c there is an empty textBox
            }

            // sets string to user input
            dataEntry = txtName.Text + "\r\n" + txtAddress.Text + "\r\n" + txtCity.Text + "\r\n" + txtState.Text + "\r\n" + txtZip.Text;
            MessageBox.Show(dataEntry, "Data Entry", MessageBoxButtons.OK); // Shows user input
            btnClear.PerformClick();//Clears the TxtBoxes After hitting ok on the messageBox
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e) //Associated with pressing a key
        {
            string textBoxSender = ((TextBox)sender).Name; // TextBox cast then save the name of the text box in a string

            if (e.KeyChar == 13) //the value of enter in KeyChar is 13
            {
                switch(textBoxSender) // will go to the next text box if the user hits enter
                {
                    case "txtName":
                        txtAddress.Focus();
                        break;
                    case "txtAddress":
                        txtCity.Focus();
                        break;
                    case "txtCity":
                        txtState.Focus();
                        break;
                    case "txtState":
                        txtZip.Focus();
                        break;
                    case "txtZip":
                        btnAccept.Focus();
                        break;
                }
            }

            if (textBoxSender.Equals("txtZip")) // Will only allow numbers for zip
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
                {
                    e.Handled = false; // nothing to handle
                }
                else
                {
                    e.Handled = true; // will not allow anything but numbers or backspace
                }
            }

        }

        private void btnButtons_Hover(object sender, EventArgs e) //Change color of button on Hover
        {
            ((Button)sender).BackColor = Color.Aqua;
        }

        private void btnButtons_LeaveHover(object sender, EventArgs e) //Return normal color of button after no longer hovering over button
        {
            ((Button)sender).BackColor = SystemColors.Control;

        }
    }
}
