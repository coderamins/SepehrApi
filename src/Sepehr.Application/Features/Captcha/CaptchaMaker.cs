using SixLabors.Fonts;
using SixLaborsCaptcha.Core;

namespace Sepehr.Application.Features.Captcha
{
    public static class CaptchaMaker
    {
        //private static Random rnd = new Random();
        //const string Letters = "0123456789";
        //private const int length = 4;
        //public static int Distortion { get; set; } = 14;
        //const int HEIGHT = 65;
        //const int WIDTH = 250;
        //const string FONTFAMILY = "Tahoma";
        //const int FONTSIZE = 26;
        //public static Color BackgroundColor { get; set; } = Color.Wheat;
        //public static string GenerateCaptchaCode()
        //{
        //    Random rand = new Random();
        //    int maxRand = Letters.Length - 1;

        //    StringBuilder sb = new StringBuilder();

        //    for (int i = 0; i < length; i++)
        //    {
        //        int index = rand.Next(maxRand);
        //        sb.Append(Letters[index]);
        //    }

        //    return sb.ToString();
        //}

        //public static byte[] GetCaptcha(out string code)
        //{
        //    Color bgColor = Color.FromArgb(33, 6, 145, 255);
        //    code = GenerateCaptchaCode();
        //    var printCode = System.Text.RegularExpressions.Regex.Replace(code, ".{1}", "$0  "); // add space after each number
        //    printCode = printCode.PadLeft(printCode.Length + new Random().Next(2, 5), ' ');// add space before
        //    return GenerateCaptcha(WIDTH, HEIGHT, code);//, FONTSIZE, Distortion, bgColor);
        //}

        //static Color GetRandomDeepColor()
        //{
        //    var rand = new Random();

        //    int redlow = 200, greenLow = 255, blueLow = 255;
        //    //return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
        //    return Color.FromArgb(12, 43, 92);
        //}

        //public static byte[] GenerateCaptchaImage(int imageWidth, int imageHeight, string captchaCode,
        //    int fontSize, int distortion, Color bgColor)
        //{
        //    int newX, newY;
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        Bitmap captchaImage = new Bitmap(imageWidth, imageHeight, PixelFormat.Format64bppArgb);
        //        Bitmap cache = new Bitmap(imageWidth, imageHeight, PixelFormat.Format64bppArgb);


        //        Graphics graphicsTextHolder = Graphics.FromImage(captchaImage);
        //        graphicsTextHolder.Clear(bgColor);
        //        graphicsTextHolder.DrawString(captchaCode, new Font(FONTFAMILY, fontSize, FontStyle.Italic),
        //            new SolidBrush(Color.FromArgb(12, 43, 92)), new PointF(1.4F, 3.4F));

        //        //Distort the image with a wave function
        //        for (int y = 0; y < imageHeight; y++)
        //        {
        //            for (int x = 0; x < imageWidth; x++)
        //            {
        //                newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
        //                newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
        //                if (newX < 0 || newX >= imageWidth) newX = 0;
        //                if (newY < 0 || newY >= imageHeight) newY = 0;
        //                cache.SetPixel(x, y, captchaImage.GetPixel(newX, newY));
        //            }
        //        }

        //        graphicsTextHolder = Graphics.FromImage(cache);

        //        var rand = new Random();
        //        Pen linePen = new Pen(new SolidBrush(Color.Black), 1);
        //        for (int i = 0; i < rand.Next(4, 8); i++)
        //        {
        //            linePen.Color = Color.FromArgb(12, 43, 92);

        //            Point startPoint = new Point(rand.Next(0, imageWidth), rand.Next(0, imageHeight));
        //            Point endPoint = new Point(rand.Next(0, imageWidth), rand.Next(0, imageHeight));
        //            graphicsTextHolder.DrawLine(linePen, startPoint, endPoint);

        //            //Point bezierPoint1 = new Point(rand.Next(0, width), rand.Next(0, height));
        //            //Point bezierPoint2 = new Point(rand.Next(0, width), rand.Next(0, height));

        //            //graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
        //        }

        //        cache.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        memoryStream.Position = 0;
        //        return memoryStream.ToArray();
        //    }

        //}


        //public static byte[] GenerateCaptcha(int width, int height, string captchaText)
        //{
        //    Bitmap bitmap = new Bitmap
        //    (width, height, PixelFormat.Format32bppArgb);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    g.SmoothingMode = SmoothingMode.AntiAlias;
        //    Rectangle rect = new Rectangle(0, 0, width, height);
        //    HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti,
        //        Color.LightGray, Color.White);
        //    g.FillRectangle(hatchBrush, rect);
        //    SizeF size;
        //    float fontSize = rect.Height + 1;
        //    Font font;

        //    do
        //    {
        //        fontSize--;
        //        font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
        //        size = g.MeasureString(captchaText, font);
        //    } while (size.Width > rect.Width);
        //    StringFormat format = new StringFormat();
        //    format.Alignment = StringAlignment.Center;
        //    format.LineAlignment = StringAlignment.Center;
        //    GraphicsPath path = new GraphicsPath();
        //    //path.AddString(this.text, font.FontFamily, (int) font.Style, 
        //    //    font.Size, rect, format);
        //    path.AddString(captchaText, font.FontFamily, (int)font.Style, 75, rect, format);
        //    float v = 4F;
        //    PointF[] points =
        //    {
        //        new PointF(rnd.Next(rect.Width) / v, rnd.Next(
        //           rect.Height) / v),
        //        new PointF(rect.Width - rnd.Next(rect.Width) / v,
        //            rnd.Next(rect.Height) / v),
        //        new PointF(rnd.Next(rect.Width) / v,
        //            rect.Height - rnd.Next(rect.Height) / v),
        //        new PointF(rect.Width - rnd.Next(rect.Width) / v,
        //            rect.Height - rnd.Next(rect.Height) / v)
        //  };
        //    Matrix matrix = new Matrix();
        //    matrix.Translate(0F, 0F);
        //    path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        //    hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
        //    g.FillPath(hatchBrush, path);
        //    int m = Math.Max(rect.Width, rect.Height);
        //    for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
        //    {
        //        int x = rnd.Next(rect.Width);
        //        int y = rnd.Next(rect.Height);
        //        int w = rnd.Next(m / 50);
        //        int h = rnd.Next(m / 50);
        //        g.FillEllipse(hatchBrush, x, y, w, h);
        //    }
        //    font.Dispose();
        //    hatchBrush.Dispose();
        //    g.Dispose();
        //    return ImageToByte(bitmap);
        //}

        public static byte[] GenerateCaptcha(out string key)
        {
            key = "";
            var slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
            {
                DrawLines = 7,
                TextColor = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.Blue, SixLabors.ImageSharp.Color.Black },

                FontFamilies = [""],
                FontStyle=SixLabors.Fonts.FontStyle.Regular,
            });

            key = Extensions.GetUniqueKey(6);
            var result = slc.Generate(key);

            return result;
        }

        //public static byte[] ImageToByte(Image img)
        //{
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}


    }
}
