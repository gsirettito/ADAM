using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ADAM {
    public class NumberBox : TextBox {
        private List<Key> keys = new List<Key>() {
            Key.C, Key.X, Key.V, Key.Tab,
            Key.Home, Key.End, Key.Enter, Key.Return,
            Key.Delete, Key.Decimal, Key.Left,
            Key.Right, Key.NumPad0, Key.NumPad1,
            Key.NumPad2, Key.NumPad3, Key.NumPad4, 
            Key.NumPad5, Key.NumPad6, Key.NumPad7, 
            Key.NumPad8, Key.NumPad9, Key.OemMinus,
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4,
            Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.Back, Key.Subtract, Key.OemPeriod };

        protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (keys.Contains(e.Key)) {
                double clipb;
                if (e.Key == Key.V && (Keyboard.Modifiers != ModifierKeys.Control
                    || !double.TryParse(Clipboard.GetText(), out clipb)
                    || !double.TryParse(Text.Insert(CaretIndex, Clipboard.GetText()), out clipb))) e.Handled = true;
                if (e.Key == Key.C && Keyboard.Modifiers != ModifierKeys.Control) e.Handled = true;
                if (e.Key == Key.X && Keyboard.Modifiers != ModifierKeys.Control) e.Handled = true;
                if (e.Key == Key.OemMinus || e.Key == Key.Subtract) {// menos
                    if (Text.Contains("-")) e.Handled = true;
                    if (Text.Length != 0 && CaretIndex > 0) e.Handled = true;
                } else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal) {// punto
                    if (Text.Length > 0 && Text[0] == '-' && CaretIndex == 0) e.Handled = true;
                    if (Text.Contains(".")) e.Handled = true;
                }
            } else e.Handled = true;
        }
    }
}
