using System;
using System.IO;
using System.Net;
using Microsoft.Win32;
using IWshRuntimeLibrary;

namespace SKProCH__Installer_1._
{
    class Program
    {
        public static void Main(string[] args)
        {
            string base_url = "ftp://updater:thisispassword@31.25.29.138/usb1_1/minecraft/DontTouchThisFolder/";
            string save_path = @"C:\Program Files\SKProCH Lab\MC Updater\";
            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata_launcher_path = appdata_path + @"\SKProCH Lab\MC Updater\";
                       
            Console.CursorVisible = false;

            Console.WriteLine("Программа для автоматического обновления сейчас будет установлена...");
            Console.WriteLine("Создание директорий...");
            Directory.CreateDirectory(save_path);
            Directory.CreateDirectory(appdata_launcher_path);
            Directory.CreateDirectory(appdata_launcher_path + "Temp");
            Directory.CreateDirectory(appdata_launcher_path + "Needed Mods");

            WebClient wc = new WebClient();

            Console.WriteLine("Скачивание файлов...");

            string url = base_url + "Updater.exe"; // изменён способ представления переменной удалённого расположения файлов 
            string url1 = base_url + "Dont_Touch_This_EXE.exe"; // изменён способ представления переменной удалённого расположения файлов 
            string url2 = base_url + "DotNetZip.dll"; // изменён способ представления переменной удалённого расположения файлов 
            string url3 = base_url + "L_Version.txt";

            string name = "Modpack Updater.exe";
            string name1 = "Dont Touch This EXE.exe";
            string name2 = "DotNetZip.dll";

            wc.DownloadFile(url, save_path + name);
            wc.DownloadFile(url1, save_path + name1);
            wc.DownloadFile(url2, save_path + name2);
            wc.DownloadFile(base_url + "ForgeVersion.txt", appdata_launcher_path + @"Temp\ForgeVersion.txt");
            wc.DownloadFile(url3, appdata_launcher_path + "L_Version.txt");
            wc.DownloadFile(base_url + "serverlogo1.ico", save_path + "Icon.ico");

            Console.WriteLine("Завершено...");

            Console.WriteLine("Создание дополнительных файлов...");

            System.IO.File.Create(appdata_launcher_path + "M_Version.txt");            
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

            Console.WriteLine("Завершено...\nСоздание ярлыков...");

            WshShell shell = new WshShell();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MC Updater.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.Description = "Ярлык для автоматического обновления клиента!";
            shortcut.TargetPath = save_path + @"Modpack Updater.exe";
            shortcut.IconLocation = save_path + "Icon.ico";
            shortcut.Save();

            Console.WriteLine("Завершено...");
            /*
            string[] DriveList = Environment.GetLogicalDrives();
            for (int i = 0; i < DriveList.Length; i++)
                
            {

                Console.WriteLine(DriveList[i]);

                
            }
            Console.ReadKey(true); */

            Console.WriteLine("Установка завершена. Сейчас вы должны подготовить рабочую папку Minecraft'a.");
            Console.ForegroundColor = ConsoleColor.Green;
            string new_l_v = System.IO.File.ReadAllText(appdata_launcher_path + @"Temp\ForgeVErsion.txt");
            Console.WriteLine("Для того, что бы правильно подготовить рабочую папку Minecraft создайте новый модпак в Curse(Twitch) или MultiMC. Установите Forge");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new_l_v);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Далее зайдите в папку лаунчера, в Modpacks (Instances), в в папку созданного модпака, там, где находятся директории Mods и Config.");
            Console.WriteLine("Скопируйте адрес данной папки... Нажмите ПКМ на название консоли, выберите <Изменить> и Вставить.");
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Gray;
            string MCPath = Console.ReadLine();
            System.IO.File.WriteAllText(appdata_launcher_path + "MCPath.txt", MCPath);
            System.IO.File.Delete(appdata_launcher_path + @"Temp\ForgeVersion.txt");
            Console.WriteLine("Файл с путем к папке Minecraft'a находится тут: " + appdata_launcher_path + "MCPath.txt");
            Console.WriteLine("Потом вы можете вручную изменить путь, открыв данный файл.");
            
            /*Console.WriteLine("Нажмите любую клавишу и в браузере откроется инструкция...");
            Console.ReadKey(true);
            System.Diagnostics.Process.Start(@"https://cdn.discordapp.com/attachments/236018668889309185/331019888443260928/unknown.png");
            Console.WriteLine(@"Откройте %appdata%\scproch_updater\MCLink.txt");
            Console.WriteLine("И введите туда путь до рабочей папки...");
            Console.WriteLine(base_url);
            Console.WriteLine(save_path);
            Console.WriteLine(appdata_path);
            Console.WriteLine(appdata_launcher_path);
            Console.WriteLine(url);
            Console.WriteLine(url1);
            Console.WriteLine(url2);
            Console.WriteLine(url3);
            Console.WriteLine(name);
            Console.WriteLine(name1);
            Console.WriteLine(name2);
            */
            Console.Write("Установка завершена. Нажмите любую клавишу для завершения...");
            Console.ReadKey(true);
        }      
    }
}