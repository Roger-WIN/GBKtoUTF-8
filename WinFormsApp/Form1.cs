using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string[] Files; // 转换文件路径
        private string Directory; // 转换文件夹路径
        private string Output; // 输出文件夹路径
        private string OriginalOutput;
        private bool SameOutputEverChecked;

        private static TranscodeService service = new TranscodeService();

        public Form1()
        {
            InitializeComponent();
        }

        public void ShowFiles(string[] files)
        {
            if (files == null) // 未选取文件时，结束
            {
                Files = null;
                textBox_file.Clear();
                return;
            }

            textBox_file.Clear();
            var count = files.Length;

            // 下面在文本框中显示选取的若干文件的文件名
            if (count == 1) // 若只有一个文件
            {
                textBox_file.Text = files[0]; // 直接显示文件名
            }

            else // 若不止一个文件
            {
                for (var i = 0; i < count; i++)
                {
                    textBox_file.Text += "\"" + files[i] + "\""; // 在文件名两边加上半角引号
                    if (i >= count - 1)
                        continue;
                    textBox_file.Text += " "; // 使用空格分隔各文件名
                }
            }

            ShowDirectory(null); // 转换文件和转换文件夹不可同时选择
        }

        public void ShowDirectory(string directory)
        {
            if (directory == null) // 未选取目录时，直接结束
            {
                Directory = null;
                textBox_directory.Clear();
                return;
            }

            textBox_directory.Clear();
            textBox_directory.Text = directory;

            ShowFiles(null); // 转换文件和转换文件夹不可同时选择
        }

        public void ShowOutput(string output)
        {
            if (output == null && !SameOutputEverChecked) // 未选取输出目录时，直接结束
            {
                Output = null;
                textBox_output.Clear();
                return;
            }

            textBox_output.Clear();
            textBox_output.Text = output;
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true, // 允许选择多个文件
                Title = "选择待转换文件",
                Filter = "所有文件(*.*)|*.*"
            }; // 打开文件对话框
            Files = (fileDialog.ShowDialog() == DialogResult.OK) ? fileDialog.FileNames : null;
            ShowFiles(Files);
            if (checkBox_sameOutput.Checked) // 选择了输出到相同文件夹
            {
                Output = Path.GetDirectoryName(Files[0]);
                ShowOutput(Output);
            }
        }

        private void button_directory_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择待转换文件夹",
                ShowNewFolderButton = false // 不允许在该对话框中新建文件夹
            }; // 打开文件夹对话框
            Directory = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : null;
            ShowDirectory(Directory);
            if (checkBox_sameOutput.Checked) // 选择了输出到相同文件夹
            {
                Output = Directory;
                ShowOutput(Output);
            }
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            OriginalOutput = Output;
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择输出文件夹"
            }; // 打开文件夹对话框
            Output = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : null;
            ShowOutput(Output);
            if (OriginalOutput != Output) // 更改了输出文件夹
            {
                checkBox_sameOutput.Checked = false;
            }
        }

        private void button_convert_Click(object sender, EventArgs e)
        {
            try
            {
                if (Files != null) // 已选取转换文件
                {
                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFiles(Files), checkBox_BOM.Checked, checkBox_override.Checked), Output);
                }
                else if (Directory != null) // 已选取转换文件夹
                {
                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFolder(Directory, checkBox_recur.Checked), checkBox_BOM.Checked, checkBox_override.Checked), Output);
                }
                else // 二者均未选择
                {
                    throw new ArgumentNullException("转换文件和转换文件夹均未选择");
                }

                MessageBox.Show("转换成功！");
                if (checkBox_openOutput.Checked && Output != null)
                {
                    Process.Start("Explorer.exe", Output);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_openDirectory_Click(object sender, EventArgs e)
        {
            if (Directory != null)
            {
                Process.Start("Explorer.exe", Directory);
            }
        }

        private void button_openOutput_Click(object sender, EventArgs e)
        {
            if (Output != null)
            {
                Process.Start("Explorer.exe", Output);
            }
        }

        // TODO: 修复此复选框和输出文件夹在多种情况下可能存在的显示错误的问题
        private void checkBox_sameOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_sameOutput.Checked) // 取消勾选
            {
                checkBox_override.Checked = false;
            }

            if (Files != null || Directory != null)
            {
                if (checkBox_sameOutput.Checked)
                {
                    OriginalOutput = Output;
                    Output = (Files != null) ? Path.GetDirectoryName(Files[0]) : Directory;
                    ShowOutput(Output);

                    SameOutputEverChecked = true;
                }
                else
                {
                    Output = OriginalOutput;
                    ShowOutput(Output);
                }
            }
        }

        private void checkBox_override_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_override.Checked)
            {
                checkBox_sameOutput.Checked = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            service.ClearFiles();
        }
    }
}