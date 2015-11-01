using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAA
{
    public class BlokGaris : Blok
    {
        public BlokGaris()
            : base()
        {
            _elemen[0, 0] = true;
            _elemen[1, 0] = true;
            _elemen[2, 0] = true;
            _elemen[3, 0] = true;

            _warnaBlok = PapanPermainan.MERAH;
        }

        public override void RotateAtas()
        {
            base.ResetElemen();
            _elemen[0, 0] = true;
            _elemen[1, 0] = true;
            _elemen[2, 0] = true;
            _elemen[3, 0] = true;
        }

        public override void RotateBawah()
        {
            this.RotateAtas();
        }

        public override void RotateKanan()
        {
            base.ResetElemen();
            _elemen[3, 0] = true;
            _elemen[3, 1] = true;
            _elemen[3, 2] = true;
            _elemen[3, 3] = true;
        }

        public override void RotateKiri()
        {
            this.RotateKanan();
        }
    }
}
