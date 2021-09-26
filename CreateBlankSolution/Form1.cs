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
        private string _rootFolder => "C:\\OED\\Dotnetland\\VS2019";
        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(_rootFolder))
            {
                CreateButton.Enabled = false;
                label1.Text = $"Missing {_rootFolder}\nCreate this folder and rerun,";
            }
            else
            {
                SetLabelText();
            }

            
        }

        private void SetLabelText()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Provides the ability to create a new Visual Studio solution using dotnet command.");
            builder.AppendLine("");
            builder.AppendLine("If a solution file already exist it's overwritten");
            builder.AppendLine("");
            builder.AppendLine($"Root path: {_rootFolder}");
            builder.AppendLine("");
            builder.AppendLine("By Karen Payne");

            label1.Text = builder.ToString();
        }


        private void CreateButton_Click(object sender, EventArgs e)
        {

            using (var dialog = new BetterFolderBrowser() { RootFolder = _rootFolder, Title = "Solution folder"})
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
