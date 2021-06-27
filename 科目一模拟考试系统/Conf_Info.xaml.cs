using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Data.Common;
using System.Data;
using System.Windows.Media.Animation;
using MySql.Data.MySqlClient;

namespace 科目一模拟考试系统
{
    /// <summary>
    /// Conf_Info.xaml 的交互逻辑
    /// </summary>
    public partial class Conf_Info : Window
    {
        Hint hint = new Hint();
        DispatcherTimer timer = new DispatcherTimer();
        private string IDNumber;
        private void opacityAnimation(double startValue, double endValue, int durationInMilliseconds)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = startValue;//透明度初始值
            opacityAnimation.To = endValue;//透明度值
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            this.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }
        public Conf_Info(string IDNumber)
        {
            this.IDNumber = IDNumber;
            InitializeComponent();
            hint.ShowHint("登录成功！", 2000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            timer.Start();
            this.Opacity = 0;

            opacityAnimation(0, 1, 2000);
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
            //    //timer.Tick -= timer_Tick;
            //}

            this.DateLabel.Content = DateTime.Now.ToString();
        }      

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        string name;
        string sex;
        string age;
        string photoUri;
        ImageSource photo;
        DataTable table = new DataTable();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnectionStringBuilder SqlStrBuilder = new DbConnectionStringBuilder();
            SqlStrBuilder.ConnectionString = GetConnectionString();
            SqlStrBuilder.Remove("Provider");
            using (MySqlConnection SqlConnect = new MySqlConnection(SqlStrBuilder.ConnectionString))
            {

                SqlConnect.Open();
                //hint = new Hint("数据库连接成功！");
                //string queryString = "select physical_name from sys.master_files where name = 'test'";
                string queryString = "select * from XY where 身份证号=" + IDNumber;//身份证号唯一性
                DbCommand command = SqlConnect.CreateCommand();
                command.CommandText = queryString;
                command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                //reader.Read();
                photoUri = "http://118.89.18.226/image/";
                //reader.Close();


                //queryString = "select * from XY where 身份证号=" + IDNumber;//身份证号唯一性
                //string queryString = "select 姓名,身份证号 from XY";              
                //command.CommandText = queryString;
                //command.CommandType = CommandType.Text;
                //reader = command.ExecuteReader();
                reader.Read();
                name = reader.GetString(0);
                sex = reader.GetString(1);
                age = reader.GetInt32(2).ToString();
                photo = new BitmapImage(new Uri(photoUri + reader["照片"].ToString().Replace(@"E:\大二下学期\C#\大作业\", ""), UriKind.Absolute));
                GetName.Content = name;
                GetSex.Content = sex;
                GetAge.Content = age;
                GetNativePlace.Content = reader.GetString(7);
                GetNation.Content = reader.GetString(4) + "族";
                GetIDNumber.Content = reader.GetString(5);
                GetPhoneNumber.Content = reader.GetString(6);
                this.GetPhoto.Source = photo;
                //MessageBox.Show(load.Replace("test.mdf", reader["照片"].ToString().Replace(@"E:\大二下学期\C#\大作业\", "")));
                reader.Close();

                MySqlDataAdapter adpter = new MySqlDataAdapter("SELECT * FROM kt ORDER BY RAND() LIMIT 25", SqlConnect);//SQL语句，任意选择二十条数据
                adpter.Fill(table);

            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            counter = 0;
            closeflag = true;
            opacityAnimation(1, 0, 1000);
        }

        private void BeginTest_Button_Click(object sender, RoutedEventArgs e)
        {
            Examination Examination_Ins = new Examination(IDNumber, name, sex, age, photoUri, photo, table);
            Examination_Ins.Show();
            counter = 0;
            closeflag = true;
            opacityAnimation(1, 0, 1000);
        }
    }
}
