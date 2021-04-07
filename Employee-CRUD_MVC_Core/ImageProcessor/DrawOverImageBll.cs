using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageProcessor
{
    public class DrawOverImageBll
    {
        public static string DrawTextOverImage(string QuoteText, string ImageName, string QuotesPosition, string ColorCode, int FontSize)
        {
            Image bitmap = Bitmap.FromFile(ImageName);

            FileInfo fileInfo = new FileInfo(ImageName);
            int imageHeight = bitmap.Height;
            int imageWidth = bitmap.Width;

            Graphics graphicsImage = Graphics.FromImage(bitmap);
            StringFormat stringformat = new StringFormat();
            StringFormat leftSideFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            StringFormat rightSideFormat = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            stringformat.FormatFlags = StringFormatFlags.NoClip;
            //string ColorCodePattern = @"^#([0-9A-F]{3}){1,2}$";
            //bool isValidColor = Regex.IsMatch(ColorCode, ColorCodePattern);
            //if (!isValidColor)
            //{
            //    ColorCode = "#fff";
            //}
            Color StringColor = System.Drawing.ColorTranslator.FromHtml(ColorCode);
            RectangleF position = new RectangleF(0, 0, 0.0F, 0.0F);
            if (QuotesPosition == "Top Left")
            {
                position = new RectangleF(imageWidth * 10 / 100, 10, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.Alignment = StringAlignment.Near;
                stringformat.LineAlignment = StringAlignment.Near;
            }
            else if (QuotesPosition == "Top Center")
            {
                position = new RectangleF(imageWidth * 25 / 100, 10, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.LineAlignment = StringAlignment.Near;
            }
            else if (QuotesPosition == "Top Right")
            {
                position = new RectangleF(imageWidth * 50 / 100, 10, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.Alignment = StringAlignment.Far;
                stringformat.LineAlignment = StringAlignment.Near;
            }
            else if (QuotesPosition == "Center")
            {
                position = new RectangleF(imageWidth * 25 / 100, imageHeight * 25 / 100, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
            }
            else if (QuotesPosition == "Bottom Left")
            {
                position = new RectangleF(imageWidth * 5 / 100, imageHeight * 35 / 100, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.Alignment = StringAlignment.Near;
                stringformat.LineAlignment = StringAlignment.Far;
            }
            else if (QuotesPosition == "Bottom Center")
            {
                position = new RectangleF(imageWidth * 25 / 100, imageHeight * 35 / 100, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.LineAlignment = StringAlignment.Far;
            }
            else if (QuotesPosition == "Bottom Right")
            {
                position = new RectangleF(imageWidth * 50 / 100, imageHeight * 35 / 100, imageWidth - (imageWidth * 50 / 100), imageHeight - (imageWidth * 50 / 100));
                stringformat.Alignment = StringAlignment.Far;
                stringformat.LineAlignment = StringAlignment.Far;
            }

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, imageWidth, imageHeight));
            }

            graphicsImage.DrawString(QuoteText, new Font("arial", FontSize, FontStyle.Bold), new SolidBrush(StringColor), position, stringformat);
            string fileName = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + fileInfo.Extension;

            if (bitmap == null) return "";
            bitmap.Save(@".\wwwroot\Assets\Image\QuotesData\" + fileName, ImageFormat.Jpeg);
            return fileName;
        }
    }
}