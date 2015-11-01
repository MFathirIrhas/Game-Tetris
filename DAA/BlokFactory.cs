using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAA
{
    public sealed class BlokFactory
    {
        public static Blok BuatkanBlok(int spesifikasi)
        {
            Blok b = null;
            switch (spesifikasi)
            {
                case Blok.BARIS:
                    b = new BlokGaris();
                    break;

                case Blok.KOTAK:
                    b = new BlokKotak();
                    break;

                case Blok.KROS:
                    b = new BlokKros();
                    break;

                case Blok.ZNORMAL:
                    b = new BlokZNormal();
                    break;

                case Blok.ZTERBALIK:
                    b = new BlokZTerbalik();
                    break;

                case Blok.LNORMAL:
                    b = new BlokLNormal();
                    break;

                case Blok.LTERBALIK:
                    b = new BlokLTerbalik();
                    break;
            }

            return b;
        }
    }
}
