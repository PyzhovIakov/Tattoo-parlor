using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace tattoo_parlor
{
    public partial class auth : Form
    {
        public static DataTable dt;
        public static bool StatusUserAdmin = false;

        public auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "") { MessageBox.Show("Логин не введен"); return; }
                if (textBox2.Text == "") { MessageBox.Show("Пароль не введен"); return; }
                if (dt.Rows.Count <= 0) { MessageBox.Show("Ошибка БД"); return; }
                if (comboBox1.SelectedIndex == -1)
                {
                    string password = textBox2.Text;
                    string login = textBox1.Text;
                    bool loginFlag = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][2].ToString() == login)
                        {
                            loginFlag = false;
                            if (dt.Rows[i][3].ToString() == password)
                            {
                                if (dt.Rows[i][4].ToString() == "user") { StatusUserAdmin = false; } else { StatusUserAdmin = true; }  
                                main_form form = new main_form();
                                this.Visible = false;
                                form.ShowDialog();
                            }
                            else { MessageBox.Show("Пароль введен неверно"); return; }
                        }
                    }
                    if (loginFlag) { MessageBox.Show("Пользователя с таким логином нет"); loginFlag = false; return; }
                }
                else
                {
                    int indexCombobox = comboBox1.SelectedIndex;
                    string password = textBox2.Text;
                    string login = textBox1.Text;
                    if (dt.Rows[indexCombobox][2].ToString() == login)
                    {
                        if (dt.Rows[indexCombobox][3].ToString() == password)
                        {
                            if (dt.Rows[indexCombobox][4].ToString() == "user") { StatusUserAdmin = false; } else { StatusUserAdmin = true; }
                            main_form form = new main_form();
                            this.Visible = false;
                            form.ShowDialog();
                        }
                        else { MessageBox.Show("Пароль введен неверно"); return; }
                    }
                    else { MessageBox.Show("Логин введен неверно"); return; }
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
            if (textBox2.UseSystemPasswordChar)
            {   
                button2.BackgroundImage = Properties.Resources.hidden as Bitmap;
            }
            else
            {
                button2.BackgroundImage = Properties.Resources.view as Bitmap;
            }
        }

        private void auth_Load(object sender, EventArgs e)
        {
            try 
            { 
                dt = Requests.UsersDB();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comboBox1.Items.Add(dt.Rows[i][1].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка БД"); 
                    return;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); return; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexCombobox = comboBox1.SelectedIndex;
            textBox1.Text = dt.Rows[indexCombobox][2].ToString();
        }
    }
}
