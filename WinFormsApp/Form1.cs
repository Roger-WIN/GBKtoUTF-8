using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string Directory; // 转换文件夹路径
        private List<string> Files; // 转换文件路径
        private string OriginalOutput;
        private string Output; // 输出文件夹路径

        private bool SameOutputOverChecked;

        public Form1()
        {
            InitializeComponent();
        }

        public void ShowFiles(List<string> files, List<string> binaryFileNames)
        {
            if (binaryFileNames != null)
            {
                var message = string.Empty;

                for (var i = 0; i < binaryFileNames.Count; i++)
                {
                    message += "「" + binaryFileNames[i] + "」";
                    if (i >= binaryFileNames.Count - 1)
                        continue;
                    message += "、";
                }

                message += "不是文本文件，不可转换。";
                MessageBox.Show(message);
            }

            if (files == null)
                return; // 未选取文件时，直接结束

            textBox_file.Clear();
            var count = files.Count;

            if (count == 1)
                textBox_file.Text = files[0];
            else
                for (var i = 0; i < count; i++)
                {
                    textBox_file.Text += "\"" + files[i] + "\"";
                    if (i >= count - 1)
                        continue;
                    textBox_file.Text += " ";
                }

            // TODO
            textBox_directory.Clear();
        }

        public void ShowDirectory(string directory, List<string> files, List<string> binaryFileNames)
        {
            if (binaryFileNames != null)
            {
                var message = string.Empty;

                for (var i = 0; i < binaryFileNames.Count; i++)
                {
                    message += "「" + binaryFileNames[i] + "」";
                    if (i >= binaryFileNames.Count - 1)
                        continue;
                    message += "、";
                }

                message += "不是文本文件，不可转换。";
                MessageBox.Show(message);
            }

            if (directory == null)
                return; // 未选取目录时，直接结束
            if (files == null)
                return;

            textBox_directory.Clear();
            textBox_directory.Text = directory;

            // TODO
            textBox_file.Clear();
        }

        public void ShowOutput(string output)
        {
            if (output == null && !SameOutputOverChecked)
                return; // 未选取输出目录时，直接结束

            textBox_output.Clear();
            textBox_output.Text = output;
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            var filesChoose = new Choose();
            var files = filesChoose.ChooseFiles();

            try
            {
                Files = new List<string>(files);
            }
            catch (ArgumentNullException)
            {
                Files = null;
            }

            var filesCheck = new Check();
            var binaryFileNames = filesCheck.FilterTextFile(ref Files);

            ShowFiles(Files, binaryFileNames);
        }

        private void button_directory_Click(object sender, EventArgs e)
        {
            var directoryChoose = new Choose();
            var (directory, files) = directoryChoose.ChooseDirectory();

            try
            {
                Directory = directory;
                Files = new List<string>(files);
            }
            catch (ArgumentNullException)
            {
                Files = null;
            }

            var filesCheck = new Check();
            var binaryFileNames = filesCheck.FilterTextFile(ref Files);

            ShowDirectory(directory, Files, binaryFileNames);
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            var outputChoose = new Choose();

            try
            {
                Output = outputChoose.ChooseOutput();
            }
            catch (ArgumentNullException)
            {
            }

            ShowOutput(Output);
        }

        private void button_convert_Click(object sender, EventArgs e)
        {
            var transcode = new Transcode();
            transcode.ToAim(Files, Output, checkBox_BOM.Checked, checkBox_override.Checked);
            if (checkBox_openOutput.Checked)
                if (Output != null)
                    Process.Start("Explorer.exe", Output);
        }

        private void button_openDirectory_Click(object sender, EventArgs e)
        {
            if (Directory != null) Process.Start("Explorer.exe", Directory);
        }

        private void button_openOutput_Click(object sender, EventArgs e)
        {
            if (Output != null) Process.Start("Explorer.exe", Output);
        }

        private void checkBox_sameOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_sameOutput.Checked) checkBox_override.Checked = false;

            if (Files != null || Directory != null)
            {
                if (checkBox_sameOutput.Checked)
                {
                    OriginalOutput = Output;
                    Output = Files != null ? Path.GetDirectoryName(Files[0]) : Directory;
                    ShowOutput(Output);

                    SameOutputOverChecked = true;
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
            if (checkBox_override.Checked) checkBox_sameOutput.Checked = true;
        }
    }
}