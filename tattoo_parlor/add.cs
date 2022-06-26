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
    public partial class add : Form
    {
        public add()
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox1.Text == " " || textBox2.Text == "" || textBox2.Text == " " || textBox3.Text == " " || textBox3.Text == "" || textBox4.Text == " " || textBox4.Text == "" || textBox5.Text == " " || textBox5.Text == "")
                {
                    MessageBox.Show("Поля добавления клиента не могут быть путыми"); return;
                }
                string F = textBox1.Text;
                string N = textBox2.Text;
                string P = textBox3.Text;
                string T = textBox4.Text;
                string A = textBox5.Text;
                bool FlagINSERT = false;
                FlagINSERT = Requests.INSERTClientDB(F, N, P, T, A);
                if (FlagINSERT)
                {
                    MessageBox.Show("Успешное добавление");
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private async void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1) { await View_clients_masters_DB(); }
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Выходной день"); return;
            }
            if (textBox11.Text == "" || textBox11.Text == " " || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Поля добавления клиента не могут быть путыми"); return;
            }
            string C = comboBox2.Text;
            string M = comboBox3.Text;
            DateTime D = dateTimePicker1.Value;
            string T = comboBox4.Text;
            string S = textBox11.Text; ;
            bool FlagINSERT = false;
            FlagINSERT = Requests.INSERTWorkDB(C, M, D.ToString("dd/MM/yyyy"), T, S);
            if (FlagINSERT)
            {
                MessageBox.Show("Успешное добавление");
                textBox11.Text = "";
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Дата уже занята или проблемы с БД");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox10.Text == "" || textBox10.Text == " " || textBox9.Text == "" || textBox9.Text == " " || textBox8.Text == " " || textBox8.Text == "" || textBox7.Text == " " || textBox7.Text == "" || textBox6.Text == " " || textBox6.Text == "" || comboBox1.SelectedIndex==-1)
                {
                    MessageBox.Show("Поля добавления мастера не могут быть путыми"); return;
                }
                string F = textBox10.Text;
                string N = textBox9.Text;
                string P = textBox8.Text;
                string T = textBox7.Text;
                string S = textBox6.Text;
                string D = comboBox1.Text;
                bool FlagINSERT = false;
                FlagINSERT = Requests.INSERTMasterDB(F, N, P, T, S, D);
                if (FlagINSERT)
                {
                    MessageBox.Show("Успешное добавление");
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

       


    }
}
