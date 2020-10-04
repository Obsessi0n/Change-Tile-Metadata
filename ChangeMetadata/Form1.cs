using ExifLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeMetadata
{
    
    public partial class Form1 : Form
    {
        

        List<string> listFiles = new List<string>();
        //List<string> filePaths = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            listFiles.Clear();
            listView1.Items.Clear();
            //filePaths.Clear();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxPath.Text = fbd.SelectedPath;
                    foreach(string item in Directory.GetFiles(fbd.SelectedPath))
                    {
                        imageList1.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                        FileInfo fi = new FileInfo(item);
                        listFiles.Add(fi.FullName);
                        //filePaths.Add(fbd.SelectedPath.ToString() + fi.FullName);
                        listView1.Items.Add(fi.Name, imageList1.Images.Count - 1);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Current Title metadata will be the same as filename.",
                                     "Are you sure? ",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (listFiles != null)
                {
                    foreach (var item in listFiles)
                    {
                        if(System.IO.Path.GetExtension(item) == ".mp4" || System.IO.Path.GetExtension(item) == ".wmv")
                        {
                            var tfile = TagLib.File.Create(item);
                            string title = tfile.Tag.Title;
                            //TimeSpan duration = tfile.Properties.Duration;

                            //// change title in the file
                            tfile.Tag.Title = Path.GetFileNameWithoutExtension(item);
                            tfile.Save();
                        }

                    }
                    MessageBox.Show("Done!");
                }
            }
            else
            {

            }



 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Current Title metadata will be gone.",
                                    "Are you sure? ",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (listFiles != null)
                {
                    foreach (var item in listFiles)
                    {
                        if (System.IO.Path.GetExtension(item) == ".mp4" || System.IO.Path.GetExtension(item) == ".wmv")
                        {
                            var tfile = TagLib.File.Create(item);
                            string title = tfile.Tag.Title;
                            //TimeSpan duration = tfile.Properties.Duration;

                            //// change title in the file
                            tfile.Tag.Title = "";
                            tfile.Save();
                        }

                    }
                    MessageBox.Show("Done!");
                }
            }
            else
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}


