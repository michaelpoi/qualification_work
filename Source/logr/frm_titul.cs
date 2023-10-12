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
    public partial class frm_titul : Form
    {
        public frm_titul()
        {
            InitializeComponent();
            AddOwnedForm(next);
        }

        private void label8_MouseHover(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Blue;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }
        Form_menu next = new Form_menu();
        private void label8_Click(object sender, EventArgs e)
        {
            Hide();
            next.ShowDialog();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
