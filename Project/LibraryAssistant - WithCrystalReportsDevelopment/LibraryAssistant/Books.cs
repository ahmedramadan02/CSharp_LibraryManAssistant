using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using Mylibrary;

namespace LibraryAssistant
{
    public partial class Books : Form
    {
        //Library decleration
        Mylibrary.DataStructureLayer.Books bookGUI;

        public Books()
        {
            InitializeComponent();

            //Library defination
            bookGUI = new DataStructureLayer.Books(Properties.Settings.Default.connectionString);
        }

        private void btnBAdd_Click(object sender, EventArgs e)
        {
           //
           //Validiation Code done in the DataStructureLayer
            
           bookGUI.bTitle = txtBTitleAdd.Text;
           bookGUI.bAuthor = txtBAuthorAdd.Text;
           bookGUI.bSubject = txtBSubjectAdd.Text;
           bookGUI.bISBN = txtBISBNAdd.Text;
           bookGUI.bAccNo = txtBAccNoAdd.Text;
           bookGUI.bClassNo = txtBClassNoAdd.Text;
           bookGUI.bPublisher = txtBPublisherAdd.Text;
           bookGUI.bPlaceofPub = txtBPlaceOfPubAdd.Text;
           bookGUI.BDateOfPub = Convert.ToString(dateOfPubBAdd.Value.Year);

            int aff; 
            aff = bookGUI.AddNewBook();

            if (aff > 0)
            {
                MessageBox.Show("تم التسجيل بنجاح");
            }
            else {
                MessageBox.Show("خطأ في تسجيل الكتاب");
            }

            //Clearing the textboxes
            //
            txtBTitleAdd.Text = "";
            txtBAuthorAdd.Text = "";
            txtBSubjectAdd.Text = "";
            txtBISBNAdd.Text = "";
            txtBAccNoAdd.Text = "";
            txtBClassNoAdd.Text = "";
            txtBPublisherAdd.Text = "";
            txtBPlaceOfPubAdd.Text = "";
            dateOfPubBAdd.Value = new DateTime(2000,1,1);
            //
        }

        private void btnBEdit_Click(object sender, EventArgs e)
        {
            //bookGUI.bTitle = txtBTitleEdit.Text;
            bookGUI.bAuthor = txtBAuthorEdit.Text;
            bookGUI.bSubject = txtBSubjectEdit.Text;
            bookGUI.bISBN = txtBISBNEdit.Text;
            bookGUI.bAccNo = txtBAccNoEdit.Text;
            bookGUI.bClassNo = txtBClassNoEdit.Text;
            bookGUI.bPublisher = txtBPublisherEdit.Text;
            bookGUI.bPlaceofPub = txtBPlaceOfPubEdit.Text;
            bookGUI.BDateOfPub = txtBDateOfPubEdit.Text;

            int aff;          
            aff = bookGUI.ModifyBook();

            if (aff > 0)
            {
                MessageBox.Show("تم التعديل بنجاح");
            }
            else
            {
                MessageBox.Show("خطأ في تعديل الكتاب");
            }
        }

        private void txtBTitleEdit_TextChanged(object sender, EventArgs e)
        {
            //if no enter key
            // 1. Construct the Q 
            string cmd = "SELECT BTitle FROM Books where BTitle like @title order by BTitle asc";
            string searchTxt = txtBTitleEdit.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);

            //2. Exec command !
            txtBTitleEdit.UpdateMe(bookGUI.GetDataSet(cmd, para).Tables[0]);
        }

