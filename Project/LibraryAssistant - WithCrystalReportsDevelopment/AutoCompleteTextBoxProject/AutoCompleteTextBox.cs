using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace AutoCompleteTextBox_Project
{
    public partial class AutoCompleteTextBox : TextBox
    {
        public AutoCompleteTextBox()
        {
            InitializeComponent();

            //Default values
            this.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        public void UpdateMe(DataTable _table)
        {
            Thread.Sleep(125);
            this.AutoCompleteCustomSource.AddRange(FromDataTableToArry(_table));
        }

        public void UpdateMe(string[] autoCompleteArr)
        {
            Thread.Sleep(125);
            this.AutoCompleteCustomSource.AddRange(autoCompleteArr);
        }

        private string[] FromDataTableToArry(DataTable table)
        {
            string[] result = table.AsEnumerable().Select(r => r.Field<string>(0)).ToArray();
            return result;
        }
    }
}
