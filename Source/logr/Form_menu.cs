using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logr
{
    public partial class Form_menu : Form
    {
        public Form_menu()
        {
            InitializeComponent();
        }
        Form1 frm_nx = new Form1();
        Form_theory frm_t = new Form_theory();
       

        private void button1_Click(object sender, EventArgs e)
        {
            //Hide();
            frm_nx.ShowDialog();
        }

        
        

        private void button2_Click(object sender, EventArgs e)
        {
            //Hide();
            frm_t.ShowDialog();
        }

        private void Form_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            
            
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }
    }
}
