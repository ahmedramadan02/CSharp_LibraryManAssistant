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
    public partial class LibraryAssistant : Form
    {
        //Library decleration
        DataStructureLayer.Books bookGUI;
        DataStructureLayer.Journals journalGUI;
        private int selectedIndex;
        private int isUpdateTxt = 0;

        //Create enums decleration
        //Book enum
        string[] bookSearchBy = new string[9] { "BTitle", "BAuthor", "BSubject", "BISBN",
                "BAccNo", "BClassNo", "BPublisher", "BPlaceofPub", "BDateOfPub" };

        string[] journalSearchBy = new string[7] {"JTitle", "JSubject", "JISSN", "JPublisher", 
                "JYear", "JVolume", "JIssue"};

        // Journal enum
        

        public LibraryAssistant()
        {
            InitializeComponent();

                //Initlaize all work
                //Books form
                Books B1 = new Books();
                B1.TopLevel = false;
                B1.Size = tabBooks.Size;
                B1.Dock = DockStyle.Fill;
                tabBooks.Controls.Add(B1);
                B1.Show();

                //Journals form
                Journals J1 = new Journals();
                J1.TopLevel = false;
                J1.Size = tabJournals.Size;
                J1.Dock = DockStyle.Fill;
                tabJournals.Controls.Add(J1);
                J1.Show();

                //initial BookGUI
                bookGUI = new DataStructureLayer.Books(Properties.Settings.Default.connectionString);
                journalGUI = new DataStructureLayer.Journals(Properties.Settings.Default.connectionString);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string[] bookCollection = new string[9] { "Book Title", "Book Author", "Book Subject", "Book ISBN",
                "Book AccNo", "Book ClassNo", "Book Publisher", "Book PlaceofPub", "Book DateOfPub" };
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox1.Items.AddRange(bookCollection);

            bookGUI.Dispose();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string[] journalsCollection = new string[7] {"Journal Title", "Journal Subject", " Journal ISSN", "Journal Publisher", 
                "Journal Year", "Journal Volume", "Journal Issue"};
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox1.Items.AddRange(journalsCollection);

            journalGUI.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() != "" && (radioButton1.Checked == true || radioButton2.Checked == true))
            {
                if (radioButton1.Checked == true && txtSearch.Text.Length > 2)
                {
                    System.Threading.Thread.Sleep(125);

                    //Prepare work
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();

                    //Get the Selected values
                    selectedIndex = comboBox1.SelectedIndex;

                    //
                    //if no enter key
                    //Creating the commandTexts

                    object locker = new object();
                    string cmd4AutoComplete = "SELECT " + bookSearchBy[selectedIndex] + " FROM Books where " + bookSearchBy[selectedIndex] 
                        + " like @title order by " + bookSearchBy[selectedIndex] + " asc";

                    string cmd4GridView = "SELECT * FROM Books where " + bookSearchBy[selectedIndex]
                        + " like @title order by " + bookSearchBy[selectedIndex] + " asc";

                    string searchTxt = txtSearch.Text + "%";
                    OleDbParameter[] para = new OleDbParameter[1];
                    para[0] = new OleDbParameter("@title", searchTxt);

                    //For the databindgrid
                    DataTable myTable = new DataTable();
                    myTable = bookGUI.GetDataSet(cmd4GridView, para).Tables[0];

                    //for the AutoComplete
                    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
                    OleDbDataReader reader = bookGUI.GetReader(cmd4AutoComplete, para);
                                        
                    try
                    {
                        lock (locker)
                        {
                            while (reader.Read())
                            {
                                if (isUpdateTxt > 1)
                                    return;
                                autoComplete.Add(reader[0].ToString());
                            }
                        }
                        //AutoComplete 
                        txtSearch.AutoCompleteCustomSource.Clear();
                        txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
                        txtSearch.AutoCompleteCustomSource = autoComplete;

                        //Datagridview
                        dataGridView1.DataSource = myTable;
                    }
                    finally
                    {
                        if (reader != null) reader.Dispose();
                        if (autoComplete != null) autoComplete = null;
                        if (myTable != null) myTable.Dispose();
                        isUpdateTxt = 0;
                    }//Cleaning the memory
                    // END

                }
                if (radioButton2.Checked == true && txtSearch.Text.Length > 2)
                {
                    //Prepare work
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();

                    //Get the Selected values
                    selectedIndex = comboBox1.SelectedIndex;

                    //
                    //if no enter key
                    object locker = new object();
                    string cmd4AutoComplete = "SELECT " + journalSearchBy[selectedIndex] + " FROM Journals where " + journalSearchBy[selectedIndex]
                        + " like @title order by " + journalSearchBy[selectedIndex] + " asc";

                    string cmd4GridView = "SELECT * FROM Journals where " + journalSearchBy[selectedIndex]
                        + " like @title order by " + journalSearchBy[selectedIndex] + " asc";

                    string searchTxt = txtSearch.Text + "%";
                    OleDbParameter[] para = new OleDbParameter[1];
                    para[0] = new OleDbParameter("@title", searchTxt);

                    //For the databindgrid
                    DataTable myTable = new DataTable();
                    myTable = journalGUI.GetDataSet(cmd4GridView, para).Tables[0];

                    //for the AutoComplete
                    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
                    OleDbDataReader reader = journalGUI.GetReader(cmd4AutoComplete, para);

                    try
                    {
                        lock (locker)
                        {
                            while (reader.Read())
                            {
                                if (isUpdateTxt > 1)
                                    return;
                                autoComplete.Add(reader[0].ToString());
                            }
                        }
                        //AutoComplete 
                        txtSearch.AutoCompleteCustomSource.Clear();
                        txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
                        txtSearch.AutoCompleteCustomSource = autoComplete;

                        //Datagridview
                        dataGridView1.DataSource = myTable;
                    }
                    finally
                    {
                        if (reader != null) reader.Dispose();
                        if (autoComplete != null) autoComplete = null;
                        if (myTable != null) myTable.Dispose();
                        isUpdateTxt = 0;
                    }//Cleaning the memory
                    // END
                }
            }
            else
            {
                txtSearch.Clear();
                MessageBox.Show("اختر اسلوب البحث أولا");
            }
        }
    }
}
