using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using RFID.Properties;
using MySql.Data.MySqlClient;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using RFID.Properties;

namespace RFID
{
    public partial class StudentForm : Form
    {

        private string user = "";

        public StudentForm()
        {
            InitializeComponent();
        }

        private void LoadAdviser()
        {
            DataTable adviser = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT name FROM Teacher ORDER By Name ASC").Tables[0];
            if (adviser.Rows.Count > 0)
            {
                foreach (DataRow dataRow in adviser.Rows)
                    adviserComboBox.Items.Add(dataRow["Name"].ToString());
            }
            adviserComboBox.SelectedIndex = 0;
        }

        private void LoadSection()
        {
            DataTable section = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT Sectionname FROM Section ORDER By SectionName ASC").Tables[0];
            if (section.Rows.Count > 0)
            {
                foreach (DataRow dataRow in section.Rows)
                    sectionComboBox.Items.Add(dataRow["SectionName"].ToString());
            }
            sectionComboBox.SelectedIndex = 0;
        }
        private void StudentForm_Load(object sender, EventArgs e)
        {
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            LoadAdviser();
            LoadSection();
            RFIDTextBox.Focus();
            genderComboBox.Items.Add("Male");
            genderComboBox.Items.Add("Female");
            genderComboBox.SelectedIndex = 0;
        }

        private bool isEmpty()
        {
            bool empty = true;
            if (LRNTextBox.Text == String.Empty && lastNameTextBox.Text == String.Empty && firstNameTextBox.Text == String.Empty && RFIDTextBox.Text == String.Empty)
                empty = true;
            else
                empty = false;
            return empty;
        }
        private void ClearTextBox()
        {
            LRNTextBox.Text = String.Empty;
            lastNameTextBox.Text = String.Empty;
            firstNameTextBox.Text = string.Empty;
            middleNameTextBox.Text = string.Empty;
            RFIDTextBox.Text = String.Empty;
            pictureBox.Image = Resources.Profile_Avatar_PNG_2;
            RFIDTextBox.Focus();
        }

        private bool IsExisting(string rfidNumber)
        {
            DataTable dataTable = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT RFID, LastName, FirstName FROM Student WHERE (RFID = ?RFIDNumber)", 
                                        new MySqlParameter("?RFIDNumber", rfidNumber)).Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                user = dataTable.Rows[0]["LastName"].ToString().ToUpper() + ", " + dataTable.Rows[0]["firstName"].ToString();
                return true;
            }
            else
                return false;
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (!(isEmpty()))
            {
                try
                {

                    if (IsExisting(RFIDTextBox.Text.Trim()))
                    {
                        MessageBox.Show("RFID Number already used." + "\n " + user);
                        RFIDTextBox.Focus();
                        RFIDTextBox.SelectAll();
                        return;
                    }
                    
                    byte[] imageBytes = new byte[1];
                    Image image = pictureBox.Image;
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg); // You can choose other formats like Jpeg
                    imageBytes = memoryStream.ToArray();
  
                    MySqlHelper.ExecuteNonQuery(Program.DataConnectionString, "INSERT INTO Student (LRN, RFID, LastName, FirstName, MiddleName, Picture, Section, Adviser, Gender) VALUES (?LRN, ?RFID, ?LastName, ?FirstName, ?MiddleName, ?Picture, ?Section, ?Adviser, ?Gender)",
                           new MySqlParameter("?LRN", LRNTextBox.Text),
                           new MySqlParameter("?LastName", lastNameTextBox.Text),
                           new MySqlParameter("?FirstName", firstNameTextBox.Text),
                           new MySqlParameter("?MiddleName", middleNameTextBox.Text),
                           new MySqlParameter("?Section", sectionComboBox.Text),
                           new MySqlParameter("?Adviser", adviserComboBox.Text),
                           new MySqlParameter("?Gender", genderComboBox.Text),
                           new MySqlParameter("?RFID", RFIDTextBox.Text),
                           new MySqlParameter("?Picture", imageBytes));

                    //if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Delete this Record?", Program.DialogTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    memoryStream.Dispose();
                    if (DialogResult.Yes == MessageBox.Show("Successfully saved! Add another?","",MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        ClearTextBox();
                    }
                    else
                    {
                        this.Close();
                    }

                }
                catch (MySqlException err)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Unexpected Error" + err.Message.ToString() + "\n\n Try again., ", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Empty fields.");
                if (LRNTextBox.Text == String.Empty) LRNTextBox.Focus();
                else if (lastNameTextBox.Text == String.Empty) lastNameTextBox.Focus();
                else if (firstNameTextBox.Text == String.Empty) firstNameTextBox.Focus();
                
            }
        }
        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            pictureBox.ImageLocation = null;
            openFileDialog1.ShowDialog();
            pictureBox.ImageLocation = openFileDialog1.FileName;
        }

        private void LRNTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void RFIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RFIDTextBox.TextLength == 10)
                RFIDTextBox.SelectAll();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
