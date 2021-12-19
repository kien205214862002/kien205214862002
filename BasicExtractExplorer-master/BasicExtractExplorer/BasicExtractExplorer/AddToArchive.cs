using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SevenZip;

namespace BasicExtractExplorer
{
    public partial class AddToArchive : Form
    {
        List<string> paths;
        SevenZipCompressor zipCompressor;
        OutArchiveFormat format;
        CompressionLevel level;
        public AddToArchive(List<string> paths)
        {
            InitializeComponent();
            SevenZipCompressor.SetLibraryPath("7z.dll");
            comboBoxLevel.SelectedIndex = 0;
            zipCompressor = new SevenZipCompressor();
            this.Paths = paths;
            textBoxArchiveName.Text =Path.GetDirectoryName(Paths[0]) + "\\" + Path.GetFileNameWithoutExtension(Paths[0]) + ".zip";
            textBoxArchiveName.Text = textBoxArchiveName.Text.Replace("\\\\", "\\");
            format = OutArchiveFormat.Zip;
        }
        public List<string> Paths
        {
            get { return paths; }
            set { paths = value; }
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(textBoxArchiveName.Text);
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(textBoxArchiveName.Text);
            saveFileDialog.Filter = "zip|*.zip|tar|*.tar|7z|*.7z|bzip2|*.bz2|gzip|*.gz|xz|*.xz";
            saveFileDialog.FileOk += (sd, ev) =>
            {
                textBoxArchiveName.Text = saveFileDialog.FileName;
                switch (saveFileDialog.FilterIndex)
                {
                    case 0:
                        format = OutArchiveFormat.Zip;
                        break;
                    case 1:
                        format = OutArchiveFormat.Zip;
                        break;
                    case 2:
                        format = OutArchiveFormat.Tar;
                        break;
                    case 3:
                        format = OutArchiveFormat.SevenZip;
                        break;
                    case 4:
                        format = OutArchiveFormat.BZip2;
                        break;
                    case 5:
                        format = OutArchiveFormat.GZip;
                        break;
                    case 6:
                        format = OutArchiveFormat.XZ;
                        break;
                    default:
                        format = OutArchiveFormat.Zip;
                        break;
                }
            };
            saveFileDialog.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBoxPass.Text == textBoxRePass.Text)
            {
                //Lấy tên archive
                string archiveName = textBoxArchiveName.Text;
                //chuyển quá trình nén qua form Processing
                Processing processing = new Processing(format, true, level, Paths, archiveName, textBoxPass.Text);
                processing.StartPosition = FormStartPosition.CenterScreen;
                processing.Show();
                this.Hide();
                processing.FormClosed += delegate { Close(); };
            }
            else
            {
                MessageBox.Show("Passwords do not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLevel.SelectedIndex)
            {
                case 1:
                    level = CompressionLevel.High;
                    break;
                case 2:
                    level = CompressionLevel.Low;
                    break;
                case 3:
                    level = CompressionLevel.Ultra;
                    break;
                case 4:
                    level = CompressionLevel.None;
                    break;
                case 5:
                    level = CompressionLevel.Normal;
                    break;
                default:
                    level = CompressionLevel.Normal;
                    break;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxArchiveName_TextChanged(object sender, EventArgs e)
        {
            string[] passwordSupportFormat = { ".zip" };
            if(passwordSupportFormat.Contains(Path.GetExtension(textBoxArchiveName.Text)))
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
