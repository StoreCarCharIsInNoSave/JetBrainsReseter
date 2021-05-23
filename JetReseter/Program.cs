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
                        Console.WriteLine("JetBrains product(s) detected, registry cleanup started...");
                        if (key.OpenSubKey(@"JavaSoft") != null)
                        {
                            key.DeleteSubKeyTree("JavaSoft");
                            Console.WriteLine("JavaSoft branch removed");
                        }
                        if (key.OpenSubKey(@"JetBrains") != null)
                        {
                            key.DeleteSubKeyTree("JetBrains");
                            Console.WriteLine("JetBrains branch removed");

                        }
                        Console.WriteLine("Registry cleanup is complete.");
                        Console.WriteLine("---------------------------------------");
                    }

                    foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\"))
                    {
                        foreach (string target in targets)
                        {
                            if (dir.Contains(target))
                            {
                                Console.WriteLine("Detected " + target);
                                Console.WriteLine("The activation reset process has started...");
                                try
                                {
                                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\" + dir.Split('\\')[dir.Split('\\').Length - 1] + @"\eval\", true);
                                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\JetBrains\" + dir.Split('\\')[dir.Split('\\').Length - 1] + @"\options\other.xml");
                                    Console.WriteLine("Activation successfully reset!");
                                }
                                catch (Exception c)
                                {
                                    Console.WriteLine(c.Message);
                                    Console.WriteLine("If you are reading this, it is possible that the activation was reset earlier, try running {0}", target);
                                }
                                Console.WriteLine("---------------------------------------");

                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("No JetBrains products found");
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
