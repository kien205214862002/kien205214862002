using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace BasicExtractExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if(args.Length == 0)
                    Application.Run(new MainForm());
                else
                {
                    if (args[0] == "compress")
                    {
                        List<string> ls = new List<string>();
                        for (int i = 1; i < args.Length; i++)
                        {
                            ls.Add(args[i]);
                        }

                        AddToArchive addToArchive = new AddToArchive(ls);
                        Application.Run(addToArchive);
                    }
                    else if (args.Length == 2 && args[0] == "extract")
                    {
                        ExtractTo extractTo = new ExtractTo(args[1]);
                        Application.Run(extractTo);
                    }
                    else if (args.Length > 2 && args[0] == "extract")
                    {
                        List<int> ls = new List<int>();
                        for (int i = 2; i < args.Length; i++)
                        {
                            ls.Add(int.Parse(args[i]));
                        }
                        ExtractTo extractTo = new ExtractTo(args[1],ls);
                        Application.Run(extractTo);
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
