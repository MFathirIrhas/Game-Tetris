using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAA
{
    public class BlokZTerbalik:Blok
    {
        public BlokZTerbalik()
            : base()
        {
            this.RotateAtas();

            _warnaBlok = PapanPermainan.MAGENTA;
        }

        public override void RotateAtas()
        {
            base.ResetElemen();
            _elemen[3, 0] = true;
            _elemen[3, 1] = true;
            _elemen[2, 1] = true;
            _elemen[2, 2] = true;
        }

        public override void RotateBawah()
        {
            this.RotateAtas();
        }

        public override void RotateKanan()
        {
            base.ResetElemen();
            _elemen[1, 0] = true;
            _elemen[2, 0] = true;
            _elemen[2, 1] = true;
            _elemen[3, 1] = true;
        }

        public override void RotateKiri()
        {
            this.RotateKanan();
        }
    }
}
