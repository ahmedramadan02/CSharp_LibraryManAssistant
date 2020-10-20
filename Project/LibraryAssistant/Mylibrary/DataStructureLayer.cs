using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Mylibrary
{

    //DataStructureLayer is a class for library dataStructure
    public class DataStructureLayer : DataAccessLayer 
    {
        //Constructors
        //1. Defualt Contructor 
        /*
        public DataStructureLayer()
        {

        }
        
        public DataStructureLayer(string connectionString)
        {
            
        }
        */
        
        //2. Second Constructors
        public DataStructureLayer(string connectionString) : base(connectionString) 
        {
            // NOP
        }

        public void Ahmed() { }
        
        //Structures
        //---------------------------------- 1. Books
        public class Books : DataStructureLayer
        {        
            //default constrctor
            public Books(string connectionString) : base(connectionString) { // NOP 
            }

            //Variables
           public string bTitle;
           public string bAuthor;
           public string bSubject;
           public string bISBN;
           public string bAccNo;
           public string bClassNo;
           public string bPublisher;
           public string bPlaceofPub;
            
            //Properties
           private string bDateOfPub;
           public string BDateOfPub
           {
               get { return bDateOfPub; }
               set
               {
                   value = value.Trim();
                   if(value.Length!=4) bDateOfPub = "N/A";
                   else bDateOfPub = value; 
               }
           }

            // enumeration
           public enum bSearch { 
           bTilte,
           bAuthor,
           bSubject,
           bISBN,
           bAccNo,
           bClassNo,
           bPublisher,
           bPlaceofPub,
           bDateOfPub,
           }
           
            //Functions
            //------------------------------ 1. AddNewBook
           public int AddNewBook() {
               string Cmd;
               
               try
               {
                   //Check of primary keys repeatation
                   Cmd = "SELECT * FROM Books Where BISBN = @bisbn";
                   //Big error ==> why?Erro An object reference is required for the non-static field, method, or property 
                   //'Mylibrary.DataAccessLayer.RunDML(string, System.Data.OleDb.OleDbParameter[])
                   //Summary ==> You can't access DataAccessLayer class (non-static fucntions) from nested member strurture book 
                   // Example :: DataSet Set = DataAccessLayer.GetDataSet(Cmd);
                   // Use :: Scope ==> this.MemberName
                   //static nested class is not a part of an instance of the enclosing class.
                   OleDbParameter[] Para = new OleDbParameter[1];
                   Para[0] = new OleDbParameter("@bisbn", bISBN);

                   DataTable MyTable =  GetDataSet(Cmd,Para).Tables[0];

                   // if the book exists ==> ERROR
                   if (MyTable.Rows.Count > 0)
                       throw new Exception();

                   //Validiation
                   if (bDateOfPub == "N/A") 
                       throw new Exception();

                   // Construct the query
                   Cmd = "INSERT INTO Books (BTitle,BAuthor,BSubject,BISBN,BAccNo,BClassNo,BPublisher,BPlaceOfPub,BDateOfPub)"
                   + " VALUES (@title,@author,@subject,@isbn,@accno,@classno,@publisher,@placeofpub,@dateofpub)";


                   //Parameters
                   OleDbParameter[] CmdParameters = new OleDbParameter[9];
                   CmdParameters[0] = new OleDbParameter("@title", bTitle);
                   CmdParameters[1] = new OleDbParameter("@author", bAuthor);
                   CmdParameters[2] = new OleDbParameter("@subject", bSubject);
                   CmdParameters[3] = new OleDbParameter("@isbn", bISBN);
                   CmdParameters[4] = new OleDbParameter("@accno", bAccNo);
                   CmdParameters[5] = new OleDbParameter("@classno", bClassNo);
                   CmdParameters[6] = new OleDbParameter("@publisher", bPublisher);
                   CmdParameters[7] = new OleDbParameter("@placeofpub", bPlaceofPub);
                   CmdParameters[8] = new OleDbParameter("@dateofpub", bDateOfPub);
                   
                   //Results
                   int aff = this.RunDML(Cmd, CmdParameters);
                   return aff;
               }
               catch (Exception ex)
               {
                   ErrorLog(ex.Message, "Books class", "AddNewBook" , DateTime.Now);
                   return -1;
               } 
           }

            //------------------------------ 2. ModifyBook
           public int ModifyBook()
           {
               string Cmd;
               try
               {
                   //Validiation
                   if (bDateOfPub == "N/A")
                       throw new Exception();

                   // Construct the query
                   Cmd = "UPDATE Books SET BTitle=@btitle , BAuthor=@bauthor , BSubject=@bsubject"
                   + " , BAccNo=@baccno , BClassNo=@bclassno , BPublisher=@bpublisher , BPlaceOfPub=@bplaceofpub"
                   + " , BDateOfPub=@bdateofpub WHERE BISBN=@bisbn";

                   //Parameters
                   OleDbParameter[] CmdParameters = new OleDbParameter[9];
                   CmdParameters[0] = new OleDbParameter("@btitle", bTitle);
                   CmdParameters[1] = new OleDbParameter("@bauthor", bAuthor);
                   CmdParameters[2] = new OleDbParameter("@bsubject", bSubject);
                   CmdParameters[3] = new OleDbParameter("@baccno", bAccNo);
                   CmdParameters[4] = new OleDbParameter("@bclassno", bClassNo);
                   CmdParameters[5] = new OleDbParameter("@bpublisher", bPublisher);
                   CmdParameters[6] = new OleDbParameter("@bplaceofpub", bPlaceofPub);
                   CmdParameters[7] = new OleDbParameter("@bdateofpub", bDateOfPub);
                   CmdParameters[8] = new OleDbParameter("@bisbn", bISBN);

                   //Results
                   int aff = this.RunDML(Cmd, CmdParameters);
                   return aff;
               }
               catch (Exception ex)
               {
                   ErrorLog(ex.Message, "Books Class", "ModifyBook function", DateTime.Now);
                   return -1;
               }
           }

            //------------------------------ 3. DeleteBook
            public int DeleteBook()
           {
               string Cmd;
               try {
                   // Construct the query
                   Cmd = "DELETE * FROM Books WHERE Books.BISBN = @isbn";

                   //Parameters
                   OleDbParameter[] CmdParameters = new OleDbParameter[1];
                   CmdParameters[0] = new OleDbParameter("@isbn", bISBN); 
                  
                   //Results
                   int aff = this.RunDML(Cmd, CmdParameters);
                   return aff;
               }
               catch (Exception ex)
               {
                   ErrorLog(ex.Message, "Books Class", "DeleteBook function", DateTime.Now);
                   return -1;
               }
           }


            //------------------------------ 4. Search4Book
           public DataTable BSearch(string txtbFind,bSearch findBy) {
               string cmd;
               cmd = "Select * FROM Books WHERE" + findBy.ToString() + " = " + txtbFind;

               DataSet Set = this.GetDataSet(cmd);
               return Set.Tables[0];
           }
           
        }

        //---------------------------------- 1. Journals
        public class Journals : DataStructureLayer
        {
            //default constructor 
            public Journals(string connectionString) : base(connectionString)
            {
                // NOP
            }

            //Variables
            public string jTitle;
            public string jSubject;
            public string jISSN;
            public string jPublisher;
            public string jVolume;
            public string JIssue;

            //Properties
            private string jYear;
            public string JYear
            {
                get { return jYear; }
                set
                {
                    value = value.Trim();
                    if (value.Length != 4) jYear = "N/A";
                    else jYear = value;
                }
            }

            // enumeration
            public enum jSearch
            {
            jTilte,
            jSubject,
            jISSN,
            jPublisher,
            jYear,
            jVolume,
            jIssue,
            }

            //Functions
            //------------------------------ 1. AddNewJournal
            public int AddNewJournal()
            {
                string Cmd;

                try
                {
                    //Check of primary keys repeatation
                    Cmd = "SELECT * FROM Journals Where JISSN = @jissn";
                    OleDbParameter[] Para = new OleDbParameter[1];
                    Para[0] = new OleDbParameter("@jissn", jISSN);

                    DataTable MyTable = GetDataSet(Cmd, Para).Tables[0];

                    // if the book exists ==> ERROR
                    if (MyTable.Rows.Count > 0)
                        throw new Exception();

                    //Validiation
                    if (jYear == "N/A") throw new Exception();

                    // Construct the query
                    Cmd = "INSERT INTO Journals(JTitle, JSubject, JISSN, JPublisher, JYear, JVolume, JIssue)"
                    + "VALUES (@title, @subject, @issn, @publisher, @year, @volume, @issue)";


                    //Parameters
                    OleDbParameter[] CmdParameters = new OleDbParameter[7];
                    CmdParameters[0] = new OleDbParameter("@title", jTitle);
                    CmdParameters[1] = new OleDbParameter("@subject", jSubject);
                    CmdParameters[2] = new OleDbParameter("@issn", jISSN);
                    CmdParameters[3] = new OleDbParameter("@publisher", jPublisher);
                    CmdParameters[4] = new OleDbParameter("@year", jYear);
                    CmdParameters[5] = new OleDbParameter("@volume", jVolume);
                    CmdParameters[6] = new OleDbParameter("@issue", JIssue);
                    

                    //Results
                    int aff = this.RunDML(Cmd, CmdParameters);
                    return aff;
                }
                catch
                {
                    return -1;
                }
            }

            //------------------------------ 2. ModifyJournal
            public int ModifyJournal()
            {
                string Cmd;
                try
                {
                    //Validiation
                    if (jYear == "N/A") throw new Exception();

                    // Construct the query
                    Cmd = "UPDATE Journals SET JTitle=@title,JSubject=@subject,JPublisher=@publisher"
                    + ",JYear=@year,JVolume=@volume,JIssue=@issue"
                    + " WHERE JISSN=@issn";

                    //Parameters
                    OleDbParameter[] CmdParameters = new OleDbParameter[7];
                    CmdParameters[0] = new OleDbParameter("@title", jTitle);
                    CmdParameters[1] = new OleDbParameter("@subject", jSubject);
                    CmdParameters[2] = new OleDbParameter("@publisher", jPublisher);
                    CmdParameters[3] = new OleDbParameter("@year", jYear);
                    CmdParameters[4] = new OleDbParameter("@volume", jVolume);
                    CmdParameters[5] = new OleDbParameter("@issue", JIssue);
                    CmdParameters[6] = new OleDbParameter("@issn", jISSN);
                    
                    //Results
                    int aff = this.RunDML(Cmd, CmdParameters);
                    return aff;
                }
                catch (Exception ex)
                {
                    ErrorLog(ex.Message, "Journals Class", " ModifyJournal function", DateTime.Now);
                    return -1;
                }
            }

            //------------------------------ 3. DeleteJournal
            public int DeleteJournal()
            {
                string Cmd;
                try
                {
                    // Construct the query
                    Cmd = "DELETE * FROM Journals WHERE Journals.JISSN = @issn";

                    //Parameters
                    OleDbParameter[] CmdParameters = new OleDbParameter[1];
                    CmdParameters[0] = new OleDbParameter("@issn", jISSN);

                    //Results
                    int aff = this.RunDML(Cmd, CmdParameters);
                    return aff;
                }
                catch (Exception ex)
                {
                    ErrorLog(ex.Message, "Journals Class", "DeleteJournal function", DateTime.Now);
                    return -1;
                }
            }

            //------------------------------ 4. Search4Journal
            public DataTable JSearch(string txtjFind, jSearch findBy)
            {
                string cmd;
                cmd = "Select * FROM Books WHERE " + findBy.ToString() + txtjFind;

                DataSet Set = this.GetDataSet(cmd);
                return Set.Tables[0];
            }
            
        }


        // Create error logs
        public void ErrorLog(string ErrMsg, string objName, string FormName, DateTime ErrTime)
        {
            //int i = FreeFile();
            string Logs;
            //StreamWriter myFile = File.CreateText("\\ErrLogs.txt");
            StreamWriter myFile = File.AppendText("D:\\ErrLogs.txt");

            //txt
            Logs = ErrTime.ToString() + " - " + objName + " - " + FormName + " - " + ErrMsg;
            
            //Write my log
            myFile.WriteLine(Logs);
            myFile.Flush();
            myFile.Close();

            //myFile.
        }
    }

    public static class Addons {

        public static bool IsFileExist(string path) {
            return File.Exists(path);
        }

        private static Collection<Version> InstalledDotNetVersions()
        {
            Collection<Version> versions = new Collection<Version>();
            RegistryKey NDPKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            if (NDPKey != null)
            {
                string[] subkeys = NDPKey.GetSubKeyNames();
                foreach (string subkey in subkeys)
                {
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey), subkey, versions);
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey).OpenSubKey("Client"), subkey, versions);
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey).OpenSubKey("Full"), subkey, versions);
                }
            }
            return versions;
        }

        private static void GetDotNetVersion(RegistryKey parentKey, string subVersionName, Collection<Version> versions)
        {
            if (parentKey != null)
            {
                string installed = Convert.ToString(parentKey.GetValue("Install"));
                if (installed == "1")
                {
                    string version = Convert.ToString(parentKey.GetValue("Version"));
                    if (string.IsNullOrEmpty(version))
                    {
                        if (subVersionName.StartsWith("v"))
                            version = subVersionName.Substring(1);
                        else
                            version = subVersionName;
                    }

                    Version ver = new Version(version);

                    if (!versions.Contains(ver))
                        versions.Add(ver);
                }
            }
        }

        public static bool VersionCheck(){
            bool isCorrectVersion = false;
            foreach(Version ver in InstalledDotNetVersions())
                if (Environment.Version.Major == ver.Major) 
                    isCorrectVersion = true;

            return isCorrectVersion;
        }
    }
}

