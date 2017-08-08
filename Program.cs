using System;
using System.IO;
using System.Net;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Threading;
using System.Threading.Tasks;

namespace SKProCH__Installer_1._
{
    class Program
    {
        public static string TTW = "";
        public static bool Writecompleted = false;
        public static int GlobalPos = 0;
        public static void Main(string[] args)
        {
            string base_url = "ftp://updater:thisispassword@31.25.29.138/usb1_1/minecraft/DontTouchThisFolder/";
            string save_path = @"C:\Program Files\SKProCH Lab\MC Updater\";
            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata_launcher_path = appdata_path + @"\SKProCH Lab\MC Updater\";
                       
            Console.CursorVisible = false;
            OTW("Программа для автоматического обновления сейчас будет установлена...");
            WriteBegin("Создание директорий...");
            Directory.CreateDirectory(save_path);
            Directory.CreateDirectory(appdata_launcher_path);
            Directory.CreateDirectory(appdata_launcher_path + "Temp");
            Directory.CreateDirectory(appdata_launcher_path + "Needed Mods");
            AfterWrite();
            WebClient wc = new WebClient();

            WriteBegin("Скачивание файлов...");

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

            AfterWrite();

            WriteBegin("Создание дополнительных файлов...");

            System.IO.File.Create(appdata_launcher_path + "M_Version.txt");            
            AfterWrite();

            /*
                       Console.ReadKey(true);
                       System.Diagnostics.Process.Start(@"C:\Program Files\SKProCH Updater\MCPath.txt");
                       WriteBegin("ВНИМАНИЕ: Введите путь до предустановленной папки клиента Minecraft.\n" +
                       "Вы сможете изменить его позже, путем реадктирования файла MCLink.txt\nВ папке проекта.");
            */

            WriteBegin("Помещение в автозагрузку...");

            RegistryKey reg;                                                                                // Этот код отвечает 
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"); // за автозагрузку 
            reg.SetValue(name, save_path + name);                                                           // приложения лаунчера
            AfterWrite();
            WriteBegin("Создание ярлыков...");

            WshShell shell = new WshShell();
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MC Updater.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.Description = "Ярлык для автоматического обновления клиента!";
            shortcut.TargetPath = save_path + @"Modpack Updater.exe";
            shortcut.IconLocation = save_path + "Icon.ico";
            shortcut.Save();

            AfterWrite();
            /*
            string[] DriveList = Environment.GetLogicalDrives();
            for (int i = 0; i < DriveList.Length; i++)
                
            {

                WriteBegin(DriveList[i]);

                
            }
            Console.ReadKey(true); */
            Console.ForegroundColor = ConsoleColor.Cyan;
            string new_l_v = System.IO.File.ReadAllText(appdata_launcher_path + @"Temp\ForgeVErsion.txt");
            OTW("Установка завершена. Сейчас вы должны подготовить рабочую папку Minecraft'a.\nДля того, что бы правильно подготовить рабочую папку Minecraft создайте новый модпак в Curse(Twitch) или MultiMC. Установите Forge\n" + new_l_v + "\nДалее зайдите в папку лаунчера, в Modpacks (Instances), в в папку созданного модпака, там, где находятся директории Mods и Config.\nСкопируйте адрес данной папки... Нажмите ПКМ на название консоли, выберите <Изменить> и Вставить.");
            /*Console.WriteLine("Установка завершена. Сейчас вы должны подготовить рабочую папку Minecraft'a.");
            Console.ForegroundColor = ConsoleColor.Green;
            string new_l_v = System.IO.File.ReadAllText(appdata_launcher_path + @"Temp\ForgeVErsion.txt");
            Console.WriteLine("Для того, что бы правильно подготовить рабочую папку Minecraft создайте новый модпак в Curse(Twitch) или MultiMC. Установите Forge");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new_l_v);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Далее зайдите в папку лаунчера, в Modpacks (Instances), в в папку созданного модпака, там, где находятся директории Mods и Config.");
            Console.WriteLine("Скопируйте адрес данной папки... Нажмите ПКМ на название консоли, выберите <Изменить> и Вставить."); */
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Gray;
            string MCPath = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            System.IO.File.WriteAllText(appdata_launcher_path + "MCPath.txt", MCPath);
            System.IO.File.Delete(appdata_launcher_path + @"Temp\ForgeVersion.txt");
            OTW("Файл с путем к папке Minecraft'a находится тут: " + appdata_launcher_path + "MCPath.txt");
            OTW("Потом вы можете вручную изменить путь, открыв данный файл.");

            /*WriteBegin("Нажмите любую клавишу и в браузере откроется инструкция...");
            Console.ReadKey(true);
            System.Diagnostics.Process.Start(@"https://cdn.discordapp.com/attachments/236018668889309185/331019888443260928/unknown.png");
            WriteBegin(@"Откройте %appdata%\scproch_updater\MCLink.txt");
            WriteBegin("И введите туда путь до рабочей папки...");
            WriteBegin(base_url);
            WriteBegin(save_path);
            WriteBegin(appdata_path);
            WriteBegin(appdata_launcher_path);
            WriteBegin(url);
            WriteBegin(url1);
            WriteBegin(url2);
            WriteBegin(url3);
            WriteBegin(name);
            WriteBegin(name1);
            WriteBegin(name2);
            */
            OTW("Установка завершена. Нажмите любую клавишу для завершения...");
            Console.ReadKey(true);
        }   
        static void OTW(string TextToWrite)
        {
            TTW = TextToWrite;
            GlobalPos = Console.CursorTop;
            for (int i = 2; i < TTW.Length; i++)
            {
                Console.SetCursorPosition(0, GlobalPos);
                Console.Write(TTW.Remove(i));
                Thread.Sleep(45);
            }
            Console.SetCursorPosition(0, GlobalPos);
            Console.WriteLine(TTW);
        }
        static void WriteBegin(string TextToWrite)
        {
            Console.CursorVisible = false;
            Writecompleted = false;
            TTW = TextToWrite;
            GlobalPos = Console.CursorTop;
            Thread WT = new Thread(Write);
            WT.IsBackground = false;
            WT.Start();
        }
        static void Write ()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 2; i < TTW.Length; i++)
            {
                Console.SetCursorPosition(0, GlobalPos);
                Console.Write(TTW.Remove(i));
                Thread.Sleep(50);
            }
            Console.Write(".");
            Writecompleted = true;
        }
        static void AfterWrite()
        {
            while (Writecompleted != true)
            {
                Thread.Sleep(100);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, GlobalPos);
            Console.WriteLine(TTW);
            Console.CursorVisible = true;
        }
    }
}