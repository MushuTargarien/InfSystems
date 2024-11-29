using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;

namespace Monitoring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox2.Text = ShowProcesses();
            InitializeTimer4Procces();
            InitializeTimer();
            Hardware();


        }
        List<Tuple<string, float, float>> processList = new List<Tuple<string, float, float>>();

        private void Hardware()
        {
            richTextBox1.Text += $"���������: \n {CPU()}";
            richTextBox1.Text += $"\n����������� ������: {Ram()} ";
            richTextBox1.Text += $"\n������� ����: {Drive()} ";
            richTextBox1.Text += $"\n����������� ���������: {GPU()} ";
        }


        private PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private Timer timer, timerLog;
        int interval;

        //  private Timer timer4Procces;
        private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");

        private void InitializeTimer()
        {
            // ������� ������ ��� ���������� ���������� � �������� ����������
            timer = new Timer();
            timer.Interval = 1000; // �������� 1 �������
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        private void InitializeTimer4Procces()
        {
            timerLog = new Timer();
            timerLog.Tick += Timer_Tick4Procces;

        }

        private string ShowProcesses()                           // ��������� ��������
        {
            StringBuilder sb = new StringBuilder();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    float cpuUsage = GetCpuUsage(process);
                    float memoryUsage = process.WorkingSet64 / (1024f * 1024f);
                    processList.Add(new Tuple<string, float, float>(process.ProcessName, cpuUsage, memoryUsage));
                    sb.AppendLine($"{process.ProcessName} - CPU: {cpuUsage:F2}% - Memory: {memoryUsage:F2} MB");
                }
                catch (Exception ex)
                {

                    // processList.Add(new Tuple<string, float, float>(process.ProcessName, -1, -1));           // �������� � ������� ��� �������
                    // sb.AppendLine($"{process.ProcessName} - ������: {ex.Message}");
                }
            }

            return sb.ToString();
        }
        private float GetCpuUsage(Process process)                                     // ���������� �������� �������� �� ��������� 
        {
            // �������� ���������� � ����������
            var startTime = process.StartTime;
            var totalProcessorTime = process.TotalProcessorTime;
            var elapsedTime = DateTime.Now - startTime;

            // ��������� �������� ���������� � ���������
            return (float)(totalProcessorTime.TotalMilliseconds / elapsedTime.TotalMilliseconds * 100);
        }

        private void Timer_Tick(object sender, EventArgs e)                                            //  �� ��� ����� ���������� ������ ��� �������
        {
            // �������� ������� �������� �������� ����������
            float currentCpuLoad = Convert.ToSingle(Math.Round(CPUCounter.NextValue(), 2));
            label1.Text = $"������� �������� �� ���������: {currentCpuLoad}%";
            UpdateMemoryUsage();
            UpdateSSD();
        }
        private void Timer_Tick4Procces(object sender, EventArgs e)
        {
            Log();
        }
        private void UpdateSSD()                                              //           ���������� � ����� ��� �������
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                label3.Text = ($" \n {d.Name}{d.VolumeLabel}: \n�������� - {FormatBytes(d.TotalFreeSpace)} \n������ - {FormatBytes(d.TotalSize - d.TotalFreeSpace)} ");
            }
        }

        private void UpdateMemoryUsage()                                         // ���������� � ���������� ������ ��� ��������
        {
            long totalMemory = GetTotalMemory();
            long availableMemory = Convert.ToInt64(ramCounter.NextValue());
            long usedMemory = totalMemory - availableMemory;


            label2.Text = $" ������������� ����������� ������: \n ������������: {FormatBytes(usedMemory)}\n ��������: {FormatBytes(availableMemory)}";
        }

        private long GetTotalMemory()                                        // ��������� ������� ����� ������ � ����������
        {
            // ���������� ����� ����� ���������� ������ � ������
            var memoryStatus = new MEMORYSTATUSEX();
            memoryStatus.dwLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(ref memoryStatus);
            return (long)memoryStatus.ullTotalPhys;
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullExtendedVirtual;
        }
        private string FormatBytes(long bytes)                   // ��� �������������� � ��
        {
            string[] sizes = { "�", "��", "��", "��" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{Math.Round(len, 2)} {sizes[order]}";
        }

        private string CPU()              // ��� � ��
        {
            string info = "";
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");

                foreach (ManagementObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString() ?? "����������";
                    string cores = obj["NumberOfCores"]?.ToString() ?? "����������";
                    string threads = obj["NumberOfLogicalProcessors"]?.ToString() ?? "����������";
                    string clockSpeed = obj["CurrentClockSpeed"]?.ToString() ?? "����������";

                    info += $"������: {name}\n";
                    info += $"���������� ����: {cores}\n";
                    info += $"���������� �������: {threads}\n";
                    info += $"������� ������: {clockSpeed} MHz\n";
                }
            }
            catch (Exception ex)
            {
                info = "������ ��� ��������� ���������� � ����������: " + ex.Message;
            }
            return info;
        }

        private string Ram()           // ��� � ����������
        {
            string InfoRAM = "";
            var memoryStatus = new MEMORYSTATUSEX();
            memoryStatus.dwLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(ref memoryStatus);
            InfoRAM = $" \n ����� ������: {FormatBytes((long)memoryStatus.ullTotalPhys)},  ������������ �����: {FormatBytes(((long)memoryStatus.ullTotalPhys) - Convert.ToInt64(ramCounter.NextValue()))}, ��������� �����: {FormatBytes(Convert.ToInt64(ramCounter.NextValue()))} \n";
            return InfoRAM;

        }


        public string Drive()            // ��� � ������
        {
            string InfoDrive = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                InfoDrive += $" \n {d.Name} - {d.VolumeLabel} - {d.DriveFormat} ����� - {FormatBytes(d.TotalSize)},  �������� - {FormatBytes(d.TotalFreeSpace)}, ������ - {FormatBytes(d.TotalSize - d.TotalFreeSpace)} \n ";

            }
            return InfoDrive;

        }

        public string GPU()            // ��� � ����������� �����������
        {
            string InfoGPU = "";
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (var gpu in searcher.Get())
            {
                InfoGPU += $" \n {gpu["Name"]} {Convert.ToSingle(gpu["AdapterRAM"]) / 1024 / 1024 / 1024} ��  ";

            }
            return InfoGPU;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)                          // ��� ���������� ���������
        {
            StringBuilder sb = new StringBuilder();
            var sortedProcesses = processList;
            switch (comboBox1.SelectedIndex)
            {
                case 0:                                                                                  // �� ��������
                    comboBox1_TextChanged(sender, e);
                    break;

                case 1:
                    comboBox1_TextChanged(sender, e);                                           // �� ������������ ������
                    sortedProcesses = processList.OrderByDescending(p => p.Item3).ToList();
                    foreach (var process in sortedProcesses)
                    {
                        if (process.Item3 >= 0)
                        {
                            sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                        }
                        else
                        {
                            //      sb.AppendLine($"{process.Item1}  - ������: �� ������� �������� ������");
                        }
                    }

                    richTextBox2.Text = sb.ToString();
                    break;
                case 2:                                                                                  //�� �������� �� ���������
                    sortedProcesses = processList.OrderByDescending(p => p.Item2).ToList();
                    foreach (var process in sortedProcesses)
                    {
                        if (process.Item3 >= 0)
                        {
                            sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                        }
                        else
                        {
                            //      sb.AppendLine($"{process.Item1}  - ������: �� ������� �������� ������");
                        }
                    }

                    richTextBox2.Text = sb.ToString();

                    break;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)                                                       // ��� ������ ��������� �� ��������
        {
            StringBuilder sb = new StringBuilder();
            var sortedProcesses = processList;
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                sortedProcesses = sortedProcesses.Where(p => p.Item1.IndexOf(comboBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();          //���� ��������� ��������� �������

            }

            foreach (var process in sortedProcesses)
            {
                if (process.Item3 >= 0)
                {
                    sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                }
                else
                {
                    //    sb.AppendLine($"{process.Item1} (ID: {process.Item2}) - ������: �� ������� �������� ������");
                }
            }

            richTextBox2.Text = sb.ToString();
        }

        private void Log()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine(DateTime.Now);
                    if (checkBox1.Checked)
                    {
                        float currentCpuLoad = Convert.ToSingle(Math.Round(CPUCounter.NextValue(), 2));
                        sw.WriteLine($"������� �������� �� ���������: {currentCpuLoad}%");
                    }

                    if (checkBox2.Checked)
                    {
                        sw.WriteLine(Ram());
                    }

                    if (checkBox3.Checked)
                    {
                        sw.WriteLine(Drive());
                    }

                    if (checkBox5.Checked)
                    {
                        sw.WriteLine(ShowProcesses());
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", "log.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                if (int.TryParse(textBox1.Text, out int number) && int.Parse(textBox1.Text) > 0)
                {
                    checkBox4.Text = "�����������: ���";
                    timerLog.Interval = number * 1000;
                    timerLog.Start();
                }
                else
                {
                    MessageBox.Show("�������� ������ �������, ��������� ������");
                    checkBox4.Checked = false;
                    textBox1.Clear();
                }

            }
            else
            {
                checkBox4.Text = "�����������: ����";
                timerLog.Stop();
            }
        }
      
        private void textBox1_Enter(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
        }
    }
}
