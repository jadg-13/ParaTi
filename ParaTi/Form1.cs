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

namespace ParaTi
{
    public partial class Form1 : Form
    {
        // Lista para almacenar las rutas de las imágenes
        private List<string> imagePaths;
        // Índice actual de la imagen mostrada
        private int currentImageIndex;

        // Variables para controlar el movimiento del formulario
        private bool isDragging = false;
        private Point dragStartPoint;

        public Form1()
        {
            InitializeComponent();
            InitializeImagePaths();
            // Configurar el formulario
            this.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            this.BackColor = Color.Black; // Color de fondo que se volverá transparente
            this.TransparencyKey = Color.Black; // El mismo color que BackColor será transparente
            this.StartPosition = FormStartPosition.CenterScreen;


        }

        private void InitializeImagePaths()
        {
            // Inicializa la lista de rutas de las imágenes
            imagePaths = new List<string>();
            string imagesFolder = Path.Combine(Application.StartupPath, "Images");

            // Verifica si la carpeta existe
            if (Directory.Exists(imagesFolder))
            {
                // Obtiene todas las rutas de las imágenes en la carpeta
                string[] files = Directory.GetFiles(imagesFolder, "*.png"); // Puedes cambiar el filtro si usas otro formato
                imagePaths.AddRange(files);
            }

            // Inicializa el índice de la imagen actual
            currentImageIndex = 0;

            // Muestra la primera imagen si hay alguna
            if (imagePaths.Count > 0)
            {
                pictureBox1.Image = Image.FromFile(imagePaths[currentImageIndex]);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point difference = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                this.Location = new Point(this.Location.X + difference.X, this.Location.Y + difference.Y);
            }
        }

        private void crecerAlto()
        {
            if (Height < 580) Height += 10;

        }

        private void crecerAncho()
        {
            if (Width < 767)
                Width += 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            crecerAlto();
            crecerAncho();
            florAleatoria();
            if (Opacity < 1)
                Opacity += 0.01;
            else
                timer1.Stop();

        }

        private void florAleatoria()
        {
            Random rnd = new Random();
            int index = rnd.Next(imagePaths.Count);
            pictureBox1.Image = Image.FromFile(imagePaths[index]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Height = 0;
            Width = 0;
            Opacity = 0;
            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            florAleatoria();    
        }
    }
}
