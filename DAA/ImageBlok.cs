using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DAA
{
    public class ImageBlok
    {
        public static Image MERAH;
        public static Image KUNING;
        public static Image HIJAU;
        public static Image BIRU;
        public static Image MAGENTA;
        public static Image CYAN;
        public static Image COKLAT;

        static ImageBlok()
        {
            MERAH = Image.FromFile("Merah.gif");
            KUNING = Image.FromFile("Kuning.gif");
            HIJAU = Image.FromFile("Hijau.gif");
            BIRU = Image.FromFile("Biru.gif");
            MAGENTA = Image.FromFile("Magenta.gif");
            CYAN = Image.FromFile("Cyan.gif");
            COKLAT = Image.FromFile("Coklat.gif");

        }
    }

    
}
