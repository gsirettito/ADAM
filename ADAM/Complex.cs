using System;
using System.Windows;

namespace ADAM {
    public class Complex {
        public Complex(double real, double imaginary) {
            Real = real;
            Imaginary = imaginary;
        }

        public Complex(Point point) {
            Real = point.X;
            Imaginary = point.Y;
        }

        public Complex(Polar polar) {
            double r = polar.Module;
            double o = polar.Phase;
            //double tan = Math.Tan(o / 180d * Math.PI);
            //double a = r / Math.Sqrt(1 + tan * tan);
            //double b = a * tan;
            double a = r * Math.Cos(o / 180d * Math.PI);
            double b = r * Math.Sin(o / 180d * Math.PI);
            Real = a;
            Imaginary = b;
        }

        public double Real { get; set; }

        public double Imaginary { get; set; }

        public static Complex operator +(Complex c1, Complex c2) {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator +(Complex c, double d) {
            return new Complex(c.Real + d, c.Imaginary);
        }

        public static Complex operator +(double d, Complex c) {
            return new Complex(d + c.Real, c.Imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2) {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex operator -(Complex c, double d) {
            return new Complex(c.Real - d, c.Imaginary);
        }

        public static Complex operator -(double d, Complex c) {
            return new Complex(d - c.Real, c.Imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2) {
            return new Complex(c1.ToPolar() * c2.ToPolar());
        }

        public static Complex operator *(Complex c, double d) {
            return new Complex(c.Real * d, c.Imaginary * d);
        }

        public static Complex operator *(double d, Complex c) {
            return new Complex(d * c.Real, d * c.Imaginary);
        }

        public static Complex operator /(Complex c1, Complex c2) {
            return new Complex(c1.ToPolar() / c2.ToPolar());
        }

        public static Complex operator /(Complex c, double d) {
            return new Complex(c.Real / d, c.Imaginary / d);
        }

        public static Complex operator /(double d, Complex c) {
            return new Complex(d / c.Real, d / c.Imaginary);
        }

        public Complex Conjugate() {
            return new Complex(Real, -Imaginary);
        }

        public Polar ToPolar() {
            return new Polar(this);
        }

        public override string ToString() {
            return string.Format("{0} " + ((Imaginary >= 0) ? "+" : "") + " {1}i", Real, Imaginary);
        }
    }
}
