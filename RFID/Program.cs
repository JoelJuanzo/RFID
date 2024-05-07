using RFID;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace RFID
{
    internal static class Program
    {
        #region Declaration of Global Variables
        public static string DialogTitle = "RNCHS RFID";
        public static string userName = null;
        public static string serverName = null;
        public static string currentAccountType = null;

        public static bool isSettings = false;
        public static bool isLogInHistory = false;
        public static bool isUserAccounts = false;



        public static int identificationNumber = 0;


        public static string usersFullName = null;
        public static int scheduleId = 0;
        public static string logInDate = null;
        public static string logInTime = null;
        public static string dateTimeLogIn = null;
        public static string dateTimeLogOut = null;

        public static bool isAddingPriest = false;
        public static bool isAddingChurch = false;

        public static bool openUserAccounts = false;
        public static bool openSettings = false;
        public static bool openLogTransaction = false;
        public static bool openLogInHistory = false;

        public static string systemExit = "Exiting...";
        public static string systemSaving = "Saving...";
        public static string systemSaved = "Saved Successfully...";
        public static string systemAdding = "Adding...";
        public static string systemAdded = "Added Successfully...";
        public static string systemLoggingOff = "Logging Off...";
        public static string systemSearch = "Search...";
        public static string systemRecordFound = "Total Record(s) Found:";

        public static bool isConfirmation = false;
        public static bool isBaptismal = false;
        public static bool isWedding = false;
        public static bool isDeath = false;


        #endregion

        private static string _connectionString = null;

        public static string[] gender = { "Male", "Female" };
        public static string[] accountTypes = { "Administrator", "Client", "Guest" };
        public static string[] civilStatus = { "Single", "Married", "Legally Separated", "Divorced", "Widow(er)" };
        //public static string dialogTitle = "RNCHS RFID System";

        public static void SetConnectionString(string serverName, string databaseName)
        {
            _connectionString = "Server = " + serverName + ";Database=" + databaseName + ";User Id = root; Password=admin";
        }

        public static string DataConnectionString
        {
            get { return _connectionString; }
        }

        public static string ConvertToProper(string stringName)
        {
            string newStr = "";
            if (stringName != string.Empty)
            {
                string _stringName = stringName.Trim();
                int lengthString = _stringName.Length;
                bool isSpace = true;

                for (int i = 0; i <= lengthString - 1; i++)
                {
                    if (isSpace)
                    {
                        newStr += Char.ToUpper(_stringName[i], CultureInfo.CurrentCulture);
                        isSpace = false;
                    }
                    else
                    {
                        if (char.IsUpper(_stringName[i]) == true)
                            newStr += Char.ToLower(_stringName[i], CultureInfo.CurrentCulture);
                        else
                            newStr += _stringName[i];
                    }
                    if (Char.IsWhiteSpace(_stringName[i]) == true)
                        isSpace = true;
                }
            }
            return newStr;
        }


        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }





        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            SetConnectionString("LocalHost", "rnchs");
            ApplicationConfiguration.Initialize();
            //Application.Run(new StudentForm());
            //Application.Run(new LogInForm());

            


            LogInForm logInWindowForm = new LogInForm(null);
            logInWindowForm.ShowDialog();
            if (logInWindowForm.logInSuccessFully == true)
            {
                logInWindowForm.Close();
                Application.Run(new MDIForm());
            }
            else
                logInWindowForm.Close();


        }
    }
}