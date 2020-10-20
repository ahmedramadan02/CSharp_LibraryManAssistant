using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibraryAssistant
{
    public partial class ToAdmin : Form
    {
        public ToAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet Set = new DataSet();
            Set.ReadXml("adminPasswords.XML");

            //Decrypt
            Byte[] Decrypted = Convert.FromBase64String(Set.Tables[0].Rows[0][0].ToString());
            string decryptedString = UTF8Encoding.UTF8.GetString(Decrypted);

            if (Set.Tables[0].Rows[0][0].ToString().Trim() != "" &&
                decryptedString == textBox1.Text)
            {
                AdminBoard adminBoard = new AdminBoard();
                adminBoard.ShowDialog();
            }
            else {
                MessageBox.Show("عفوا .. الرقم السري غير صحيح");
            }
        }
            
    }
}

