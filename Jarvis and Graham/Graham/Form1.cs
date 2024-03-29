﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graham
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics grp;
        Bitmap bmp;
        List<Point> points;
        Point[] pointArray;
        Dictionary<Point, string> hash = new Dictionary<Point, string>();
        int index;
        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pB.Width, pB.Height);
            grp = Graphics.FromImage(bmp);
            points = new List<Point>();


           /* points.Add(new Point(20, 100));
             points.Add(new Point(20, 90));
             points.Add(new Point(30, 100));
             points.Add(new Point(40, 90));
             points.Add(new Point(50, 70));
             points.Add(new Point(60, 70));
             points.Add(new Point(40, 90));
            points.Add(new Point(30, 60));
            points.Add(new Point(70, 120));
            points.Add(new Point(70, 110));*/

            Random rnd = new Random();
             int n = 15;
            for (int i = 0; i < n; i++)
            {
                Point toAdd;
                do
                {
                    toAdd = new Point(rnd.Next() % (pB.Width / 30), rnd.Next() % (pB.Height / 30));
                    

                } while (points.Contains(toAdd));
                points.Add(toAdd);
             }
             

           labelIndic.Visible = false;
            labelPuncte.Visible = false;

            buttonNext.Enabled = false;


        }
        private List<Point> Graham(Point[] points)
        {
            int index = 0;
            for (int i = 1; i < points.Length; i++)
                if (points[i].X < points[index].X ||
                    (points[i].X == points[index].X && points[i].Y > points[index].Y))
                    index = i;

            Swap(ref points[0], ref points[index]);

            Sort(points);
            Insert(ref points);


            int nrPuncte = 2;
#pragma warning disable CS0219 // The variable 'steps' is assigned but its value is never used
            int steps = 0;
#pragma warning restore CS0219 // The variable 'steps' is assigned but its value is never used
            for (int i = 3; i < points.Length; i++)
            {
                while (nrPuncte > 1 && Orientare(points[nrPuncte - 1], points[nrPuncte], points[i]) >= 0)
                    nrPuncte--;
                nrPuncte++;
                Swap(ref points[nrPuncte], ref points[i]);
            }
            Array.Resize(ref points, nrPuncte + 1);
            return points.ToList();
        }
        int nrPuncte = 2;
        public void GrahamStep(int i)
        {
            grp.DrawEllipse(new Pen(Color.Aquamarine), pointArray[i].X, pointArray[i].Y, 3, 3);
            grp.FillEllipse(new SolidBrush(Color.Aquamarine), pointArray[i].X, pointArray[i].Y, 3, 3);
            while (nrPuncte > 1 && Orientare(pointArray[nrPuncte - 1], pointArray[nrPuncte], pointArray[i]) >= 0)
            {
                grp.DrawLine(new Pen(pB.BackColor), pointArray[nrPuncte - 1], pointArray[nrPuncte]);
                nrPuncte--;



            }
            nrPuncte++;


            Swap(ref pointArray[nrPuncte], ref pointArray[i]);
            labelPuncte.Text = "";
            for (int j = 0; j < nrPuncte; j++)
                labelPuncte.Text += hash[pointArray[j]] + " ";

            for (int j = 0; j < nrPuncte - 1; j++)
                grp.DrawLine(new Pen(Color.Black), pointArray[j], pointArray[j + 1]);
            pB.Image = bmp;
        }
        public void Swap(ref Point a, ref Point b)
        {
            Point aux = new Point();
            aux = a;
            a = b;
            b = aux;


        }
        private float Orientare(Point A, Point B, Point C)
        {
            double temp = (B.Y - A.Y) * (C.X - A.X) - (C.Y - A.Y) * (B.X - A.X);
            if (temp < 0)
                return -1;
            else if (temp == 0)
                return 0;
            else
                return 1;
        }
        private void Sort(Point[] points)
        {
            for (int i = 1; i < points.Length - 1; i++)
            {
                float pantaiX = points[0].X - points[i].X;
                float pantaiY = points[0].Y - points[i].Y;
                for (int j = i + 1; j < points.Length; j++)
                {
                    float pantajX = points[0].X - points[j].X;
                    float pantajY = points[0].Y - points[j].Y;
                    if (pantaiY * pantajX > pantajY * pantaiX)
                    {
                        Swap(ref points[i], ref points[j]);
                        pantaiX = points[0].X - points[i].X;
                        pantaiY = points[0].Y - points[i].Y;
                    }
                }
            }
        }
        private void Insert(ref Point[] points)
        {
            Array.Resize(ref points, points.Length + 1);

            for (int i = points.Length - 1; i > 0; i--)
                points[i] = points[i - 1];
            points[0] = points[points.Length - 1];
        }
        int curent = 3;
        private void ButtonStart_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < points.Count; i++)
            {
                hash.Add(new Point(points[i].X * 30, points[i].Y * 30), "P" + (i + 1));
                
            }
            int pointwidth = 5;

            pointArray = points.ToArray();
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = new Point(pointArray[i].X * 30, pointArray[i].Y * 30);
                grp.DrawEllipse(new Pen(Color.Black), pointArray[i].X, pointArray[i].Y, pointwidth, pointwidth);
                grp.FillEllipse(new SolidBrush(Color.Black), pointArray[i].X, pointArray[i].Y, pointwidth, pointwidth);
                grp.DrawString("P" + (i + 1), new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold), new SolidBrush(Color.Black), pointArray[i]);
            }
            index = 0;
            for (int i = 1; i < pointArray.Length; i++)
                if (pointArray[i].X < pointArray[index].X ||
                    (pointArray[i].X == pointArray[index].X && pointArray[i].Y > pointArray[index].Y))
                    index = i;
            Swap(ref pointArray[0], ref pointArray[index]);
            Sort(pointArray);
            Insert(ref pointArray);
           labelIndic.Visible = false;//true
            labelPuncte.Visible = false;//true
          labelPuncte.Text = "";

            for (int i = 0; i <= 2; i++)
                labelPuncte.Text += hash[pointArray[i]] + " ";
            for (int i = 0; i < 2; i++)
                grp.DrawLine(new Pen(Color.Black), pointArray[i], pointArray[i + 1]);



            ButtonStart.Enabled = false;
            buttonNext.Enabled = true;

           

            pB.Image = bmp;

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (curent < pointArray.Length)
            {
                GrahamStep(curent);
                curent++;
            }
            else
            {

                Array.Resize(ref pointArray, nrPuncte + 1);
                for (int j = 0; j < pointArray.Length - 1; j++)
                    grp.DrawLine(new Pen(Color.Black), pointArray[j], pointArray[j + 1]);
                grp.DrawLine(new Pen(Color.Black), pointArray[0], pointArray[pointArray.Length - 1]);
                pB.Image = bmp;

            }
        }
    }
}
