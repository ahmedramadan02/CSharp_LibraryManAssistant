using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace Mylibrary
{
    public class DataAccessLayer
    { 
        //Definations
        private OleDbConnection Cn;
        private OleDbCommand command;
        private OleDbDataAdapter Adapter;
        private OleDbDataReader Reader;
        string connString;
        

        //Constructors
        //Needs the connections string
        public DataAccessLayer(string _connectionString)
        {
            connString = _connectionString;
            InitiatizeConnetions();      
        }


        //Functions
        //--------------------------------   InitiatizeConnections
        private void InitiatizeConnetions() {
            //1. initial variables
            Cn = new OleDbConnection(connString);
            command = new OleDbCommand();
            Adapter = new OleDbDataAdapter(command);

            //2. Initial all 
            if (Cn.State == 0)
            {
                command.Connection = Cn;
                Cn.Open();
            }
        }

        //------------------------------- 1. GetDataSet
        public DataSet GetDataSet(string cmd) {
            DataSet Ds = new DataSet("Library");
            command.CommandText = cmd; 

            Adapter.Fill(Ds);
            return Ds;
        }

        public DataSet GetDataSet(string cmd,OleDbParameter[] parameters)
        {
            DataSet Ds = new DataSet("Library");
            command.CommandText = cmd;
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);

            Adapter.Fill(Ds);
            return Ds;
        }

        //------------------------------- 2. GetReader
        public OleDbDataReader GetReader(string cmd) {
            command.CommandText = cmd;

            Reader = command.ExecuteReader();
            return Reader;
        }

        public OleDbDataReader GetReader(string cmd,OleDbParameter[]parameters)
        {
            command.CommandText = cmd;
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);

            Reader = command.ExecuteReader();
            return Reader;
        }

        //------------------------------- 3. RunDML
        public int RunDML(string cmd) {
            command.CommandText = cmd;

            int aff = command.ExecuteNonQuery();
            return aff;
        }

        public int RunDML(string cmd,OleDbParameter [] parameters)
        {
            command.CommandText = cmd;
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);

            int aff = command.ExecuteNonQuery();
            return aff;
        }

        // Destructing
        //Destruct "Library" object from memory
        public void Dispose()
        {
            GC.Collect(GC.GetGeneration(this), GCCollectionMode.Forced);
            GC.Collect();
        }

        //Destructive
        ~DataAccessLayer() {
            //Cn.Close(); //error handle is not initialized - error from microsoft!!
            if (Cn != null) Cn = null;
            this.Dispose();
        }

    }
}
