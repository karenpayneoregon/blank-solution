using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreateBlankSolution.Classes;
using WK.Libraries.BetterFolderBrowserNS;

namespace CreateBlankSolution
{
    public partial class Form1 : Form
    {
        //private string _rootFolder => "C:\\OED\\Dotnetland\\VS2019";
        public Form1()
        {
            InitializeComponent();
            DotnetOperations.Initialize();

            if (!Directory.Exists(DotnetOperations.SolutionRootFolder))
            {
                CreateButton.Enabled = false;
                label1.Text = $"Missing {DotnetOperations.SolutionRootFolder}\nCreate this folder and rerun,";
            }
            else
            {
                label1.Text = DotnetOperations.AboutText;
                toolTip1.SetToolTip(CreateButton, DotnetOperations.SolutionRootFolder);
            }
            
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {

            using (var dialog = new BetterFolderBrowser() { RootFolder = DotnetOperations.SolutionRootFolder, Title = "Solution folder"})
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var ( _ , exception) = DotnetOperations.Create(dialog.SelectedFolder);

                if (exception != null)
                {
                    MessageBox.Show($"Failed to create solution\n{exception.Message}");
                }
                else
                {
                    Process.Start(dialog.SelectedFolder);
                }
            }

            

        }
    }
}
