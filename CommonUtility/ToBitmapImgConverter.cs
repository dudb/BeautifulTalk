using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CommonUtility
{
    public class ToBitmapImgConverter
    {
        public static BitmapImage DecodeImage(byte[] arValue)
        {
            if (null == arValue) return null;

            try
            {
                MemoryStream memStream = new MemoryStream(arValue);
                BitmapImage BitmapImg = new BitmapImage();
                BitmapImg.BeginInit();
                BitmapImg.StreamSource = memStream;
                BitmapImg.EndInit();
                return BitmapImg;
            }
            catch (Exception UnExpectedException)
            {
                throw UnExpectedException;
            }
        }

        public static byte[] LoadImage(string strAbsolutePath)
        {
            Byte[] arBuffer = null;

            try
            {
                string strFileExtension = Path.GetExtension(strAbsolutePath);
                BitmapEncoder ImgEncoder = null;

                switch (strFileExtension)
                {
                    case ".jpg":
                    case ".jpeg":   { ImgEncoder = new JpegBitmapEncoder(); break; }
                    case ".png":    { ImgEncoder = new PngBitmapEncoder(); break; }
                    case ".gif":    { ImgEncoder = new GifBitmapEncoder(); break; }
                    default: return null;
                }

                Uri AbsoluteUri = new Uri(strAbsolutePath, UriKind.Absolute);
                BitmapImage BitmapImg = new BitmapImage(AbsoluteUri);
                ImgEncoder.Frames.Add(BitmapFrame.Create(BitmapImg));

                using (MemoryStream ms = new MemoryStream())
                {
                    ImgEncoder.Save(ms);
                    arBuffer = ms.ToArray();
                }
            }
            catch (Exception UnExpectedException)
            {
                throw UnExpectedException;
            }

            return arBuffer;
        }
    }
}
