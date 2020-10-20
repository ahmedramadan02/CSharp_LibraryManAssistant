using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using Mylibrary;

namespace LibraryAssistant
{
    public partial class Login : Form
    {
        public Login()
        {
            try
            {
                //
                //First do some checks
                if (!Addons.VersionCheck()) { MessageBox.Show("Please check your .Net version, install version 4"); throw new Exception(); }
                if (!Addons.IsFileExist("Mylibrary.dll")) { MessageBox.Show("File MyLibrary.dll not found, please check your files"); throw new Exception(); }
                if (!Addons.IsFileExist("LibraryAssistant.accdb")) { MessageBox.Show("File LibraryAssistant.accdb not found, please check your files"); throw new Exception(); }
                if (!Addons.IsFileExist("adminPasswords.xml")) { MessageBox.Show("File adminPasswords.xml not found, please check your files"); throw new Exception(); }

                InitializeComponent();
            }
            catch
            {
                MessageBox.Show("Application Error");
            }
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            OleDbConnection Cn;
            Cn = new OleDbConnection(Properties.Settings.Default.connectionString);
            //MessageBox.Show("Done");

            OleDbCommand Cmd = new OleDbCommand();

            try
            {

                //Constructing the command
                Cmd.Connection = Cn;
                Cmd.CommandText = "SELECT Name, Password" +
                    " FROM User_Accounts" +
                    " WHERE (Name = @name) AND (Password = @pass)";

                //Parameters
                OleDbParameter[] cmdParamaters = new OleDbParameter[2];
                cmdParamaters[0] = new OleDbParameter("@name", uName.Text);
                cmdParamaters[1] = new OleDbParameter("@pass", Password.Text);
                Cmd.Parameters.AddRange(cmdParamaters);

                //executing the command
                Cn.Open();
                OleDbDataReader Reader = Cmd.ExecuteReader();

                //Reads the data
                Reader.Read();
                if (Reader.HasRows)
                {
                    LibraryAssistant Assistant = new LibraryAssistant();
                    Assistant.ShowDialog();
                }
                else { MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة"); }

                Cn.Close();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Cn != null) Cn = null;
                if (Cmd != null) Cmd = null;
            }
        }

        private void NewUsr_lbl_Click(object sender, EventArgs e)
        {

            ToAdmin isAdmin = new ToAdmin();
            isAdmin.ShowDialog();
        }

    }
}
