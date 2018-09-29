using System;
using System.Collections.Generic;
using System.Windows.Forms;



[Serializable]
public class book
{
    public string name { get; set; }
    public string author { get; set; }
    public int price { get; set; }
    public double rating { get; set; }
    public string publisher { get; set; }
    public int year { get; set; }
    public int quanity { get; set; }
    public string part { get; set; }

    public book()
	{
    }

    public book(string name, string author, int price, double rating, string publisher, int year, int quanity = 1, string part = "-")
    {
        this.name = name;
        this.author = author;
        this.price = price;
        this.rating = rating;
        this.publisher = publisher;
        this.year = year;
        this.quanity = quanity;
        this.part = part;
    }

    public book(List<string> list)
    {
        this.name = list[0];
        this.author = list[1];
        this.price = Convert.ToInt32(list[2]);
        this.rating = Convert.ToDouble(list[3]);
        this.publisher = list[4];
        this.year= Convert.ToInt32(list[5]);
        this.quanity = Convert.ToInt32(list[6]);
        if(list.Count == 8)
        {
            this.part = list[7];
        }
        else
        {
            this.part = "-";
        }
    }

    public book(DataGridViewRow list)
    {

            this.name = list.Cells[0].Value.ToString();
            this.author = list.Cells[1].Value.ToString();
            this.price = Convert.ToInt32(list.Cells[2].Value.ToString());
            this.rating = Convert.ToDouble(list.Cells[3].Value.ToString());
            this.publisher = list.Cells[4].Value.ToString();
            this.year = Convert.ToInt32(list.Cells[5].Value.ToString());
            this.quanity = Convert.ToInt32(list.Cells[6].Value.ToString());
            this.part = list.Cells[7].Value.ToString();

    }
}
