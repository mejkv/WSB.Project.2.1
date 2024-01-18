using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Internal;
using System.Reflection;

namespace AirShop.ExternalServices.Services.Printout
{
    public class MyFontResolver : FontResolverBase, IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            if (faceName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
                return LoadFontData("AirShop.ExternalServices.Font.arial.ttf");

            return base.GetFont(faceName);
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
                return new FontResolverInfo("Arial");

            return base.ResolveTypeface(familyName, isBold, isItalic);
        }
        static byte[] LoadFontData(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }
    }
}
