using System;
using System.Windows;

namespace ADAM {
    /// <summary>
    /// Lógica de interacción para GraphAW.xaml
    /// </summary>
    public partial class GraphAW : Window {
        private double[,] _gs = { { 2.0,    1.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 1.4142, 1.4142, 1.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 1.0,    2.0,    1.0,    1.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 0.7654, 1.8478, 1.8478, 0.7654, 1.0,    0.0,    0.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 0.6180, 1.6180, 2.0,    1.6180, 0.6180, 1.0,    0.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 0.5176, 1.4142, 1.9318, 1.9318, 1.4142, 0.5176, 1.0,    0.0,    0.0,    0.0,    0.0 },
                                  { 0.4450, 1.2470, 1.8019, 2.0,    1.8019, 1.2470, 0.4450, 1.0,    0.0,    0.0,    0.0 },
                                  { 0.3902, 1.1111, 1.6629, 1.9615, 1.9615, 1.6629, 1.1111, 0.3902, 1.0,    0.0,    0.0 },
                                  { 0.3473, 1.0,    1.5321, 1.8794, 2.0,    1.8794, 1.5321, 1.0,    0.3473, 1.0,    0.0 },
                                  { 0.3129, 0.9080, 1.4142, 1.7820, 1.9754, 1.9754, 1.7820, 1.4142, 0.9080, 0.3129, 1.0 }};

        public GraphAW() {
            InitializeComponent();
        }

        private void CopyClick(object sender, RoutedEventArgs e) {
            gph.GetBitmap();
        }

        private void calcule(object sender, RoutedEventArgs e) {
            double factor = (fatt.Value / freq.Value) - 1.0;
            double wc = freq.Value * Math.PI * 2d * freq.UnitItems[freq.SelectedIndex].Factor;
            fatt.Value = (Math.Round(gph.Factor, 2) + 1.0) * freq.Value;
            int n = gph.GetN();

            gs.Children.Clear();
            for (int i = 0; i < n; i++) {
                double gi = Math.Round(_gs[n - 1, i], 4);
                gs.Children.Add(
                    new LabeledControl() {
                        Header = new WpfMath.Controls.FormulaControl() {
                            Formula = string.Format(
                                "g_{0}={1}_{0}={2}",
                                i + 1,
                                i % 2 == 0 ? 'C' : 'L',
                                gi)
                        },
                        Content = new UnitBox() {
                            Value = i % 2 == 0 ? Math.Round(gi / (ro.Value * wc) * 1E+12, 3) :
                                                 Math.Round((ro.Value * gi / wc) * 1E+9, 3),
                            Width = 100,
                            UnitItems = i % 2 == 0 ? new UnitCollection() { new Unit() { Factor = 1E-12, Name = "Pico Faradio", Symbol = "pF" },
                                                                            new Unit() { Factor = 1E-9, Name = "Nano Faradio", Symbol = "nF" },
                                                                            new Unit() { Factor = 1E-6, Name = "Micro Faradio", Symbol = "µF" },
                                                                            new Unit() { Factor = 1E-3, Name = "Mili Faradio", Symbol = "mF" },
                                                                            new Unit() { Factor = 1, Name = "Faradio", Symbol = "F" },} :
                                                     new UnitCollection() { new Unit() { Factor = 1E-12, Name = "Pico Henrio", Symbol = "pH" },
                                                                            new Unit() { Factor = 1E-9, Name = "Nano Henrio", Symbol = "nH" },
                                                                            new Unit() { Factor = 1E-6, Name = "Micro Henrio", Symbol = "µH" },
                                                                            new Unit() { Factor = 1E-3, Name = "Mili Henrio", Symbol = "mH" },
                                                                            new Unit() { Factor = 1, Name = "Henrio", Symbol = "H" },},
                            SelectedIndex = i % 2 == 0 ? 0 : 1
                        },
                        Margin = new Thickness(0, 5, 0, 0)
                    });
            }

            for (int i = 0; i < n; i++) {
                double gi = Math.Round(_gs[n - 1, i], 4);
                gs.Children.Add(
                    new LabeledControl() {
                        Header = new WpfMath.Controls.FormulaControl() {
                            Formula = string.Format(
                                @"\beta \ell_{0}={1}",
                                i + 1,
                                i % 2 == 0 ? Math.Round(gi * zl.Value / ro.Value * 180d / Math.PI, 2) :
                                             Math.Round(gi * ro.Value / zh.Value * 180d / Math.PI, 2))
                        },
                        Content = new UnitBox() {
                            Value = i % 2 == 0 ? Math.Round(gi * zl.Value / ro.Value * 180d / Math.PI, 2) :
                                                 Math.Round(gi * ro.Value / zh.Value * 180d / Math.PI, 2),
                            Width = 100,
                            UnitItems = new UnitCollection() { new Unit() { Factor = 1E-12, Name = "Deg", Symbol = "°" } },
                            SelectedIndex = 0
                        },
                        Margin = new Thickness(0, 5, 0, 0)
                    });
            }
        }

        public double Frequency {
            get { return freq.Value; }
            set { freq.Value = value; }
        }

        public double Impedance {
            get { return ro.Value; }
            set { ro.Value = value; }
        }

        public double Zh {
            get { return zh.Value; }
            set { zh.Value = value; }
        }

        public double Zl {
            get { return zl.Value; }
            set { zl.Value = value; }
        }
    }
}
