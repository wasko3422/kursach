using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace kursach
{
    public partial class gost : Form
    {
        public List<int> indexs = new List<int>();
        public string name = "";
        public gost()
        {
            InitializeComponent();
            this.Text = System.IO.Directory.GetCurrentDirectory();
        }
        public List<book> books = new List<book>();
        
        private void gost_Load(object sender, EventArgs e)
        {
            bookBindingSource.DataSource = books;
            dataGridView1.Rows.ToString();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            stars.clear();

            if (!indexs.Contains(dataGridView1.CurrentRow.Index))
            {
                stars.Rated = false;
                stars.redraw();
            }
            else
            {
                stars.Rated = true;
                stars.fullfill(dataGridView1.CurrentRow.Index);
            }
        }


        private void gost_FormClosed(object sender, FormClosedEventArgs e)
        {
            List<book> kek = new List<book>();
            foreach(DataGridViewRow i in dataGridView1.Rows)
            {
                kek.Add(new book(i));
            }


            if (name.Length == 0)
            {
                help_class.save_xml(kek);
                return;

            }

            help_class.save_xml(kek, name);
            help_class.reg.create_set(name);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
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


            indexs.Clear();
            stars.pairs.Clear();
            stars.clear();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовая работа была сделана Егором Пановым из группы ПИ2-1 \nМоя почта - panow.egor2015@yandex.ru");
        }

        private void оПриложенииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "\\note.txt", "Это приложение было разработано для книжного магазина, чтобы оптимизировать добавление книг и упростить поиск для посетителей!");

            Process p = Process.Start("NotePad.exe", System.IO.Directory.GetCurrentDirectory() + "\\note.txt");
        }
    }
}
