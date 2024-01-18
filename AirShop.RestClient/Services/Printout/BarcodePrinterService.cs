using System.Drawing;
using PdfSharp.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace AirShop.ExternalServices.Services.Printout
{
    public class BarcodePrinterService
    {
        public static void DrawBarcode(XGraphics gfx, string code, int x, int y)
        {
            BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>();
            barcodeWriter.Format = BarcodeFormat.EAN_13;
            barcodeWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = 200,
                Height = 50
            };
            barcodeWriter.Renderer = new AirBarcodeRenderer();
            var bitmap = barcodeWriter.Write(code);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                XImage image = XImage.FromStream(ms);
                gfx.DrawImage(image, x, y);
            }
        }
    }

    public class AirBarcodeRenderer : IBarcodeRenderer<Bitmap>
    {
        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, null);
        }

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            int width = matrix.Width;
            int height = matrix.Height;

            Bitmap bitmap = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = matrix[x, y] ? Color.Black : Color.White;
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
