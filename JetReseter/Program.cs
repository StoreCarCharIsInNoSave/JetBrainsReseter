using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace JetReseter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> targets = new List<string>
            { "WebStorm", "CLion", "Rider", "GoLand", "PhpStorm","PyCharm","ReSharper","Upsource","TeamCity","YouTrack","dotCover","dotMemory","dotTrace","AppCode","RubyMine","DataGrip","IntelliJIdea"};
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\"))
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true))
                    {
                        Console.WriteLine("Обнаружен(ы) продукт(ы) JetBrains, запущена очистка реестра...");
                        if (key.OpenSubKey(@"JavaSoft") != null)
                        {
                            key.DeleteSubKeyTree("JavaSoft");
                            Console.WriteLine("Удалена ветка JavaSoft");
                        }
                        if (key.OpenSubKey(@"JetBrains") != null)
                        {
                            key.DeleteSubKeyTree("JetBrains");
                            Console.WriteLine("Удалена ветка JetBrains");

                        }
                        Console.WriteLine("Очистка реестра завершена.");
                        Console.WriteLine("---------------------------------------");
                    }

                    foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\"))
                    {
                        foreach (string target in targets)
                        {
                            if (dir.Contains(target))
                            {
                                Console.WriteLine("Обнаружен " + target);
                                Console.WriteLine("Запущен процесс сброса активации...");
                                try
                                {
                                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\" + dir.Split('\\')[dir.Split('\\').Length - 1] + @"\eval\", true);
                                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\" + dir.Split('\\')[dir.Split('\\').Length - 1] + @"\options\other.xml");
                                    Console.WriteLine("Активация успешно сброшена!");
                                }
                                catch (Exception c)
                                {
                                    Console.WriteLine(c.Message);
                                    Console.WriteLine("Если вы это читаете, возможно, что активация была сброшена раньше, попробуйте запустить {0}",target);
                                }
                                Console.WriteLine("---------------------------------------");

                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Продуктов компании JetBrains не обнаружено");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
