using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DateAppPC.EXPL.ANIMATIONS {
    public class SimpleAnimation {
        public SimpleAnimation() {
            DoubleAnimation = new DoubleAnimation();
        }
        private DoubleAnimation DoubleAnimation { get; set; }
        public void MoveGrid(Grid grid, int firstPos, int secondPos, int duration) {
            DoubleAnimation.From       = firstPos;
            DoubleAnimation.To         = secondPos;
            DoubleAnimation.Duration = TimeSpan.FromSeconds(duration);
            grid.BeginAnimation(FrameworkElement.HeightProperty, DoubleAnimation);
        }
    }
}