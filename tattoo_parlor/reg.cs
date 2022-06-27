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
            try
            {
                dataGridView1.Rows.Clear();
                DataTable users = new DataTable();
                await Task.Run(() =>
                {
                    users = Requests.SELECTUsersDB();
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
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            main_form form = new main_form();
            this.Visible = false;
            form.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox1.Text == " " || textBox2.Text == "" || textBox2.Text == " " || textBox3.Text == " " || textBox3.Text == "" || comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Поля регистрации не могут быть путыми"); return;
                }
                string N = textBox1.Text;
                string L = textBox2.Text;
                string P = textBox3.Text;
                string S=comboBox1.Text;
                bool FlagINSERT = false;
                FlagINSERT = Requests.INSERTUsersDB(N, L, P, S);
                if (FlagINSERT)
                {
                    MessageBox.Show("Успешное добавление");
                    await ViewUsersDB();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] arr = new int[12];
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            textBox3.Text = Password;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = !textBox3.UseSystemPasswordChar;
            if (textBox3.UseSystemPasswordChar)
            {
                button4.BackgroundImage = Properties.Resources.hidden as Bitmap;
            }
            else
            {
                button4.BackgroundImage = Properties.Resources.view as Bitmap;
            }
        }

        private void reg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
