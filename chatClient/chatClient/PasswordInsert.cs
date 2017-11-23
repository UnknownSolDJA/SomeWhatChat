using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatClient
{
    public partial class PasswordInsert : Form
    {
        public bool fullFill = false;
        public PasswordInsert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null)
            {    
                if(textBox1.Text == "123qaz")
                { 
                    fullFill = true;
                    this.Hide();
                }
                else
                { 
                    MessageBox.Show("Password is incorrect, try again", "Insert error"); 
                }     
            }
        }
    }
}
