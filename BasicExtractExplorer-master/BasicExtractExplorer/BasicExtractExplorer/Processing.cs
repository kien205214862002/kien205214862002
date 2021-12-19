using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SevenZip;

namespace BasicExtractExplorer
{
    public partial class Processing : Form
    {
        SevenZipCompressor compressor;
        List<string> paths;
        List<int> fileIndex;
        string archiveName;
        string folder;
        string pass;
        BackgroundWorker worker;
        bool isCancel = false;
        #region Compress
        public Processing(OutArchiveFormat format, bool preserveDirectoryRoot, CompressionLevel level, List<string> paths, string archiveName, string pass)
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            this.Shown += Processing_Shown;
            worker.DoWork += Worker_DoCompress;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //DoubleBuffered = true;
            this.Text = "Compressing";
            //thực hiện nén
            compressor = new SevenZipCompressor
            {
                ArchiveFormat = format,
                PreserveDirectoryRoot = true,
                CompressionLevel = level,
                IncludeEmptyDirectories = true,
                DirectoryStructure = true,
                EncryptHeaders = true,
        };
            this.paths = paths;
            this.archiveName = archiveName;
            this.pass = pass;
        }
        private void Worker_DoCompress(object sender, DoWorkEventArgs e)
        {
            compressor.Compressing += ZipCompressor_Compressing;
            compressor.CompressionFinished += ZipCompressor_CompressionFinished;
            compressor.FileCompressionStarted += SevenZipCompressor_FileCompressionStarted;
            foreach (string path in paths)
            {
                compressor.CompressionMode = File.Exists(archiveName) ? SevenZip.CompressionMode.Append : SevenZip.CompressionMode.Create;
                try
                {
                    if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) // là thư mục
                    {
                        compressor.CompressDirectory(path, archiveName, pass);
                    }
                    else
                    {
                        compressor.CompressFilesEncrypted(archiveName,pass,path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (File.Exists(archiveName))
                        File.Delete(archiveName);
                }

            }
        }

        private void SevenZipCompressor_FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            e.Cancel = isCancel;
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Refresh();
                    labelArchiveName.Text = archiveName;
                    labelFile.Text = e.FileName;
                    progressBarTotal.Value = e.PercentDone;
                    labelPercent.Text = e.PercentDone.ToString() + "%";

                });
            }
        }

        private void ZipCompressor_CompressionFinished(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBarTotal.Value = 100;
                    Refresh();
                });
            }
        }

        private void ZipCompressor_Compressing(object sender, SevenZip.ProgressEventArgs e)
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    Refresh();
                    progressBarTotal.Value = e.PercentDone;
                });
            }
        }

        #endregion Compress

        #region Decompress
        public Processing(string filePath, string folder)
        {
            InitializeComponent();
            labelArchiveName.Text = Path.GetFileName(filePath);
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            this.Shown += Processing_Shown;
            worker.DoWork += Worker_DoExtract;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            this.Text = "Extracting";
            this.archiveName = filePath;
            this.folder = folder;
        }
        public Processing(string filePath, string folder, List<int> fileIndex)
        {
            InitializeComponent();
            labelArchiveName.Text = Path.GetFileName(filePath);
            this.fileIndex = fileIndex;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            this.Shown += Processing_Shown;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoExtractFiles;
            this.Text = "Extracting";
            this.archiveName = filePath;
            this.folder = folder;
        }

        private void Worker_DoExtract(object sender, DoWorkEventArgs e)
        {
            try
            {
                SevenZipExtractor.SetLibraryPath("7z.dll");
                var extractor = new SevenZipExtractor(archiveName);
                var files = new ArchiveFileInfo[extractor.ArchiveFileData.Count];
                extractor.ArchiveFileData.CopyTo(files, 0);
                if (Array.Find(files,x => !x.IsDirectory).Encrypted)
                {
                    Password p = new Password
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        extractor = new SevenZipExtractor(archiveName, p.PasswordString);
                        extractor.Extracting += Extractor_Extracting;
                        extractor.FileExtractionStarted += Extractor_FileExtractionStarted;
                        extractor.ExtractionFinished += Extractor_ExtractionFinished;
                        extractor.ExtractArchive(folder);
                    }
                }
                else
                {
                        extractor.Extracting += Extractor_Extracting;
                        extractor.FileExtractionStarted += Extractor_FileExtractionStarted;
                        extractor.ExtractionFinished += Extractor_ExtractionFinished;
                        extractor.ExtractArchive(folder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void Extractor_ExtractionFinished(object sender, EventArgs e)
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    Refresh();
                    labelPercent.Text = "100%";
                    progressBarTotal.Value = 100;
                });
            }
        }
        private void Extractor_FileExtractionStarted(object sender, FileInfoEventArgs e)
        {
            e.Cancel = isCancel;
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    Refresh();
                    labelArchiveName.Text = archiveName;
                    labelFile.Text = Path.GetFileName(e.FileInfo.FileName);
                    progressBarTotal.Value = e.PercentDone;
                    labelPercent.Text = e.PercentDone.ToString() + "%";

                });
            }
        }
        private void Extractor_Extracting(object sender, ProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    Refresh();
                    progressBarTotal.Value = e.PercentDone;
                });
            }
        }
        private void Worker_DoExtractFiles(object sender, DoWorkEventArgs e)
        {
            try
            {
                SevenZipExtractor.SetLibraryPath("7z.dll");
                var extractor = new SevenZipExtractor(archiveName);
                var files = new ArchiveFileInfo[extractor.ArchiveFileData.Count];
                extractor.ArchiveFileData.CopyTo(files, 0);
                if (Array.Find(files, x => !x.IsDirectory).Encrypted || !extractor.Check())
                {
                    Password p = new Password
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        extractor = new SevenZipExtractor(archiveName, p.PasswordString);
                        extractor.Extracting += Extractor_Extracting;
                        extractor.FileExtractionStarted += Extractor_FileExtractionStarted;
                        extractor.ExtractionFinished += Extractor_ExtractionFinished;
                        foreach (int i in fileIndex)
                        {
                            extractor.ExtractFiles(folder, i);
                        }
                    }
                }
                else
                {
                    extractor.Extracting += Extractor_Extracting;
                    extractor.FileExtractionStarted += Extractor_FileExtractionStarted;
                    extractor.ExtractionFinished += Extractor_ExtractionFinished;
                    foreach (int i in fileIndex)
                    {
                        extractor.ExtractFiles(folder, i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion Decompress

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Processing_Shown(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void Processing_Load(object sender, EventArgs e)
        {

        }

        private void Processing_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCancel = true;
            worker.CancelAsync();
        }

        private void progressBarTotal_Click(object sender, EventArgs e)
        {

        }
    }
}
