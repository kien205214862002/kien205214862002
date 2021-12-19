using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using SevenZip;
using Microsoft.VisualBasic.FileIO;
using nUpdate.Updating;
using System.Globalization;
using System.Threading;

namespace BasicExtractExplorer
{
    public partial class MainForm : Form
    {
    
        UpdateManager manager;
        UpdaterUI updater;
        ImageList listView_ImageList = new ImageList();
        ImageList treeView_ImageList = new ImageList();
        ImageList archiveView_ImageList = new ImageList();
        
        PathNode pnode;
        SevenZip.SevenZipExtractor sevenZipExtractor;
        int isCopying; //0: nothing, 1: đang copy, 2: đang cut
        List<string> fileSelectedName; //Danh sách tên các file/folder đang được chọn để copy hoặc cut
        List<string> typeSelectedFile; //Danh sách Loại các item đang được chọn để copy hoặc cut
        string old_selected_node_path; //Đường dẫn cũ tại folder chọn copy hoặc cut, trước khi Paste
        int treeView_ImageIndex = 2;

        List<string> pathBack = new List<string>(); //stack Back
        int Back; //vị trí node trong stack dùng cho back
        bool isBacking;//Đang Back

        List<string> pathForward = new List<string>(); //stack Forward
        int Forward; //vị trí node trong stack dùng cho Forward
        bool isForwarding;//Đang Forwarding

        public MainForm()
        {
            InitializeComponent();
            initialization();
            manager = new UpdateManager(new Uri("http://static.realapps.xyz/BasicExtractExplorer/updates.json"), "<RSAKeyValue><Modulus>8XLionI5Lxw177Z3viTOUvxXoltwWH/pRgw5oAMpMLSl60COlKxZh6XW7vSC0WxmZiqekbVv7v9ubEJCg8UNu6ytecJpIH21eEZvg9TwfRVzc5i6H66KWa2icmR3Z/Ggfsryw1FvFmiVhknTw2gveZjG4hYhlfK1rOP8Md8ZFIhzT1hYkGO5K3UHjEDZ1u9NNg00vqSx7U6tMQl1tjJ/eDIZbSA2gGXveCeYWFNuTuU3L9DeCKSr34/AOiAT42aWn8eVH3VpbbDK6JEm+0Y5IXdTFywrXauP4HPsdYeybQNR5MRpO1Ob5xJqNT1BFAMXh4FCkd11HQqjo0Tz5b8xLs/FAWe16ygCIlKDUwncmMZ22SKsAsLGklfMO9gCkSKx0hggoZGptDA65i9nV82tbdSd6/fIUXrdSwh8aw18HEHJzoRHr/gIqwQWyehgE9zsYQrIKyohpyaw2BZqXZpIS3AqBoHcvKpMXv/bbpt3/Hx3GoKYpFhNCMkzOW6D7HjDhVytKNJ0KELM7Ca7olUSCfDzjHUttzKVXEGupko49FVwuqHXUVnt5z5DeGa2FK09r8RYeX+7m7YOJInW2EFPsXH0y2obFHm0MQ0U3X04q+vXLIvmicN3GkUzBWicwD5f83Jg9EwhzivdNL89C1mJPzRfhxzzfhgZNIpU78xHtDwCCgiOTdtN1OmYdPRaKwdvVT8o22ptvW0fPkXCoulblQBXy13e9+CJFFOaVlVL8LpM9C8o+nlzimRtV0v5CExpcMSoRct72J35jf1dLUO3TpAfQ+e6CpQ2PU2jd1HBDB/zDfRZgRDTzolW4Q8on9LVtXybyshRoyzptmWmGYnY/vWA863VW/jcD+w5OldVk/HiAuQm/HD/QvgTzpJ5LM+9WGBOhBem4B7/yQjyBTkad2Te2VCYaDpyvja5rgsDeia19PHQN7tbBtq6MZdTHbGI8qE54Ll3dPmniTOgAPPIYv5SgiY/My4caw3k+GwySA8U8pvLLdrmownzLgiwmcaO1p4BspWeljSYf+H/pUdYFxpjwSeNnDQ9WCqZ0GbCK5x38Umg0wpV6goS1kBJF8sD7D0UqBAtD6tUr0Rrw2qC3oT/I5N0y9iWSBLa10GjZ4V1sCqhUIEAau8QHvkafkleM+IeTLqziTVaTaxwJICC3YcRQJJpmDqEKci5nN+LtQX2kxZ3boYtizxA78YNy7yeJF/tBPZYlE2NQ6BuCMfqLkdTjaGWZVbPNWd1deBDI6EvbPk/mfHPLNkfc7Oqb3CM5u4tvEntdIZC9JbSqij+2KH440gnM5LjXqSJ60e/Yd42LyMSgcE1chnUr79RZZAM/37fU6GWHOVCtn2PWClDFQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", new CultureInfo("en"));
            updater = new UpdaterUI(manager,null);
          
        }

