using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ADAM {
    public class Unit {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Factor { get; set; }
    }

    public class UnitCollection : List<Unit> { }

    /// <summary>
    /// Lógica de interacción para UnitBox.xaml
    /// </summary>
    public partial class UnitBox : UserControl {
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
        private UnitCollection unitCollection;

        public UnitBox() {
            InitializeComponent();
            unitCollection = new UnitCollection();
            units.SelectionChanged += units_SelectionChanged;
            units.ItemsSource = unitCollection;
        }

        public UnitCollection UnitItems {
            get { return unitCollection; }
            set {
                unitCollection = value;
                units.ItemsSource = null;
                units.ItemsSource = value;
            }
        }

        public int SelectedIndex {
            get { return units.SelectedIndex; }
            set { units.SelectedIndex = value; }
        }

        public double Value {
            get { return Convert.ToDouble(this.value.Text); }
            set { this.value.Text = value.ToString(); }
        }

        private void units_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (SelectionChanged != null) {
                SelectionChanged(this, e);
            }
        }
    }
}
