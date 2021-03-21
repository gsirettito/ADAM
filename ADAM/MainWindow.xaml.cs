using System;
using System.Windows;
using System.Runtime.InteropServices;
using MMath = Microsoft.MicrosoftMath;


namespace ADAM {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private double Gc;
        private double Gc_dB;
        private double Gg;
        private double Gg_dB;
        private double Go;
        private double Go_dB;
        private double Gtu;
        private double Gtu_dB;

        public MainWindow() {
            InitializeComponent();
        }

        private void calcule(object sender, RoutedEventArgs e) {
            Polar s11 = _s11.ToPolar();
            Polar s12 = _s12.ToPolar();
            Polar s21 = _s21.ToPolar();
            Polar s22 = _s22.ToPolar();

            //Delta
            Polar d = s11 * s22 - s12 * s21;
            double D = d.Module;
            _delta.Text = D.ToString();

            //K
            double K = (1d - Math.Pow(s11.Module, 2d) - Math.Pow(s22.Module, 2d) + D * D) / (2d * (s12 * s21).Module);
            _k.Text = K.ToString();

            //RhoS
            double B1 = 1d + Math.Pow(s11.Module, 2d) - Math.Pow(s22.Module, 2d) - D * D;
            Polar C1 = s11 - d * s22.Conjugate();
            Polar rsg = (B1 - Math.Sqrt(B1 * B1 - 4d * Math.Pow(C1.Module, 2d))) / (2d * C1);
            Polar rg = rsg.Conjugate();
            _roS.Phase = rg.Phase; _roS.Module = rg.Module;

            //Rhol
            double B2 = 1d + Math.Pow(s22.Module, 2d) - Math.Pow(s11.Module, 2d) - D * D;
            Polar C2 = s22 - d * s11.Conjugate();
            Polar rsc = (B2 - Math.Sqrt(B2 * B2 - 4d * Math.Pow(C2.Module, 2d))) / (2d * C2);
            Polar rc = rsc.Conjugate();
            _roL.Phase = rc.Phase; _roL.Module = rc.Module;

            //Gg Ganacia o pérdida producida por la red de entrada
            Gg = (1d - Math.Pow(rg.Module, 2d)) / Math.Pow((1d - s11 * rg).Module, 2d);
            Gg_dB = 10d * Math.Log10(Gg);

            //Go Ganacia del elemento activo
            Go = Math.Pow(s21.Module, 2d);
            Go_dB = 10d * Math.Log10(Go);

            //Gc Ganacia o pérdida
            Gc = (1d - Math.Pow(rc.Module, 2d)) / Math.Pow((1d - s22 * rc).Module, 2d);
            Gc_dB = 10d * Math.Log10(Gc);

            //Gt
            Gtu = Gg * Go * Gc;
            Gtu_dB = 10d * Math.Log10(Gtu);

            if (_db_.IsChecked != true) {
                _gS.Text = Gg.ToString();
                _g0.Text = Go.ToString();
                _gL.Text = Gc.ToString();
                _gT.Text = Gtu.ToString();
            } else {
                _gS.Text = Gg_dB.ToString();
                _g0.Text = Go_dB.ToString();
                _gL.Text = Gc_dB.ToString();
                _gT.Text = Gtu_dB.ToString();
            }
        }

        private void _db__Checked(object sender, RoutedEventArgs e) {
            try {
                _gS.Text = Gg_dB.ToString();
                _g0.Text = Go_dB.ToString();
                _gL.Text = Gc_dB.ToString();
                _gT.Text = Gtu_dB.ToString();
            } catch { }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {
            try {
                _gS.Text = Gg.ToString();
                _g0.Text = Go.ToString();
                _gL.Text = Gc.ToString();
                _gT.Text = Gtu.ToString();
            } catch { }
        }

        [DllImport("user32.dll")]
        public extern static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public extern static int MoveWindow(IntPtr hWnd, int x, int y,
           int nWidth, int nHeight, int bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private MMath.CasContext cas;
        private void LoadHandle(object sender, RoutedEventArgs e) {
            GraphAW gph = new GraphAW();
            gph.Frequency = _frequency.Value;
            gph.Impedance = _impedance.Value;
            gph.Show();
        }
    }
}
