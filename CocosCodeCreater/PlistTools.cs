using kernel.plist;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml;

namespace CocosCodeCreater
{
    public class PListTools
    {
        public static PListInfo readPlist(String filepath)
        {
            XmlDocument doc;

            doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档中的注释
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(filepath, settings);
            doc.Load(reader);
            XmlNode plist = doc.LastChild;
            XmlNode dics = plist.ChildNodes[0];
            XmlNode frames = dics.ChildNodes[1];
            XmlNode metadata = dics.ChildNodes[3];
            PListInfo pListInfo = new PListInfo(filepath);
            pListInfo = readMetaData(pListInfo, metadata);
            for (int i = 0; i < frames.ChildNodes.Count; i += 2)
            {
                XmlNode key = frames.ChildNodes[i];
                XmlNode imageDic = frames.ChildNodes[i+1];
                pListInfo.Frames.Add(readImageInfo(key.InnerText, imageDic));
            }
            
            return pListInfo;
        }

        public static PListInfo newPlist(String fileName,int width,int height)
        {
            if (!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
            PListInfo pListInfo = new PListInfo(fileName);
            pListInfo.RealTextureFileName = pListInfo.Name.Replace(".plist",".png");
            pListInfo.Size = new Size(width, height);
            pListInfo.TextureFileName = pListInfo.Name.Replace(".plist", ".png");
            pListInfo.Save();

            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.Save();
            g.Dispose();
            bitmap.MakeTransparent(Color.Transparent);
            bitmap.Save(fileName.Replace(".plist", ".png"), ImageFormat.Png);
            bitmap.Dispose();
            return pListInfo;
        }

        private static ImageInfo readImageInfo(String name,XmlNode dic)
        {
            ImageInfo imageInfo = new ImageInfo();
            imageInfo.Name = (name);
            String frame = dic.ChildNodes[1].InnerText;
            String offset = dic.ChildNodes[3].InnerText;
            String rotated = dic.ChildNodes[5].Name;
            String sourceColorRect = dic.ChildNodes[7].InnerText;
            String sourceSize = dic.ChildNodes[9].InnerText;
            imageInfo.Frame=(parseRectangle(frame));
            imageInfo.Offset=(parsePoint(offset));
            imageInfo.Rotated=(rotated == "true");
            imageInfo.SourceColorRect=(parseRectangle(sourceColorRect));
            imageInfo.SourceSize=(parseSize(sourceSize));
            return imageInfo;
        }

        private static PListInfo readMetaData(PListInfo info,XmlNode dic)
        {
            info.Format = int.Parse(dic.ChildNodes[1].InnerText);
            info.RealTextureFileName = dic.ChildNodes[3].InnerText;
            info.Size = parseSize(dic.ChildNodes[5].InnerText);
            info.TextureFileName = dic.ChildNodes[9].InnerText;
            return info;
        }

        private static Rectangle parseRectangle(String frame)
        {
            frame = frame.Replace("{", "");
            frame = frame.Replace("}", "");
            String[] ss = frame.Split(',');
            Rectangle result = new Rectangle(int.Parse(ss[0].Trim()),int.Parse(ss[1].Trim()), int.Parse(ss[2].Trim()), int.Parse(ss[3].Trim()));
            return result;
        }
        private static Point parsePoint(String frame)
        {
            frame = frame.Replace("{", "");
            frame = frame.Replace("}", "");
            String[] ss = frame.Split(',');
            Point result = new Point(int.Parse(ss[0].Trim()), int.Parse(ss[1].Trim()));
            return result;
        }
        private static Size parseSize(String frame)
        {
            frame = frame.Replace("{", "");
            frame = frame.Replace("}", "");
            String[] ss = frame.Split(',');
            Size result = new Size(int.Parse(ss[0].Trim()), int.Parse(ss[1].Trim()));
            return result;
        }

        public static Rectangle getNextPosition(PListInfo pList,int width,int height)
        {
            if (pList.Frames.Count == 0)
                return new Rectangle(0, 0,width,height);
            ImageInfo ii = pList.Frames[pList.Frames.Count - 1];

            Rectangle rect = pList.Frames[pList.Frames.Count - 1].DisplayFrame;

            if (rect.Right + width+1 < pList.Size.Width)
                return new Rectangle(rect.Right + 1,rect.Top, width, height);
            else
            {
                int maxY = 0;
                for(int i=0;i< pList.Frames.Count; i++)
                {
                    ImageInfo imageInfo = pList.Frames[i];

                    if(maxY < imageInfo.DisplayFrame.Bottom + 1)
                    {
                        maxY = imageInfo.DisplayFrame.Bottom + 1;
                    }
                }
                return new Rectangle(0, maxY, width, height);
            }
        }

        public static Image RotateImage90(Image img)
        {
            try
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return img;
            }
            catch
            {
                return null;
            }
        }
    }
}
