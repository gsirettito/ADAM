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
    /// Lógica de interacción para PolarCartesianBox.xaml
    /// </summary>
    public partial class PolarCartesianBox : UserControl {
        public PolarCartesianBox() {
            InitializeComponent();
        }

        public static DependencyProperty ModuleProperty = DependencyProperty.Register(
            "Module", typeof(double), typeof(PolarCartesianBox), new PropertyMetadata(0d, ChangeProperty));

        public static DependencyProperty PhaseProperty = DependencyProperty.Register(
            "Phase", typeof(double), typeof(PolarCartesianBox), new PropertyMetadata(0d, ChangeProperty));

        public static DependencyProperty RealProperty = DependencyProperty.Register(
            "Real", typeof(double), typeof(PolarCartesianBox), new PropertyMetadata(0d, ChangeProperty));

        public static DependencyProperty ImaginaryProperty = DependencyProperty.Register(
            "Imaginary", typeof(double), typeof(PolarCartesianBox), new PropertyMetadata(0d, ChangeProperty));

        public static DependencyProperty IsPolarProperty = DependencyProperty.Register(
            "IsPolar", typeof(bool), typeof(PolarCartesianBox), new PropertyMetadata(true, ChangeProperty));
        private static bool stopChange;

        private static void ChangeProperty(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PolarCartesianBox self = d as PolarCartesianBox;
            if (e.Property == ModuleProperty) {
                //self.module.Text = e.NewValue.ToString();
                if (stopChange) return;
                double r = Convert.ToDouble(e.NewValue);
                double o = self.Phase;
                double tan = Math.Tan(o / 180d * Math.PI);
                double a = r / Math.Sqrt(1 + tan * tan);
                double b = a * tan;
                stopChange = true;
                self.Real = a;
                self.Imaginary = b;
            } else if (e.Property == PhaseProperty) {
                //self.phase.Text = e.NewValue.ToString();
                if (stopChange) return;
                double r = self.Module;
                double o = Convert.ToDouble(e.NewValue);
                double tan = Math.Tan(o / 180d * Math.PI);
                double a = r / Math.Sqrt(1 + tan * tan);
                double b = a * tan;
                stopChange = true;
                self.Real = a;
                self.Imaginary = b;
            } else if (e.Property == RealProperty) {
                //self.real.Text = e.NewValue.ToString();
                if (stopChange) return;
                double b = self.Imaginary;
                double a = Convert.ToDouble(e.NewValue);
                double atan2 = Math.Atan2(b, a);
                double o = atan2 / Math.PI * 180d;
                double r = Math.Sqrt(a * a + b * b);
                stopChange = true;
                self.Module = r;
                self.Phase = o;
            } else if (e.Property == ImaginaryProperty) {
                //self.imag.Text = e.NewValue.ToString();
                if (stopChange) return;
                double a = self.Real;
                double b = Convert.ToDouble(e.NewValue);
                double atan2 = Math.Atan2(b, a);
                double o = atan2 / Math.PI * 180d;
                double r = Math.Sqrt(a * a + b * b);
                stopChange = true;
                self.Module = r;
                self.Phase = o;
            } else if (e.Property == IsPolarProperty) {
                self.pBox.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
                self.cBox.Visibility = !(bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
            stopChange = false;
        }

        public double Module {
            get { return (double)GetValue(ModuleProperty); }
            set { SetValue(ModuleProperty, value); }
        }

        public double Phase {
            get { return (double)GetValue(PhaseProperty); }
            set { SetValue(PhaseProperty, value); }
        }

        public double Real {
            get { return (double)GetValue(RealProperty); }
            set { SetValue(RealProperty, value); }
        }

        public double Imaginary {
            get { return (double)GetValue(ImaginaryProperty); }
            set { SetValue(ImaginaryProperty, value); }
        }

        public bool IsPolar {
            get { return (bool)GetValue(IsPolarProperty); }
            set { SetValue(IsPolarProperty, value); }
        }

        public Complex ToComplex() {
            return new Complex(Real, Imaginary);
        }

        public Polar ToPolar() {
            return new Polar(Module, Phase);
        }

        public override string ToString() {
            return IsPolar ? ToPolar().ToString() : ToComplex().ToString();
        }
    }
}
