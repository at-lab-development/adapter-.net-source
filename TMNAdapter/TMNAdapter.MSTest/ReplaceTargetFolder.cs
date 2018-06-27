using System.IO;
using System.Linq;

namespace TMNAdapter.MSTest
{
    public static class ReplaceTargetFolder
    {
        public static void ReplaceFolder(string source, string destination)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(destination);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(destination);

            DirectoryInfo dir = new DirectoryInfo(source);
            DirectoryInfo[] directories = dir.GetDirectories();

            string[] files = Directory.GetFiles(source);

            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file);
                    string destFile = Path.Combine(destination, name);
                    if (name != "file") File.Move(file, destFile);
                }
                catch {}
            }

            foreach (DirectoryInfo subdir in directories)
            {
                string temppath = Path.Combine(destination, subdir.Name);
                if (!Directory.Exists(temppath))
                    try
                    {
                        Directory.Move(subdir.FullName, temppath);
                    }
                    catch{}
            }
        }
    }
}
