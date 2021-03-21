using System.Windows;
using System.Windows.Controls;

namespace ADAM {
    public enum Direction {
        Left, Top, Right, Bottom
    }

    public class LabeledControl : HeaderedContentControl {
        public LabeledControl() {
        }

        public static readonly DependencyProperty HeaderDirectionProperty = DependencyProperty.Register(
            "HeaderDirection", typeof(Direction), typeof(LabeledControl), new PropertyMetadata());

        public Direction HeaderDirection {
            get { return (Direction)GetValue(HeaderDirectionProperty); }
            set { SetValue(HeaderDirectionProperty, value); }
        }
    }
}
