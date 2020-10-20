using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace LibraryAssistant
{
    public partial class AdminBoard : Form
    {
        //General decleration
        Mylibrary.DataStructureLayer myWork = new Mylibrary.DataStructureLayer(Properties.Settings.Default.connectionString);

        public AdminBoard()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd;
                //1. Check for primary key
                cmd = "SELECT * FROM User_Accounts Where UserName = @name";
                OleDbParameter[] Parameters = new OleDbParameter[1];
                Parameters[0] = new OleDbParameter("@name",uName_txt.Text);
  
                DataTable myTable = myWork.GetDataSet(cmd,Parameters).Tables[0];
                if (myTable.Rows.Count > 0)
                {
                    MessageBox.Show("اسم عضو موجود مسبقا");
                    throw new Exception();
                }

                cmd = "INSERT INTO User_Accounts VALUES (@name,@pass)";

                OleDbParameter[] cmdParameters = new OleDbParameter[2];
                cmdParameters[0] = new OleDbParameter("@name", uName_txt.Text);
                cmdParameters[1] = new OleDbParameter("@pass", uPass_txt.Text);

                int aff = myWork.RunDML(cmd, cmdParameters);
                if (aff > 0)
                    MessageBox.Show("تمت اضافة العضو بنجاح");
                else
                    MessageBox.Show("فشلت العملية");

                //Upadate the cmd
                cmd = "SELECT * FROM User_Accounts";
                myTable = myWork.GetDataSet(cmd).Tables[0];
                dataGridView1.DataSource = myTable;
            }
            catch { 
            
            }
        }

        private void uName_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                string cmd;
                cmd = "SELECT * FROM User_Accounts Where UserName like  @name";
                OleDbParameter[] cmdParameters = new OleDbParameter[1];
                cmdParameters[0] = new OleDbParameter("@name",uName_txt.Text + "%");
  
                DataTable myTable = myWork.GetDataSet(cmd,cmdParameters).Tables[0];

                if (myTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = myTable;
                    uPass_txt.Text = myTable.Rows [0][1].ToString ();
                }
                else
                {
                    MessageBox.Show("اسم العضو غير موجود");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (uName_txt.Text.Trim() != "" && uPass_txt.Text.Trim() != "")
            {
                // Tip :: Password is a reserved keyword so you need to put it between [] 
                // ==> Write it [Password] NOT Password
                string cmd = "UPDATE User_Accounts SET [Password] = @password Where UserName = @user";

                //Parameters
                OleDbParameter[] CmdParameters = new OleDbParameter[2];
                CmdParameters[0] = new OleDbParameter("@pass", uPass_txt.Text);
                CmdParameters[1] = new OleDbParameter("@user", uName_txt.Text);

                //EXEC UPDATE
                int aff = myWork.RunDML(cmd, CmdParameters);
                if (aff > 0)
                    MessageBox.Show("تم تغيير كلمة المرور");
                else
                    MessageBox.Show("فشلت العملية");

                //Upadate dataView
                cmd = "SELECT * FROM User_Accounts";
                DataTable myTable = myWork.GetDataSet(cmd).Tables[0];
                dataGridView1.DataSource = myTable;

            }else
                MessageBox.Show("من فضلك أملا البيانات أولا");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (uName_txt.Text.Trim() != "") {
                string cmd = "DELETE * FROM User_Accounts WHERE UserName=@user";

                //Parameters
                OleDbParameter[] CmdParameters = new OleDbParameter[1];
                CmdParameters[0] = new OleDbParameter("@user", uName_txt.Text);

                int aff = myWork.RunDML(cmd, CmdParameters);
                if (aff > 0)
                    MessageBox.Show("تم مسح العضو");
                else
                    MessageBox.Show("فشلت العملية");

                //Upadate dataView
                cmd = "SELECT * FROM User_Accounts";
                DataTable myTable = myWork.GetDataSet(cmd).Tables[0];
                dataGridView1.DataSource = myTable;
            
            }
        }
    }
}
