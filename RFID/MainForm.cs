using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.IO.Ports;
using MySql.Data.MySqlClient;
using System.Web;
using System.Diagnostics;

namespace RFID
{
    public partial class MainForm : Form
    {
        public string fullName = "";
        public int _studentIdNumber = 0;
        public string timeNow = null;
        public string timeStatus = "";
        
        public void RFID()
        {       
            if (_studentIdNumber != 0)
            {
                
            }
        }

        private void CheckStatus(int idNumber, string timeNow)
        {
            int counter = 0;
            DataTable attendance = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT *FROM Attendance WHERE StudentId = '" + idNumber + "' AND Date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'").Tables[0];
            MessageBox.Show(attendance.Rows.Count.ToString());
            if (attendance.Rows.Count > 0)
            {
                counter = Convert.ToInt16(attendance.Rows[0]["counter"].ToString());
                UpdateTime(idNumber, timeNow, counter);
            }
            else if(attendance.Rows.Count == 0)  
            {
                AddTime(idNumber, timeNow);
            }    

        }
        private void AddTime(int studentId, string timeNow)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Program.DataConnectionString, "INSERT INTO Attendance (StudentID, TimeInAM, Date, Counter) VALUES (?StudentID, ?TimeInAM, ?Date, ?Counter)",
                    new MySqlParameter("?StudentId", studentId),
                    new MySqlParameter("?TimeInAm", Convert.ToDateTime(timeNow).ToShortTimeString()),
                    new MySqlParameter("?Date", DateTime.Now),
                    new MySqlParameter("?Counter", 1 ));
                statusLabel.Text = "TIME IN - AM: " + Convert.ToDateTime(timeNow).ToShortTimeString();
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void UpdateTime(int studentId, string timeNow, int counter)
        {
            try
            {
                if (counter == 4)
                {
                    MessageBox.Show("Already timed out.");
                    return;
                }
                else if (counter == 1)
                {
                    MySqlHelper.ExecuteNonQuery(Program.DataConnectionString, "UPDATE Attendance SET TimeOutAM = ?TimeOutAM, Counter = ?Counter WHERE (StudentId = ?StudentId AND Date = ?Date)",
                        new MySqlParameter("?TimeOutAM", timeNow),
                        new MySqlParameter("?studentID", studentId),
                        new MySqlParameter("?Date", DateTime.Now.ToString("yyyy-MM-dd")),
                        new MySqlParameter("?Counter", counter + 1));
                    statusLabel.Text = "TIME OUT - AM: " + Convert.ToDateTime(timeNow).ToShortTimeString();
                }
                else if (counter == 2)
                {
                    MySqlHelper.ExecuteNonQuery(Program.DataConnectionString, "UPDATE Attendance SET TimeInPM = ?TimeInPM,  Counter = ?Counter WHERE (StudentId = ?StudentId AND Date = ?Date)",
                        new MySqlParameter("?TimeInPM", timeNow),
                        new MySqlParameter("?studentID", studentId),
                        new MySqlParameter("?Date", DateTime.Now.ToString("yyyy-MM-dd")),
                        new MySqlParameter("?Counter", counter + 1));
                    statusLabel.Text = "TIME IN - PM: " + Convert.ToDateTime(timeNow).ToShortTimeString();
                }
                else if (counter == 3)
                {
                    MySqlHelper.ExecuteNonQuery(Program.DataConnectionString, "UPDATE Attendance SET TimeOutPM = ?TimeOutPM,  Counter = ?Counter WHERE (StudentId = ?StudentId AND Date = ?Date)",
                        new MySqlParameter("?TimeOutPM", timeNow),
                        new MySqlParameter("?studentID", studentId),
                        new MySqlParameter("?Date", DateTime.Now.ToString("yyyy-MM-dd")),
                        new MySqlParameter("?Counter", counter + 1));
                    statusLabel.Text = "TIME OUT - PM:" + Convert.ToDateTime(timeNow).ToShortTimeString();
                }


            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void Search(string RFIDNumber)
        {
            //var watch = Stopwatch.StartNew();
            byte[] imageByte = null;
            Image image = null;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            timeNow = timeLabel.Text;
            DataTable record = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT StudentId, LRN, RFID, LastName, FirstName, Middlename, Picture FROM Student WHERE RFID = '" + RFIDNumber + "'").Tables[0];
            if (record.Rows.Count > 0)
            {
                timeNow = Convert.ToDateTime(timeLabel.Text).ToShortTimeString();
                //statusLabel.Text = timeNow;

                fullName = record.Rows[0]["Lastname"].ToString() + "," + record.Rows[0]["firstName"];
                fullNameLabel.Text = fullName;
                _studentIdNumber = Convert.ToInt32(record.Rows[0]["StudentId"].ToString());
                CheckStatus(_studentIdNumber, timeNow);
                lrnLabel.Text = record.Rows[0]["LRN"].ToString();
                imageByte = (byte[])record.Rows[0]["picture"];
                if (imageByte != null)
                {
                    using (var ms = new MemoryStream(imageByte))
                    {
                        image = Image.FromStream(ms);
                    }
                    pictureBox.Image = image;
                }
                //watch.Stop();
                //MessageBox.Show(watch.ElapsedMilliseconds + " ms") ;
                /*
                DataTable RFIDTable = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT *FROM RFID WHERE StudentId = '" + _studentIdNumber + "' AND '" + "Date = '" + DateTime.Now + "'").Tables[0];
                if (RFIDTable.Rows.Count > 0)
                { 
                
                }
                */


                //foreach (DataRow dataRow in section.Rows)
                //  sectionComboBox.Items.Add(dataRow["SectionName"].ToString());
            }
            else
            {
                MessageBox.Show("Student not yet registered. Register first its RFID Card.");
            }    

        }

        private void LoadImage()
        { 

        }


        public MainForm()
        {
            InitializeComponent();
            UpdateTimeLabel();
            timer1.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //timeLabel.Text = DateTime.Now.TimeOfDay.ToString();
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            dateLabel.Text = DateTime.Now.ToLongDateString();
            
            
        }
        private void UpdateTimeLabel()
        {
            timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTimeLabel();
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }


        private void RFIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RFIDTextBox.TextLength == 10)
            {
                RFIDTextBox.SelectAll();
                Search(RFIDTextBox.Text.Trim());

            }
                
        }


    }
    
}
