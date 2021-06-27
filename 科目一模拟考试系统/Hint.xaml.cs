using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace 科目一模拟考试系统
{
    /// <summary>
    /// Hint.xaml 的交互逻辑
    /// </summary>
    public partial class Hint : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private void opacityAnimation(double startValue, double endValue, int durationInMilliseconds)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = startValue;//透明度初始值
            opacityAnimation.To = endValue;//透明度值
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            this.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }
        public Hint(string str)
        {
            InitializeComponent();
            this.HintLabel.Content = str;
            //this.Opacity = 0;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            //timer.Start();
            this.Show();
        }
        public Hint()
        {
            InitializeComponent();
            //this.Opacity = 0;
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = TimeSpan.FromMilliseconds(10);   //设置刷新的间隔时间
            //timer.Start();
            //this.Close();
        }

        public void ShowHint(string str, int showTimeInMilliseconds)
        {
            //counter = 0;
            this.HintLabel.Content = str;
            this.Opacity = 0;
            //flag = true;
            this.Show();
            //opacityAnimation(0, 1, 5000);
            opacityAnimation(1, 0, showTimeInMilliseconds);
            //if (counter >= 5)
            //{
            //    counter = 0;
            //    opacityAnimation(0.9, 1, showTimeInMilliseconds);
            //}
            //if (counter >= showTimeInMilliseconds / 100)
            //{
            //    counter = 0;
            //    opacityAnimation(1, 0, 1500);
            //}

        }

        public void ShowHint(string str)
        {
            //counter = 0;
            this.HintLabel.Content = str;
            this.Opacity = 0;
            //flag = true;
            this.Show();
            //opacityAnimation(0, 1, 2000);
            opacityAnimation(0.9, 0, 1500);
            //if (counter >= 5)
            //{
            //    counter = 0;
            //    opacityAnimation(0.9, 1, 500);
            //}
            //if (counter >= 5)
            //{
            //    counter = 0;
            //    opacityAnimation(1, 0, 1500);
            //}
        }

        //bool flag = true;
        //int counter = 0;
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    counter++;
        //    //if (this.Opacity < 1 && flag)
        //    //{
        //    //    this.Opacity += 0.10;
        //    //    if (this.Opacity > 0.75)
        //    //    {
        //    //        flag = false;
        //    //        counter = 0;
        //    //    }
        //    //}
        //    //else if (!flag && counter > 50)
        //    //{
        //    //    if (this.Opacity < 0)
        //    //    {
        //    //        //timer.Tick -= timer_Tick;
        //    //        //this.Close();                   
        //    //    }
        //    //    else
        //    //    {
        //    //        this.Opacity -= 0.03;
        //    //    }
        //    //}
        //}

        //this.ShowDialog();后执行
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Width = this.HintLabel.ActualWidth + 40;
            //this.WindowStartupLocation = WindowStartupLocation.Manual;
        }

    }
}
