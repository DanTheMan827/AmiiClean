using System;
using System.IO;

namespace amiiclean
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                var file = new FileInfo(arg);
                if (file.Exists)
                {
                    var length = file.Length;
                    if (length == 532 || length == 540 || length == 572)
                    {
                        Console.WriteLine(file.FullName);

                        var fileData = File.ReadAllBytes(file.FullName);
                        new byte[32].CopyTo(fileData, 20);
                        new byte[392].CopyTo(fileData, 128);

                        var crypted = LibAmiibo.Data.AmiiboTag.FromNtagData(fileData).EncryptWithKeys();

                        using (var outStream = File.Create(file.FullName))
                        {
                            outStream.Write(crypted, 0, Convert.ToInt32(length));
                        }
                    }
                }
            }
        }
    }
}
