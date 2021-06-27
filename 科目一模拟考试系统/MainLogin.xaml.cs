using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using System.Data.Common;
using System.Data;
using System.Windows.Threading;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using MySql.Data.MySqlClient;


namespace 科目一模拟考试系统
{

    public delegate void MyDel(string str);

    /// <summary>
    /// MainLogin.xaml 的交互逻辑
    /// </summary>
    public partial class MainLogin : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public class Test : INotifyPropertyChanged
        {
            public Test(double a = 1)
            {
                this.age = a;
            }
          
            private double age;

            public event PropertyChangedEventHandler PropertyChanged;

            public double getAge
            {
                get { return age; }
                set
                {
                    age = value;
                    if (PropertyChanged != null)
                    {
                        //给getAge赋值时触发事件
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("getAge"));
                    }
                }
            }

        }

        private string GetConnectionString()
        {
            XElement SqlConfig = XElement.Load("DbLinkConfig.xml");          
            string ConnectionString = "";
            foreach (var Element in SqlConfig.Elements("Option"))
            {
                ConnectionString += Element.Attribute("Type").Value + "=" + Element.Value + ";";
            }
            return ConnectionString;
        }

        //public Test stu1=new Test();
        public MainLogin()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            timer.Start();
            ////新建绑定类，引用为System.Windows.Data
            //Binding newbind = new Binding();
            ////绑定源为Student的实例stu1
            //newbind.Source = stu1;
            ////绑定属性为"getAge"
            //newbind.Path = new PropertyPath("getAge");
            ////设置绑定TextBox控件，显示在TextProperty中
            ////TextBox是System.Windows.Controls中的，不是System.Windows.Forms中的，需要注意
            //this.grid.Opacity.SetBinding(TextBox.TextProperty, newbind);
            //this.Opacity = 0;

            opacityAnimation(0, 1, 1500);
        }

        private void opacityAnimation(double startValue, double endValue, int durationInMilliseconds)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = startValue;//透明度初始值
            opacityAnimation.To = endValue;//透明度值
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            this.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }

        int counter = 0;
        bool closeflag = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (closeflag && counter == 10)
            {
                hint.Close();
                this.Close();
            }
            //if (this.Opacity < 1)
            //{
            //    this.Opacity += 0.06;
            //}
            //else
            //{
            //    timer.Tick -= timer_Tick;
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                                
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        Hint hint = new Hint();
        private void Login_Click(object sender, RoutedEventArgs e)
        {

            //hint.ShowHint("Hello World!");

            string IDNumber = this.Password.Text;
            if (this.UserName.Text.Length == 0)
            {
                hint.ShowHint("姓名不能为空！");
            }
            else if (IDNumber.Length != 18) //身份证号位数少与正常位数时运行
            {
                hint.ShowHint("身份证号位数错误！请重新输入！");
            }
            else
            {
                DbConnectionStringBuilder SqlStrBuilder = new DbConnectionStringBuilder();
                SqlStrBuilder.ConnectionString = GetConnectionString();
                SqlStrBuilder.Remove("Provider");
                using (MySqlConnection SqlConnect = new MySqlConnection(SqlStrBuilder.ConnectionString))
                {

                    SqlConnect.Open();
                    //hint = new Hint("数据库连接成功！");

                    string queryString = "select 姓名 from XY where 身份证号='" + IDNumber + "'";//身份证号唯一性
                    //string queryString = "select 姓名,身份证号 from XY";
                    DbCommand command = SqlConnect.CreateCommand();
                    command.CommandText = queryString;
                    command.CommandType = CommandType.Text;
                    DbDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        hint.ShowHint("身份证号不存在！");
                    }
                    else
                    {
                        bool flag = false;
                        string Uname = "";
                        while (reader.Read())
                        {
                            Uname = reader["姓名"].ToString();
                            if (this.UserName.Text == Uname)
                            {
                                flag = true;
                                //hint.ShowHint("登录成功！");
                                Conf_Info Conf_Info_Ins = new Conf_Info(IDNumber);
                                Conf_Info_Ins.Show();
                                counter = 0;
                                closeflag = true;
                                opacityAnimation(1, 0, 1000);
                                break;
                            }
                        }
                        if (!flag)
                        {
                            hint.ShowHint("用户名不存在！");
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void Close_B_Click(object sender, RoutedEventArgs e)
        {
            counter = 0;
            closeflag = true;
            opacityAnimation(1, 0, 1000);
            //Thread.Sleep(10000);
         
        }

        private void Close_B_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Close_B.Foreground = Brushes.Red;
        }


        private void Close_B_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close_B.Foreground = Brushes.White;
        }

        private void Password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex(@"[^0-9]");

            e.Handled = re.IsMatch(e.Text);
        }

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
        }

        private void UserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
        }

    }
}
