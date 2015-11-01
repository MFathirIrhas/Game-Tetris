using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DAA
{
    public class Blok : ICloneable
    {
        //Variable-variable konstan
        public const int LEBAR = 4;
        public const int PANJANG = 4;

        public const int BARIS = 0;
        public const int KOTAK = 1;
        public const int KROS = 2;
        public const int ZNORMAL = 3;
        public const int ZTERBALIK = 4;
        public const int LNORMAL = 5;
        public const int LTERBALIK = 6;

        public Point koordKiriAtas;

        protected bool[,] _elemen;
        //protected Image _warnaBlok;
        protected int _warnaBlok;

        public Blok()
        {
            _elemen = new bool[PANJANG, LEBAR];
        }

        protected void ResetElemen()
        {
            for (int i = 0; i < PANJANG; i++)
                for (int j = 0; j < LEBAR; j++)
                    _elemen[i, j] = false;
        }

        public void Draw(PapanPermainan papan)
        {
            //Graphics g = PapanPermainan.ActiveForm.CreateGraphics();
            int offset_i = koordKiriAtas.Y / PapanPermainan.OFFSETPIXEL;
            int offset_j = koordKiriAtas.X / PapanPermainan.OFFSETPIXEL;

            for(int i=0;i<PANJANG; i++)
                for (int j = 0; j < LEBAR; j++)
                {
                    try
                    {
                        if (_elemen[i, j] == true)
                        {
                            papan.SetElemen(offset_i + i, offset_j + j, _warnaBlok);
                        }
                            //g.DrawImage(_warnaBlok, new Rectangle(koordKiriAtas.X + (j * PapanPermainan.OFFSETPIXEL), koordKiriAtas.Y + (i * PapanPermainan.OFFSETPIXEL), PapanPermainan.OFFSETPIXEL, PapanPermainan.OFFSETPIXEL));

                        //g.Dispose();
                    }
                    catch (ArgumentException ex)
                    {

                    }
                }

        }

        public void HapusDariPapan(PapanPermainan papan)
        {
            int offset_i = koordKiriAtas.Y / PapanPermainan.OFFSETPIXEL;
            int offset_j = koordKiriAtas.X / PapanPermainan.OFFSETPIXEL;
            for(int i=0; i<PANJANG;i++)
                for (int j = 0; j < LEBAR; j++)
                {
                    if (_elemen[i, j] == true)
                        papan.SetElemen(offset_i + i, offset_j + j, PapanPermainan.HITAM);
                }
        }

        public bool GetElement(int i, int j)
        {
            return _elemen[i, j];
        }

        public virtual void RotateAtas()
        {

        }

        public virtual void RotateBawah()
        {
        }

        public virtual void RotateKanan()
        {

        }

        public virtual void RotateKiri()
        {
        }

        public object Clone()
        {
            //throw new NotImplementedException();
            Blok b = null;

            if (this is BlokGaris)
                b = new BlokGaris();
            else if (this is BlokKotak)
                b = new BlokKotak();
            else if (this is BlokKros)
                b = new BlokKros();
            else if (this is BlokZNormal)
                b = new BlokZNormal();
            else if (this is BlokZTerbalik)
                b = new BlokZTerbalik();
            else if (this is BlokLNormal)
                b = new BlokLNormal();
            else if (this is BlokLTerbalik)
                b = new BlokLTerbalik();
            
            b.koordKiriAtas = this.koordKiriAtas;
            b._warnaBlok = this._warnaBlok;

            for(int i=0; i <LEBAR; i++)
                for(int j = 0; j< PANJANG ; j++)
                    b._elemen[i,j] = this._elemen[i,j];

            return b;
        }
    }
}
