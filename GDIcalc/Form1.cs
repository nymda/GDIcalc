using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDIcalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap face = new Bitmap(300, 400);

        List<Point> startPoints = new List<Point> { new Point(9, 67), new Point(64, 67), new Point(119, 67), new Point(174, 67), new Point(9, 122), new Point(64, 122), new Point(119, 122), new Point(174, 122), new Point(9, 177), new Point(64, 177), new Point(119, 177), new Point(174, 177), new Point(9, 232), new Point(64, 232), new Point(119, 232), new Point(174, 232) };
        List<Point> points_7 = new List<Point> { };
        List<Point> points_8 = new List<Point> { };
        List<Point> points_9 = new List<Point> { };
        List<Point> points_4 = new List<Point> { };
        List<Point> points_5 = new List<Point> { };
        List<Point> points_6 = new List<Point> { };
        List<Point> points_1 = new List<Point> { };
        List<Point> points_2 = new List<Point> { };
        List<Point> points_3 = new List<Point> { };
        List<Point> points_ADD = new List<Point> { };
        List<Point> points_SUB = new List<Point> { };
        List<Point> points_MULT = new List<Point> { };
        List<Point> points_DIV = new List<Point> { };
        List<Point> points_0 = new List<Point> { };
        List<Point> points_EQL = new List<Point> { };
        List<Point> points_CLEAR = new List<Point> { };
        List<Point> points_SAVE = new List<Point> { };

        string disp = "";
        int tot = 0;
        string sumData = "";

        List<String> items = new List<String> { };

        private void Form1_Load(object sender, EventArgs e)
        {
            drawCalc();
        }

        public void drawCalc()
        {
            populateAreaLists();

            Graphics g = Graphics.FromImage(face);
            Rectangle textBox = new Rectangle(new Point(12, 12), new Size(209, 50));
            Pen Outline = new Pen(Color.Gray, 3);
            Font Nametag = new Font("Arial", 12.0f);

            g.FillRectangle(Brushes.White, textBox);
            g.DrawRectangle(Outline, textBox);
            g.DrawImage(getButtonBmp("7"), new Point(9, 67));
            g.DrawImage(getButtonBmp("8"), new Point(64, 67));
            g.DrawImage(getButtonBmp("9"), new Point(119, 67));
            g.DrawImage(getButtonBmp("+"), new Point(174, 67));

            g.DrawImage(getButtonBmp("4"), new Point(9, 122));
            g.DrawImage(getButtonBmp("5"), new Point(64, 122));
            g.DrawImage(getButtonBmp("6"), new Point(119, 122));
            g.DrawImage(getButtonBmp("-"), new Point(174, 122));

            g.DrawImage(getButtonBmp("1"), new Point(9, 177));
            g.DrawImage(getButtonBmp("2"), new Point(64, 177));
            g.DrawImage(getButtonBmp("3"), new Point(119, 177));
            g.DrawImage(getButtonBmp("X"), new Point(174, 177));

            g.DrawImage(getButtonBmp("0"), new Point(9, 232));
            g.DrawImage(getButtonBmp("="), new Point(64, 232));
            g.DrawImage(getButtonBmp("C"), new Point(119, 232));
            g.DrawImage(getButtonBmp("÷"), new Point(174, 232));

            g.DrawString("Made by kned!", Nametag, Brushes.Black, new Point(9, 285));

            pictureBox1.Image = face;
        }

        public void drawDisplay(string dat)
        {
            Graphics g = Graphics.FromImage(face);

            if (dat == "CLEAR")
            {               
                disp = "";
                Rectangle textBox = new Rectangle(new Point(12, 12), new Size(209, 50));
                Pen Outline = new Pen(Color.Gray, 3);
                g.FillRectangle(Brushes.White, textBox);
                g.DrawRectangle(Outline, textBox);              
            }
            else
            {
                float fontSize = 15.0f;
                Font font = new Font("Arial", fontSize);

                SizeF stringSize = g.MeasureString(dat, font);

                while(stringSize.Width > 200)
                {
                    Rectangle textBox = new Rectangle(new Point(12, 12), new Size(209, 50));
                    Pen Outline = new Pen(Color.Gray, 3);
                    g.FillRectangle(Brushes.White, textBox);
                    g.DrawRectangle(Outline, textBox);
                    fontSize = fontSize - 1;
                    font = new Font("Arial", fontSize);
                    stringSize = g.MeasureString(dat, font);
                }
               
                g.DrawString(dat, font, Brushes.Black, 17, 17);
            }

            pictureBox1.Image = face;
        }

        public Bitmap getButtonBmp(string btnLbl)
        {
            Bitmap btn = new Bitmap(50, 50);
            Font font = new Font("Arial", 18.0f);
            Graphics g = Graphics.FromImage(btn);
            Pen Outline = new Pen(Color.Gray, 3);
            Rectangle btnOutl = new Rectangle(new Point(3, 3), new Size(44, 44));
            g.DrawRectangle(Outline, btnOutl);
            g.DrawString(btnLbl, font, Brushes.Black, new Point(5, 5));
            return btn;
        }

        bool firstAfterEQL = false;
        private void Picturebox1_Mousedown(object sender, MouseEventArgs e)
        {
            if (firstAfterEQL)
            {
                drawDisplay("CLEAR");
                firstAfterEQL = false;
            }

            string itm = getButtonClicked(e);
            if (!(itm == "EQL") && !(itm == "CLEAR") && !(itm == "ADD") && !(itm == "SUB") && !(itm == "MULT") && !(itm == "DIV") && !(itm == "NOBTN"))
            {
                items.Add(itm);
                disp = disp + itm.ToString();
                drawDisplay(disp);
            }

            sumData = sumData + itm;
            
            if (itm == "ADD") { drawDisplay("CLEAR"); }
            if (itm == "SUB") { drawDisplay("CLEAR"); }
            if (itm == "MULT") { drawDisplay("CLEAR"); }
            if (itm == "DIV") { drawDisplay("CLEAR"); }

            if (itm == "CLEAR")
            {
                int dispNum = 0;

                try
                {
                    dispNum = Int32.Parse(disp);
                }
                catch
                {
                    //invalid data in display
                }

                drawDisplay("CLEAR");
                tot = 0;
                sumData = "";
            }

            if (itm == "EQL")
            {
                firstAfterEQL = true;

                int dispNum = 0;

                try
                {
                    dispNum = Int32.Parse(disp);
                }
                catch
                {
                    //invalid data in display
                }

                drawDisplay("CLEAR");

                sumData = sumData.Replace("ADD", "+");
                sumData = sumData.Replace("SUB", "-");
                sumData = sumData.Replace("MULT", "*");
                sumData = sumData.Replace("DIV", "/");
                sumData = sumData.Replace("EQL", "");
                sumData = sumData.Replace("NOBTN", "");

                double sum = 0;

                try
                {
                    sum = Evaluate(sumData);
                    drawDisplay(sum.ToString());
                }
                catch
                {
                    drawDisplay("ERR");
                }

                tot = 0;
                sumData = sum.ToString();
            }

        }

        public static double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        public string getButtonClicked(MouseEventArgs mev)
        {
            Point clicked = new Point(mev.X, mev.Y);

            if (points_1.Contains(clicked)) { return ("1"); };
            if (points_2.Contains(clicked)) { return ("2"); };
            if (points_3.Contains(clicked)) { return ("3"); };
            if (points_4.Contains(clicked)) { return ("4"); };
            if (points_5.Contains(clicked)) { return ("5"); };
            if (points_6.Contains(clicked)) { return ("6"); };
            if (points_7.Contains(clicked)) { return ("7"); };
            if (points_8.Contains(clicked)) { return ("8"); };
            if (points_9.Contains(clicked)) { return ("9"); };
            if (points_ADD.Contains(clicked)) { return ("ADD"); };
            if (points_SUB.Contains(clicked)) { return ("SUB"); };
            if (points_MULT.Contains(clicked)) { return ("MULT"); };
            if (points_DIV.Contains(clicked)) { return ("DIV"); };
            if (points_0.Contains(clicked)) { return ("0"); };
            if (points_EQL.Contains(clicked)) { return ("EQL"); };
            if (points_CLEAR.Contains(clicked)) { return ("CLEAR"); };
            return ("NOBTN");
        }

        public void populateAreaLists()
        {
            Graphics g = Graphics.FromImage(face);

            Point start_7 = startPoints[0];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_7.X + i;
                    int currentY = start_7.Y + e;
                    points_7.Add(new Point(currentX, currentY));
                }
            }

            Point start_8 = startPoints[1];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_8.X + i;
                    int currentY = start_8.Y + e;
                    points_8.Add(new Point(currentX, currentY));
                }
            }

            Point start_9 = startPoints[2];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_9.X + i;
                    int currentY = start_9.Y + e;
                    points_9.Add(new Point(currentX, currentY));
                }
            }

            Point start_ADD = startPoints[3];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_ADD.X + i;
                    int currentY = start_ADD.Y + e;
                    points_ADD.Add(new Point(currentX, currentY));
                }
            }

            Point start_4 = startPoints[4];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_4.X + i;
                    int currentY = start_4.Y + e;
                    points_4.Add(new Point(currentX, currentY));
                }
            }

            Point start_5 = startPoints[5];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_5.X + i;
                    int currentY = start_5.Y + e;
                    points_5.Add(new Point(currentX, currentY));
                }
            }

            Point start_6 = startPoints[6];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_6.X + i;
                    int currentY = start_6.Y + e;
                    points_6.Add(new Point(currentX, currentY));
                }
            }

            Point start_SUB = startPoints[7];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_SUB.X + i;
                    int currentY = start_SUB.Y + e;
                    points_SUB.Add(new Point(currentX, currentY));
                }
            }

            Point start_1 = startPoints[8];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_1.X + i;
                    int currentY = start_1.Y + e;
                    points_1.Add(new Point(currentX, currentY));
                }
            }

            Point start_2 = startPoints[9];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_2.X + i;
                    int currentY = start_2.Y + e;
                    points_2.Add(new Point(currentX, currentY));
                }
            }

            Point start_3 = startPoints[10];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_3.X + i;
                    int currentY = start_3.Y + e;
                    points_3.Add(new Point(currentX, currentY));
                }
            }

            Point start_MULT = startPoints[11];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_MULT.X + i;
                    int currentY = start_MULT.Y + e;
                    points_MULT.Add(new Point(currentX, currentY));
                }
            }

            Point start_0 = startPoints[12];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_0.X + i;
                    int currentY = start_0.Y + e;
                    points_0.Add(new Point(currentX, currentY));
                }
            }

            Point start_EQL = startPoints[13];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_EQL.X + i;
                    int currentY = start_EQL.Y + e;
                    points_EQL.Add(new Point(currentX, currentY));
                }
            }

            Point start_CLEAR = startPoints[14];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_CLEAR.X + i;
                    int currentY = start_CLEAR.Y + e;
                    points_CLEAR.Add(new Point(currentX, currentY));

                }
            }

            Point start_DIV = startPoints[15];
            for (int i = 0; i < 50; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_DIV.X + i;
                    int currentY = start_DIV.Y + e;
                    points_DIV.Add(new Point(currentX, currentY));
                }
            }

            Point start_KNED = new Point(9, 285);
            for (int i = 0; i < 20; i++)
            {
                for (int e = 0; e < 50; e++)
                {
                    int currentX = start_DIV.X + i;
                    int currentY = start_DIV.Y + e;
                    points_DIV.Add(new Point(currentX, currentY));
                }
            }

            pictureBox1.Image = face;
        }
    }
}
