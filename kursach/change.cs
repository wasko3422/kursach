using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public partial class change : Form
    {
        public kursach.admin admin;
        public change()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin.dataGridView1.CurrentRow.Cells[0].Value = textBox1.Text;
            admin.dataGridView1.CurrentRow.Cells[1].Value = textBox2.Text;

            if (!textBox3.Text.All(char.IsDigit))
            {
                MessageBox.Show("Цена должна состоять из чисел", "Ошибка");
                return;
            }
            admin.dataGridView1.CurrentRow.Cells[2].Value = textBox3.Text;
            if (!textBox4.Text.All(char.IsDigit))
            {
                MessageBox.Show("Рейтинг должен состоять из чисел", "Ошибка");
                return;
            }
            else
            {
                if (!(Convert.ToDouble(textBox4.Text) <= 5 && Convert.ToDouble(textBox4.Text) >= 1))
                {
                    MessageBox.Show("Рейтинг должен быть в интервале от 1 до 5", "Ошибка");
                    return;
                }
            }
            admin.dataGridView1.CurrentRow.Cells[3].Value = textBox4.Text;
            admin.dataGridView1.CurrentRow.Cells[4].Value = textBox5.Text;

            if (!textBox6.Text.All(char.IsDigit))
            {
                MessageBox.Show("Год должен состоять из чисел", "Ошибка");
                return;
            }
            admin.dataGridView1.CurrentRow.Cells[5].Value = textBox6.Text;

            if (!textBox7.Text.All(char.IsDigit))
            {
                MessageBox.Show("Количество должно состоять из чисел", "Ошибка");
                return;
            }
            admin.dataGridView1.CurrentRow.Cells[6].Value = textBox7.Text;

            if (!(textBox8.Text.All(char.IsDigit)) && !(textBox8.Text == string.Empty))
            {
                MessageBox.Show("Часть или том должны состоять из чисел или из пустой строки, если частей или томов нету", "Ошибка");
                return;

            }
            admin.dataGridView1.CurrentRow.Cells[7].Value = textBox8.Text;
            this.Close();

        }
    }
}
