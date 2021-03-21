using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADAM {
    /// <summary>
    /// Lógica de interacción para GraphA.xaml
    /// </summary>
    public partial class GraphA : UserControl {
        double[] _col;
        double[] _x;
        double[] _row;
        double[] _y;
        private bool isOver;
        private Path pathHost;
        private int _n;
        private double _factor;
        private double _attenuation;

        public GraphA() {
            InitializeComponent();
            _col = new double[] { 60, 97, 58, 74, 50, 52, 98, 59, 74, 50, 50, 22 };
            _x = new double[] { 0.0, 0.1, 0.2, 0.3, 0.5, 0.7, 1.0, 2.0, 3.0, 5.0, 7.0, 10.0 };
            _row = new double[] { 18, 65, 64, 64, 64, 64, 64, 64, 65, 67 };
            _y = new double[] { -1, 70, 60, 50, 40, 30, 20, 10, 00, -1 };
        }

        public double Factor {
            get { return _factor; }
            set {
                _factor = value;
                factor.Text = Math.Round(value, 2).ToString();
                double _xdif = 0.0;
                double _wp = 0.0;
                double _pf = 0.0;
                double _xstep = 0.0;
                if (value >= 0.1 && value <= 10) {
                    for (int i = 0; i < _x.Length; i++) {
                        if (value < _x[i]) {
                            _xdif = value - _x[i - 1];
                            _xstep = _x[i] - _x[i - 1];
                            _pf = _xdif / _xstep;
                            _wp -= _col[i - 1];
                            double left = (_wp + _col[i - 1] * _pf);
                            Canvas.SetLeft(xpoint, left - xpoint.Width / 2d);
                            return;
                        } else if (value == _x[i]) {
                            _xdif = 0;
                            _xstep = _x[i + 1] - _x[i];
                            _pf = _xdif / _xstep;
                            double left = (_wp + _col[i] * _pf);
                            Canvas.SetLeft(xpoint, left - xpoint.Width / 2d);
                            return;
                        }
                        _wp += _col[i];
                    }
                }
            }
        }

        public double Attenuation {
            get { return _attenuation; }
            set {
                _attenuation = value;
                double _ydif = 0.0;
                double _wp = 0.0;
                double _pf = 0.0;
                double _ystep = 0.0;
                if (value >= 0 && value <= 70) {
                    for (int i = 0; i < _y.Length; i++) {
                        if (value > _y[i] && _y[i] != -1) {
                            _ydif = value - _y[i];
                            _ystep = -_y[i] + _y[i - 1];
                            _pf = _ydif / _ystep;
                            double top = (_wp - _row[i - 1] * _pf);
                            Canvas.SetTop(ypoint, top - xpoint.Width / 2d);
                            return;
                        } else if (value == _y[i]) {
                            _ydif = 0;
                            _ystep = _y[i + 1] - _y[i];
                            _pf = _ydif / _ystep;
                            double top = (_wp - _row[i] * _pf);
                            Canvas.SetTop(ypoint, top - xpoint.Width / 2d);
                            return;
                        }
                        _wp += _row[i];
                    }
                }
            }
        }

        private double GetAtValueFromCol(double f) {
            double _xdif = 0.0;
            double _wp = 0.0;
            double _pf = 0.0;
            double _xstep = 0.0;
            if (f >= 60.0 && f <= 722) {
                for (int i = 0; i < _col.Length; i++) {
                    if (f <= _col[i]) {
                        _xstep = _col[i];
                        _pf = f / _xstep;
                        _xdif = -_x[i] + _x[i + 1];
                        double value = _pf * _xdif + _x[i];
                        Factor = value;
                        return value;
                    }
                    _wp += _col[i];
                    f -= _col[i];
                }
            }
            return 0;
        }

        private double GetAtValueFromRow(double f) {
            double _ydif = 0.0;
            double _wp = 0.0;
            double _pf = 0.0;
            double _ystep = 0.0;
            if (f >= 18 && f <= 468) {
                for (int i = 0; i < _row.Length; i++) {
                    if (f <= _row[i]) {
                        _ystep = _row[i];
                        _pf = f / _ystep;
                        _ydif = _y[i] - _y[i + 1];
                        double value = _y[i] - _pf * _ydif;
                        Attenuation = value;
                        return value;
                    }
                    _wp += _row[i];
                    f -= _row[i];
                }
            }
            return 0;
        }

        private void MouseMoves(object sender, MouseEventArgs e) {
            Point pos = e.GetPosition(this);
            Vector v1 = new Vector(pos.X, pos.Y);
            foreach (Path i in g1241.Children) {
                foreach (Point p in ((i.Data as PathGeometry).Figures[0].Segments[0] as PolyLineSegment).Points) {
                    Point _p = i.TranslatePoint(p, this);
                    double dist = Math.Sqrt(Math.Pow(v1.X - _p.X, 2d) + Math.Pow(v1.Y - _p.Y, 2d));
                    if (dist <= 4) {
                        isOver = true;
                        pathHost = i;
                        _n = Convert.ToInt32(i.Name.Substring(4));
                        if (Keyboard.Modifiers == ModifierKeys.Control)
                            SiretT.Interop.User32.GlobalMouse.Position = i.PointToScreen(p);
                        i.Stroke = Brushes.Red;
                        break;
                    } else {
                        isOver = false;
                        pathHost = null;
                        i.Stroke = Brushes.Black;
                    }
                }
                if (isOver) break;
            }
        }

        private void MouseDowns(object sender, MouseButtonEventArgs e) {
            Point pos = e.GetPosition(this);
            double left = this.TranslatePoint(pos, grid).X;
            double top = this.TranslatePoint(pos, grid).Y;
            if (isOver) {
                GetAtValueFromCol(left);
                double _att = GetAtValueFromRow(top);
                xLine.X1 = left - xLine.StrokeThickness / 2d; xLine.Y1 = 466; xLine.X2 = left - xLine.StrokeThickness / 2d; xLine.Y2 = top;
                yLine.X1 = 60; yLine.Y1 = top - xLine.StrokeThickness / 2d; yLine.X2 = left; yLine.Y2 = top - xLine.StrokeThickness / 2d;
                Canvas.SetTop(att, top - 20);
                att.Text = Math.Round(_att, 2) + " dB";
            }
        }

        public void GetBitmap() {
            DrawingVisual dwg = new DrawingVisual();
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)Math.Ceiling(grid.Width), (int)Math.Ceiling(grid.Height), 96, 96, PixelFormats.Default);
            rtb.Render(grid);
            Clipboard.SetImage(rtb);
        }

        internal int GetN() {
            Point pos = new Point(Canvas.GetLeft(xpoint) + xpoint.Width / 2d, Canvas.GetTop(ypoint) + ypoint.Width / 2d);
            double left = pos.X;
            double top = pos.Y;
            pos = grid.TranslatePoint(pos, this);
            xLine.X1 = left - xLine.StrokeThickness / 2d; xLine.Y1 = 466; xLine.X2 = left - xLine.StrokeThickness / 2d; xLine.Y2 = top;
            yLine.X1 = 60; yLine.Y1 = top - xLine.StrokeThickness / 2d; yLine.X2 = left; yLine.Y2 = top - xLine.StrokeThickness / 2d;
            foreach (Path i in g1241.Children) {
                foreach (Point p in ((i.Data as PathGeometry).Figures[0].Segments[0] as PolyLineSegment).Points) {
                    Point _p = i.TranslatePoint(p, this);
                    double dist = Math.Sqrt(Math.Pow(pos.X - _p.X, 2d) + Math.Pow(pos.Y - _p.Y, 2d));
                    if (dist <= 4) {
                        pathHost = i;
                        _n = Convert.ToInt32(i.Name.Substring(4));
                        i.Stroke = Brushes.Red;
                        break;
                    } else {
                        isOver = false;
                        pathHost = null;
                        i.Stroke = Brushes.Black;
                    }
                }
            }
            return _n;
        }
    }
}