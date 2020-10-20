using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mylibrary;
using System.Data.OleDb;

namespace LibraryAssistant
{
    public partial class Journals : Form
    {
        //Definations
        DataStructureLayer.Journals journalGUI;
        private int isUpdateTxt4Edit = 0;
        private int isUpdateTxt4Del = 0;

        public Journals()
        {
            InitializeComponent();
            
            //referances
            journalGUI = new DataStructureLayer.Journals(Properties.Settings.Default.connectionString);
        }

        private void btnJAdd_Click(object sender, EventArgs e)
        {
            //
            // Validiate controls
            //
            journalGUI.jTitle = txtJTitleAdd.Text;
            journalGUI.jSubject = txtJSubjectAdd.Text;
            journalGUI.jISSN = txtJISSNAdd.Text;
            journalGUI.jPublisher = txtJPublisherAdd.Text;
            journalGUI.JYear = Convert.ToString(datetimeJYearAdd.Value.Year);
            journalGUI.jVolume = txtJVolumeAdd.Text;
            journalGUI.JIssue = txtJIssueAdd.Text;

            //
            //Clear controls
            txtJTitleEdit.Text = "";
            txtJSubjectEdit.Text = "";
            txtJISSNEdit.Text = "";
            txtJPublisherEdit.Text = "";
            txtJVolumeEdit.Text = "";
            txtJIssueEdit.Text = "";
            txtJYearEdit.Text = "";
            //
            
            int aff;
            aff = journalGUI.AddNewJournal();

            if (aff > 0)
                MessageBox.Show("تم التسجيل بنجاح");
            else 
                MessageBox.Show("خطأ في تسجيل المجلة");      
        }

        private void txtJTitleEdit_TextChanged(object sender, EventArgs e)
        {
            //
            //if no enter key
            if (txtJTitleEdit.Text.Length > 2)
            {
                System.Threading.Thread.Sleep(125);

                object locker = new object();
                string cmd = "SELECT JTitle FROM Journals WHERE JTitle like @title order by JTitle asc";
                string searchTxt = txtJTitleEdit.Text + "%";
                OleDbParameter[] para = new OleDbParameter[1];
                para[0] = new OleDbParameter("@title", searchTxt);
                AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
                OleDbDataReader reader = journalGUI.GetReader(cmd, para);

                try
                {
                    lock (locker)
                    {
                        while (reader.Read())
                        {
                            if (isUpdateTxt4Edit > 1)
                                return;
                            autoComplete.Add(reader[0].ToString());
                        }
                    }

                    txtJTitleEdit.AutoCompleteCustomSource.Clear();
                    txtJTitleEdit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtJTitleEdit.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtJTitleEdit.AutoCompleteCustomSource = autoComplete;
                }
                finally
                {
                    if (reader != null) reader.Dispose();
                    if (autoComplete != null) autoComplete = null;
                    isUpdateTxt4Edit = 0;
                }//Cleaning the memory
                // END
            }//if two charachers at least
        }

        private void txtJTitleEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ++isUpdateTxt4Edit; 
        }

