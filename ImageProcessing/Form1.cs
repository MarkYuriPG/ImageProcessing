using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loadedImage, processedImage;
        Color pixel;

        public Form1()
        {
            InitializeComponent();
            this.Height = 489;
            this.Width = 830;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loadedImage = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loadedImage;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for(int x = 0; x < loadedImage.Width; x++)
            {
                for(int y = 0; y < loadedImage.Height; y++)
                {
                    pixel = loadedImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, pixel);
                }
            }

            pictureBox2.Image = processedImage;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for (int x = 0; x < loadedImage.Width; x++)
            {
                for (int y = 0; y < loadedImage.Height; y++)
                {
                    pixel = loadedImage.GetPixel(x, y);
                    int greyScale = ((pixel.R + pixel.G + pixel.B) / 3);
                    processedImage.SetPixel(x, y, Color.FromArgb(greyScale, greyScale, greyScale));
                }
            }

            pictureBox2.Image = processedImage;
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for (int x = 0; x < loadedImage.Width; x++)
            {
                for (int y = 0; y < loadedImage.Height; y++)
                {
                    pixel = loadedImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, Color.FromArgb(255-pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }

            pictureBox2.Image = processedImage;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for (int x = 0; x < loadedImage.Width; x++)
            {
                for (int y = 0; y < loadedImage.Height; y++)
                {
                    pixel = loadedImage.GetPixel(x, y);
                    int greyScale = ((pixel.R + pixel.G + pixel.B) / 3);
                    processedImage.SetPixel(x, y, Color.FromArgb(greyScale, greyScale, greyScale));
                }
            }

            Color sample;
            int[] histogramData = new int[256];

            for (int x = 0; x < loadedImage.Width; x++)
            {
                for (int y = 0; y < loadedImage.Height; y++)
                {
                    sample = processedImage.GetPixel(x, y);

                    histogramData[sample.R]++;
                }
            }

            Bitmap histogram = new Bitmap(256, 800);

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    histogram.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histogramData[x]/5,800); y++)
                {
                    histogram.SetPixel(x, 799-y, Color.Black);
                }
            }

            pictureBox2.Image = histogram;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for (int x = 0; x < loadedImage.Width; x++)
            {
                for (int y = 0; y < loadedImage.Height; y++)
                {
                    pixel = loadedImage.GetPixel(x, y);
                    int alpha = pixel.A;
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    int sepiaRed = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int sepiaGreen = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int sepiaBlue = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    if (sepiaRed > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = sepiaRed;
                    }

                    if (sepiaGreen > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = sepiaGreen;
                    }

                    if (sepiaBlue > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = sepiaBlue;
                    }

                    processedImage.SetPixel(x, y, Color.FromArgb(alpha, r, g, b));
                }
            }

            pictureBox2.Image = processedImage;
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox
                        .Show("Do you want to exit?", 
                            "Exit Confirmation", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = saveFileDialog1.FileName;

            if (!fileName.ToLower().EndsWith(".png"))
            {
                fileName += ".png";
            }

            processedImage.Save(fileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }


    }
}
