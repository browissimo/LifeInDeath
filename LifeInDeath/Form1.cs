using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeInDeath
{
    public partial class LifeInDeath : Form
    {
        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;

        public LifeInDeath()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGenegation();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
            {
                return;
            }
            
            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            resolution = (int)nudResolution.Value;

            gameEngine = new GameEngine
                (
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                density: (int)nudDensity.Minimum + (int)nudDensity.Maximum - (int)nudDensity.Value
                ) ;

            Text = $"Generation {gameEngine.CurrentGeneration}";
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }

        private void StopGame()
        {
            if (!timer1.Enabled)
            {
                return;
            }

            timer1.Stop();
            nudDensity.Enabled = true;
            nudResolution.Enabled = true;
        }

        private void DrawNextGenegation()
        {
            graphics.Clear(Color.Black);
            var field = gameEngine.GetCurrentGeneration();

            foreach (var cell in field)
            {
                if (cell.isAlife)
                {
                    if (cell.fracrion == 1)
                    {
                        graphics.FillRectangle(Brushes.Crimson, cell.xPos * resolution, cell.yPos * resolution, resolution - 1, resolution - 1);
                    }
                    else
                    {
                        graphics.FillRectangle(Brushes.Green, cell.xPos * resolution, cell.yPos * resolution, resolution - 1, resolution - 1);
                    }
                   
                }
            }

            pictureBox1.Refresh();
            Text = $"Generation {gameEngine.CurrentGeneration}";
            gameEngine.NextGenegation();
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.AddCell(x, y);

            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.RemoveCell(x, y);
            }
        }

    }
}
