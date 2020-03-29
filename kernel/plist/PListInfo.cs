using kernel.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace kernel.plist
{
    public class PListInfo : Resource
    {
        public PListInfo(string path):base(path)
        {
            Frames = new List<ImageInfo>();
            Type = "Plist";
            Format = 2;
            SmartUpdate = "$Aguzai:SmartUpdate:1234567890$";
        }
        public int Format { get; set; }
        public String RealTextureFileName { get; set; }
        public Size Size { get; set; }
        public String SmartUpdate { get; set; }
        public String TextureFileName { get; set; }
        public List<ImageInfo> Frames { get; set; }

        private void doSave(String path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sw.WriteLine("<!DOCTYPE plist PUBLIC \"-//Apple Computer//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">");
            sw.WriteLine("<plist version=\"1.0\">");
            sw.WriteLine("    <dict>");
            sw.WriteLine("        <key>frames</key>");
            sw.WriteLine("        <dict>");
            for (int i = 0; i < Frames.Count; i++)
            {
                ImageInfo imageInfo = Frames[i];
                sw.WriteLine("        <key>" + imageInfo.Name + "</key>");
                sw.WriteLine("        <dict>");
                sw.WriteLine("            <key>frame</key>");
                sw.WriteLine("            <string>{{" + imageInfo.Frame.Left + "," + imageInfo.Frame.Top + "},{" + imageInfo.Frame.Width + "," + imageInfo.Frame.Height + "}}</string>");
                sw.WriteLine("            <key>offset</key>");
                sw.WriteLine("            <string>{" + imageInfo.Offset.X + "," + imageInfo.Offset.Y + "}</string>");
                sw.WriteLine("            <key>rotated</key>");
                sw.WriteLine("            <" + imageInfo.Rotated.ToString() + "/>");
                sw.WriteLine("            <key>sourceColorRect</key>");
                sw.WriteLine("            <string>{{" + imageInfo.SourceColorRect.Left + "," + imageInfo.SourceColorRect.Top + "},{" + imageInfo.SourceColorRect.Width + "," + imageInfo.SourceColorRect.Height + "}}</string>");
                sw.WriteLine("            <key>sourceSize</key>");
                sw.WriteLine("            <string>{" + imageInfo.SourceSize.Width + "," + imageInfo.SourceSize.Height + "}</string>");
                sw.WriteLine("        </dict>");
            }
            sw.WriteLine("        </dict>");
            sw.WriteLine("        <key>metadata</key>");
            sw.WriteLine("        <dict>");
            sw.WriteLine("            <key>format</key>");
            sw.WriteLine("            <integer>2</integer>");
            sw.WriteLine("            <key>realTextureFileName</key>");
            sw.WriteLine("            <string>" + RealTextureFileName + "</string>");
            sw.WriteLine("            <key>size</key>");
            sw.WriteLine("            <string>{" + Size.Width + "," + Size.Height + "}</string>");
            sw.WriteLine("            <key>smartupdate</key>");
            sw.WriteLine("            <string>" + SmartUpdate + "</string>");
            sw.WriteLine("            <key>textureFileName</key>");
            sw.WriteLine("            <string>" + TextureFileName + "</string>");
            sw.WriteLine("        </dict>");
            sw.WriteLine("    </dict>");
            sw.WriteLine("</plist>");
            sw.Flush();
            sw.Close();
        }
        public void SaveAtPath(String path)
        {
            doSave(path+"\\"+ RealTextureFileName.Replace(".png",".plist"));
            doSaveImage(path + "\\" + RealTextureFileName);
        }

        private void doSaveImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Image image = new Bitmap(Size.Width, Size.Height);
            Graphics g = null;
            try
            {
                g = Graphics.FromImage(image);
            }
            catch (Exception e)
            {
                throw;
            }
            g.Clear(Color.Transparent);
            for (int i = 0; i < Frames.Count; i++)
            {
                ImageInfo imageInfo = Frames[i];
                Image src = Image.FromFile(imageInfo.SourceFile);
                g.DrawImage(src, imageInfo.Frame, new Rectangle(0, 0, imageInfo.Frame.Width, imageInfo.Frame.Height), GraphicsUnit.Pixel);
            }
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void Save()
        {
            doSave(base.Path);
            /*if (File.Exists(base.Path))
            {
                File.Delete(base.Path);
            }
            StreamWriter sw = new StreamWriter(base.Path, false, Encoding.UTF8);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sw.WriteLine("<!DOCTYPE plist PUBLIC \"-//Apple Computer//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">");
            sw.WriteLine("<plist version=\"1.0\">");
            sw.WriteLine("    <dict>");
            sw.WriteLine("        <key>frames</key>");
            sw.WriteLine("        <dict>");
            for(int i = 0; i < Frames.Count; i++)
            {
                ImageInfo imageInfo = Frames[i];
                sw.WriteLine("        <key>"+imageInfo.Name+"</key>");
                sw.WriteLine("        <dict>");
                sw.WriteLine("            <key>frame</key>");
                sw.WriteLine("            <string>{{"+imageInfo.Frame.Left+","+imageInfo.Frame.Top+"},{"+imageInfo.Frame.Width+","+imageInfo.Frame.Height+"}}</string>");
                sw.WriteLine("            <key>offset</key>");
                sw.WriteLine("            <string>{" + imageInfo.Offset.X + "," + imageInfo.Offset.Y + "}</string>");
                sw.WriteLine("            <key>rotated</key>");
                sw.WriteLine("            <"+imageInfo.Rotated.ToString() +"/>");
                sw.WriteLine("            <key>sourceColorRect</key>");
                sw.WriteLine("            <string>{{" + imageInfo.SourceColorRect.Left + "," + imageInfo.SourceColorRect.Top + "},{" + imageInfo.SourceColorRect.Width + "," + imageInfo.SourceColorRect.Height + "}}</string>");
                sw.WriteLine("            <key>sourceSize</key>");
                sw.WriteLine("            <string>{" + imageInfo.SourceSize.Width + "," + imageInfo.SourceSize.Height + "}</string>");
                sw.WriteLine("        </dict>");
            }
            sw.WriteLine("        </dict>");
            sw.WriteLine("        <key>metadata</key>");
            sw.WriteLine("        <dict>");
            sw.WriteLine("            <key>format</key>");
            sw.WriteLine("            <integer>2</integer>");
            sw.WriteLine("            <key>realTextureFileName</key>");
            sw.WriteLine("            <string>"+RealTextureFileName+"</string>");
            sw.WriteLine("            <key>size</key>");
            sw.WriteLine("            <string>{" + Size.Width+","+Size.Height + "}</string>");
            sw.WriteLine("            <key>smartupdate</key>");
            sw.WriteLine("            <string>" + SmartUpdate + "</string>");
            sw.WriteLine("            <key>textureFileName</key>");
            sw.WriteLine("            <string>" + TextureFileName + "</string>");
            sw.WriteLine("        </dict>");
            sw.WriteLine("    </dict>");
            sw.WriteLine("</plist>");
            sw.Flush();
            sw.Close();
            */
        }
    }
}
