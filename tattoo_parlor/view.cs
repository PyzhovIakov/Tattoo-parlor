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
    public partial class view : Form
    {
        public view()
        {
            InitializeComponent();
        }

        private async void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) { await ViewClientDB(); }
            if (tabControl1.SelectedIndex == 1) { await ViewWorkDB(); }
            if (tabControl1.SelectedIndex == 2) { await ViewMasterDB(); }
        }

        private async Task ViewClientDB()
        {
            try
            {
                dataGridView1.Rows.Clear();
                DataTable clients = new DataTable();
                await Task.Run(() =>
                {
                    clients = Requests.SELECTClientDB();
                });
                for (int i = 0; i < clients.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = clients.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = clients.Rows[i][2].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = clients.Rows[i][3].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = clients.Rows[i][4].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = clients.Rows[i][5].ToString();
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }
        private async Task ViewWorkDB()
        {
            try
            {
                dataGridView2.Rows.Clear();
                DataTable work = new DataTable();
                DataTable clients = new DataTable();
                DataTable masters = new DataTable();
                await Task.Run(() =>
                {
                    work = Requests.SELECTWorkDB();
                    masters = Requests.SELECTMasterDB();
                    clients = Requests.SELECTClientDB();
                });
                for (int i = 0; i < work.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    string client="";
                    for (int j = 0; j < clients.Rows.Count; j++)
                    {
                        if(Convert.ToInt32(work.Rows[i][1])==Convert.ToInt32(clients.Rows[j][0]))
                        {
                            client = clients.Rows[j][1].ToString()+" "+clients.Rows[j][2].ToString()+" "+clients.Rows[j][3].ToString();
                            break;
                        }
                    }
                    string master = "";
                    for (int j = 0; j < masters.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(work.Rows[i][2]) == Convert.ToInt32(masters.Rows[j][0]))
                        {
                            master = masters.Rows[j][1].ToString() + " " + masters.Rows[j][2].ToString() + " " + masters.Rows[j][3].ToString();
                            break;
                        }
                    }
                    dataGridView2.Rows[i].Cells[0].Value = client;
                    dataGridView2.Rows[i].Cells[1].Value = master;
                    dataGridView2.Rows[i].Cells[2].Value = work.Rows[i][3].ToString();
                    dataGridView2.Rows[i].Cells[3].Value = work.Rows[i][4].ToString();
                    dataGridView2.Rows[i].Cells[4].Value = work.Rows[i][5].ToString();

                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }
        private async Task ViewMasterDB()
        {
            try
            {
                dataGridView3.Rows.Clear();
                DataTable masters = new DataTable();
                await Task.Run(() =>
                {
                    masters = Requests.SELECTMasterDB();
                });
                for (int i = 0; i < masters.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = masters.Rows[i][1].ToString();
                    dataGridView3.Rows[i].Cells[1].Value = masters.Rows[i][2].ToString();
                    dataGridView3.Rows[i].Cells[2].Value = masters.Rows[i][3].ToString();
                    dataGridView3.Rows[i].Cells[3].Value = masters.Rows[i][4].ToString();
                    dataGridView3.Rows[i].Cells[4].Value = masters.Rows[i][5].ToString();
                    dataGridView3.Rows[i].Cells[5].Value = masters.Rows[i][6].ToString();
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private async void view_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            await ViewClientDB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }

        private void view_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
       
    }
}
