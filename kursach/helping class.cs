using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;


public class help_class
{


    public help_class()
    {
    }

    static public List<int> Search(DataGridViewRowCollection data, string destination)
    {
        var indexs = new List<int>();

        foreach (DataGridViewRow row in data)
        {
            if (DataToString(row).ToLower().Contains(destination.ToLower()))
            {
                indexs.Add(row.Index);
            }
        }
        return indexs;
    }

    static public string DataToString(DataGridViewRow row)
    {
        StringBuilder builder = new StringBuilder();

        foreach (DataGridViewCell cell in row.Cells)
        {
            builder.Append(cell.Value);
            builder.Append(" ");
        }
        builder.Remove(builder.Length - 1, 1);

        return builder.ToString();
    }

    static public void getting_visible(DataGridView data, List<int> list)
    {
        foreach (DataGridViewRow row in data.Rows)
        {
            if (!list.Contains(row.Index))
            {
                row.Visible = false;
                continue;
            }

            row.Visible = true;
        }
    }

    static public void save_xml(List<book> books, string name = "list_1")
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<book>));


        string fullPath = Path.GetFullPath(Directory.GetCurrentDirectory()).TrimEnd(Path.DirectorySeparatorChar);

        string name_1 = "";

        name_1 = String.Format(fullPath + "\\lists of books\\{0}.xml", name);


        using (FileStream path = new FileStream(name_1, FileMode.OpenOrCreate))
        {
            xml.Serialize(path, books);
        }
    }

    static public List<book> open_xml(string path)
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<book>));

        List<book> kek;
        using (FileStream open = new FileStream(path, FileMode.Open))
        {
            kek = (List<book>)xml.Deserialize(open);
        }

        return kek;
    }


    static public List<book> to_book(DataGridView dataGridView1)
    {
        List<book> kek = new List<book>();
        foreach (DataGridViewRow i in dataGridView1.Rows)
        {
            kek.Add(new book(i));
        }

        return kek;
    }


    public class reg
    {
        static RegistryKey user = Registry.CurrentUser;
        static RegistryKey podk;
        static string name = "shop";
        static string value = "File";

        public static void create_set(string name_1)
        {
            try
            {
                podk = user.OpenSubKey(name, true);


                if (podk == null)
                {
                    podk = user.CreateSubKey(name);
                }
                podk.SetValue(value, name_1);
                podk.Close();
            }
            catch
            {
                MessageBox.Show("Не получилось создать и записать имя");
                podk.Close();
            }

        }

        public static void delete()
        {
            try
            {
                podk = user.OpenSubKey(name, true);
                podk.DeleteSubKey("");
                podk.Close();
            }
            catch
            {
                MessageBox.Show("не удалось удалить подраздел");
            }



        }

        public static string get()
        {
            podk = user.OpenSubKey(name, true);

            if( podk != null)
            {
                object buff = podk.GetValue(value);
                if( buff != null)
                {
                    return buff.ToString();
                }
            }

            return string.Empty;
        }

        public static bool check()
        {

            if(user.OpenSubKey(name, true) == null)
            {
                return false;
            }

            return true;
        }
    }
}
