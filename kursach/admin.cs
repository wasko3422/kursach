using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace kursach
{
    public partial class admin : Form
    {
        bool saved = false;

        public string name = "";

        public admin()
        {
            InitializeComponent();
            this.Text = System.IO.Directory.GetCurrentDirectory();
        }
        public List<book> books = new List<book>();

        private void Form2_Load(object sender, EventArgs e)
        {
            bookBindingSource.DataSource = books;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            add dobav = new add();
            dobav.admin = this;
            dobav.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            change izmena = new change();


            izmena.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            izmena.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            izmena.textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            izmena.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            izmena.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            izmena.textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            izmena.textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            izmena.textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            izmena.admin = this;
            izmena.ShowDialog();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "ВНИМАНИЕ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            dataGridView1.CurrentCell = null;

            if (!(textBox1.Text.Length == 0))
            {
                list = help_class.Search(dataGridView1.Rows, textBox1.Text);
                help_class.getting_visible(dataGridView1, list);
            }
            else
            {
                dataGridView1.Rows.OfType<DataGridViewRow>().ToList().ForEach(row => { row.Visible = true; });
            }
        }

        private void admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = MessageBox.Show("Вы хотите сохранить изменения?", "ВНИМАНИЕ", MessageBoxButtons.YesNoCancel);



            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (!(name.Length == 0))
                {
                    help_class.save_xml(help_class.to_book(dataGridView1), name);
                    help_class.reg.create_set(name);
                    return;
                }
                else
                {
                    save save = new save();
                    save.admin = this;
                    save.ShowDialog();
                    if( name.Length == 0)
                    {
                        e.Cancel = true;
                        return;
                    }
                    help_class.save_xml(help_class.to_book(dataGridView1), name);
                }
            }
            if(result == DialogResult.Cancel || result == DialogResult.Abort)
            {
                e.Cancel = true;
            }
            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
             OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + "\\list of book";
            openFileDialog1.Filter = "Xml файлы (*.xml) | *.xml";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bookBindingSource.DataSource = help_class.open_xml(openFileDialog1.FileName);
                name = openFileDialog1.FileName.Split(Path.DirectorySeparatorChar).Last();
                name = name.Substring(0, name.Length - 4);

            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!(name.Length == 0))
            {
                help_class.save_xml(help_class.to_book(dataGridView1), name);
                MessageBox.Show("Файл успешно сохранен!");
                help_class.reg.create_set(name);
                return;
            }


            save save = new save();
            save.admin = this;
            save.ShowDialog();
            help_class.save_xml(help_class.to_book(dataGridView1), name);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("При создании нового файла изменения текущего не будут сохранены", "ВНИМАНИЕ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                name = "";
                saved = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save save = new save();
            save.admin = this;
            save.ShowDialog();
            help_class.save_xml(help_class.to_book(dataGridView1), name);
            help_class.reg.create_set(name);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовая работа была сделана Егором Пановым из группы ПИ2-1 \nМоя почта - panow.egor2015@yandex.ru");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\note.txt", "Это приложение было разработано для книжного магазина, чтобы оптимизировать добавление книг и упростить поиск для посетителей!");

            System.Diagnostics.Process p = System.Diagnostics.Process.Start("NotePad.exe", System.IO.Directory.GetCurrentDirectory() + "\\note.txt");
        }
    }
}
