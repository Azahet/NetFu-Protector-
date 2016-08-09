using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using Colorful;
using Encrypt;
using Console = Colorful.Console;

namespace NetFu_Protector___par_Azahet
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = @"Cache\Save\a.xml";
            var content = string.Empty;
            var process = string.Empty;
            var star = new[]
            {
                new Formatter("     [*]", Color.SeaGreen)
            };
            var validate = new[]
            {
                new Formatter("[-] ", Color.Lime)
            };
            var error = new[]
            {
                new Formatter("[!] ", Color.Red)
            };
            Console.Title = "NetFu Protector - par Azahet";
            Console.Write("Net", Color.OrangeRed);
            Console.WriteLine("Fu(c) Account Protector ", Color.WhiteSmoke);
            Console.WriteLine("A program by Azahet", Color.WhiteSmoke);
            Console.WriteLine("https://github.com/Azahet", Color.WhiteSmoke);
            Console.WriteLine();
            Console.WriteLineFormatted("{0} Detection of accounts ...", Color.WhiteSmoke, star);
            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLineFormatted("{0}File open : " + path, Color.WhiteSmoke, validate);
                }
            }
            catch (IOException)
            {
                Console.WriteLineFormatted("{0}File can't open : " + path, Color.WhiteSmoke, error);
            }

            Console.WriteLine();
            Console.WriteLineFormatted("{0} Decryption of accounts ...", Color.WhiteSmoke, star);


            try
            {
                Aes_Encrypt.DecryptFile(path, "a");
                Console.WriteLineFormatted("{0} All accounts have been decrypted : " + path, Color.WhiteSmoke, validate);
            }
            catch (Exception)
            {
                Console.WriteLineFormatted("{0} Failed to decrypt accounts: " + path, Color.WhiteSmoke, error);
            }


            Console.WriteLine();
            Console.WriteLineFormatted("{0} List of accounts :", Color.WhiteSmoke, star);
            Console.WriteLine(File.ReadAllText(path), Color.WhiteSmoke);
            var psi = new ProcessStartInfo();
            psi.FileName = "Netfu.exe";
            psi.WorkingDirectory = @"";
            Process.Start(psi);
            System.Threading.Thread.Sleep(3000);
            process = helper.get_proces();
            while (process.Contains("Netfu"))
            {
                process = helper.get_proces();
            }

            Console.WriteLine();
            Console.WriteLineFormatted("{0} Closing netfu detect ...", Color.WhiteSmoke, star);
            Aes_Encrypt.EncryptFile(path, "a");
            Console.WriteLineFormatted("{0} All accounts have been encrypted : " + path, Color.WhiteSmoke, validate);
            Console.WriteLineFormatted("{0} Closing in 3 second" + path, Color.WhiteSmoke, validate);
            System.Threading.Thread.Sleep(3000);
            
        }
    }
}