        private void initialization() //Khởi tạo form
        {
            listViewArchive.Visible = false;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            listView_ImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView_ImageList.ImageSize = new Size(20, 20);
            listView.SmallImageList = listView_ImageList;
            listView.LargeImageList = listView_ImageList;
            //treeView Icons
            treeView_ImageList.ImageSize = new Size(20, 20);
            treeView_ImageList.ColorDepth = ColorDepth.Depth32Bit;
            treeView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 3, true));
            treeView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 15, true));//load thispc icon
            treeView.ImageList = treeView_ImageList;
            //archiveView Icons
            archiveView_ImageList.ColorDepth = ColorDepth.Depth32Bit;
            archiveView_ImageList.ImageSize = new Size(20, 20);
            listViewArchive.SmallImageList = archiveView_ImageList;
            treeViewArchive.ImageList = archiveView_ImageList;
            archiveView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 3, true));//load folder icon
            archiveView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 0, true));//load file icon
            archiveView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 2, true));
            //Đảm bảo treeView trống
            if (treeView != null)
                treeView.Nodes.Clear();
            //Tạo node gốc: My Computer;
            TreeNode ThisPC = new TreeNode("This PC");
            ThisPC.ImageIndex = 1;
            ThisPC.SelectedImageIndex = 1;
            treeView.Nodes.Add(ThisPC);
            //Thêm các node là các ổ drives vào node gốc
            string[] folders = Directory.GetLogicalDrives();
            foreach (string folder in folders)
            {
                treeView_ImageList.Images.Add(IconHelper.GetIcon(folder));
            }
            foreach (string folder in folders)
            {
                TreeNode treeNode = ThisPC.Nodes.Add(folder);
                treeNode.ImageIndex = treeView_ImageIndex++;
                treeNode.SelectedImageIndex = treeNode.ImageIndex;
            }
            ThisPC.Expand();

            isCopying = 0; //không đang copy hay cut
            fileSelectedName = new List<string>();
            typeSelectedFile = new List<string>();
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";

            pathBack.Add(ThisPC.FullPath);
            isBacking = false;
            isForwarding = false;
            Back = 0;
            Forward = -1;
            toolStripButton9.Enabled = false;

            enableButtonInit();
           
        } 

        private void enableButtonInit()
        {
            pasteCtrlVToolStripMenuItem.Enabled = false;
            copyCrltCToolStripMenuItem.Enabled = false;
            cutCtrlXToolStripMenuItem.Enabled = false;
            selectAllCrltAToolStripMenuItem.Enabled = false;
            renameToolStripMenuItem.Enabled = false;
            deleteDelToolStripMenuItem.Enabled = false;
            folderToolStripMenuItem.Enabled = false;
        }

        //Enable buttons
        private void enableButton()
        {
            if (listView.SelectedItems.Count > 0)
            {
                copyCrltCToolStripMenuItem.Enabled = true;
                cutCtrlXToolStripMenuItem.Enabled = true;
                selectAllCrltAToolStripMenuItem.Enabled = true;
                renameToolStripMenuItem.Enabled = true;
                deleteDelToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyCrltCToolStripMenuItem.Enabled = false;
                cutCtrlXToolStripMenuItem.Enabled = false;
                selectAllCrltAToolStripMenuItem.Enabled = false;
                renameToolStripMenuItem.Enabled = false;
                deleteDelToolStripMenuItem.Enabled = false;
            }
        }

        private string GetPath(string treeNodePath) //Lấy đường dẫn từ treeNodePath
        {
            string[] nodes = treeNodePath.Split('\\');
            string result = nodes[1];
            for (int i = 2; i < nodes.Length; i++)
            {
                result += nodes[i] + '\\';
            }
            return result;
        }
        private void CloseArchive()
        {
            listViewArchive.Visible = false;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;
            toolStripButton5.Visible = true;
            toolStripButton6.Visible = true;
            toolStripButton12.Text = "Refresh";
            treeViewArchive.Nodes.Clear();
            listView.Focus();
        }
        private void OpenArchive(string str)
        {
            listViewArchive.Visible = true;
            toolStripButton2.Visible = false;
            toolStripButton3.Visible = false;
            toolStripButton4.Visible = false;
            toolStripButton5.Visible = false;
            toolStripButton6.Visible = false;
            toolStripButton12.Text = "Close";
            ShowArchiveFiles(str);
            listViewArchive.Focus();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (listViewArchive.Visible)
            {
                CloseArchive();
            }
            try
            {
                if (e.Node.Text.CompareTo("This PC") != 0)
                {
                    folderToolStripMenuItem.Enabled = true;
                    //Xóa các node con cũ của node được chọn
                    if (e.Node != null)
                        e.Node.Nodes.Clear();
                    //Lấy danh sách thư mục của thư mục hiện tại
                    string selected_node_path = GetPath(e.Node.FullPath);
                    //Kiểm tra đường dẫn hợp lệ
                    if (Directory.Exists(selected_node_path))
                    {
                        var folders = Directory.GetDirectories(selected_node_path)
                            .Where(d => !new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden));

                        //Lấy tên các thư mục là node con của node được chọn
                        foreach (string folder in folders)
                        {
                            TreeNode treeNode = e.Node.Nodes.Add(Path.GetFileName(folder));
                            treeNode.ImageIndex = 0;
                            treeNode.SelectedImageIndex = 0;
                        }
                        //Hiện các files và folder lên listView
                        ShowFilesAndFolders();
                        //Hiện đường dẫn lên address
                        toolStripComboBox1.Text = GetPath(treeView.SelectedNode.FullPath);
                    }

                    e.Node.Expand();
                }
                else
                {
                    if (treeView.SelectedNode.Text == "This PC") listView.Items.Clear();
                    toolStripComboBox1.Text = "This PC";
                    folderToolStripMenuItem.Enabled = false;
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Not found this folder");
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Access is denied", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            toolStripStatusLabel1.Text = listView.Items.Count.ToString() + " items";
            
            if (isBacking == false && isForwarding == false)
            {
                if (treeView.SelectedNode.Text == "This PC") return;
                pathBack.Add(treeView.SelectedNode.FullPath);
                Back++;
                pathForward.Clear();
                Forward = -1;
                toolStripButton9.Enabled = false;

            }
        }

        private void ShowFilesAndFolders()
        {
            int listViewImageIndex = 1;
            TreeNode node = treeView.SelectedNode;
            // Xóa các item cũ của listView
            if (listView != null)
                listView.Items.Clear();
            if (listView_ImageList != null) listView_ImageList.Images.Clear();

            //listView_ImageList.Images.Add(IconHelper.GetIcon(folder));
            listView_ImageList.Images.Add(IconHelper.Extract(IconHelper.Shell32, 3, true));
            try
            {
                // Lấy danh sách thư mục
                string path = GetPath(node.FullPath);

                var folders = Directory.GetDirectories(path)
                    .Where(d => !new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden));

                // Thêm các thư mục vào listView
                foreach (string folder in folders)
                {
                    DirectoryInfo info = new DirectoryInfo(folder);
                    string[] Field = new string[5];
                    Field[0] = info.Name;
                    Field[1] = "Folder";
                    Field[2] = "...";
                    Field[3] = info.CreationTime.ToString();
                    Field[4] = info.LastWriteTime.ToString();
                    ListViewItem item = new ListViewItem(Field);
                    item.ImageIndex = 0; ;
                    listView.Items.Add(item);
                }

                //Lấy danh sách các tệp tin
                var files = Directory.GetFiles(path)
                    .Where(d => !new FileInfo(d).Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden));
                foreach (string file in files)
                {
                    listView_ImageList.Images.Add(Icon.ExtractAssociatedIcon(file));
                }
                 //Thêm các tệp vào listView
                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);
                    string[] Field = new string[5];
                    Field[0] = info.Name;
                    //string add = "";
                    //if (info.Extension != "") add = info.Extension.ToString().Substring(1);
                    //Field[1] = "File "+add;
                    Field[1] = IconHelper.GetFileTypeDescription(file);
                    Field[2] = (info.Length / 1024).ToString() + " KB";
                    Field[3] = info.CreationTime.ToString();
                    Field[4] = info.LastWriteTime.ToString();
                    ListViewItem item = new ListViewItem(Field);
                    item.ImageIndex = listViewImageIndex++;
                    listView.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        #region các hàm Delete
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text == "This PC") return;
            //Tạo đường dẫn đến node đang được chọn ở treeView
            string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
            int i = 0;
            int dem = listView.SelectedItems.Count;

            while (i<dem)   //Kiểm tra có item nào trong listView được chọn không
            {
                //Ghép đường dẫn đến item đang xét
                string path =selected_node_path + listView.SelectedItems[i].SubItems[0].Text;
                //kiem tra duong dan
                try
                {
                    if (listView.SelectedItems[i].SubItems[1].Text.CompareTo("Folder") == 0)
                    {
                        //Kiểm tra và xóa Folder
                        if (Directory.Exists(path))
                            // Directory.Delete(path, true);
                            FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        listView.Items.Remove(listView.SelectedItems[i]); // Xóa SelectedItems ở đầu
                    }
                    else
                    {
                        //Kiểm tra và xóa File
                        if (File.Exists(path))
                            // File.Delete(path);
                            FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        listView.Items.Remove(listView.SelectedItems[i]); // Xóa SelectedItems ở đầu
                    }
                }
                catch (Exception)
                {
                    i++;
                    MessageBox.Show("Access to "+path+" is denied", "warning");
                }
                dem = listView.SelectedItems.Count;

            }
            listView_Click(sender, e);
        }

        private void deleteDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }
        #endregion

        #region refresh
        private void toolStripButton12_Click(object sender, EventArgs e)
        { 
            treeView_AfterSelect(sender, new TreeViewEventArgs(treeView.SelectedNode));
            listView_Click(sender, e);
        }
        #endregion

        #region View
        private void largeIconToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView.View = View.LargeIcon;
            listView_ImageList.ImageSize = new Size(60, 60);
            toolStripButton12_Click(sender, e);
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            largeIconToolStripMenuItem1_Click(sender, e);
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.SmallIcon;
            listView_ImageList.ImageSize = new Size(20, 20);
            toolStripButton12_Click(sender, e);
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smallIconsToolStripMenuItem_Click(sender, e);
        }

        private void listToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView.View = View.List;
            listView_ImageList.ImageSize = new Size(20, 20);
            toolStripButton12_Click(sender, e);
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listToolStripMenuItem1_Click(sender, e);
        }

        private void detailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView.View = View.Details;
            listView_ImageList.ImageSize = new Size(20, 20);
            toolStripButton12_Click(sender, e);
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detailsToolStripMenuItem1_Click(sender, e);
        }
        #endregion

        #region Các hàm Copy
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(!listViewArchive.Visible) // nếu không đang mở file nén
            {
                if (listView.SelectedItems.Count == 0) return;

                if (isCopying != 2) isCopying = 1; // Xác định xem đang copy hay cut
                old_selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                if (!(Directory.Exists(old_selected_node_path))) //Kiểm tra đường dẫn tồn tại
                {
                    isCopying = 0;
                    return;
                }

                fileSelectedName.Clear();
                typeSelectedFile.Clear();

                for (int i = 0; i < listView.SelectedItems.Count; i++)
                {
                    fileSelectedName.Add(listView.SelectedItems[i].SubItems[0].Text);
                    typeSelectedFile.Add(listView.SelectedItems[i].SubItems[1].Text);

                }

                pasteCtrlVToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (listViewArchive.SelectedItems.Count == 0) return;
                fileSelectedName.Clear();
                typeSelectedFile.Clear();
                List<int> fileIndexes = new List<int>();//Danh sách chỉ số các files được chọn trong file nén
                PathNode current_path_node = FindPathNode(treeViewArchive.SelectedNode.FullPath);
                //Add vào list index của tệp/thư mục cần giải nén
                foreach(ListViewItem item in listViewArchive.SelectedItems)
                {
                    current_path_node.nodes.TryGetValue(item.Text, out PathNode _node);
                    GetFileIndexes(_node, ref fileIndexes);
                }
                if (listView.FocusedItem != null)
                {
                    string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                    //Giải nén files vào thư mục tạm C:\Users\<username>\AppData\Local\Temp\BEE_temp\
                    Processing processing = new Processing(selected_node_path + listView.FocusedItem.Text, Path.GetTempPath() + "BEE_temp\\", fileIndexes);
                    processing.Text = "Copy";
                    processing.StartPosition = FormStartPosition.CenterScreen;
                    processing.FormClosing += delegate {
                        old_selected_node_path = (Path.GetTempPath() + "BEE_temp\\" + Path.GetDirectoryName(sevenZipExtractor.ArchiveFileNames[fileIndexes[0]]) + "\\").Replace("\\\\", "\\");
                        foreach (ListViewItem item in listViewArchive.SelectedItems)
                        {
                            if(Directory.Exists(old_selected_node_path + item.Text) || File.Exists(old_selected_node_path + item.Text))
                            {
                                fileSelectedName.Add(item.Text);
                                typeSelectedFile.Add(item.SubItems[2].Text);
                                isCopying = 1;
                            }
                        }
                        pasteCtrlVToolStripMenuItem.Enabled = true;
                    };
                    processing.Show();
                }
            }

        }
        private void GetFileIndexes(PathNode p, ref List<int> indexes)
        {
            indexes.Add(p.Info.Index);
            foreach(PathNode subnode in p.nodes.Values)
            {
                GetFileIndexes(subnode, ref indexes);
            }
        }
        private void copyCrltCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }
        # endregion

        #region Các hàm Cut
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0) return;
            isCopying = 2;
            toolStripButton1_Click(sender, e);
        }

        private void cutCrltXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }
        #endregion

        #region Các hàm Paste
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (isCopying == 0 || fileSelectedName.Count == 0) return; //Không đang copy hay cut
            string selected_node_path = GetPath(treeView.SelectedNode.FullPath);

            string desPath = selected_node_path; //Địa chỉ đích
            if (desPath == old_selected_node_path && isCopying == 2) //Nếu Cut và Paste tại cùng thư mục thì thoát
            {
                pasteCtrlVToolStripMenuItem.Enabled = false;
                isCopying = 0;
                return;
            }

            for (int i = 0; i < fileSelectedName.Count; i++)
            {
                if (typeSelectedFile[i].CompareTo("Folder") == 0) //Copy Folder
                {
                    int count = 0; //Đếm folder có tên trùng
                    //Kiểm tra xem có Folder nào trùng tên với item đang được Paste không
                    foreach (ListViewItem item in listView.Items)
                        if ((fileSelectedName[i] == item.Text) && (item.SubItems[1].Text.CompareTo("Folder") == 0))
                        {
                            count++;
                            break;
                        }

                    string NewName= fileSelectedName[i];
                    //Nếu có Folder trùng tên, đánh số thứ tự cho tên cũ
                    while (count > 0) 
                    {
                        int stop = 1; //Dừng lặp
                        NewName = fileSelectedName[i] + "(" + count.ToString() + ")";//Tên mới 
                        foreach (ListViewItem item in listView.Items)
                        {
                            if ((NewName == item.Text) && (item.SubItems[1].Text.CompareTo("Folder") == 0))
                            {
                                count++;
                                stop = 0;
                                break;
                            }
                        }
                        if (stop == 1) break;
                    }

                    Directory.CreateDirectory(desPath+NewName); //Tạo Folder copy

                    string old_fileSelectedPath = old_selected_node_path + fileSelectedName[i];
                    string desItem = desPath + NewName;
                    //Tạo các Folder con 
                    foreach (string dirPath in Directory.GetDirectories(old_fileSelectedPath, "*", System.IO.SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(old_fileSelectedPath, desItem));

                    //Tạo các File con
                    foreach (string newPath in Directory.GetFiles(old_fileSelectedPath, "*.*", System.IO.SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(old_fileSelectedPath, desItem), true);

                }
                else //Copy File
                {
                    
                    int count = 0; //Đếm file có tên trùng
                    //Kiểm tra xem có File nào trùng tên với item đang được Paste không
                    foreach (ListViewItem item in listView.Items)
                        if ((fileSelectedName[i] == item.Text) && (item.SubItems[1].Text.CompareTo("Folder") != 0))
                        {
                            count++;
                            break;
                        }

                    string NewName = fileSelectedName[i]; //Tên mới
                    //Nếu có File trùng tên, đánh số thứ tự cho tên cũ
                    while (count > 0)
                    {
                        int stop = 1;
                        NewName = fileSelectedName[i].Substring(0, fileSelectedName[i].LastIndexOf(".")) + "(" + count.ToString() + ")";
                        foreach (ListViewItem item in listView.Items)
                            if((item.SubItems[1].Text.CompareTo("Folder") != 0) && (NewName==item.Text.Substring(0, item.Text.LastIndexOf("."))))
                            {
                                count++;
                                stop = 0;
                                break;
                            }

                        if (stop == 1)
                        {
                            NewName += fileSelectedName[i].Substring(fileSelectedName[i].LastIndexOf("."));
                            break;
                        }
                    }
                    
                    //Copy File tới địa chỉ mới
                    try
                    {
                        Console.WriteLine(old_selected_node_path + fileSelectedName[i]);
                        Console.WriteLine(desPath + NewName);
                        File.Copy(old_selected_node_path + fileSelectedName[i], desPath + NewName);
                    }
                    catch
                    {
                        MessageBox.Show("Access is denied","Warning");
                        return;
                    }
                    
                }
                
            }

            toolStripButton12_Click(sender, e); //Refresh lại listView

            //Nếu đang cut: Xóa items bị cut
            if (isCopying == 2) 
            {
                for (int i = 0; i < fileSelectedName.Count; i++)
                    if (typeSelectedFile[i] == "Folder")
                    {
                        Directory.Delete(old_selected_node_path+fileSelectedName[i], true);
                    }
                    else
                    {
                        File.Delete(old_selected_node_path + fileSelectedName[i]);
                    }
                isCopying = 0;
                pasteCtrlVToolStripMenuItem.Enabled = false;
            }

        }

        private void pasteCrltVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }
        #endregion

        #region Rename
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Lưu lại vị trí item đầu tiên được chọn
            ListViewItem tmp = listView.SelectedItems[0];
            //Bật chế độ chỉnh sửa
            tmp.BeginEdit();
        }

        private void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //Lấy đường dẫn của thư mục hiện tại
            string currentPath = GetPath(treeView.SelectedNode.FullPath);
            //Lấy tên trước chỉnh sửa
            string currentName = listView.SelectedItems[0].Text;
            //Lấy tên sau khi chỉnh sửa
            string newName = e.Label;
            if (newName == null) return;
            if(newName=="")
                MessageBox.Show("You must type a file name.", "Can't rename");

            string extension = ""; //Phần mở rộng trong tên
            //Lấy phần mở rộng nếu là File
            if ((listView.SelectedItems[0].SubItems[1].Text != "Folder") && (listView.SelectedItems[0].Text.LastIndexOf(".") >= 0))
                extension = listView.SelectedItems[0].Text.Substring(listView.SelectedItems[0].Text.LastIndexOf("."));
            //Kiểm tra có tồn tại tên giống newName không
            if (!Directory.Exists(currentPath + newName) && !File.Exists(currentPath + newName))
            {
                bool change = true;
                if (extension != "") //Nếu là File
                {
                    if ((newName.Length < currentName.Length) || (newName.Substring(newName.Length - extension.Length) != extension))
                    {
                        DialogResult result = MessageBox.Show("New Name can change the type of this file!", "Warning", MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.No)
                            change = false;
                    }
                }

                if (change == true)
                {
                    Directory.Move(currentPath + listView.SelectedItems[0].Text, currentPath + newName);
                }
            }
            else
                MessageBox.Show("Tên \"" + newName + "\" đã tồn tại!", "Can't rename", MessageBoxButtons.OK);

            e.CancelEdit = true; //Kết thúc Edit
            //Làm mới lại listView
            toolStripButton12.PerformClick();
        }
        #endregion

        #region Other Buttons

        //Up
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if(!listViewArchive.Visible) //nếu không ở chế độ xem trước file nén
            {
                if (treeView.SelectedNode.Text == "This PC") return;
                treeView.SelectedNode = treeView.SelectedNode.Parent;
                if (treeView.SelectedNode.Text == "This PC")
                    listView.Items.Clear();
                listView_Click(sender, e);
            }
            else
            {
                if (treeViewArchive.SelectedNode == treeViewArchive.Nodes[0])
                    toolStripButton12_Click(sender, e);
                else
                    treeViewArchive.SelectedNode = treeViewArchive.SelectedNode.Parent;

            }

            enableButton();
        }

        //Close App
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Select All items
        private void selectAllCrltAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!listViewArchive.Visible)
            {
                listView.SelectedItems.Clear();
                foreach (ListViewItem item in listView.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                listViewArchive.SelectedItems.Clear();
                foreach(ListViewItem item in listViewArchive.Items)
                {
                    item.Selected = true;
                }
            }

        }

        //Go to Path
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            string path = toolStripComboBox1.Text; //Lấy đường dẫn được nhập vào từ thanh address
            //Thay path= stack[back] nếu đang back
            if (isBacking) 
            {
                if (Back > 0)
                    path = pathBack[Back - 1];
                else
                    path = pathBack[Back];
            }
            if (isForwarding)
            {
                path = pathForward[Forward];
            }

            string[] nodes = path.Split('\\'); //Cắt lấy từng phần folder
            path = "";
            for (int i = 0; i < nodes.Length; i++)
                if (i != 0 || nodes[i] != "This PC")
                    path += nodes[i]+"\\";
            string[] newNodes = path.Split('\\'); //danh sách mới

            if (path == "")
            {
                while (treeView.SelectedNode.Parent != null)
                {
                    treeView.SelectedNode = treeView.SelectedNode.Parent;
                }
                return;
            }

            if (!Directory.Exists(path))
            {
                toolStripButton12_Click(sender, e);
                return;
            }

            //Trở về nốt gốc This PC
            while (treeView.SelectedNode.Parent!=null)
            {
                treeView.SelectedNode = treeView.SelectedNode.Parent;
            }

            for (int i = 0; i < newNodes.Length - 1; i++) //Xét các node từ cao đến thấp
            {
                string add = "";
                if (i == 0) add += "\\";
                foreach (TreeNode node in treeView.SelectedNode.Nodes) //Xét các node con
                    if (node.Text == newNodes[i] + add)
                    {
                        treeView.SelectedNode = node;
                        break;
                    }
            }

        }

        //Back
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            isBacking = true;
            toolStripButton11_Click(sender, e);
            isBacking = false;
            if (Back > 0)
            {
                pathForward.Add(pathBack[pathBack.Count - 1]);
                Forward++;
                toolStripButton9.Enabled = true;
                pathBack.RemoveAt(pathBack.Count - 1);
                Back--;
            }
        }

        //Forward
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            isForwarding = true;
            toolStripButton11_Click(sender, e);
            isForwarding = false;

            pathBack.Add(pathForward[pathForward.Count - 1]);
            Back++;
            pathForward.RemoveAt(pathForward.Count - 1);
            Forward--;

            if (Forward < 0) toolStripButton9.Enabled = false;
        }

        //About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        //New folder
        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text == "This PC") return;
            int count = 0;
            foreach (ListViewItem item in listView.Items)
                if (item.Text == "New folder")
                {
                    count++;
                    break;
                }
            string folderName = toolStripComboBox1.Text + "\\New folder";
            if (count > 0)
            {
                while (Directory.Exists(folderName + "(" + count.ToString() + ")"))
                {
                    count++;
                }
                folderName += "(" + count.ToString() + ")";
            }
            Directory.CreateDirectory(folderName);
            toolStripButton12_Click(sender, e); //refresh

            ListViewItem tmp = null;
            foreach (ListViewItem item in listView.Items)
                if ((count>0 && item.Text == "New folder(" + count.ToString() + ")") || (count==0 && item.Text == "New folder"))
                {
                    tmp = item;
                    break;
                }
            if (tmp != null)
                tmp.BeginEdit();
        }

        private void folderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            folderToolStripMenuItem_Click(sender, e);
        }

        //enter Address Bar
        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toolStripButton11_Click(sender, e);
        }

        //button Add
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                List<string> ls = new List<string>();
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    ls.Add(selected_node_path + item.Text);
                }
                AddToArchive addToArchive = new AddToArchive(ls);
                addToArchive.FormClosing += delegate { toolStripButton12_Click(sender, e); };
                addToArchive.Show();
            }
            else
            {
                MessageBox.Show("Please select a file/folder", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //button Extract
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            string[] archiveExtension = { ".zip", ".rar", ".7z", ".tar", ".xz", ".bz2", ".gz" };
            if (!listViewArchive.Visible)
            {
                if (listView.FocusedItem != null && archiveExtension.Contains(Path.GetExtension(listView.FocusedItem.Text)))
                {
                    string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                    ExtractTo extractTo = new ExtractTo(selected_node_path + listView.FocusedItem.Text);
                    extractTo.FormClosing += delegate { toolStripButton12_Click(sender, e); };
                    extractTo.Show();
                }
                else
                {
                    MessageBox.Show("Please select a file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                List<int> fileIndexes = new List<int>();//Danh sách chỉ số các files được chọn trong file nén
                PathNode current_path_node = FindPathNode(treeViewArchive.SelectedNode.FullPath);
                //Add vào list index của tệp/thư mục cần giải nén
                foreach (ListViewItem item in listViewArchive.SelectedItems)
                {
                    current_path_node.nodes.TryGetValue(item.Text, out PathNode _node);
                    GetFileIndexes(_node, ref fileIndexes);
                }
                if (listView.FocusedItem != null && archiveExtension.Contains(Path.GetExtension(listView.FocusedItem.Text)))
                {
                    string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                    ExtractTo extractTo = new ExtractTo(selected_node_path + listView.FocusedItem.Text, fileIndexes);
                    extractTo.FormClosing += delegate { toolStripButton12_Click(sender, e); };
                    extractTo.Show();
                }
                else
                {
                    MessageBox.Show("Please select a file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void extractToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton7_Click(sender, e);
        }
        #endregion

        #region Right Click Menu
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {

            bool Focusing = false;
            if (treeView.SelectedNode.Text.Equals("This PC")) return;
            if (e.Button == MouseButtons.Right)
            {
                foreach (ListViewItem item in listView.Items)
                {
                    if (item.Bounds.Contains(e.Location))
                    {
                        Focusing = true;
                        break;
                    }
                }

                if (!Focusing)
                {
                    contextMenuStrip1.Items[contextMenuStrip1.Items.IndexOf(viewToolStripMenuItem1)].Enabled = true;
                    contextMenuStrip1.Items[contextMenuStrip1.Items.IndexOf(refreshToolStripMenuItem)].Enabled = true;
                    contextMenuStrip1.Items[contextMenuStrip1.Items.IndexOf(newToolStripMenuItem)].Enabled = true;
                    contextMenuStrip1.Items[contextMenuStrip1.Items.IndexOf(pasteToolStripMenuItem)].Enabled = (isCopying != 0);
                    contextMenuStrip1.Show(Cursor.Position);
                }
                else
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton12_Click(sender, e);
        }

        private void pasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void copyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }

        private void renameToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            renameToolStripMenuItem_Click(sender, e);
        }

        private void addToToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }

        private void largeIconsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            largeIconToolStripMenuItem1_Click(sender, e);
        }

        private void smallIconsToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            smallIconsToolStripMenuItem_Click(sender, e);
        }

        private void listToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            listToolStripMenuItem1_Click(sender, e);
        }

        private void detailsToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            detailsToolStripMenuItem1_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_DoubleClick(sender, e);
        }
        #endregion

        #region Other Events
        //Hiện status bar
        private void listView_Click(object sender, EventArgs e)
        {
            string status1;
            string status2;
            //string status3;
            //status1
            if (listView.Items.Count == 1) status1 = " item";
            else status1 = " items";
            toolStripStatusLabel1.Text = listView.Items.Count.ToString() + status1;
            //status2
            if (listView.SelectedItems.Count > 0)
            {
                if (listView.SelectedItems.Count == 1)
                    status2 = " item selected";
                else status2 = " items selected";
                toolStripStatusLabel2.Text = listView.SelectedItems.Count.ToString() + status2;
            }
            else toolStripStatusLabel2.Text = "";
            //status3
            toolStripStatusLabel3.Text = "";
            if (listView.SelectedItems.Count > 0)
            {
                int size = 0;//Tổng dung lượng các File được chọn
                bool check = false;//Số File trong selecteditems
                foreach (ListViewItem item in listView.SelectedItems)
                    if (item.SubItems[1].Text != "Folder")
                    {
                        size += int.Parse(item.SubItems[2].Text.Substring(0, item.SubItems[2].Text.Length - 3));
                        check = true;
                    }

                if (check == true) toolStripStatusLabel3.Text = size.ToString() + " KB";

            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            listView_Click(sender, e);
            enableButton();
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            string[] archiveExtension = { ".zip", ".rar", ".7z", ".tar", ".xz", ".bz2", ".gz" };
            String tmpNode = listView.SelectedItems[0].SubItems[0].Text;
            string fullpath = treeView.SelectedNode.FullPath;
            string str = GetPath(fullpath);
            str += listView.SelectedItems[0].SubItems[0].Text;
            if (!Directory.Exists(str))
            {
                try
                {
                    if (archiveExtension.Contains(Path.GetExtension(str)))
                    {
                        OpenArchive(str);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(str);
                    }

                }
                catch (Win32Exception)
                {
                    MessageBox.Show("Mở file thất bại, bạn vui lòng xem lại!");
                }
            }
            else
            {
                foreach (TreeNode node in treeView.SelectedNode.Nodes)
                {
                    if (node.Text.Equals(tmpNode)) treeView.SelectedNode = node;
                }
                listView_Click(sender, e);
            }
            listView_Click(sender, e);
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control == true)
                selectAllCrltAToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region CheckSum
        private static string CalculateMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        private void mD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text.Equals("This PC")) return;
            //Copy danh sách các items sang mảng các items để đảm bảo vị trí
            ListViewItem[] items = new ListViewItem[listView.SelectedItems.Count];
            listView.SelectedItems.CopyTo(items, 0);
            //Lưu lại đường dẫn các thư mục (tệp tin) cần copy
            string[] tmpPathssum = new string[items.Length];
            string currentPath = GetPath(treeView.SelectedNode.FullPath);
            for (int j = 0; j < items.Length; j++)
            {
                tmpPathssum[j] = currentPath + items[j].SubItems[0].Text;
                // textBox1.Text += tmpPathsNen[j];
                if (Path.GetExtension(tmpPathssum[j]).CompareTo("") != 0)
                {
                    MessageBox.Show(CalculateMD5(tmpPathssum[j]), tmpPathssum[j] + " ");
                }
            }
        }
        private static string GetSHA256(string fileName)
        {
            // 7z
            using (FileStream stream = File.OpenRead(fileName))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
        private void sHA256ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text.Equals("This PC")) return;
            //Copy danh sách các items sang mảng các items để đảm bảo vị trí
            ListViewItem[] items = new ListViewItem[listView.SelectedItems.Count];
            listView.SelectedItems.CopyTo(items, 0);
            //Lưu lại đường dẫn các thư mục (tệp tin) cần copy
            string[] tmpPathssum = new string[items.Length];
            string currentPath = GetPath(treeView.SelectedNode.FullPath);
            for (int j = 0; j < items.Length; j++)
            {
                tmpPathssum[j] = currentPath + items[j].SubItems[0].Text;
                // textBox1.Text += tmpPathsNen[j];
                if (Path.GetExtension(tmpPathssum[j]).CompareTo("") != 0)
                {
                    MessageBox.Show(GetSHA256(tmpPathssum[j]), tmpPathssum[j] + "  SHA-256 ");
                }
            }
        }
        private static string GetCRC32(string fileName)
        {
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;

            // using (FileStream fs = File.Open("c:\\myfile.txt", FileMode.Open))
            using (FileStream stream = File.OpenRead(fileName))
                foreach (byte b in crc32.ComputeHash(stream)) hash += b.ToString("x2");
            return hash;
        }
        private void cRC32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text.Equals("This PC")) return;
            //Copy danh sách các items sang mảng các items để đảm bảo vị trí
            ListViewItem[] items = new ListViewItem[listView.SelectedItems.Count];
            listView.SelectedItems.CopyTo(items, 0);
            //Lưu lại đường dẫn các thư mục (tệp tin) cần copy
            string[] tmpPathssum = new string[items.Length];
            string currentPath = GetPath(treeView.SelectedNode.FullPath);
            for (int j = 0; j < items.Length; j++)
            {
                tmpPathssum[j] = currentPath + items[j].SubItems[0].Text;
                // textBox1.Text += tmpPathsNen[j];
                if (Path.GetExtension(tmpPathssum[j]).CompareTo("") != 0)
                {
                    MessageBox.Show(GetCRC32(tmpPathssum[j]), tmpPathssum[j] + "  CRC-32 ");
                }
            }
        }
        private static string GetSHA1(string fileName)
        {
            try
            {
                using (var shaHasher = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                using (FileStream stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(shaHasher.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void sHA1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Text.Equals("This PC")) return;
            //Copy danh sách các items sang mảng các items để đảm bảo vị trí
            ListViewItem[] items = new ListViewItem[listView.SelectedItems.Count];
            listView.SelectedItems.CopyTo(items, 0);
            //Lưu lại đường dẫn các thư mục (tệp tin) cần copy
            string[] tmpPathssum = new string[items.Length];
            string currentPath = GetPath(treeView.SelectedNode.FullPath);
            for (int j = 0; j < items.Length; j++)
            {
                tmpPathssum[j] = currentPath + items[j].SubItems[0].Text;
                // textBox1.Text += tmpPathsNen[j];
                if (Path.GetExtension(tmpPathssum[j]).CompareTo("") != 0)
                {
                    MessageBox.Show(GetSHA1(tmpPathssum[j]), tmpPathssum[j] + "  SHA-1 ");
                }
            }
        }

        #endregion

        #region Path
        //https://stackoverflow.com/questions/10870443/how-to-create-hierarchical-structure-with-list-of-path
        public class PathNode
        {
            public readonly IDictionary<string, PathNode> nodes = new Dictionary<string, PathNode>();
            public string Path { get; set; }
            public ArchiveFileInfo Info { get; set; }
            public void AddPath(string path, ArchiveFileInfo info)
            {
                char[] charSeparators = new char[] { '\\' };

                // Parse into a sequence of parts.
                string[] parts = path.Split(charSeparators,
                    StringSplitOptions.RemoveEmptyEntries);

                // The current node.  Start with this.
                PathNode current = this;
                // Iterate through the parts.
                foreach (string part in parts)
                {
                    // The child node.
                    PathNode child;

                    // Does the part exist in the current node?  If
                    // not, then add.
                    if (!current.nodes.TryGetValue(part, out child))
                    {
                        // Add the child.
                        child = new PathNode
                        {
                            Path = part,
                            Info = new ArchiveFileInfo() { IsDirectory = true }
                        };

                        // Add to the dictionary.
                        current.nodes[part] = child;
                    }

                    // Set the current to the child.
                    current = child;
                }
                current.Info = info;
            }
        }

        #endregion Path

        #region tree view archive
        private void listViewArchive_DoubleClick(object sender, EventArgs e)
        {
            string tmpNode = listViewArchive.SelectedItems[0].SubItems[0].Text;
            if(listViewArchive.FocusedItem.SubItems[2].Text == "Folder")
            {
                foreach (TreeNode node in treeViewArchive.SelectedNode.Nodes)
                {
                    if (node.Text.Equals(tmpNode)) treeViewArchive.SelectedNode = node;
                }
            }
            else
            {
                List<int> fileIndex = new List<int>();//Danh sách chỉ số các files được chọn trong file nén
                PathNode current_path_node = FindPathNode(treeViewArchive.SelectedNode.FullPath);
                current_path_node.nodes.TryGetValue(listViewArchive.FocusedItem.Text, out PathNode _node);
                if (_node.nodes.Count == 0)
                    fileIndex.Add(_node.Info.Index);
                if (listView.FocusedItem != null)
                {
                    string selected_node_path = GetPath(treeView.SelectedNode.FullPath);
                    //Xóa file cũ
                    if (File.Exists(Path.GetTempPath() + sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName))
                    {
                        File.SetAttributes(Path.GetTempPath() + sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName,FileAttributes.Normal);
                        File.Delete(Path.GetTempPath() + sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName);
                    }

                    Processing processing = new Processing(selected_node_path + listView.FocusedItem.Text,Path.GetTempPath(),fileIndex);
                    processing.Text = "Opening...";
                    processing.StartPosition = FormStartPosition.CenterScreen;
                    processing.FormClosing += delegate {
                        if(File.Exists(Path.GetTempPath()+sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName))
                        {
                            File.SetAttributes(Path.GetTempPath() + sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName, FileAttributes.ReadOnly);//Chỉ đọc
                            System.Diagnostics.Process.Start(Path.GetTempPath() + sevenZipExtractor.ArchiveFileData[fileIndex[0]].FileName);
                        }
                    };
                    processing.Show();
                }
            }
        }
        private PathNode FindPathNode(string treeNodePath)
        {
            //Loại bỏ phần "My Computer\"
            var parts = treeNodePath.Split('\\');
            PathNode current = pnode;
            for (int i = 1; i < parts.Length; i++)
            {
                current = current.nodes[parts[i]];
            }
            return current;
        }
        private bool IsFile(string treeNodePath)
        {
            PathNode pathNode = FindPathNode(treeNodePath);
            if(!pathNode.Info.IsDirectory)
            {
                return true;
            }
            return false;
        }
        private void treeViewArchive_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                PathNode current_path_node;
                if (e.Node != null)
                    e.Node.Nodes.Clear();
                if (e.Node != treeViewArchive.Nodes[0]) // Nếu không phải node gốc
                {
                    current_path_node = FindPathNode(e.Node.FullPath);
                }
                else
                {
                    current_path_node = pnode;
                }
                foreach (string folder in current_path_node.nodes.Keys)
                {
                    if (!IsFile(e.Node.FullPath + "\\" + folder))
                    {
                        TreeNode treeNode = new TreeNode(folder);
                        treeNode.ImageIndex = 0;
                        treeNode.SelectedImageIndex = 0;
                        e.Node.Nodes.Add(folder);
                    }
                        
                }
                ShowFoldersAndFilesArchive();
                toolStripComboBox1.Text = e.Node.FullPath;
                e.Node.Expand();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        private void ShowFoldersAndFilesArchive()
        {
            if (listViewArchive != null)
                listViewArchive.Items.Clear();
            try
            {
                PathNode current_path_node;
                if (treeViewArchive.SelectedNode != treeViewArchive.Nodes[0]) // Nếu không phải node gốc
                {
                    current_path_node = FindPathNode(treeViewArchive.SelectedNode.FullPath);
                }
                else
                {
                    current_path_node = pnode;
                }
                for (int i = 0; i < current_path_node.nodes.Count; i++)
                {
                    if (!IsFile(treeViewArchive.SelectedNode.FullPath + "\\" + current_path_node.nodes.Keys.ElementAt(i)))
                    {
                        ArchiveFileInfo info = current_path_node.nodes.Values.ElementAt(i).Info;// lấy đường dẫn file/folder hiện tại                                                                //sevenZipExtractor.
                        string[] tmp = new string[9];
                        tmp[0] = current_path_node.nodes.Values.ElementAt(i).Path;
                        tmp[1] = "";
                        tmp[2] = "Folder";
                        tmp[3] = "";
                        tmp[4] = "";
                        tmp[5] = "";
                        tmp[6] = "";
                        tmp[7] = "";
                        tmp[8] = "";
                        ListViewItem item = new ListViewItem(tmp);
                        item.ImageIndex = 0;
                        listViewArchive.Items.Add(item);
                    }
                    //listViewArchive.Items.Add(pnode.nodes.ElementAt(i).Key);
                }
                for (int i = 0; i < current_path_node.nodes.Count; i++)
                {
                    //rootNode.Nodes.Add(pnode.nodes.ElementAt(i).Key);
                    if (IsFile(treeViewArchive.SelectedNode.FullPath + "\\" + current_path_node.nodes.Keys.ElementAt(i)))
                    {
                        ArchiveFileInfo info = current_path_node.nodes.Values.ElementAt(i).Info;// lấy đường dẫn file/folder hiện tại                                                                //sevenZipExtractor.
                        string[] tmp = new string[9];
                        tmp[0] = current_path_node.nodes.Values.ElementAt(i).Path;
                        tmp[1] = info.Size.ToString();
                        tmp[2] = "File";
                        tmp[3] = info.LastWriteTime.ToString();
                        tmp[4] = info.CreationTime.ToString();
                        tmp[5] = info.LastAccessTime.ToString();
                        tmp[6] = info.Attributes.ToString();
                        tmp[7] = info.Crc.ToString("X2");
                        tmp[8] = info.Comment;
                        ListViewItem item = new ListViewItem(tmp);
                        item.ImageIndex = 1;
                        listViewArchive.Items.Add(item);
                    }
                }
                //}


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowArchiveFiles(string archivePath)
        {
            if (listViewArchive != null) listViewArchive.Items.Clear();
            if (treeViewArchive != null) treeViewArchive.Nodes.Clear();
            try
            {
                SevenZip.SevenZipExtractor.SetLibraryPath("7z.dll");
                sevenZipExtractor = new SevenZip.SevenZipExtractor(archivePath);
                #region
                var files = sevenZipExtractor.ArchiveFileNames;
                var filedata = sevenZipExtractor.ArchiveFileData;
                pnode = new PathNode();
                TreeNode rootNode = new TreeNode(Path.GetFileName(archivePath));
                rootNode.ImageIndex = 2;
                rootNode.SelectedImageIndex = 2;
                treeViewArchive.Nodes.Add(rootNode);
                treeViewArchive.SelectedNode = rootNode;
                for (int i = 0; i < files.Count; i++)
                {
                    pnode.AddPath(files[i], filedata[i]);
                }
                for (int i = 0; i < pnode.nodes.Count; i++)
                {
                    if (!IsFile(rootNode.FullPath + "\\" + pnode.nodes.Keys.ElementAt(i)))
                    {
                        TreeNode treeNode = new TreeNode(pnode.nodes.ElementAt(i).Key);
                        treeNode.ImageIndex = 0;
                        treeNode.SelectedImageIndex = 0;
                        rootNode.Nodes.Add(treeNode);
                        ArchiveFileInfo info = pnode.nodes.Values.ElementAt(i).Info;// lấy đường dẫn file/folder hiện tại                                                                //sevenZipExtractor.
                        string[] tmp = new string[9];
                        tmp[0] = pnode.nodes.Values.ElementAt(i).Path;
                        tmp[1] = "";
                        tmp[2] = "Folder";
                        tmp[3] = "";
                        tmp[4] = "";
                        tmp[5] = "";
                        tmp[6] = "";
                        tmp[7] = "";
                        tmp[8] = "";
                        ListViewItem item = new ListViewItem(tmp);
                        item.ImageIndex = 0;
                        listViewArchive.Items.Add(item);
                    }
                }
                for (int i = 0; i < pnode.nodes.Count; i++)
                {
                    if (IsFile(rootNode.FullPath + "\\" + pnode.nodes.Keys.ElementAt(i)))
                    {
                        ArchiveFileInfo info = pnode.nodes.Values.ElementAt(i).Info;// lấy đường dẫn file/folder hiện tại                                                                //sevenZipExtractor.
                        string[] tmp = new string[9];
                        tmp[0] = pnode.nodes.Values.ElementAt(i).Path;
                        tmp[1] = info.Size.ToString();
                        tmp[2] = "File";
                        tmp[3] = info.LastWriteTime.ToString();
                        tmp[4] = info.CreationTime.ToString();
                        tmp[5] = info.LastAccessTime.ToString();
                        tmp[6] = info.Attributes.ToString();
                        tmp[7] = info.Crc.ToString("X2");
                        tmp[8] = info.Comment;
                        ListViewItem item = new ListViewItem(tmp);
                        item.ImageIndex = 1;
                        listViewArchive.Items.Add(item);
                    }
                }
                rootNode.Expand();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        
        
        

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updater.ShowUserInterface();
        }

        private void listViewArchive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
