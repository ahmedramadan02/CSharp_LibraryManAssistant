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
        private int isUpdateTxt4Edit = 0;
        private int isUpdateTxt4Del = 0;
       
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
            bookGUI.bTitle = txtBTitleEdit.Text;
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
            //
            //et2al my friend
            

            //
            //if no enter key
            if (txtBTitleEdit.Text.Length > 2){
                System.Threading.Thread.Sleep(125);

                object locker = new object();
                string cmd = "SELECT BTitle FROM Books where BTitle like @title order by BTitle asc";
                string searchTxt = txtBTitleEdit.Text + "%";
                OleDbParameter[] para = new OleDbParameter[1];
                para[0] = new OleDbParameter("@title", searchTxt);
                AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
                OleDbDataReader reader = bookGUI.GetReader(cmd,para);

                try
                {
                    lock (locker)
                    {
                        while (reader.Read())
                        {
                            if(isUpdateTxt4Edit > 1) 
                                return; //return to save memory
                            autoComplete.Add(reader[0].ToString());
                        }
                    }

                    txtBTitleEdit.AutoCompleteCustomSource.Clear();
                    txtBTitleEdit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtBTitleEdit.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtBTitleEdit.AutoCompleteCustomSource = autoComplete;
                }
                finally {
                    isUpdateTxt4Edit = 0;
                    if (reader != null) reader.Dispose();
                    if (autoComplete != null) autoComplete = null;
                }//Cleaning the memory
                // END
            }//if two charachers at least
        }
        
        private void txtBTitleEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ++isUpdateTxt4Edit;            
        }

        private void txtBTitleEdit_KeyDown(object sender, KeyEventArgs e)
        {
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
            if (txtBTitleDelete.Text.Length > 2){
            System.Threading.Thread.Sleep(125);

            object locker = new object();
            string cmd = "SELECT BTitle FROM Books where BTitle like @title order by BTitle asc";
            string searchTxt = txtBTitleDelete.Text + "%";
            OleDbParameter[] para = new OleDbParameter[1];
            para[0] = new OleDbParameter("@title", searchTxt);
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            OleDbDataReader reader = bookGUI.GetReader(cmd, para);

            try
            {
                lock (locker)
                {
                    while (reader.Read())
                    {
                        if (isUpdateTxt4Del > 1) return;
                        autoComplete.Add(reader[0].ToString());
                    }
                }

                txtBTitleDelete.AutoCompleteCustomSource.Clear();
                txtBTitleDelete.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtBTitleDelete.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtBTitleDelete.AutoCompleteCustomSource = autoComplete;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                if (autoComplete != null) autoComplete = null;
                if (locker != null) locker = null;
                isUpdateTxt4Del = 0;
            }//Cleaning the memory
            // END
            }//if two charachers at least
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
