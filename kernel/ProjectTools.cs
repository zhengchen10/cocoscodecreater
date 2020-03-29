using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kernel
{
    public class ProjectTools
    {
        public static bool saveProejct(String path,String name,ProjectInfo project)
        {
            String json = JsonUtils.ToJSON(project);
            using (StreamWriter sw = new StreamWriter(path + "\\" + name, false, Encoding.UTF8))
            {
                sw.Write(json);
            }
            return true;
        }

        public static ProjectInfo openProject(String path)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }
            return JsonUtils.FromJSON<ProjectInfo>(sb.ToString()); 
        }

        public static bool createProejct(string projectPath, string projectName)
        {
            if(Directory.Exists(projectPath + "\\" + projectName))
            {
                return false;
            }
            Directory.CreateDirectory(projectPath + "\\" + projectName);
            Directory.CreateDirectory(projectPath + "\\" + projectName+"\\temp");

            return true;
        }
    }
}
