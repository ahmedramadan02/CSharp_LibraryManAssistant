using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

namespace LibraryAssistant
{
    public partial class Form1 : Form
    {
        DataTable tb;
        BReport BReport;
        JReport JReport; 

        public Form1(DataTable _table, string rptType)
        {
            InitializeComponent();
            //Get data
            //tb = new DataTable();
            tb = _table;
            //tb.WriteXmlSchema("JSample.Xml");

            // Initial reporting
            InitialReporting(rptType);
        }

        // Initial reporting
        private void InitialReporting(string type)
        {
            // Note that the schema of the report MUST be the same as passed table
            switch (type)
            {
                case "Book_Report":
                    BReport = new BReport();
                    BReport.Database.Tables[0].SetDataSource(tb);
                    crystalReportViewer1.ReportSource = BReport;
                    crystalReportViewer1.Refresh();
                    this.Show();
                    break;

                case "Journal_Report":
                    JReport = new JReport();
                    JReport.Database.Tables[0].SetDataSource(tb);
                    crystalReportViewer1.ReportSource = JReport;
                    crystalReportViewer1.Refresh();
                    this.Show();
                    break;

                default:
                    break;
            }
            
        }
    }
}
