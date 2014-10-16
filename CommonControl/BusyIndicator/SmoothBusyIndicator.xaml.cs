using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CommonControl.BusyIndicator
{
    /// <summary>
    /// Interaction logic for SmoothBusyIndicator.xaml
    /// </summary>
    public partial class SmoothBusyIndicator : UserControl
    {
        private Storyboard m_LoadingStoryboard;

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool),
            typeof(SmoothBusyIndicator), new PropertyMetadata(false, IsBusyPropertyChangedCallback));
        public bool IsBusy
        {
            get { return (bool)this.GetValue(IsBusyProperty); }
            set
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Render, new ThreadStart(() =>
                {
                    this.SetValue(IsBusyProperty, value);
                }));
            }
        }

        private static void IsBusyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool bNewValue = (bool)e.NewValue;
            SmoothBusyIndicator targetIndicator = d as SmoothBusyIndicator;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Render, new ThreadStart(() =>
            {
                if (true == bNewValue)
                {
                    targetIndicator.Start();
                }
                else
                {
                    targetIndicator.Stop();
                }
            }));
        }
        public SmoothBusyIndicator()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLoadingStoryboard();   
        }

        private void InitializeLoadingStoryboard()
        {
            double dWidthOfEllipse = e1.ActualWidth;
            double dHalfWidthOfEllipse = dWidthOfEllipse / 2.0;
            double dHeightOfEllipse = e1.ActualHeight;
            double dFirstEllipseBaseLeft = (EllipseContainer.ActualWidth * (1.0 / 4.0)) - dHalfWidthOfEllipse;
            double dSecondEllipseBaseLeft = (EllipseContainer.ActualWidth * (2.0 / 4.0)) - dHalfWidthOfEllipse;
            double dThirdEllipseBaseLeft = (EllipseContainer.ActualWidth * (3.0 / 4.0)) - dHalfWidthOfEllipse;
            double dEllipseBaseTop = (EllipseContainer.ActualHeight - dHeightOfEllipse) / 2.0;

            Canvas.SetLeft(e1, dFirstEllipseBaseLeft);
            Canvas.SetLeft(e2, dSecondEllipseBaseLeft);
            Canvas.SetLeft(e3, dThirdEllipseBaseLeft);

            m_LoadingStoryboard = new Storyboard();
            m_LoadingStoryboard.RepeatBehavior = RepeatBehavior.Forever;

            EasingDoubleKeyFrame Frame1 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2)));
            EasingDoubleKeyFrame Frame2 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25)));
            EasingDoubleKeyFrame Frame3 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3)));
            EasingDoubleKeyFrame Frame4 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.35)));
            EasingDoubleKeyFrame Frame5 = new EasingDoubleKeyFrame(dFirstEllipseBaseLeft, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8)));

            DoubleAnimationUsingKeyFrames E1UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E1UsingKeyFrames.KeyFrames.Add(Frame1);
            E1UsingKeyFrames.KeyFrames.Add(Frame2);
            E1UsingKeyFrames.KeyFrames.Add(Frame3);
            E1UsingKeyFrames.KeyFrames.Add(Frame4);
            E1UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E1UsingKeyFrames);
            Storyboard.SetTargetProperty(E1UsingKeyFrames, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTarget(E1UsingKeyFrames, e1);

            Frame1 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)));
            Frame2 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.45)));
            Frame3 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)));
            Frame4 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55)));
            Frame5 = new EasingDoubleKeyFrame(dSecondEllipseBaseLeft, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)));

            DoubleAnimationUsingKeyFrames E2UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E2UsingKeyFrames.KeyFrames.Add(Frame1);
            E2UsingKeyFrames.KeyFrames.Add(Frame2);
            E2UsingKeyFrames.KeyFrames.Add(Frame3);
            E2UsingKeyFrames.KeyFrames.Add(Frame4);
            E2UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E2UsingKeyFrames);
            Storyboard.SetTargetProperty(E2UsingKeyFrames, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTarget(E2UsingKeyFrames, e2);

            Frame1 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6)));
            Frame2 = new EasingDoubleKeyFrame(-dWidthOfEllipse, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.65)));
            Frame3 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7)));
            Frame4 = new EasingDoubleKeyFrame(EllipseContainer.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.75)));
            Frame5 = new EasingDoubleKeyFrame(dThirdEllipseBaseLeft, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)));

            DoubleAnimationUsingKeyFrames E3UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E3UsingKeyFrames.KeyFrames.Add(Frame1);
            E3UsingKeyFrames.KeyFrames.Add(Frame2);
            E3UsingKeyFrames.KeyFrames.Add(Frame3);
            E3UsingKeyFrames.KeyFrames.Add(Frame4);
            E3UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E3UsingKeyFrames);
            Storyboard.SetTargetProperty(E3UsingKeyFrames, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTarget(E3UsingKeyFrames, e3);

            Frame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2)));
            Frame2 = new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8)));

            E1UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E1UsingKeyFrames.KeyFrames.Add(Frame1);
            E1UsingKeyFrames.KeyFrames.Add(Frame2);

            m_LoadingStoryboard.Children.Add(E1UsingKeyFrames);
            Storyboard.SetTargetProperty(E1UsingKeyFrames, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(E1UsingKeyFrames, e1);

            Frame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)));
            Frame2 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55)));
            Frame3 = new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)));

            E2UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E2UsingKeyFrames.KeyFrames.Add(Frame1);
            E2UsingKeyFrames.KeyFrames.Add(Frame2);
            E2UsingKeyFrames.KeyFrames.Add(Frame3);

            m_LoadingStoryboard.Children.Add(E2UsingKeyFrames);
            Storyboard.SetTargetProperty(E2UsingKeyFrames, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(E2UsingKeyFrames, e2);

            Frame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6)));
            Frame2 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.75)));
            Frame3 = new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)));

            E3UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E3UsingKeyFrames.KeyFrames.Add(Frame1);
            E3UsingKeyFrames.KeyFrames.Add(Frame2);
            E3UsingKeyFrames.KeyFrames.Add(Frame3);

            m_LoadingStoryboard.Children.Add(E3UsingKeyFrames);
            Storyboard.SetTargetProperty(E3UsingKeyFrames, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(E3UsingKeyFrames, e3);

            Frame1 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2)));
            Frame2 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25)));
            Frame3 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3)));
            Frame4 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.35)));
            Frame5 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8)));

            E1UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E1UsingKeyFrames.KeyFrames.Add(Frame1);
            E1UsingKeyFrames.KeyFrames.Add(Frame2);
            E1UsingKeyFrames.KeyFrames.Add(Frame3);
            E1UsingKeyFrames.KeyFrames.Add(Frame4);
            E1UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E1UsingKeyFrames);
            Storyboard.SetTargetProperty(E1UsingKeyFrames, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(E1UsingKeyFrames, e1);

            Frame1 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)));
            Frame2 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.45)));
            Frame3 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)));
            Frame4 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55)));
            Frame5 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)));

            E2UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E2UsingKeyFrames.KeyFrames.Add(Frame1);
            E2UsingKeyFrames.KeyFrames.Add(Frame2);
            E2UsingKeyFrames.KeyFrames.Add(Frame3);
            E2UsingKeyFrames.KeyFrames.Add(Frame4);
            E2UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E2UsingKeyFrames);
            Storyboard.SetTargetProperty(E2UsingKeyFrames, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(E2UsingKeyFrames, e2);

            Frame1 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6)));
            Frame2 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.65)));
            Frame3 = new EasingDoubleKeyFrame(-1000, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7)));
            Frame4 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.75)));
            Frame5 = new EasingDoubleKeyFrame(dEllipseBaseTop, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)));

            E3UsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            E3UsingKeyFrames.KeyFrames.Add(Frame1);
            E3UsingKeyFrames.KeyFrames.Add(Frame2);
            E3UsingKeyFrames.KeyFrames.Add(Frame3);
            E3UsingKeyFrames.KeyFrames.Add(Frame4);
            E3UsingKeyFrames.KeyFrames.Add(Frame5);

            m_LoadingStoryboard.Children.Add(E3UsingKeyFrames);
            Storyboard.SetTargetProperty(E3UsingKeyFrames, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(E3UsingKeyFrames, e3);
            Start();
            Stop();
        }

        private void Start()
        {
            if (null == this.m_LoadingStoryboard) { this.InitializeLoadingStoryboard(); }
            this.m_LoadingStoryboard.Begin(this, true);
        }

        private void Stop()
        {
            if (null != this.m_LoadingStoryboard) { this.m_LoadingStoryboard.Stop(this); }
        }
    }
}
