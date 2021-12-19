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
    
    public partial class ExtractTo : Form
    {
        List<int> fileIndexes;
        string filePath;
        public ExtractTo(string path)
        {
            InitializeComponent();
            filePath = path;
            textBoxDestination.Text = GetPathWithoutExtension(filePath);
        }
        public ExtractTo(string path, List<int> indexes)
        {
            InitializeComponent();
            fileIndexes = indexes;
            filePath = path;
            textBoxDestination.Text = GetPathWithoutExtension(filePath);
        }
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "Browse For Folder";
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDestination.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private string GetPathWithoutExtension(string filepath)
        {
            var result = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath);
            return result.Replace("\\\\", "\\");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(fileIndexes == null) //nếu không yêu cầu giải nén từng tệp chỉ định
            {
                Processing processing = new Processing(filePath, textBoxDestination.Text);
                processing.StartPosition = FormStartPosition.CenterScreen;
                processing.Show();
                Hide();
                processing.FormClosed += delegate { Close(); };
            }
            else
            {
                Processing processing = new Processing(filePath, textBoxDestination.Text, fileIndexes);
                processing.StartPosition = FormStartPosition.CenterScreen;
                processing.Show();
                Hide();
                processing.FormClosed += delegate { Close(); };
            }
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
