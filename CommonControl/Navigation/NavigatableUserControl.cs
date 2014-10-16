using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CommonControl.Navigation
{
    public enum NavigationDirection
    { 
        None,
        FadeToLeft,
        FadeToRight,
        AppearToLeft,
        AppearToRight
    }
    public class NavigatableUserControl : UserControl
    {
        public NavigationDirection LoadDirection { get; set; }
        public static readonly RoutedEvent FadeToLeftEvent = EventManager.RegisterRoutedEvent(
            "FadeToLeft", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NavigatableUserControl));

        public static readonly RoutedEvent FadeToRightEvent = EventManager.RegisterRoutedEvent(
            "FadeToRight", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NavigatableUserControl));

        public static readonly RoutedEvent AppearToLeftEvent = EventManager.RegisterRoutedEvent(
            "AppearToLeft", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NavigatableUserControl));

        public static readonly RoutedEvent AppearToRightEvent = EventManager.RegisterRoutedEvent(
            "AppearToRight", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NavigatableUserControl));

        public NavigatableUserControl() 
        {
            this.Style = (Style)this.TryFindResource("NavigatableUserControlStyle");
            this.Loaded += NavigatableUserControl_Loaded;
        }

        private void NavigatableUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (this.LoadDirection)
            {
                case NavigationDirection.FadeToLeft: this.RaiseFadeToLeftEvent(); break;
                case NavigationDirection.FadeToRight: this.RaiseFadeToRightEvent(); break;
                case NavigationDirection.AppearToLeft: this.RaiseAppearToLeftEvent(); break;
                case NavigationDirection.AppearToRight: this.RaiseAppearToRightEvent(); break;
                default: break;
            }
        }
        public event RoutedEventHandler FadeToLeft
        {
            add { AddHandler(FadeToLeftEvent, value); }
            remove { RemoveHandler(FadeToLeftEvent, value); }
        }

        public event RoutedEventHandler FadeToRight
        {
            add { AddHandler(FadeToRightEvent, value); }
            remove { RemoveHandler(FadeToRightEvent, value); }
        }

        public event RoutedEventHandler AppearToLeft
        {
            add { AddHandler(AppearToLeftEvent, value); }
            remove { RemoveHandler(AppearToLeftEvent, value); }
        }

        public event RoutedEventHandler AppearToRight
        {
            add { AddHandler(AppearToRightEvent, value); }
            remove { RemoveHandler(AppearToRightEvent, value); }
        }
        private void RaiseFadeToLeftEvent()
        {
            var args = new RoutedEventArgs(FadeToLeftEvent);
            RaiseEvent(args);
        }

        private void RaiseAppearToLeftEvent()
        {
            var args = new RoutedEventArgs(AppearToLeftEvent);
            RaiseEvent(args);
        }

        private void RaiseFadeToRightEvent()
        {
            var args = new RoutedEventArgs(FadeToRightEvent);
            RaiseEvent(args);
        }

        private void RaiseAppearToRightEvent()
        {
            var args = new RoutedEventArgs(AppearToRightEvent);
            RaiseEvent(args);
        }
    }
}
