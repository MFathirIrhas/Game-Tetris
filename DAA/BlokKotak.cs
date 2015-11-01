using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAA
{
    public class BlokKotak : Blok
    {
        public BlokKotak()
            : base()
        {
            _elemen[1, 0] = true;
            _elemen[1, 1] = true;
            _elemen[2,0] = true;
            _elemen[2, 1] = true;

            _warnaBlok = PapanPermainan.KUNING;
        }

        public override void RotateAtas()
        {
        }

        public override void RotateBawah()
        {            
        }

        public override void RotateKanan()
        {
        }

        public override void RotateKiri()
        {
        }
    }
}
