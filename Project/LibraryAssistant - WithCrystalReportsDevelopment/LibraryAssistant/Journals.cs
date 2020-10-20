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
            //if no enter key
            // 1. Construct the Q 
            string cmd = "SELECT JTitle FROM journals where JTitle like @title order by JTitle asc";
            string searchTxt = txtJTitleEdit.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);

            //2. Exec command !
            txtJTitleEdit.UpdateMe(journalGUI.GetDataSet(cmd, para).Tables[0]);
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
            //if no enter key
            // 1. Construct the Q 
            string cmd = "SELECT JTitle FROM journals where JTitle like @title order by JTitle asc";
            string searchTxt = txtJTitleDelete.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);

            //2. Exec command !
            txtJTitleDelete.UpdateMe(journalGUI.GetDataSet(cmd, para).Tables[0]);
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
                        txtJYearDelete.Text = mySet.Tables[0].Rows[0]["JYear"].ToString();
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
