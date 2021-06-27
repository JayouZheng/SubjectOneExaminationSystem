using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace 科目一模拟考试系统
{
    /// <summary>
    /// END.xaml 的交互逻辑
    /// </summary>
    public partial class END : Window
    {
        Hint hint = new Hint();
        DispatcherTimer timer = new DispatcherTimer();
        private void opacityAnimation(double startValue, double endValue, int durationInMilliseconds)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = startValue;//透明度初始值
            opacityAnimation.To = endValue;//透明度值
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            this.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }

        public END(string IDNumber, string name, ImageSource photo,int Score)
        {
            InitializeComponent();
            this.GetIDNumber.Content = IDNumber;
            this.GetName.Content = name;
            this.GetPhoto.Source = photo;
            this.TotalScores_Label.Content = Score;
            
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            timer.Start();
            this.Opacity = 0;
            opacityAnimation(0, 1, 1500);
            if (Score >= 90)
            {
                this.Result.Content = "及格";
            }
            else
            {
                this.Result.Content = "不及格";
            }
            this.Result.Opacity = 0;
        }

        int counter = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 100)
            {
                opacityAnimation(1, 0, 500);
                DoubleAnimation dou = new DoubleAnimation(822, 0, TimeSpan.FromMilliseconds(500));
                //this.BeginAnimation(Window.LeftProperty, dou);
                this.BeginAnimation(Window.HeightProperty, dou);
            }
            if (counter == 106)
            {
                hint.Close();
                this.Close();
            }
            this.GetDate.Content = DateTime.Now.ToString();
            if (this.Result.Opacity < 1)
            {
                this.Result.Opacity += 0.02;
            }
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            counter = 99;
        }
    }
}
