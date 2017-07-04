using System;
using System.IO;
using System.Net;
using Microsoft.Win32;


namespace SKProCH__Installer_1._
{
    class Program
    {
        public static void Main(string[] args)
        {
            string base_url = "ftp://updater:thisispassword@31.25.29.138/usb1_1/minecraft/DontTouchThisFolder/";
            string save_path = @"C:\Program Files\SKProCH Updater\";
            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata_launcher_path = appdata_path + @"\scproch_updater\";
                       
            Console.CursorVisible = false;

            Console.WriteLine("Программа для автоматического обновления сейчас будет установлена...");
            Console.WriteLine("Создание директорий...");
            Directory.CreateDirectory(save_path);
            Directory.CreateDirectory(appdata_launcher_path);

            WebClient wc = new WebClient();

            Console.WriteLine("Скачивание файлов...");

            string url = base_url + "Updater.exe"; // изменён способ представления переменной удалённого расположения файлов 
            string url1 = base_url + "Dont_Touch_This_EXE.exe"; // изменён способ представления переменной удалённого расположения файлов 
            string url2 = base_url + "DotNetZip.dll"; // изменён способ представления переменной удалённого расположения файлов 

            string name = "Modpack Updater.exe";
            string name1 = "Dont Touch This EXE.exe";
            string name2 = "DotNetZip.dll";

            wc.DownloadFile(url, save_path + name);
            wc.DownloadFile(url1, save_path + name1);
            wc.DownloadFile(url2, save_path + name2);

            Console.WriteLine("Завершено...");

            Console.WriteLine("Создание дополнительных файлов...");

            File.Create(appdata_launcher_path + "M_Version.txt");
            File.Create(appdata_launcher_path + "L_Version.txt");
            File.Create(appdata_launcher_path + "MCPath.txt");

            Console.WriteLine("Завершено...");

            /*
                       Console.ReadKey(true);
                       System.Diagnostics.Process.Start(@"C:\Program Files\SKProCH Updater\MCPath.txt");
                       Console.WriteLine("ВНИМАНИЕ: Введите путь до предустановленной папки клиента Minecraft.\n" +
                       "Вы сможете изменить его позже, путем реадктирования файла MCLink.txt\nВ папке проекта.");
            */

            Console.WriteLine("Помещение в автозагрузку...");

            RegistryKey reg;                                                                                // Этот код отвечает 
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"); // за автозагрузку 
            reg.SetValue(name, save_path + name);                                                           // приложения лаунчера

            Console.WriteLine("Установка завершена. Сейчас вы должны подготовить рабочую папку Minecraft'a.");
            Console.WriteLine("Нажмите любую клавишу и в браузере откроется инструкция...");
            Console.ReadKey(true);
            System.Diagnostics.Process.Start(@"https://cdn.discordapp.com/attachments/236018668889309185/331019888443260928/unknown.png");
            Console.WriteLine(@"Откройте %appdata%\scproch_updater\MCLink.txt");
            Console.WriteLine("И введите туда путь до рабочей папки...");

            Console.Write("Установка завершена. Нажмите любую клавишу для завершения...");

            Console.ReadKey(true);
        }      
    }
}