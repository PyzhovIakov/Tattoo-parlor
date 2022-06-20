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
    public partial class reg : Form
    {
        public reg()
        {
            InitializeComponent();
        }

        private async void reg_Load(object sender, EventArgs e)
        {
            await ViewUsersDB();
        }

        private async Task ViewUsersDB()
        {
            DataTable users = new DataTable();
            await Task.Run(() =>
            {
                users = Requests.UsersDB();
            });
                for (int i = 0; i < users.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = users.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = users.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = users.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = users.Rows[i][4].ToString();
                }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }
    }
}
