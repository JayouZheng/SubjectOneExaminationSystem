using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace 科目一模拟考试系统
{
    /// <summary>
    /// QAnswer.xaml 的交互逻辑
    /// </summary>
    public partial class QAnswer : Window
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

        public QAnswer(string RightAnswer, string RightAnswer_Content, string Analysis)
        {
            InitializeComponent();

            this.RightAnswer.Content = RightAnswer;
            this.RightAnswer_Content.Content = RightAnswer_Content;
            this.Analysis.Text = Analysis;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1000);   //设置刷新的间隔时间
            timer.Start();
            this.Opacity = 0;
            opacityAnimation(0, 1, 1500);

            DoubleAnimation dou = new DoubleAnimation(0, 823, TimeSpan.FromMilliseconds(800));
            //this.BeginAnimation(Window.LeftProperty, dou);
            this.BeginAnimation(Window.HeightProperty, dou);
        }

        int counter = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 4)
            {
                opacityAnimation(1, 0, 2000);
            }
            if (counter == 6)//6秒
            {
                hint.Close();
                this.Close();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
