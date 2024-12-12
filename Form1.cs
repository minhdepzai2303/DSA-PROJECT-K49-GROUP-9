using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dijkstra_WindowsForms.Program;

namespace Dijkstra_WindowsForms
{
    public partial class Form1 : Form
    {

        Graph theGraph = new Graph();
        public int[,] weights = new int[20, 20];
        private List<int> parentVer = new List<int>();
        private List<int> currentVer = new List<int>();
        public Form1()
        {
            InitializeComponent();
            this.Paint += MainForm_Paint;
            Form1_Load(this, EventArgs.Empty);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Cố định đường viền
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to
            this.MinimizeBox = false; // Vô hiệu hóa thu nhỏ
            this.Size = new Size(910, 653); // size

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            theGraph.AddVertex("A"); theGraph.AddVertex("B");
            theGraph.AddVertex("C"); theGraph.AddVertex("D");
            theGraph.AddVertex("E"); theGraph.AddVertex("F");
            theGraph.AddVertex("G"); theGraph.AddVertex("H");
            theGraph.AddVertex("I");

            theGraph.AddEdge(0, 8, 58); theGraph.AddEdge(0, 4, 43);
            theGraph.AddEdge(0, 5, 73); theGraph.AddEdge(0, 1, 66);
            theGraph.AddEdge(1, 5, 41); theGraph.AddEdge(1, 6, 78);
            theGraph.AddEdge(1, 2, 81); theGraph.AddEdge(2, 7, 47);
            theGraph.AddEdge(2, 4, 19); theGraph.AddEdge(2, 8, 29);
            theGraph.AddEdge(2, 3, 10); theGraph.AddEdge(3, 4, 34);
            theGraph.AddEdge(3, 8, 25); theGraph.AddEdge(3, 6, 13);
            theGraph.AddEdge(4, 5, 35); theGraph.AddEdge(4, 7, 97);
            theGraph.AddEdge(5, 7, 78);
            ;
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    weights[i, j] = theGraph.GetWeights(i, j);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private string[] locations = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

        private Point[] locationCoordinates = {
            new Point(560, 180),  // A(14, 14)
            new Point(560, 420),  // B(14, 6)
            new Point(80, 420),   // C(2, 6)
            new Point(80, 180),   // D(2, 14)
            new Point(240, 280),  // E(6, 10)
            new Point(350, 350),  // F(10, 8)
            new Point(160, 70),   // G(4, 18)
            new Point(350, 600),  // H(6, 2)
            new Point(250, 180)   // I(7, 14)
        };



        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

            // Vẽ các đường nối giữa các địa điểm
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);  // Pen for drawing lines
            Brush textBrush = Brushes.IndianRed;    // Brush for text

            // Loop through the adjacency matrix to find valid edges
            for (int i = 0; i < locationCoordinates.Length; i++)
            {
                for (int j = i + 1; j < locationCoordinates.Length; j++)
                {
                    // Check if there is an edge between point i and point j
                    if (weights[i, j] != 90000) // Valid edge
                    {
                        // Draw a line between the points
                        g.DrawLine(pen, locationCoordinates[i], locationCoordinates[j]);

                        // Calculate the midpoint of the line to place the weight label
                        int midX = (locationCoordinates[i].X + locationCoordinates[j].X) / 2;
                        int midY = (locationCoordinates[i].Y + locationCoordinates[j].Y + 15) / 2;

                        // Display the weight on the line
                        g.DrawString(weights[i, j].ToString(), this.Font, textBrush, midX, midY);
                    }
                }
            }

            // Vẽ các địa điểm
            Graphics g2 = e.Graphics;
            Pen pen2 = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 10);

            for (int i = 0; i < locations.Length; i++)
            {
                Rectangle rect = new Rectangle(locationCoordinates[i].X - 10, locationCoordinates[i].Y - 10, 20, 20);
                g2.FillEllipse(Brushes.LightGreen, rect); // Hình tròn
                g2.DrawEllipse(Pens.Black, rect); // Viền
                g2.DrawString(locations[i], font, Brushes.Black, locationCoordinates[i].X - 15, locationCoordinates[i].Y - 30); // Tên
            }

            //Vẽ đường đi ngắn nhất


            Pen highlightPen = new Pen(Color.Red, 3);  // Red for shortest path
            int pointRadius = 5;

            // Loop through parent and current vertices to draw the path
            for (int i = 0; i < parentVer.Count; i++)
            {
                int start = parentVer[i];  // Parent vertex in the path
                int end = currentVer[i];   // Current vertex in the path

                // Draw the red edge for the shortest path
                g.DrawLine(highlightPen, locationCoordinates[start], locationCoordinates[end]);

                // Draw small circles at both ends of the edge
                g.FillEllipse(Brushes.Red, locationCoordinates[start].X - pointRadius, locationCoordinates[start].Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                g.FillEllipse(Brushes.Red, locationCoordinates[end].X - pointRadius, locationCoordinates[end].Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
            }



        }


        private void button1_Click(object sender, EventArgs e)
        {
            parentVer.Clear();
            currentVer.Clear();

            int start = comboBox1.SelectedIndex;
            int end = comboBox2.SelectedIndex;

            int cost = theGraph.Path(start, end);
            
            textBox2.Text = cost.ToString();

            theGraph.PathTracking(start, end, parentVer, currentVer);

            this.Invalidate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
