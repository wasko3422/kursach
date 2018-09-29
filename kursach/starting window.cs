using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;


namespace kursach
{
    public partial class Form1 : Form
    {

        string file = "";
        public List<book> books = new List<book>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Стартовое окно";

            var path = "lists of books";


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                if(help_class.reg.check())
                {
                    help_class.reg.delete();
                }
            }
            else
            {
                if (!(Directory.GetFiles(Directory.GetCurrentDirectory() +"\\" + path).Length == 0))
                {

                    file = help_class.reg.get();
                    //file = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + path)[0];
                    if(file == string.Empty)
                    {

                        if (Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + path).Length == 0)
                        {
                            file = "list_1";
                            return;
                        }
                        file = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + path)[0];
                        books = help_class.open_xml(file);
                        file = file.Split(Path.DirectorySeparatorChar).Last();
                        file = file.Substring(0, file.Length - 4);
                        help_class.reg.create_set(file);

                    }
                    books = help_class.open_xml(Directory.GetCurrentDirectory() + "\\" + path + "\\" + file + ".xml");
                    //file = file.Split(Path.DirectorySeparatorChar).Last();
                    //file = file.Substring(0, file.Length - 4);
                    return;
                }

            }
            books.Add(new book("Братья Карамазовы", "Достоевский", 1000, 4.7, "Правда", 2007, 300));
            books.Add(new book("Лолита", "Набоков", 1000, 4.7, "Правда", 2007, 300));
            books.Add(new book("Герой нашего времени", "Лермонтов", 1000, 4.7, "Правда", 2007, 300));
            books.Add(new book("Евгений Онегин", "Пушкин", 1000, 4.7, "Правда", 2007, 300));
            books.Add(new book("Завтрак для чемпионов, или Прощай, Черный понедельник ", "Воннегут", 1000, 4.7, "Правда", 2007, 300));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin form2 = new admin();
            form2.books = books;
            form2.name = file;
            form2.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            gost form3 = new gost();
            form3.books = books;
            form3.name = file;
            form3.ShowDialog();
            this.Close();

        }
    }
}
