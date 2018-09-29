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
    public partial class add : Form
    {

        public kursach.admin admin;
        public add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label> { numbers, quanity, year, rating, price, };
            labels.ForEach(i => i.ForeColor = System.Drawing.Color.Red);
            labels.ForEach(i => i.Visible = false);
            bool error = false;
            var paras = new List<string>();

            paras.Add(textBox1.Text);
            paras.Add(textBox2.Text);


            if (!textBox3.Text.All(char.IsDigit))
            {
                error = true;
                price.Visible = true;
            }
            paras.Add(textBox3.Text);

            if (!textBox4.Text.All(char.IsDigit))
            {
                error = true;
                rating.Visible = true;
            }
            else
            {
                if(!(Convert.ToDouble(textBox4.Text) <= 5 && Convert.ToDouble(textBox4.Text) >= 1))
                {
                    error = true;
                    rating.Visible = true;
                }
            }

            paras.Add(textBox4.Text);

            paras.Add(textBox5.Text);

            if (!textBox6.Text.All(char.IsDigit))
            {
                error = true;
                year.Visible = true;
            }
            paras.Add(textBox6.Text);

            if (!textBox7.Text.All(char.IsDigit))
            {
                error = true;
                quanity.Visible = true;
            }
            paras.Add(textBox7.Text);

            if (textBox8.Text.All(char.IsDigit))
            {
                paras.Add(textBox8.Text);
            }

            if (!(textBox8.Text.All(char.IsDigit)) && !(textBox8.Text == string.Empty))
            {
                error = true;
                numbers.Visible = true;

            }
            if (error)
            {
                MessageBox.Show("Ошибка!", "Внимание!");
                return;
            }

            admin.bookBindingSource.Add(new book(paras));


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
