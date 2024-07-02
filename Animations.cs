using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Nexus
{
    public partial class Animations
    {
        private Storyboard StoryBoard = new Storyboard();
        private TimeSpan duration2 = TimeSpan.FromMilliseconds(50.0);
        private IEasingFunction Smooth { get; set; } = new QuarticEase
        {
            EasingMode = EasingMode.EaseInOut
        };
        public void ObjectShift(DependencyObject Object, Thickness Get, Thickness Set)
        {
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation
            {
                From = Get,
                To = Set,
                Duration = duration2,
                EasingFunction = Smooth
            };
            Storyboard.SetTarget(thicknessAnimation, Object);
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            StoryBoard.Children.Add(thicknessAnimation);
            StoryBoard.Begin();
            StoryBoard.Children.Remove(thicknessAnimation);
        }

        public void Fade(DependencyObject Object)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(duration2)
            };
            Storyboard.SetTarget(doubleAnimation, Object);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity", 1));
            StoryBoard.Children.Add(doubleAnimation);
            StoryBoard.Begin();
            StoryBoard.Children.Remove(doubleAnimation);
        }

        public void FadeOut(DependencyObject Object)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(duration2)
            };
            Storyboard.SetTarget(doubleAnimation, Object);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity", 1));
            StoryBoard.Children.Add(doubleAnimation);
            StoryBoard.Begin();
            StoryBoard.Children.Remove(doubleAnimation);
        }

    }
}
