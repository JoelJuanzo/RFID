using MySql.Data.MySqlClient;
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
    
    public partial class DisplayAttendance : Form
    {
        public Boolean isClick = false;
        public DisplayAttendance()
        {

            InitializeComponent();
            LoadSection();
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
        private void DisplayAttendance_Load(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
 
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Search(string section, string date)
        {
            //DataTable records = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT *FROM Attendance WHERE ").Tables[0];
            
            DataTable dataTable = MySqlHelper.ExecuteDataset(Program.DataConnectionString, @"SELECT Student.StudentId, Student.LRN, Student.LastName, Student.Firstname, Student.Section, Attendance.StudentId, Attendance.TimeInAM, Attendance.TimeOutAM, Attendance.TimeInPM, Attendance.TimeOutPM, Attendance.Date FROM Student INNER JOIN Attendance WHERE (Student.StudentId = Attendance.Studentid AND Attendance.Date =?Date AND Student.Section=?Section)",
                new MySqlParameter("?Date", date),
                new MySqlParameter("?Section", sectionComboBox.Text)).Tables[0];


            //MessageBox.Show(dataTable.Rows.Count.ToString());
            if (dataTable.Rows.Count == 0)
            {
                attendanceDataGridView.Rows.Clear();
                MessageBox.Show("No Record Found.", Program.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //searchToolStripTextBox.Focus();
                //searchToolStripTextBox.SelectAll();
                //this.Cursor = Cursors.Default;
                return;
            }

            if (dataTable.Rows.Count > 0)
            {
                attendanceDataGridView.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                   attendanceDataGridView.Rows.Add(Convert.ToDateTime(row["Date"].ToString()).ToShortDateString(), row["LRN"].ToString(), row["LastName"].ToString() +", " + row["FirstName"].ToString(), row["TimeInAM"].ToString(), row["TimeOutAM"].ToString(), row["TimeInPM"].ToString(), row["TimeOutPM"].ToString());
                }
            }

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void viewAttendance()
        {
            if (sectionComboBox.Text != string.Empty)
            {
                DateTime selectedDate = attendanceMonthCalendar.SelectionRange.Start;
                string shortDate = selectedDate.ToString("yyyy-MM-dd");
                Search(sectionComboBox.Text, shortDate);
            }

        }

        private void viewButton_Click(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            int counter = 1;
            if (attendanceDataGridView.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                ex.Workbooks.Add(Application.StartupPath + "\\templates\\Attendance.xlt");
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)ex.Worksheets[1];

                int rowIndex = 8;
                foreach (DataGridViewRow row in attendanceDataGridView.Rows)
                {
                    ws.Cells[rowIndex, 1] = counter.ToString();
                    ws.Cells[rowIndex, 2] = row.Cells[this.DateColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 3] = row.Cells[this.LRNColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 4] = row.Cells[this.fullNameColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 5] = row.Cells[this.timeInAMColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 6] = row.Cells[this.timeOutAMColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 7] = row.Cells[this.timeInPMColumn.Index].FormattedValue.ToString();
                    ws.Cells[rowIndex, 8] = row.Cells[this.timeOutPMColumn.Index].FormattedValue.ToString();
                    counter++;
                    rowIndex++;
                }

                this.Cursor = Cursors.Default;

                ex.Visible = true;
            }
        }

        private void sectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void attendanceMonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (sectionComboBox.Text != String.Empty)
            {
                viewAttendance();
            }
        }

        private void sectionComboBox_TextChanged(object sender, EventArgs e)
        {
            if ((sectionComboBox.Text != String.Empty) && (isClick ==true))
            {
                viewAttendance();
            }
        }

        private void sectionComboBox_Click(object sender, EventArgs e)
        {
            if (isClick == false)
                isClick = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
