using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAA
{
    public class BlokKros : Blok
    {
        public BlokKros()
            : base()
        {
            this.RotateAtas();

            _warnaBlok = PapanPermainan.HIJAU;
        }

        public override void RotateAtas()
        {
            base.ResetElemen();
            _elemen[1, 1] = true;
            _elemen[2, 0] = true;
            _elemen[2, 1] = true;
            _elemen[2, 2] = true;
        }

        public override void RotateBawah()
        {
            base.ResetElemen();
            _elemen[2, 0] = true;
            _elemen[2, 1] = true;
            _elemen[2, 2] = true;
            _elemen[3, 1] = true;
        }

        public override void RotateKanan()
        {
            base.ResetElemen();
            _elemen[1, 1] = true;
            _elemen[2, 1] = true;
            _elemen[3, 1] = true;
            _elemen[2, 2] = true;
        }

        public override void RotateKiri()
        {
            base.ResetElemen();
            _elemen[2, 0] = true;
            _elemen[1, 1] = true;
            _elemen[2, 1] = true;
            _elemen[3, 1] = true;
        }


    }
}
