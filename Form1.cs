using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImperialAgeLauncher
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private int rotationAngle = 0;
        private Timer rotationTimer;
        


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 35, 35)); // Altere 30 para ajustar o grau de arredondamento

            // Inicializar o Timer
            rotationTimer = new Timer();
            rotationTimer.Interval = 10; // Intervalo para um efeito de rotação suave
            rotationTimer.Tick += RotationTimer_Tick;

            // Conecte os eventos do PictureBox
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

            // Conectar os eventos de rotação ao pictureBox9
            pictureBox9.MouseEnter += PictureBox9_MouseEnter;
            pictureBox9.MouseLeave += PictureBox9_MouseLeave;
            pictureBox9.Paint += PictureBox9_Paint;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
            pictureBox4.Parent = pictureBox1;
            pictureBox5.Parent = pictureBox1;
            pictureBox9.Parent = pictureBox1;

        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Quando o mouse é pressionado, iniciamos o arrasto
            dragging = true;
            // Pega a posição atual do cursor e do formulário
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Se estiver arrastando, mova o formulário
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Finaliza o arrasto quando o botão do mouse é solto
            dragging = false;
        }
        //botao website
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://imperialage.com.br/") { UseShellExecute = true });
        }
        private void pictureBox2_Enter(object sender, EventArgs e)
        {
            
            pictureBox2.Image = Properties.Resources.website1;

        }
        private void pictureBox2_Leave(object sender, EventArgs e)
        {
            
            pictureBox2.Image = Properties.Resources.website;
        }

        //botao discord
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/AsY7kv8H") { UseShellExecute = true });
        }
        private void pictureBox3_Enter(object sender, EventArgs e)
        {
            //pictureBox3.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.avancar2);
            pictureBox3.Image = Properties.Resources.discord1;

        }
        private void pictureBox3_Leave(object sender, EventArgs e)
        {
            //pictureBox3.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.avancar1);
            pictureBox3.Image = Properties.Resources.discord;
        }
        
        //botao exit
        private void pictureBox5_Enter(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.exit2;
        }
        private void pictureBox5_Leave(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.exit1;
        }

        //botao Jogar
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox4_Enter(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.jogar2;
        }
        private void pictureBox4_Leave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.jogar1;
        }

        //botao exit
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Você tem certeza que deseja fechar?",
                "Confirmar saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //botao gear
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
        }
        private void PictureBox9_MouseEnter(object sender, EventArgs e)
        {
            rotationAngle = 0; // Resetar o ângulo
            rotationTimer.Start(); // Iniciar o Timer para rotacionar
        }

        private void PictureBox9_MouseLeave(object sender, EventArgs e)
        {
            rotationTimer.Stop(); // Parar o Timer ao sair do mouse
            pictureBox9.Invalidate(); // Atualizar o PictureBox para redefinir o ângulo
        }

        private void RotationTimer_Tick(object sender, EventArgs e)
        {
            rotationAngle += 5; // Incremento do ângulo de rotação
            if (rotationAngle >= 360)
            {
                rotationAngle = 0; // Resetar após completar uma volta
            }
            pictureBox9.Invalidate(); // Redesenhar o PictureBox com o novo ângulo
        }

        private void PictureBox9_Paint(object sender, PaintEventArgs e)
        {
            // Rotacionar a imagem do PictureBox
            if (pictureBox9.Image != null)
            {
                e.Graphics.TranslateTransform(pictureBox9.Width / 2, pictureBox9.Height / 2);
                e.Graphics.RotateTransform(rotationAngle);
                e.Graphics.TranslateTransform(-pictureBox9.Width / 2, -pictureBox9.Height / 2);
                e.Graphics.DrawImage(pictureBox9.Image, new Rectangle(0, 0, pictureBox9.Width, pictureBox9.Height));
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