        private void txtJTitleEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtJTitleEdit.Text.ToString().Trim() != "")
                {
                    DataSet mySet = journalGUI.GetDataSet("SELECT * FROM Journals WHERE JTitle = '" + txtJTitleEdit.Text + "'");

                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        txtJTitleEdit.Text = mySet.Tables[0].Rows[0]["JTitle"].ToString();
                        txtJSubjectEdit.Text = mySet.Tables[0].Rows[0]["JSubject"].ToString();
                        txtJISSNEdit.Text = mySet.Tables[0].Rows[0]["JISSN"].ToString();
                        txtJPublisherEdit.Text = mySet.Tables[0].Rows[0]["JPublisher"].ToString();
                        txtJVolumeEdit.Text = mySet.Tables[0].Rows[0]["JVolume"].ToString();
                        txtJIssueEdit.Text = mySet.Tables[0].Rows[0]["JIssue"].ToString();
                        txtJYearEdit.Text = mySet.Tables[0].Rows[0]["JYear"].ToString();
                    }
                    else
                    {
                        //No records
                        MessageBox.Show("عفوا لا يوجد سجلات لهذة المجلة");
                    }
                } // Invalid charachter
            }// Enter key pressed
        }

        private void btnJEdit_Click(object sender, EventArgs e)
        {
            journalGUI.jTitle = txtJTitleEdit.Text;
            journalGUI.jSubject = txtJSubjectEdit.Text;
            journalGUI.jISSN = txtJISSNEdit.Text;
            journalGUI.jPublisher = txtJPublisherEdit.Text;
            journalGUI.JYear = txtJYearEdit.Text;
            journalGUI.jVolume = txtJVolumeEdit.Text;
            journalGUI.JIssue = txtJIssueEdit.Text;

            int aff;
            aff = journalGUI.ModifyJournal();

            if (aff > 0)
            {
                MessageBox.Show("تم التعديل بنجاح");
            }
            else
            {
                MessageBox.Show("خطأ في تعديل المجلة");
            }
        }

        private void txtJTitleDelete_TextChanged(object sender, EventArgs e)
        {
            //
            //if no enter key
            if (txtJTitleEdit.Text.Length > 2){
                System.Threading.Thread.Sleep(125);

            object locker = new object();
            string cmd = "SELECT JTitle FROM Journals WHERE JTitle like @title order by JTitle asc";
            string searchTxt = txtJTitleDelete.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            OleDbDataReader reader = journalGUI.GetReader(cmd, para);

            try
            {
                lock (locker)
                {
                    while (reader.Read())
                    {
                        if (isUpdateTxt4Del > 1)
                            return;
                        autoComplete.Add(reader[0].ToString());
                    }
                }

                txtJTitleDelete.AutoCompleteCustomSource.Clear();
                txtJTitleDelete.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtJTitleDelete.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtJTitleDelete.AutoCompleteCustomSource = autoComplete;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                if (autoComplete != null) autoComplete = null;
                isUpdateTxt4Del = 0;
            }//Cleaning the memory
            // END
            }//if two charachers at least
        }

        private void txtJTitleDelete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ++isUpdateTxt4Del; 
        }

        private void txtJTitleDelete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtJTitleDelete.Text.ToString().Trim() != "")
                {
                    DataSet mySet = journalGUI.GetDataSet("SELECT * FROM Journals WHERE JTitle = '" + txtJTitleDelete.Text + "'");

                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        txtJTitleDelete.Text = mySet.Tables[0].Rows[0]["JTitle"].ToString();
                        txtJSubjectDelete.Text = mySet.Tables[0].Rows[0]["JSubject"].ToString();
                        txtJISSNDelete.Text = mySet.Tables[0].Rows[0]["JISSN"].ToString();
                        txtJPublisherDelete.Text = mySet.Tables[0].Rows[0]["JPublisher"].ToString();
                        txtJVolumeDelete.Text = mySet.Tables[0].Rows[0]["JVolume"].ToString();
                        txtJIssueDelete.Text = mySet.Tables[0].Rows[0]["JIssue"].ToString();
                        txtJYearDelete.Text  = mySet.Tables[0].Rows[0]["JYear"].ToString();
                    }
                    else
                    {
                        //No records
                        MessageBox.Show("عفوا لا يوجد سجلات لهذة المجلة");
                    }
                } // Invalid charachter
            }// Enter key pressed
        }

        private void btnJDelete_Click(object sender, EventArgs e)
        {
            if (txtJISSNDelete.Text.Trim() != "")
            {
                journalGUI.jISSN = txtJISSNDelete.Text;
                int aff = journalGUI.DeleteJournal();
                if (aff > 0)
                {
                    MessageBox.Show("تم مسح المجلة");
                }
                else
                {
                    MessageBox.Show("خطأ في العملية .. لم يتم مسح سجلات المجلة");
                }

            }
            else
            {
                MessageBox.Show("من فضلك أختر المجلة أولاً");
            }
        }

        //
        //Valiadat numbers textboxes
        private void txtJISSNAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtJYearEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }
        //End of validiating
        //
    }
}
