using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace kernel.models
{
    public class Resource 
    {
        public string Path { get; set; }

        public Resource(string path)
        {
            Path = path;
            FileInfo fi = new FileInfo(path);
            Name = fi.Name;
            WorkPath = "";
            UUID = System.Guid.NewGuid().ToString("N");
        }
        public string Name { get; set; }
        public string WorkPath { get; set; }
        [Browsable(false)]
        public string UUID { get; set; }
        [Browsable(false)]
        public string Type { get; set; }
    }
}
