using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID
{
    public partial class MDIForm : Form
    {
        public MDIForm()
        {
            InitializeComponent();
        }



        private void MDIForm_Load(object sender, EventArgs e)
        {

        }

        private void addStudentLabel_Click(object sender, EventArgs e)
        {

            StudentForm studentForm = new StudentForm();
            studentForm.MdiParent = this;
            studentForm.Show();

            /*BaptismalFormFinal baptismalForm = new BaptismalFormFinal();
            if (FormAlreadyOpen("AVT_NSAP_PIS.BaptismalFormFinal") == false)
            {
                //_confirmationForm = new ConfirmationForm();
                //_confirmationForm.MdiParent = this;
                // _confirmationForm.Show();
                baptismalForm.MdiParent = this;
                baptismalForm.Show();
            }
            else
            {
                if (FormAlreadyOpen("AVT_NSAP_PIS.BaptismalFormFinal", false) == true)
                {
                    ShowForm("AVT_NSAP_PIS.BaptismalFormFinal");
                }
            }
            */
        }

        private void timeInTimeOutLabel_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.MdiParent = this;
            mainForm.Show();
        }

        private void addStudentLabel_MouseHover(object sender, EventArgs e)
        {
            //addStudentLabel.Font = new Font(addStudentLabel.Font, FontStyle.Bold);  
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure do you want to Exit? ", "Exit the System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void viewAttendanceLabel_Click(object sender, EventArgs e)
        {
            DisplayAttendance displayAttendance = new DisplayAttendance();
            displayAttendance.MdiParent = this;
            displayAttendance.Show();

            //StudentForm studentForm = new StudentForm();
            //studentForm.MdiParent = this;
            //studentForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Under maintenance.");
        }
    }
}