        private void txtBTitleEdit_KeyDown(object sender, KeyEventArgs e)
        {
            // if enter key
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBTitleEdit.Text.ToString().Trim() != "")
                {
                    DataSet mySet = bookGUI.GetDataSet("SELECT * FROM Books WHERE BTitle = '" + txtBTitleEdit.Text + "'");

                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        txtBTitleEdit.Text = mySet.Tables[0].Rows[0]["BTitle"].ToString();
                        txtBAuthorEdit.Text = mySet.Tables[0].Rows[0]["BAuthor"].ToString();
                        txtBSubjectEdit.Text = mySet.Tables[0].Rows[0]["BSubject"].ToString();
                        txtBISBNEdit.Text = mySet.Tables[0].Rows[0]["BISBN"].ToString();
                        txtBAccNoEdit.Text = mySet.Tables[0].Rows[0]["BAccNo"].ToString();
                        txtBClassNoEdit.Text = mySet.Tables[0].Rows[0]["BClassNo"].ToString();
                        txtBPublisherEdit.Text = mySet.Tables[0].Rows[0]["BPublisher"].ToString();
                        txtBPlaceOfPubEdit.Text = mySet.Tables[0].Rows[0]["BPlaceOfPub"].ToString();
                        txtBDateOfPubEdit.Text = mySet.Tables[0].Rows[0]["BDateOfPub"].ToString();
                    }
                    else
                    {
                        //No records
                        MessageBox.Show("عفوا لا يوجد سجلات لهذا الكتاب");
                    }
                } // Invalid charachter
            }// Enter key pressed
        }

        private void btnBDelete_Click(object sender, EventArgs e)
        {
                if (txtBISBNDelete.Text.Trim() != "")
                {
                    bookGUI.bISBN = txtBISBNDelete.Text;
                    int aff = bookGUI.DeleteBook();
                    if (aff > 0)
                    {
                        MessageBox.Show("تم مسح الكتاب");
                    }
                    else {
                        MessageBox.Show("خطأ في العملية .. لم يتم مسح سجلات الكتاب");
                    }

                }
                else
                {
                    MessageBox.Show("من فضلك أختر الكتاب أولاً");
                } 
        }

        private void txtBTitleDelete_TextChanged(object sender, EventArgs e)
        {
            //
            //if no enter key
            //if no enter key
            // 1. Construct the Q 
            string cmd = "SELECT BTitle FROM Books where BTitle like @title order by BTitle asc";
            string searchTxt = txtBTitleDelete.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);

            //2. Exec command !
            txtBTitleDelete.UpdateMe(bookGUI.GetDataSet(cmd, para).Tables[0]);
        }

        private void txtBTitleDelete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBTitleDelete.Text.ToString().Trim() != "")
                {
                    DataSet mySet = bookGUI.GetDataSet("SELECT * FROM Books WHERE BTitle = '" + txtBTitleDelete.Text + "'");

                    if (mySet.Tables[0].Rows.Count > 0)
                    {
                        txtBTitleDelete.Text = mySet.Tables[0].Rows[0]["BTitle"].ToString();
                        txtBAuthorDelete.Text = mySet.Tables[0].Rows[0]["BAuthor"].ToString();
                        txtBSubjectDelete.Text = mySet.Tables[0].Rows[0]["BSubject"].ToString();
                        txtBISBNDelete.Text = mySet.Tables[0].Rows[0]["BISBN"].ToString();
                        txtBAccNoDelete.Text = mySet.Tables[0].Rows[0]["BAccNo"].ToString();
                        txtBClassNoDelete.Text = mySet.Tables[0].Rows[0]["BClassNo"].ToString();
                        txtBPublisherDelete.Text = mySet.Tables[0].Rows[0]["BPublisher"].ToString();
                        txtBPlaceOfPubDelete.Text = mySet.Tables[0].Rows[0]["BPlaceOfPub"].ToString();
                        txtBDateOfPubDelete.Text = mySet.Tables[0].Rows[0]["BDateOfPub"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("عفوا لا يوجد سجلات لهذا الكتاب");
                    }
                }
            }
        }

        //
        //Valiadat numbers textboxes
        private void txtBISBNEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);

        }

        private void txtBAccNoEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtBClassNoEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtBISBNAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtBAccNoAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtBClassNoAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtBDateOfPubEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 127 || e.KeyChar == 8) return;
            e.Handled = !char.IsNumber(e.KeyChar);
        }
        //
        //End Validating
    }
}
