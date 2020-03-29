using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace coder
{
    public abstract class FileBase
    {
        String fileName;
        StreamWriter writer;
        public FileBase()
        {
        }
        public void SetFilePath(string path)
        {
            this.fileName = path;
        }
        public abstract void WriteContent();

        public void writeCode()
        {
            using (writer = new StreamWriter(fileName))
            {
                WriteContent();
            }
        }
        public FileBase a(int text)
        {
            writer.Write(text);
            return this;
        }
        public FileBase a(String text)
        {
            writer.Write(text);
            return this;
        }
        public FileBase al(String text)
        {
            writer.WriteLine(text);
            return this;
        }
        public FileBase e()
        {
            writer.WriteLine();
            return this;
        }
        public FileBase t()
        {
            writer.Write("    ");
            return this;
        }
        public String getFileName()
        {
            return fileName;
        }

        public String mainName(String name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }
        public String objName(String name)
        {
            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }
    }
}
