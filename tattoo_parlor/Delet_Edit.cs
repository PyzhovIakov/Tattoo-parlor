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
    public partial class Delet_Edit : Form
    {
        public Delet_Edit()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
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

        private async void Delet_Edit_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            await ViewClientDB();
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
                    string client = "";
                    for (int j = 0; j < clients.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(work.Rows[i][1]) == Convert.ToInt32(clients.Rows[j][0]))
                        {
                            client = clients.Rows[j][1].ToString() + " " + clients.Rows[j][2].ToString() + " " + clients.Rows[j][3].ToString();
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
        private async Task View_clients_masters_DB()
        {
            try
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                DataTable clients = new DataTable();
                DataTable masters = new DataTable();

                await Task.Run(() =>
                {
                    masters = Requests.SELECTMasterDB();
                    clients = Requests.SELECTClientDB();
                });
                for (int j = 0; j < clients.Rows.Count; j++)
                {
                    comboBox2.Items.Add(clients.Rows[j][1].ToString() + " " + clients.Rows[j][2].ToString() + " " + clients.Rows[j][3].ToString());
                }
                for (int j = 0; j < masters.Rows.Count; j++)
                {
                    comboBox3.Items.Add(masters.Rows[j][1].ToString() + " " + masters.Rows[j][2].ToString() + " " + masters.Rows[j][3].ToString());
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private async void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            if (tabControl1.SelectedIndex == 0) { await ViewClientDB(); }
            if (tabControl1.SelectedIndex == 1) { await ViewWorkDB(); await View_clients_masters_DB(); }
            if (tabControl1.SelectedIndex == 2) { await ViewMasterDB(); }
        }
        public int id1 = -1;
        public int id2 = -1;
        public int id3 = -1;
        private async void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (id1 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                bool Flag = false;
                Flag = Requests.DELETEClientDB(cF, cN, cP, cT, cA);
                if (Flag)
                {
                    MessageBox.Show("Успешное Удаление");
                    await ViewClientDB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        public string cF = "";
        public string cN = "";
        public string cP = "";
        public string cT = "";
        public string cA = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id1 = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[id1].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[id1].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[id1].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[id1].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[id1].Cells[4].Value.ToString();
            cF = dataGridView1.Rows[id1].Cells[0].Value.ToString();
            cN = dataGridView1.Rows[id1].Cells[1].Value.ToString();
            cP = dataGridView1.Rows[id1].Cells[2].Value.ToString();
            cT = dataGridView1.Rows[id1].Cells[3].Value.ToString();
            cA = dataGridView1.Rows[id1].Cells[4].Value.ToString();
        }

        public string wc = "";
        public string wm = "";
        public DateTime wd=DateTime.Now;
        public string wt = "";
        public string ws = "";

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id2 = dataGridView2.CurrentRow.Index;
            wc = dataGridView2.Rows[id2].Cells[0].Value.ToString();
            wm = dataGridView2.Rows[id2].Cells[1].Value.ToString();
            wd = Convert.ToDateTime(dataGridView2.Rows[id2].Cells[2].Value);
            wt = dataGridView2.Rows[id2].Cells[3].Value.ToString();
            ws = dataGridView2.Rows[id2].Cells[4].Value.ToString();
            comboBox2.Text = dataGridView2.Rows[id2].Cells[0].Value.ToString();
            comboBox3.Text = dataGridView2.Rows[id2].Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView2.Rows[id2].Cells[2].Value);
            comboBox4.Text = dataGridView2.Rows[id2].Cells[3].Value.ToString();
            textBox11.Text = dataGridView2.Rows[id2].Cells[4].Value.ToString();
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (id2 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                bool Flag = false;
                Flag = Requests.DELETEWorkDB(wc, wm, wt, ws);
                if (Flag)
                {
                    MessageBox.Show("Успешное Удаление");
                    await ViewWorkDB(); 
                    await View_clients_masters_DB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                textBox11.Text = "";
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (id3 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                bool Flag = false;
                Flag = Requests.DELETEMasterDB(mF, mN, mP, mS, mD, mT);
                if (Flag)
                {
                    MessageBox.Show("Успешное Удаление");
                    await ViewMasterDB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                textBox10.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox6.Text = "";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        public string mF = "";
        public string mN = "";
        public string mP = "";
        public string mT = "";
        public string mS = "";
        public string mD = "";

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id3 = dataGridView3.CurrentRow.Index;
            mF = dataGridView3.Rows[id3].Cells[0].Value.ToString();
            mN = dataGridView3.Rows[id3].Cells[1].Value.ToString();
            mP = dataGridView3.Rows[id3].Cells[2].Value.ToString();
            mS = dataGridView3.Rows[id3].Cells[3].Value.ToString();
            mD = dataGridView3.Rows[id3].Cells[4].Value.ToString();
            mT = dataGridView3.Rows[id3].Cells[5].Value.ToString();
            textBox10.Text = dataGridView3.Rows[id3].Cells[0].Value.ToString();
            textBox9.Text = dataGridView3.Rows[id3].Cells[1].Value.ToString();
            textBox8.Text = dataGridView3.Rows[id3].Cells[2].Value.ToString();
            textBox7.Text = dataGridView3.Rows[id3].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView3.Rows[id3].Cells[4].Value.ToString();
            textBox6.Text = dataGridView3.Rows[id3].Cells[5].Value.ToString();
            
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (id1 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                if (textBox1.Text == "" || textBox1.Text == " " || textBox2.Text == "" || textBox2.Text == " " || textBox3.Text == " " || textBox3.Text == "" || textBox4.Text == " " || textBox4.Text == "" || textBox5.Text == " " || textBox5.Text == "")
                {
                    MessageBox.Show("Поля редактирования клиента не могут быть путыми"); return;
                }
                bool Flag = false;
                string F = textBox1.Text;
                string N = textBox2.Text;
                string P = textBox3.Text;
                string T = textBox4.Text;
                string A = textBox5.Text;
                Flag = Requests.UPDATEClientDB(cF, cN, cP, cT, cA,F,N,P,T,A);
                if (Flag)
                {
                    MessageBox.Show("Успешное Обновление");
                    await ViewClientDB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox10.Text == "" || textBox10.Text == " " || textBox9.Text == "" || textBox9.Text == " " || textBox8.Text == " " || textBox8.Text == "" || textBox7.Text == " " || textBox7.Text == "" || textBox6.Text == " " || textBox6.Text == "" || comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Поля редактирования мастера не могут быть путыми"); return;
                }
                if (id3 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                bool Flag = false;
                string F = textBox10.Text;
                string N = textBox9.Text;
                string P = textBox8.Text;
                string S = textBox7.Text;
                string T = textBox6.Text;
                string D = comboBox1.Text;
                Flag = Requests.UPDATEMasterDB(mF, mN, mP, mS, mD, mT, F, N, P, S, D, T);
                if (Flag)
                {
                    MessageBox.Show("Успешное Обновление");
                    await ViewMasterDB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                textBox10.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox6.Text = "";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (id2 < 0) { MessageBox.Show("Строка не выбрана"); return; }
                if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show("Выходной день"); return;
                }
                if (textBox11.Text == "" || textBox11.Text == " " || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
                {
                    MessageBox.Show("Поля редактирования работы не могут быть путыми"); return;
                }
                bool Flag = false;
                string C = comboBox2.Text;
                string M = comboBox3.Text;
                DateTime D = dateTimePicker1.Value;
                string T = comboBox4.Text;
                string S = textBox11.Text;
                Flag = Requests.UPDATEWorkDB(wc, wm, Convert.ToDateTime(wd).ToString("dd/MM/yyyy"), wt, ws, C, M, D.ToString("dd/MM/yyyy"), T, S);
                if (Flag)
                {
                    MessageBox.Show("Успешное Обновление");
                    await ViewWorkDB();
                    await View_clients_masters_DB();
                }
                else
                {
                    MessageBox.Show("Ошибка БД");
                }
                textBox11.Text = "";
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void Delet_Edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



    }
}
