using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace tattoo_parlor
{
    public partial class main_form : Form
    {
        public main_form()
        {
            InitializeComponent();
            if (!auth.StatusUserAdmin)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Delet_Edit form = new Delet_Edit();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reg form = new reg();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add form = new add();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            view form = new view();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            auth.StatusUserAdmin = false;
            auth form = new auth();
            this.Visible = false;
            form.Show();
        }
    }
}
