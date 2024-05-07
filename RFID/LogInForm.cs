using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;



using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data;
using RFID;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RFID
{
    public partial class LogInForm : Form
    {
        public bool cancelButtonIsPressed = false;
        public bool logInSuccessFully = false;
        public bool resultIsCancel = false;
        public string dialogTitle = "System Log Information";
        public bool _okSelected = false;
        public string inputPassword = "";
        public string inputUserName = "";
        public MDIForm _mdiForm = null;
        public bool isValid = false;

        public LogInForm(MDIForm mdiform)
        {
            InitializeComponent();
            _mdiForm = mdiform;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (userNameTextBox.Text == String.Empty)
                userNameTextBox.Focus();
            else if (passWordTextBox.Text == String.Empty)
                passWordTextBox.Focus();

            if (userNameTextBox.Text != String.Empty && passWordTextBox.Text != String.Empty)
            {
                _okSelected = true;
                this.Cursor = Cursors.WaitCursor;
                //logInButton.Enabled = false;
                try
                {
                    inputPassword = passWordTextBox.Text;
                    inputUserName = userNameTextBox.Text;

                    bool isValid = false;
                    DataTable _userAccountsDataTable = MySqlHelper.ExecuteDataset(Program.DataConnectionString, "SELECT UserName, Password FROM user WHERE UserName = '" + inputUserName + "' AND Password = '" + inputPassword + "'").Tables[0];


                    if (_userAccountsDataTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Successfully Log In \n", this.dialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        logInSuccessFully = true;
                        this.Close();
                    }
                    else {
                        MessageBox.Show("Invalid Password or UserName!", this.dialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //logInButton.Enabled = true;
                        logInSuccessFully = false;
                        userNameTextBox.Focus();
                        userNameTextBox.SelectAll();
                    }
                   

                    /*
                    foreach (DataRow _dataTable in _userAccountsDataTable.Rows)
                    {
                        if (Program.Decrypt(_dataTable["UserName"].ToString()) == inputUserName && Program.Decrypt(Program.Decrypt(Program.Decrypt(_dataTable["PassWord"].ToString()))) == inputPassword)
                        {
                            Program.currentAccountType = _dataTable["AccountType"].ToString();
                            Program.usersFullName = _dataTable["FirstName"].ToString() + " " + (_dataTable["MiddleName"].ToString())[0].ToString() + ". " + (_dataTable["LastName"].ToString());
                            isValid = true;
                            break;
                        }
                    }
                    */
                }
                catch (MySqlException _sqlErrorInConnection)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(_sqlErrorInConnection.Message.ToString(), "Error in Connection. Please try again.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                this.Cursor = Cursors.Default;

            }
        }


        private void ResultIsCancel()
        {
            cancelButtonIsPressed = false;
            if (userNameTextBox.Text == "")
                userNameTextBox.Focus();
            else if (passWordTextBox.Text == "")
                passWordTextBox.Focus();
            else
                okButton.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            cancelButtonIsPressed = true;
            if (!(logInSuccessFully))
            {
                DialogResult result = MessageBox.Show("Are you sure do you want to Exit? ", this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    logInSuccessFully = false;
                    this.Close();
                }
                else
                {
                    logInSuccessFully = false;
                    ResultIsCancel();
                }
            }
        }

        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        //SendMessage(textBox1.Handle, EM_SETCUEBANNER, 0, "Username");
        private void LogInForm_Load(object sender, EventArgs e)
        {

        }
        private void IfEnterKeyIsHit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userNameTextBox.Text == "")
                    userNameTextBox.Focus();
                else if (passWordTextBox.Text == "")
                    passWordTextBox.Focus();
                else
                    okButton_Click(sender, e);
            }
        }
        private void IfEscKeyIsPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                exitButton_Click(sender, e);
            }
        }
        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passWordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void userNameTextBox_Enter(object sender, EventArgs e)
        {
            if (userNameTextBox.Text != String.Empty)
                userNameTextBox.SelectAll();
        }

        private void passWordTextBox_Enter(object sender, EventArgs e)
        {
            if (passWordTextBox.Text != String.Empty)
                passWordTextBox.SelectAll();
        }

        private void userNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            IfEnterKeyIsHit(sender, e);
            IfEscKeyIsPressed(sender, e);
        }

        private void passWordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            IfEnterKeyIsHit(sender, e);
            IfEscKeyIsPressed(sender, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Under maintenance.");
        }
    }
}