using System.Diagnostics;
using System.Text;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string[] Files; // 转换文件路径
        private string Directory; // 转换文件夹路径
        private string Output; // 输出文件夹路径
        private string OriginalOutput;
        private bool SameOutputEverChecked;
        private readonly string EmptyStr = string.Empty;
        private readonly string[] EmptyStrArr = Array.Empty<string>();

        private static TranscodeService service = new TranscodeService();

        public Form1()
        {
            Files = EmptyStrArr;
            Directory = EmptyStr;
            Output = EmptyStr;
            OriginalOutput = EmptyStr;

            InitializeComponent();
        }

        public void ShowFiles(string[] files)
        {
            if (IsArrayBlank(files)) // 未选取文件时，结束
            {
                Files = EmptyStrArr;
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
                    var text = new StringBuilder(textBox_file.Text);
                    text.Append('\"').Append(files[i]).Append('\"'); // 在文件名两边加上半角引号
                    if (i < count - 1)
                    {
                        text.Append(' '); // 使用空格分隔各文件名
                    }
                    textBox_file.Text = text.ToString();
                }
            }

            ShowDirectory(EmptyStr); // 转换文件和转换文件夹不可同时选择
        }

        public void ShowDirectory(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) // 未选取目录时，直接结束
            {
                Directory = EmptyStr;
                textBox_directory.Clear();
                return;
            }

            textBox_directory.Clear();
            textBox_directory.Text = directory;

            ShowFiles(EmptyStrArr); // 转换文件和转换文件夹不可同时选择
        }

        public void ShowOutput(string output)
        {
            if (string.IsNullOrWhiteSpace(output) && !SameOutputEverChecked) // 未选取输出目录时，直接结束
            {
                Output = EmptyStr;
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
            Files = (fileDialog.ShowDialog() == DialogResult.OK) ? fileDialog.FileNames : EmptyStrArr;
            ShowFiles(Files);
            if (checkBox_sameOutput.Checked && IsArrayNotBlank(Files)) // 选择了输出到相同文件夹
            {
                Output = DirectoryName(Files[0]);
                ShowOutput(Output);
            }
        }

        private void button_chooseDirectory_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择待转换文件夹",
                ShowNewFolderButton = false // 不允许在该对话框中新建文件夹
            }; // 打开文件夹对话框
            Directory = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : EmptyStr;
            ShowDirectory(Directory);
            if (checkBox_sameOutput.Checked) // 选择了输出到相同文件夹
            {
                Output = Directory;
                ShowOutput(Output);
            }
        }

        private void button_chooseOutput_Click(object sender, EventArgs e)
        {
            OriginalOutput = Output;
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择输出文件夹"
            }; // 打开文件夹对话框
            Output = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : EmptyStr;
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
                const string warn = "您选择的输出文件夹与输入目录相同，这可能导致源文件被错误覆盖！是否继续？", title = "目录相同警告";
                var buttons = MessageBoxButtons.OKCancel;
                var icon = MessageBoxIcon.Warning;
                var defaultBtn = MessageBoxDefaultButton.Button2;

                if (IsArrayNotBlank(Files)) // 已选取转换文件
                {
                    // 不覆盖，也不添加文件名后缀
                    if (!checkBox_override.Checked && !checkBox_fileSuffix.Checked)
                    {
                        string?[] inputs = Files.Select(file => Path.GetDirectoryName(file)).Distinct().ToArray();
                        if (IsArrayNotBlank(inputs) && inputs.Contains(Output) && MessageBox.Show(warn, title, buttons, icon, defaultBtn) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFiles(Files), checkBox_bom.Checked, checkBox_fileSuffix.Checked), Output);
                }
                else if (!string.IsNullOrWhiteSpace(Directory)) // 已选取转换文件夹
                {
                    // 不覆盖，也不添加文件名后缀
                    if (!checkBox_override.Checked && !checkBox_fileSuffix.Checked && string.Equals(Directory, Output) && MessageBox.Show(warn, title, buttons, icon, defaultBtn) != DialogResult.OK)
                    {
                        return;
                    }

                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFolder(Directory, checkBox_recur.Checked), checkBox_bom.Checked, checkBox_fileSuffix.Checked), Output);
                }
                else // 二者均未选择
                {
                    throw new ArgumentNullException("转换文件和转换文件夹均未选择");
                }

                MessageBox.Show("转换成功！");
                if (checkBox_openOutput.Checked)
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
            if (!string.IsNullOrWhiteSpace(Directory))
            {
                Process.Start("Explorer.exe", Directory);
            }
        }

        private void button_openOutput_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Output))
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

            if (IsArrayNotBlank(Files) || !string.IsNullOrWhiteSpace(Directory))
            {
                if (checkBox_sameOutput.Checked)
                {
                    OriginalOutput = Output;
                    Output = IsArrayNotBlank(Files) ? DirectoryName(Files[0]) : Directory;
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
            service.ClearTempFiles();
        }

        private bool IsArrayNotBlank<T>(T[] arr)
        {
            return arr != null && arr.Any() && arr.All(ele =>
            {
                if (ele is Array)
                {
                    return IsArrayNotBlank(ele as T[]);
                }
                else if (ele is string)
                {
                    return !string.IsNullOrWhiteSpace(ele as string);
                }
                else
                {
                    return ele != null;
                }
            });
        }

        private bool IsArrayBlank<T>(T[] arr)
        {
            return !IsArrayNotBlank(arr);
        }

        private string DirectoryName(string file)
        {
            string? dir = Path.GetDirectoryName(file);
            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = EmptyStr;
            }
            return dir;
        }
    }
}