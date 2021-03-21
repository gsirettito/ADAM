using System;

namespace ADAM {
    public class Polar {
        public Polar(double module, double phase) {
            Module = module;
            Phase = phase;
        }

        public Polar(Complex complex) {
            double b = complex.Imaginary;
            double a = complex.Real;
            double atan2 = Math.Atan2(b, a);
            double o = atan2 / Math.PI * 180d;
            double r = Math.Sqrt(a * a + b * b);
            Module = r;
            Phase = o;
        }

        public double Module { get; set; }

        public double Phase { get; set; }

        public static Polar operator +(Polar p1, Polar p2) {
            return new Polar(p1.ToComplex() + p2.ToComplex());
        }

        public static Polar operator +(Polar p, double d) {
            return new Polar(p.ToComplex() + d);
        }

        public static Polar operator +(double d, Polar p) {
            return new Polar(d + p.ToComplex());
        }

        public static Polar operator -(Polar p1, Polar p2) {
            return new Polar(p1.ToComplex() - p2.ToComplex());
        }

        public static Polar operator -(Polar p, double d) {
            return new Polar(p.ToComplex() - d);
        }

        public static Polar operator -(double d, Polar p) {
            return new Polar(d - p.ToComplex());
        }

        public static Polar operator *(Polar p1, Polar p2) {
            return new Polar(p1.Module * p2.Module, p1.Phase + p2.Phase);
        }

        public static Polar operator *(Polar p, double d) {
            return new Polar(p.Module * d, p.Phase);
        }

        public static Polar operator *(double d, Polar p) {
            return new Polar(d * p.Module, p.Phase);
        }

        public static Polar operator /(Polar p1, Polar p2) {
            return new Polar(p1.Module / p2.Module, p1.Phase - p2.Phase);
        }

        public static Polar operator /(Polar p, double d) {
            return new Polar(p.Module / d, p.Phase);
        }

        public static Polar operator /(double d, Polar p) {
            return new Polar(d / p.Module, p.Phase);
        }

        public Polar Reciproco() {
            return new Polar(1d / Module, -Phase);
        }

        public Polar Conjugate() {
            return new Polar(Module, -Phase);
        }

        public Complex ToComplex() {
            return new Complex(this);
        }

        public override string ToString() {
            return string.Format("{0} ∠ {1}°", Module, Phase);
        }
    }
}
