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
    public partial class Form_theory : Form
    {
        public Form_theory()
        {
            InitializeComponent();
            //AddOwnedForm(back);
        }
        string path = Application.StartupPath.ToString();
        //frm_titul back = new frm_titul();
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            
            string text = treeView1.SelectedNode.Text;
            int sq = treeView1.SelectedNode.Index;
            webBrowser1.Visible = true;
            //webBrowser1.ScrollBarsEnabled = false;
            if (sq >= 0)
            {
                
            }
            if (text == "Лінійна інтерполяція")/*(text == "Лінійна інтерполяція")*/
            {

                webBrowser1.Navigate(path + "/sites/site1.htm");
            }
            if (text == "Квадратична інтерполяція")
            {

                webBrowser1.Navigate(path + "/sites/site2.htm"); 
            }
            if (text == "Многочлен Лагранжа")/*(text == "Лінійна інтерполяція")*/
            {

                webBrowser1.Navigate(path + "/sites/site3.htm"); //"C:\Users\poenk\Desktop\lg_reg\logr\logr\bin\Debug\sites\site1.htm"
            }
            if (text == "Многочлен Ньютона")/*(text == "Лінійна інтерполяція")*/
            {

                webBrowser1.Navigate(path + "/sites/site4.htm"); //"C:\Users\poenk\Desktop\lg_reg\logr\logr\bin\Debug\sites\site1.htm"
            }
            if (text == "Лінійна апроксимація")/*(text == "Лінійна інтерполяція")*/
            {

                webBrowser1.Navigate(path + "/sites/site5.htm"); //"C:\Users\poenk\Desktop\lg_reg\logr\logr\bin\Debug\sites\site1.htm"
            }
            //label1.Text = t.ToString();
            if (text == "Квадратична апроксимація")/*(text == "Лінійна інтерполяція")*/
            {

                webBrowser1.Navigate(path + "/sites/site6.htm"); //"C:\Users\poenk\Desktop\lg_reg\logr\logr\bin\Debug\sites\site1.htm"
            }
            if(text == "Поняття інтерполяції")
            {
                webBrowser1.Navigate(path + "/sites/inter.htm");
            }
            if(text == "Поняття апроксимації")
            {
                webBrowser1.Navigate(path + "/sites/aprok.htm");
            }
            if(text == "Криві Безьє")
            {
                webBrowser1.Navigate(path + "/sites/bezier.htm");
            }
        }

        private void Form_theory_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(path + "/sites/inter.htm");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
           
        }
    }
}
    
