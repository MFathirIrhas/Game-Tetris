using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DAA
{
    public partial class PapanPermainan : Form
    {

        public const int TINGGI = 20;
        public const int LEBAR = 12;

        private Blok _terkiniBlok;
        private int _terkiniTinggiTumpukan;
        private int[,] _elemen;
        private Random _random;

        public const int HITAM = 0;
        public const int MERAH = 1;
        public const int KUNING = 2;
        public const int HIJAU = 3;
        public const int BIRU = 4;
        public const int MAGENTA = 5;
        public const int CYAN = 6;
        public const int COKLAT = 7;

        public const int OFFSETPIXEL = 20;

        public PapanPermainan()
        {
            InitializeComponent();

            _random = new Random();

            this._elemen = new int[TINGGI, LEBAR];
            for (int i = 0; i < TINGGI; i++)
                for (int j = 0; j < LEBAR; j++)
                    _elemen[i, j] = HITAM;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // PapanPermainan
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(240, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PapanPermainan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DAA";
            this.Load += new System.EventHandler(this.PapanPermainan_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PapanPermainan_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PapanPermainan_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PapanPermainan_KeyPress_1);
            this.ResumeLayout(false);

        }

        private void CekBaris()
        {
            int jmlBarisYgDitemukan = 0;
            bool selesai = false;
            for (int i = TINGGI - 1; i >= _terkiniTinggiTumpukan && !selesai; i--)
            {
                for (int j = 0; j < LEBAR && !selesai; j++)
                {
                    if (_elemen[i, j] == HITAM)
                        selesai = true;
                }
            }
        }

        private void TurunkanBlok()
        {
            //int grid_i = _terkiniBlok.koordKiriAtas.Y / OFFSETPIXEL;
            //int grid_j = _terkiniBlok.koordKiriAtas.X / OFFSETPIXEL;
            //for (int i = grid_i, j = grid_j; j < Blok.PANJANG; j++)
            //{
            //    _elemen[i, j] = HITAM;
            //}

            //System.Drawing.Point koordLama = _terkiniBlok.koordKiriAtas;
            //_terkiniBlok.koordKiriAtas.Y += OFFSETPIXEL;

            //hapus bekas blok
            _terkiniBlok.HapusDariPapan(this);
            //turunkan satu grid unit
            System.Drawing.Point koordLama = _terkiniBlok.koordKiriAtas;
            _terkiniBlok.koordKiriAtas.Y += OFFSETPIXEL;

            //redraw blok
            _terkiniBlok.Draw(this);

            Size s = new Size((Blok.PANJANG * OFFSETPIXEL), (Blok.LEBAR * OFFSETPIXEL) + OFFSETPIXEL);
            this.Invalidate(new Rectangle(koordLama, s));

            //this.UpdateTerkiniTinggiTumpukan();

            //cek tumpukan
            //Thread t = new Thread(new ThreadStart(CekTumpukan));
            //t.Start();
        }

        //private void PapanPermainan_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //BlokGaris b1 = new BlokGaris();
        //    //b1.koordKiriAtas = new Point(0, 0);
        //    //b1.Draw();

        //    //BlokKros b2 = new BlokKros();
        //    //b1.koordKiriAtas = new Point(80, 0);
        //    //b2.Draw();

        //    //BlokZNormal b3 = new BlokZNormal();
        //    //b3.koordKiriAtas = new Point(160, 0);
        //    //b3.Draw();

        //    switch (e.KeyChar)
        //    {
        //        case 'M':
        //            goto case 'm';

        //        case 'm' :
        //            BuatBlokBaru();
        //            break;
        //    }
        //    e.Handled = true;
        //}

        private void BuatBlokBaru()
        {
            // antara 0-6(termasuk 0 dan 6)
            int spesifikasi = _random.Next(0, 7);

            _terkiniBlok = BlokFactory.BuatkanBlok(spesifikasi);
            _terkiniBlok.koordKiriAtas = new Point(100,-60);

            //hitamkan area sebelum buat blok baru
            //Graphics g = this.CreateGraphics();
            //SolidBrush hitam = new SolidBrush(Color.Black);
            int i_max = _terkiniBlok.koordKiriAtas.X + (Blok.PANJANG * OFFSETPIXEL);
            int j_max = _terkiniBlok.koordKiriAtas.Y + (Blok.LEBAR * OFFSETPIXEL);

            for (int i = _terkiniBlok.koordKiriAtas.X; i < i_max; i += OFFSETPIXEL)
            {
                for (int j = _terkiniBlok.koordKiriAtas.Y; j < j_max; j += OFFSETPIXEL)
                {
                    if(i >= 0 && j>=0)
                        _elemen[j / OFFSETPIXEL, i / OFFSETPIXEL] = HITAM;
                    //g.FillRectangle(hitam, i, j, OFFSETPIXEL, OFFSETPIXEL);
                }
            }

            //tampilkan blok baru
            _terkiniBlok.Draw(this);

            //panggil invalidate
            Size s = new Size(Blok.PANJANG * OFFSETPIXEL, Blok.LEBAR * OFFSETPIXEL);
            this.Invalidate(new Rectangle(_terkiniBlok.koordKiriAtas, s));

            //hitam.Dispose();
            //g.Dispose();
        }

        private void PapanPermainan_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush hitam = new SolidBrush(Color.Black);
            Image warnaBlok = null;

            for (int i = 0; i < TINGGI; i++)
            {
                for (int j = 0; j < LEBAR; j++)
                {
                    switch (_elemen[i, j])
                    {
                        case HITAM:
                            g.FillRectangle(hitam, j * OFFSETPIXEL, i * OFFSETPIXEL, OFFSETPIXEL, OFFSETPIXEL);
                            break;

                        case MERAH:
                            warnaBlok = ImageBlok.MERAH;
                            break;

                        case KUNING:
                            warnaBlok = ImageBlok.KUNING;
                            break;

                        case HIJAU:
                            warnaBlok = ImageBlok.HIJAU;
                            break;

                        case BIRU:
                            warnaBlok = ImageBlok.BIRU;
                            break;

                        case MAGENTA:
                            warnaBlok = ImageBlok.MAGENTA;
                            break;

                        case CYAN:
                            warnaBlok = ImageBlok.CYAN;
                            break;

                        case COKLAT:
                            warnaBlok = ImageBlok.COKLAT;
                            break;

                    }//end case

                    if (_elemen[i, j] > HITAM)
                        g.DrawImage(warnaBlok, j * OFFSETPIXEL, i * OFFSETPIXEL, OFFSETPIXEL, OFFSETPIXEL);

                }//End  for j
            }//end for i
        }//end paint

        public void SetElemen(int i, int j, int warna)
        {
            if(i >= 0 && i < TINGGI && j >= 0 && j<LEBAR)
                _elemen[i, j] = warna;
        }

        public int GetElemen(int i, int j)
        {
            return _elemen[i, j];
        }

        private void PapanPermainan_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'M':
                    goto case 'm';

                case 'm':
                    BuatBlokBaru();
                    _timer.Enabled = true;
                    break;

                case 'P':
                    goto case 'p';

                case 'p':
                    //pause game
                    _timer.Enabled = false;
                    string s = "GAME PAUSED" + "\nTekan 'm' untuk kembali main";
                    MessageBox.Show(s);
                    break;
            }

            if (e.KeyChar == 'w' || e.KeyChar == 'a' || e.KeyChar == 's' || e.KeyChar == 'd')
            {
                if (!BisaRotasi(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                _terkiniBlok.HapusDariPapan(this);

                switch (e.KeyChar)
                {
                    case 'w':
                        _terkiniBlok.RotateAtas();
                        break;

                    case 'a':
                        _terkiniBlok.RotateKiri();
                        break;

                    case 's':
                        _terkiniBlok.RotateBawah();
                        break;

                    case 'd':
                        _terkiniBlok.RotateKanan();
                        break;

                }
                _terkiniBlok.Draw(this);

                Size s = new Size(Blok.PANJANG * OFFSETPIXEL, Blok.LEBAR * OFFSETPIXEL);
                this.Invalidate(new Rectangle(_terkiniBlok.koordKiriAtas, s));

            }// end if key rotasi
            e.Handled = true;
        }

        private void PapanPermainan_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (BisaDiturunkan())
                    {
                        TurunkanBlok();
                    }
                    break;

                case Keys.Left:
                    if (BisaDigeserKiri())
                    {
                        GeserKiriBlok();
                    }
                    break;

                case Keys.Right:
                    if (BisaDigeserKanan())
                    {
                        GeserKananBlok();
                    }
                    break;
                    
            }
        }

        private void GeserKiriBlok()
        {
            _terkiniBlok.HapusDariPapan(this);
            _terkiniBlok.koordKiriAtas.X -= OFFSETPIXEL;
            _terkiniBlok.Draw(this);

            Size s = new Size((Blok.PANJANG * OFFSETPIXEL) + OFFSETPIXEL, (Blok.LEBAR * OFFSETPIXEL));
            this.Invalidate(new Rectangle(_terkiniBlok.koordKiriAtas,s));
        }

        private void GeserKananBlok()
        {
            _terkiniBlok.HapusDariPapan(this);
            System.Drawing.Point koordLama = _terkiniBlok.koordKiriAtas;
            _terkiniBlok.koordKiriAtas.X += OFFSETPIXEL;

            _terkiniBlok.Draw(this);

            Size s = new Size((Blok.PANJANG * OFFSETPIXEL) + OFFSETPIXEL, (Blok.LEBAR * OFFSETPIXEL));
            this.Invalidate(new Rectangle(koordLama, s));
        }

        private bool BisaDiturunkan()
        {
            Point koordBaru = new Point(_terkiniBlok.koordKiriAtas.X, _terkiniBlok.koordKiriAtas.Y + OFFSETPIXEL);

            int offset_i = koordBaru.Y / OFFSETPIXEL;
            int offset_j = koordBaru.X / OFFSETPIXEL;

            int barisTerbawah = Blok.LEBAR - 1;
            bool found = false;
            for (int i = barisTerbawah; i >= 0 && !found; i--)
            {
                for (int j = 0; j < Blok.PANJANG && !found; j++)
                {
                    if (_terkiniBlok.GetElement(i, j) == true)
                        found = true;
                }
                if (!found)
                    --barisTerbawah;
            }

            for (int j = 0; j < Blok.PANJANG; j++)
            {
                if (_terkiniBlok.GetElement(barisTerbawah, j) == true && offset_i + barisTerbawah >= PapanPermainan.TINGGI)
                    return false;

                if (_terkiniBlok.GetElement(barisTerbawah, j) == true && _elemen[offset_i + barisTerbawah, offset_j + j] > HITAM)
                {
                    return false;
                }
            }

            //tidak menyentuh apa2
            return true;
        }

        private bool BisaDigeserKiri()
        {
            Point koordBaru = new Point(_terkiniBlok.koordKiriAtas.X - OFFSETPIXEL, _terkiniBlok.koordKiriAtas.Y);

            int offset_i = koordBaru.Y / OFFSETPIXEL;
            int offset_j = koordBaru.X / OFFSETPIXEL;

            int kolomTerkiri = 0;
            bool found = false;

            for (int i = kolomTerkiri; i < Blok.PANJANG && !found; i++)
            {
                for (int j = 0; j < Blok.LEBAR && !found; j++)
                {
                    if (_terkiniBlok.GetElement(j, i) == true)
                        found = true;
                }
                if (!found)
                    ++kolomTerkiri;
            }

            for (int i = 0; i < Blok.PANJANG; i++)
            {
                if (_terkiniBlok.GetElement(i, kolomTerkiri) == true && (offset_j + kolomTerkiri) < 0)
                    return false;

                if (_terkiniBlok.GetElement(i, kolomTerkiri) == true && offset_i + i >= 0 && _elemen[offset_i + i, offset_j + kolomTerkiri] > HITAM)
                {
                    return false;
                }
            }

            //tidak menyentuh apa2
            return true;

        }

        private bool BisaDigeserKanan()
        {
            Point koordBaru = new Point(_terkiniBlok.koordKiriAtas.X + OFFSETPIXEL, _terkiniBlok.koordKiriAtas.Y);

            int offset_i = koordBaru.Y / OFFSETPIXEL;
            int offset_j = koordBaru.X / OFFSETPIXEL;

            int kolomTerkanan = Blok.PANJANG - 1;
            bool found = false;

            for (int i = kolomTerkanan; i >= 0 && !found; i--)
            {
                for (int j = 0; j < Blok.LEBAR && !found; j++)
                {
                    if (_terkiniBlok.GetElement(j, i) == true)
                        found = true;
                }
                if (!found)
                    --kolomTerkanan;
            }

            for (int i = 0; i < Blok.PANJANG; i++)
            {
                if (_terkiniBlok.GetElement(i, kolomTerkanan) == true && (offset_j + kolomTerkanan) >= PapanPermainan.LEBAR)
                    return false;

                if (_terkiniBlok.GetElement(i, kolomTerkanan) == true && offset_i+i >=0 && _elemen[offset_i + i, offset_j + kolomTerkanan] > HITAM)
                {
                    return false;
                }
            }

            //tidak menyentuh apa2
            return true;
        }

        private bool BisaRotasi(char key)
        {
            Blok b = (Blok)_terkiniBlok.Clone();

            switch (key)
            {
                case 'w':
                    b.RotateAtas();
                    break;

                case 'a':
                    b.RotateKiri();
                    break;

                case 's':
                    b.RotateBawah();
                    break;

                case 'd':
                    b.RotateKanan();
                    break;
            }

            int offset_i = b.koordKiriAtas.Y / OFFSETPIXEL;
            int offset_j = b.koordKiriAtas.X / OFFSETPIXEL;

            for (int i = 0; i < Blok.LEBAR; i++)
            {
                for (int j = 0; j < Blok.PANJANG; j++)
                {
                    if (offset_j + j >= 0 && offset_j + j <= PapanPermainan.LEBAR - 1 && offset_i + i >= 0 && offset_i + i <= PapanPermainan.TINGGI - 1 && b.GetElement(i, j) == true && _terkiniBlok.GetElement(i, j) == false && _elemen[offset_i + i, offset_j + j] > HITAM)
                    {
                        return false;
                    }
                    
                }
            }
            return true;
        }

        private void CekTumpukan()
        {
            if (this._terkiniTinggiTumpukan >= PapanPermainan.TINGGI)
                return; //lantai masih kosong

            int barisYgDicek = PapanPermainan.TINGGI - 1;

            int[] barisKomplet = new int[20];
            int jmlBarisKomplet = 0;
            bool found;
            do
            {
                found = false;
                for (int j = 0; j < PapanPermainan.LEBAR && !found; j++)
                {
                    if (_elemen[barisYgDicek, j] == HITAM)
                        found = true;
                }

                if (!found)
                {
                    barisKomplet[jmlBarisKomplet++] = barisYgDicek;
                }
                --barisYgDicek;

                Graphics g = this.CreateGraphics();
                SolidBrush brushHitam = new SolidBrush(Color.Black);
                //Blink 
                for (int jmlhBlink = 0; jmlhBlink < 2; jmlhBlink++)
                {
                    Thread.Sleep(100);
                    for (int baris = 0; baris < jmlBarisKomplet; baris++)
                    {
                        int barisBlink = barisKomplet[baris];
                        //hitamkan baris ini
                        for (int i = 0; i < TINGGI; i++)
                        {
                            g.FillRectangle(brushHitam, i * OFFSETPIXEL, barisBlink * OFFSETPIXEL, OFFSETPIXEL, OFFSETPIXEL);
                        }
                    }

                    Thread.Sleep(100);
                    //restore setelah blink
                    for (int baris = 0; baris < jmlBarisKomplet; baris++)
                    {
                        int barisBlink = barisKomplet[baris];
                        //kembalikan warna semula
                        for (int j = 0; j < LEBAR; j++)
                            DrawElemen(barisBlink, j, g);
                    }
                }

                brushHitam.Dispose();
                g.Dispose();

                

            } while (barisYgDicek >= this._terkiniTinggiTumpukan);

            
            //if (jmlBarisKomplet == 0)
            //    return;

            ////hitamkan sebentar untuk memunculkan "Blink" effect
            //Graphics g = this.CreateGraphics();
            //SolidBrush brushHitam = new SolidBrush(Color.Black);
            ////Blink 
            //for (int jmlhBlink = 0; jmlhBlink < 2; jmlhBlink++)
            //{
            //    Thread.Sleep(100);
            //    for (int baris = 0; baris < jmlBarisKomplet; baris++)
            //    {
            //        int barisBlink = barisKomplet[baris];
            //        //hitamkan baris ini
            //        for (int i = 0; i < TINGGI; i++)
            //        {
            //            g.FillRectangle(brushHitam, i * OFFSETPIXEL, barisBlink * OFFSETPIXEL, OFFSETPIXEL, OFFSETPIXEL);
            //        }
            //    }

            //    Thread.Sleep(100);
            //    //restore setelah blink
            //    for (int baris = 0; baris < jmlBarisKomplet; baris++)
            //    {
            //        int barisBlink = barisKomplet[baris];
            //        //kembalikan warna semula
            //        for (int j = 0; j < LEBAR; j++)
            //            DrawElemen(barisBlink, j, g);
            //    }
            //}

            //brushHitam.Dispose();
            //g.Dispose();

            //int tinggiLama = this._terkiniTinggiTumpukan;
            //for (int i = jmlBarisKomplet - 1; i >= 0; i--)
            //{
            //    int barisDel = barisKomplet[i];
            //    //turunkan baris di atas baris yang di delete
            //    for (int baris = barisDel; baris >= this._terkiniTinggiTumpukan; baris--)
            //    {
            //        for (int j = 0; j < LEBAR; j++)
            //        {
            //            _elemen[baris, j] = _elemen[baris - 1, j];
            //        }
            //    }
            //}
            int tinggiLama = this._terkiniTinggiTumpukan;
            for (int i = jmlBarisKomplet - 1; i >= 0; i--)
            {
                int barisDel = barisKomplet[i];
                //turunkan baris di atas baris yang di delete
                for (int baris = barisDel; baris >= this._terkiniTinggiTumpukan; baris--)
                {
                    for (int j = 0; j < LEBAR; j++)
                    {
                        _elemen[baris, j] = _elemen[baris - 1, j];
                    }
                }
            }

            this.UpdateTerkiniTinggiTumpukan();

            //redraw
            Point koord = new Point(0, tinggiLama * OFFSETPIXEL);
            Size s = new Size(LEBAR * OFFSETPIXEL, (TINGGI - tinggiLama) * OFFSETPIXEL);
            this.Invalidate(new Rectangle(koord, s));
        }

        private void DrawElemen(int i, int j, Graphics g)
        {
            Image warnaBlok = null;
            switch (_elemen[i, j])
            {
                case MERAH:
                    warnaBlok = ImageBlok.MERAH;
                    break;

                case KUNING:
                    warnaBlok = ImageBlok.KUNING;
                    break;

                case HIJAU:
                    warnaBlok = ImageBlok.HIJAU;
                    break;

                case BIRU:
                    warnaBlok = ImageBlok.BIRU;
                    break;

                case MAGENTA:
                    warnaBlok = ImageBlok.MAGENTA;
                    break;

                case CYAN:
                    warnaBlok = ImageBlok.CYAN;
                    break;

                case COKLAT:
                    warnaBlok = ImageBlok.COKLAT;
                    break;
            }//end case

            g.DrawImage(warnaBlok, j * OFFSETPIXEL, i * OFFSETPIXEL, OFFSETPIXEL, OFFSETPIXEL);

        }

        private void UpdateTerkiniTinggiTumpukan()
        {
            int barisYgDicek = PapanPermainan.TINGGI - 1;
            bool found;
            do
            {
                found = true;
                for (int j = 0; j < PapanPermainan.LEBAR && found; j++)
                {
                    if (_elemen[barisYgDicek, j] > HITAM)
                        found = false;
                }

                if (!found)
                    --barisYgDicek;
            } while (barisYgDicek >= 0 && !found);

            //terkini tinggi tumpukan sekarang berada dibawah garis yang hitam semua
            this._terkiniTinggiTumpukan = barisYgDicek + 1;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_terkiniBlok == null)
                BuatBlokBaru();

            if (BisaDiturunkan())
                TurunkanBlok();
            else
            {
                this.UpdateTerkiniTinggiTumpukan();

                Thread t = new Thread(new ThreadStart(CekTumpukan));
                t.Start();

                BuatBlokBaru();
            }
        }

        private void PapanPermainan_Load(object sender, EventArgs e)
        {
            String s = "=== Block Of Square===" + "\n\n Tekan 'M' untuk mulai!";
            MessageBox.Show(s);
        }

        private void CekGameOver()
        {
            if (this._terkiniTinggiTumpukan >= 1)
                return; // ga ngapa2in

            _timer.Enabled = false;
            for (int i = 0; i < TINGGI; i++)
                for (int j = 0; j < LEBAR; j++)
                    _elemen[i, j] = HITAM;

            String s = "~~~~ GAME OVER ~~~~" + "\nTekan OK untuk mulai lagi";
            MessageBox.Show(s);

            this.Invalidate();
        }
    }
}
