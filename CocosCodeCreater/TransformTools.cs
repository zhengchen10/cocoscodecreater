using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public class TransformTools
    {
        public static float XR2D(float v, SceneEditInfos seInfo)
        {
            return (v - (float)seInfo.AnchorX) * (float)seInfo.Scale + (float)seInfo.OffsetX;
            //return (v + offset) * scale;
        }
        public static float YR2D(float v, SceneEditInfos seInfo)
        {
            if (seInfo.InvertedYAxis)
                return seInfo.Rectangle.Height - (float)((v - seInfo.AnchorY) * seInfo.Scale + seInfo.OffsetY);
            else
                return (float)((v - seInfo.AnchorY) * seInfo.Scale + seInfo.OffsetY);
            //return ((v - anchor) * scale + offset);
            //return height - ((v - anchor) * scale + offset);
        }
        public static float XD2R(float v, SceneEditInfos seInfo)
        {
            return (float)((v - seInfo.OffsetX) / seInfo.Scale + seInfo.AnchorX);
            //return (v - offset) / scale + anchor;
        }
        public static float YD2R(float v, SceneEditInfos seInfo)
        {
            if (seInfo.InvertedYAxis)
            {
                return (float)((seInfo.Rectangle.Height - v - seInfo.OffsetY) / seInfo.Scale + seInfo.AnchorY);
            }
            else
            {
                return (float)((v - seInfo.OffsetY) / seInfo.Scale + seInfo.AnchorY);
            }
        }

        public static float YD2R(float v, float anchor, float offset, float height, float scale)
        {
            return (height - v - offset) / scale + anchor;
        }

        public static PointF R2D(PointF p, SceneEditInfos seInfo)
        {
            float x = XR2D(p.X, seInfo);
            float y = YR2D(p.Y, seInfo);
            return new PointF(x, y);
        }

        public static PointF D2R(PointF p, SceneEditInfos seInfo)
        {
            float x = XD2R(p.X, seInfo);
            float y = YD2R(p.Y, seInfo);
            return new PointF(x, y);
        }
    }
}
