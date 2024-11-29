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
            richTextBox1.Text += $"Процессор: \n {CPU()}";
            richTextBox1.Text += $"\nОперативная память: {Ram()} ";
            richTextBox1.Text += $"\nЖесткий диск: {Drive()} ";
            richTextBox1.Text += $"\nГрафический процессор: {GPU()} ";
        }


        private PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private Timer timer, timerLog;
        int interval;

        //  private Timer timer4Procces;
        private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");

        private void InitializeTimer()
        {
            // Создаем таймер для обновления информации о загрузке процессора
            timer = new Timer();
            timer.Interval = 1000; // Интервал 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        private void InitializeTimer4Procces()
        {
            timerLog = new Timer();
            timerLog.Tick += Timer_Tick4Procces;

        }

        private string ShowProcesses()                           // активнные процессы
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

                    // processList.Add(new Tuple<string, float, float>(process.ProcessName, -1, -1));           // Процессы к которым нет доступа
                    // sb.AppendLine($"{process.ProcessName} - Ошибка: {ex.Message}");
                }
            }

            return sb.ToString();
        }
        private float GetCpuUsage(Process process)                                     // вычисление нагрузки процесса на процессор 
        {
            // Получаем информацию о процессоре
            var startTime = process.StartTime;
            var totalProcessorTime = process.TotalProcessorTime;
            var elapsedTime = DateTime.Now - startTime;

            // Вычисляем загрузку процессора в процентах
            return (float)(totalProcessorTime.TotalMilliseconds / elapsedTime.TotalMilliseconds * 100);
        }

        private void Timer_Tick(object sender, EventArgs e)                                            //  то что будет происходит каждый тик таймера
        {
            // Получаем текущее значение загрузки процессора
            float currentCpuLoad = Convert.ToSingle(Math.Round(CPUCounter.NextValue(), 2));
            label1.Text = $"Текущая нагрузка на процессор: {currentCpuLoad}%";
            UpdateMemoryUsage();
            UpdateSSD();
        }
        private void Timer_Tick4Procces(object sender, EventArgs e)
        {
            Log();
        }
        private void UpdateSSD()                                              //           информация о диске для таймера
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                label3.Text = ($" \n {d.Name}{d.VolumeLabel}: \nсвободно - {FormatBytes(d.TotalFreeSpace)} \nзанято - {FormatBytes(d.TotalSize - d.TotalFreeSpace)} ");
            }
        }

        private void UpdateMemoryUsage()                                         // Информация о опертивной памяти для таймераа
        {
            long totalMemory = GetTotalMemory();
            long availableMemory = Convert.ToInt64(ramCounter.NextValue());
            long usedMemory = totalMemory - availableMemory;


            label2.Text = $" Использование оперативной памяти: \n Использовано: {FormatBytes(usedMemory)}\n Свободно: {FormatBytes(availableMemory)}";
        }

        private long GetTotalMemory()                                        // находится сколько всего памяти в оперативке
        {
            // Возвращает общий объем физической памяти в байтах
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
        private string FormatBytes(long bytes)                   // для преобразования в ГБ
        {
            string[] sizes = { "Б", "КБ", "МБ", "ГБ" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{Math.Round(len, 2)} {sizes[order]}";
        }

        private string CPU()              // инф о ЦП
        {
            string info = "";
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");

                foreach (ManagementObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString() ?? "Неизвестно";
                    string cores = obj["NumberOfCores"]?.ToString() ?? "Неизвестно";
                    string threads = obj["NumberOfLogicalProcessors"]?.ToString() ?? "Неизвестно";
                    string clockSpeed = obj["CurrentClockSpeed"]?.ToString() ?? "Неизвестно";

                    info += $"Модель: {name}\n";
                    info += $"Количество ядер: {cores}\n";
                    info += $"Количество потоков: {threads}\n";
                    info += $"Частота работы: {clockSpeed} MHz\n";
                }
            }
            catch (Exception ex)
            {
                info = "Ошибка при получении информации о процессоре: " + ex.Message;
            }
            return info;
        }

        private string Ram()           // инф о оперативке
        {
            string InfoRAM = "";
            var memoryStatus = new MEMORYSTATUSEX();
            memoryStatus.dwLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(ref memoryStatus);
            InfoRAM = $" \n Общая память: {FormatBytes((long)memoryStatus.ullTotalPhys)},  используемый объём: {FormatBytes(((long)memoryStatus.ullTotalPhys) - Convert.ToInt64(ramCounter.NextValue()))}, свободный объём: {FormatBytes(Convert.ToInt64(ramCounter.NextValue()))} \n";
            return InfoRAM;

        }


        public string Drive()            // инф о дисках
        {
            string InfoDrive = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                InfoDrive += $" \n {d.Name} - {d.VolumeLabel} - {d.DriveFormat} Всего - {FormatBytes(d.TotalSize)},  свободно - {FormatBytes(d.TotalFreeSpace)}, занято - {FormatBytes(d.TotalSize - d.TotalFreeSpace)} \n ";

            }
            return InfoDrive;

        }

        public string GPU()            // инф о графических процессорах
        {
            string InfoGPU = "";
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (var gpu in searcher.Get())
            {
                InfoGPU += $" \n {gpu["Name"]} {Convert.ToSingle(gpu["AdapterRAM"]) / 1024 / 1024 / 1024} ГБ  ";

            }
            return InfoGPU;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)                          // для фильтрации процессов
        {
            StringBuilder sb = new StringBuilder();
            var sortedProcesses = processList;
            switch (comboBox1.SelectedIndex)
            {
                case 0:                                                                                  // по названию
                    comboBox1_TextChanged(sender, e);
                    break;

                case 1:
                    comboBox1_TextChanged(sender, e);                                           // по потребляемой памяти
                    sortedProcesses = processList.OrderByDescending(p => p.Item3).ToList();
                    foreach (var process in sortedProcesses)
                    {
                        if (process.Item3 >= 0)
                        {
                            sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                        }
                        else
                        {
                            //      sb.AppendLine($"{process.Item1}  - Ошибка: Не удалось получить данные");
                        }
                    }

                    richTextBox2.Text = sb.ToString();
                    break;
                case 2:                                                                                  //по нагрузке на процессор
                    sortedProcesses = processList.OrderByDescending(p => p.Item2).ToList();
                    foreach (var process in sortedProcesses)
                    {
                        if (process.Item3 >= 0)
                        {
                            sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                        }
                        else
                        {
                            //      sb.AppendLine($"{process.Item1}  - Ошибка: Не удалось получить данные");
                        }
                    }

                    richTextBox2.Text = sb.ToString();

                    break;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)                                                       // для поиска процессов по названию
        {
            StringBuilder sb = new StringBuilder();
            var sortedProcesses = processList;
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                sortedProcesses = sortedProcesses.Where(p => p.Item1.IndexOf(comboBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();          //ищет подстроку игнорируя регистр

            }

            foreach (var process in sortedProcesses)
            {
                if (process.Item3 >= 0)
                {
                    sb.AppendLine($"{process.Item1} - CPU: {process.Item2:F2}% - Memory: {process.Item3:F2} MB");
                }
                else
                {
                    //    sb.AppendLine($"{process.Item1} (ID: {process.Item2}) - Ошибка: Не удалось получить данные");
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
                        sw.WriteLine($"Текущая нагрузка на процессор: {currentCpuLoad}%");
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
                    checkBox4.Text = "Логирование: ВКЛ";
                    timerLog.Interval = number * 1000;
                    timerLog.Start();
                }
                else
                {
                    MessageBox.Show("Интервал указан неверно, проверьте данные");
                    checkBox4.Checked = false;
                    textBox1.Clear();
                }

            }
            else
            {
                checkBox4.Text = "Логирование: ВЫКЛ";
                timerLog.Stop();
            }
        }
      
        private void textBox1_Enter(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
        }
    }
}
