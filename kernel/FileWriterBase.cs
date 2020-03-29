using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kernel
{
    public abstract class  FileWriterBase
    {
        public String FileName { get; set; }
        public String FilePath { get; set; }

        private StreamWriter sw;
        private void init()
        {
            sw = new StreamWriter(FilePath +"\\"+ FileName, false, Encoding.UTF8);
        }
        private void close()
        {
            sw.Close();
        }
        public void append(String str)
        {
            sw.Write(str);
        }
        public void appendLine(String str)
        {
            sw.Write(str+"\n");
        }

        public void WriteFile()
        {
            init();
            WriteBody();
            close();
        }
        public abstract void WriteBody();
    }
}
