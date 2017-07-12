using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public bool lewo = false, prawo = false;
        FolderBrowserDialog fbd;
        DialogResult dr;
        string[] fileEntries;
        string[] images;
        int i = 0, pliki = 0;
        Rectangle resolution = Screen.PrimaryScreen.Bounds;
        public Form1()
        {
            InitializeComponent();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                i = 0;
                fbd = new FolderBrowserDialog();
                dr = fbd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    fileEntries = Directory.GetFiles(fbd.SelectedPath);
                    if (fileEntries.Length == 0)
                    {
                        MessageBox.Show("W tej sciezce nie ma plików");
                        openToolStripMenuItem_Click(sender, e);
                    }
                    int j = 0;
                    for (int i = 0; i < fileEntries.Length; i++)
                    {

                        if (fileEntries[i].EndsWith(".jpg") | fileEntries[i].EndsWith(".gif") | fileEntries[i].EndsWith(".bmp") | fileEntries[i].EndsWith(".png"))
                        {
                            j++;
                        }
                    }
                    images = new string[j];
                    j = 0;
                    for (int i = 0; i < fileEntries.Length; i++)
                    {

                        if (fileEntries[i].EndsWith(".jpg") | fileEntries[i].EndsWith(".gif") | fileEntries[i].EndsWith(".bmp") | fileEntries[i].EndsWith(".png"))
                        {
                            images[j] = fileEntries[i];
                            j++;
                        }
                    }
                    if (images.Length == 0)
                    {
                        MessageBox.Show("W tej sciezce nie ma zdjęć");
                        openToolStripMenuItem_Click(sender, e);
                    }
                    pliki = images.Length;
                }                
                //ofd = new OpenFileDialog();
                //ofd.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png|gif (*.gif)|*.gif";
                //ofd.Multiselect = true;
                //dr = ofd.ShowDialog();
                //pliki = ofd.FileNames.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            wyswietl();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void right_arrow_Click(object sender, EventArgs e)
        {
            try
            {
                prawo = true;
                lewo = false;
                GC.Collect();
                if (i < pliki - 1)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            wyswietl();
        }

        private void left_arrow_Click(object sender, EventArgs e)
        {
            try
            {
                lewo = true;
                prawo = false;
                GC.Collect();
                if (i > 0)
                {
                    i--;
                }
                else
                {
                    i = pliki - 1;
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            wyswietl();
        }
        private void wyswietl()
        {
            try
            {
                if (dr == DialogResult.OK)
                {
                    String file = images[i];
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = Image.FromFile(file);
                    Image img = System.Drawing.Image.FromFile(file);
                    if (resolution.Height - 100 < img.Height | resolution.Width < img.Width)
                    {
                        Size = new System.Drawing.Size(resolution.Width, resolution.Height);
                        this.WindowState = FormWindowState.Maximized;
                    }
                    else if (img.Height < 200 | img.Width < 200)
                    {
                        Size = new System.Drawing.Size(200, 200);
                        this.WindowState = FormWindowState.Normal;
                        this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - 200) / 2, (Screen.PrimaryScreen.WorkingArea.Height - 200) / 2);
                    }
                    else
                    {
                        Size = new System.Drawing.Size(img.Width, img.Height);
                        this.WindowState = FormWindowState.Normal;
                        this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - img.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - img.Height) / 2);
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Plik nie może zostać otworzony");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Brak uprawnien do wyswietlenia tego pliku");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
