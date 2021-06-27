using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Data;

namespace 科目一模拟考试系统
{
    /// <summary>
    /// Examination.xaml 的交互逻辑
    /// </summary>
    public partial class Examination : Window
    {
        Hint hint = new Hint();
        DispatcherTimer timer_UpdateUI = new DispatcherTimer();
        DispatcherTimer timer_Counter = new DispatcherTimer();
        DataTable table = new DataTable();
        string photoUri;

        private void ShowQuestion(int QuestionID)
        {
            this.GetQuestion.Text = (QuestionID + 1).ToString() + "、" + table.Rows[QuestionID]["题目"].ToString();
            this.Selection_A.Text = table.Rows[QuestionID]["选项A"].ToString().Replace("A、", "");
            this.Selection_B.Text = table.Rows[QuestionID]["选项B"].ToString().Replace("B、", "");

            if (table.Rows[QuestionID]["选项C"].ToString() != "")
            {
                this.Selection_C.Text = table.Rows[QuestionID]["选项C"].ToString().Replace("C、", "");
                if (!this.Label_C.IsVisible)
                {
                    this.Label_C.Visibility = Visibility.Visible;
                    this.Selection_C.Visibility = Visibility.Visible;
                }
            }
            else
            {
                this.Label_C.Visibility = Visibility.Hidden;
                this.Selection_C.Visibility = Visibility.Hidden;
            }

            if (table.Rows[QuestionID]["选项D"].ToString() != "")
            {
                this.Selection_D.Text = table.Rows[QuestionID]["选项D"].ToString().Replace("D、", "");
                if (!this.Label_D.IsVisible)
                {
                    this.Label_D.Visibility = Visibility.Visible;
                    this.Selection_D.Visibility = Visibility.Visible;
                }
            }
            else
            {
                this.Label_D.Visibility = Visibility.Hidden;
                this.Selection_D.Visibility = Visibility.Hidden;
            }

            if (table.Rows[QuestionID]["题目图片"].ToString() != "")
            {
                this.ImageforTest.Source = new BitmapImage(new Uri(photoUri + table.Rows[QuestionID]["题目图片"].ToString().Replace(@"E:\大二下学期\C#\大作业\", ""), UriKind.Absolute));
                if (!this.ImageforTest.IsVisible)
                {
                    this.ImageforTest.Visibility = Visibility.Visible;
                }
            }
            else
            {
                this.ImageforTest.Visibility = Visibility.Hidden;
            }
        }
        private void ShowQAnswer(int QuestionID)
        {
            string RightAnswer = this.table.Rows[QuestionID]["正确答案"].ToString();
            string Analysis = this.table.Rows[QuestionID]["解析"].ToString();
            string RightAnswer_Content = this.table.Rows[QuestionID]["选项" + RightAnswer].ToString().Replace(RightAnswer + "、", "");
            QAnswer qAnswer = new QAnswer(RightAnswer, RightAnswer_Content, Analysis);
            qAnswer.Show();
        }


        private void opacityAnimation(double startValue, double endValue, int durationInMilliseconds)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = startValue;//透明度初始值
            opacityAnimation.To = endValue;//透明度值
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            this.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }

        Label[] QID_Nums = new Label[25];
        ImageBrush imageCurrent_Gray = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/Current_Gray.png", UriKind.Absolute)));
        ImageBrush imageCurrent_Blue = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/Current_Blue.png", UriKind.Absolute)));
        ImageBrush imageCorrect_Blue = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/Correct_Blue.png", UriKind.Absolute)));
        ImageBrush imageIncorrect_Red = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/Incorrect_Red.png", UriKind.Absolute)));
        string IDNumber;
        string name;
        ImageSource photo;
        public Examination(string IDNumber, string name, string sex, string age, string photoUri, ImageSource photo, DataTable table)
        {
            InitializeComponent();

            this.IDNumber = IDNumber;
            this.name = name;
            this.photo = photo;

            this.GetIDNumber.Content = IDNumber;
            this.GetName.Content = name;
            this.GetSex.Content = sex;
            this.GetAge.Content = age;
            this.photoUri = photoUri;
            this.GetPhoto.Source = photo;
            this.table = table;
            this.ShowQuestion(0);
            hint.ShowHint("考试开始！", 2000);
            timer_UpdateUI.Tick += new EventHandler(timer_Tick);
            timer_UpdateUI.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            timer_UpdateUI.Start();

            timer_Counter.Tick += new EventHandler(timer_Counter_Tick);
            timer_Counter.Interval = TimeSpan.FromMilliseconds(100);   //设置刷新的间隔时间
            timer_Counter.Start();

            QID_Nums[0] = this.QID_01;
            QID_Nums[1] = this.QID_02;
            QID_Nums[2] = this.QID_03;
            QID_Nums[3] = this.QID_04;
            QID_Nums[4] = this.QID_05;
            QID_Nums[5] = this.QID_06;
            QID_Nums[6] = this.QID_07;
            QID_Nums[7] = this.QID_08;
            QID_Nums[8] = this.QID_09;
            QID_Nums[9] = this.QID_10;
            QID_Nums[10] = this.QID_11;
            QID_Nums[11] = this.QID_12;
            QID_Nums[12] = this.QID_13;
            QID_Nums[13] = this.QID_14;
            QID_Nums[14] = this.QID_15;
            QID_Nums[15] = this.QID_16;
            QID_Nums[16] = this.QID_17;
            QID_Nums[17] = this.QID_18;
            QID_Nums[18] = this.QID_19;
            QID_Nums[19] = this.QID_20;
            QID_Nums[20] = this.QID_21;
            QID_Nums[21] = this.QID_22;
            QID_Nums[22] = this.QID_23;
            QID_Nums[23] = this.QID_24;
            QID_Nums[24] = this.QID_25;

            foreach (var x in QID_Nums)
            {
                x.Background = null;
            }

            this.QID_01.Background = imageCurrent_Gray;
            this.AnsweredNums_Label.Content = "0";
            this.TotalScores_Label.Content = "0";

            opacityAnimation(0, 1, 2000);
        }

        int counter = 0;
        int countDown = 1200;
        int countDown_Minutes = 0;
        int countDown_Seconds = 0;
        private void timer_Counter_Tick(object sender, EventArgs e)
        {
            

            counter++;
            if (closeflag && counter == 10)
            {
                hint.Close();
                this.Close();
            }
            if (countDown > 0)
            {
                countDown--;
            }
  
        }

        
        bool closeflag = false;
        bool setCursorflag = false;
        bool flagtimeout = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (countDown == 0 && !flagtimeout)
            {
                flagtimeout = true;
                END end = new END(this.IDNumber, this.name, this.photo, this.Score);
                end.Show();
                counter = 0;
                closeflag = true;
                opacityAnimation(1, 0, 1000);
            }

            this.GetDate.Content = DateTime.Now.ToString();
            //this.Conf_S.Content = "选择：" + selection;

            if (!setCursorflag && clickflag)
            {
                setCursorflag = true;
                this.Selection_A.Cursor = Cursors.Arrow;
                this.Selection_B.Cursor = Cursors.Arrow;
                this.Selection_C.Cursor = Cursors.Arrow;
                this.Selection_D.Cursor = Cursors.Arrow;
                this.Label_A.Cursor = Cursors.Arrow;
                this.Label_B.Cursor = Cursors.Arrow;
                this.Label_C.Cursor = Cursors.Arrow;
                this.Label_D.Cursor = Cursors.Arrow;
            }

            countDown_Minutes = countDown / 600;
            countDown_Seconds = countDown % 600 / 10;
            if (countDown_Minutes < 10 && countDown_Seconds > 10)
            {
                this.Countdown_Label.Content = "0" + countDown_Minutes.ToString() + ":" + countDown_Seconds.ToString();
            }
            else if (countDown_Minutes > 10 && countDown_Seconds < 10)
            {
                this.Countdown_Label.Content = countDown_Minutes.ToString() + ":0" + countDown_Seconds.ToString();
            }
            else if (countDown_Minutes < 10 && countDown_Seconds < 10)
            {
                this.Countdown_Label.Content = "0" + countDown_Minutes.ToString() + ":0" + countDown_Seconds.ToString();
            }
            else
            {
                this.Countdown_Label.Content = countDown_Minutes.ToString() + ":" + countDown_Seconds.ToString();
            }
            
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            counter = 0;
            closeflag = true;
            opacityAnimation(1, 0, 1000);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        bool clickflag = false;
        string selection = "";
        SolidColorBrush colorBlue = new SolidColorBrush(Color.FromArgb(255, 0, 162, 255));//#FF00A2FF
        ImageBrush imageSelected = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/Selected.png", UriKind.Absolute)));
        ImageBrush imageUnSelected = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/科目一模拟考试系统;component/Resources/UnSelected.png", UriKind.Absolute)));
        private void Label_A_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_A.Foreground = colorBlue;
                this.Label_A.Foreground = colorBlue;
            }
        }

        private void Label_A_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_A.Foreground = Brushes.White;
                this.Label_A.Foreground = Brushes.White;
            }
        }

        private void Label_A_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.ImageforTest.Source = new BitmapImage(new Uri("77.jpg", UriKind.Relative));

            if (!clickflag)
            {
                this.Label_A.Background = imageSelected;
                selection = "A";
            }
            clickflag = true;
        }

        private void Selection_A_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_A.Foreground = colorBlue;
                this.Label_A.Foreground = colorBlue;
            }
        }

        private void Selection_A_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_A.Foreground = Brushes.White;
                this.Label_A.Foreground = Brushes.White;
            }
        }

        private void Selection_A_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_A.Background = imageSelected;
                selection = "A";
            }
            clickflag = true;
        }

        private void Label_B_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_B.Foreground = colorBlue;
                this.Label_B.Foreground = colorBlue;
            }
        }

        private void Label_B_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_B.Foreground = Brushes.White;
                this.Label_B.Foreground = Brushes.White;
            }
        }

        private void Label_B_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_B.Background = imageSelected;
                selection = "B";
            }
            clickflag = true;
        }

        private void Selection_B_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_B.Foreground = colorBlue;
                this.Label_B.Foreground = colorBlue;
            }
        }

        private void Selection_B_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_B.Foreground = Brushes.White;
                this.Label_B.Foreground = Brushes.White;
            }
        }

        private void Selection_B_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_B.Background = imageSelected;
                selection = "B";
            }
            clickflag = true;
        }

        private void Label_C_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_C.Foreground = colorBlue;
                this.Label_C.Foreground = colorBlue;
            }
        }

        private void Label_C_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_C.Foreground = Brushes.White;
                this.Label_C.Foreground = Brushes.White;
            }
        }

        private void Label_C_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_C.Background = imageSelected;
                selection = "C";
            }
            clickflag = true;
        }

        private void Selection_C_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_C.Foreground = colorBlue;
                this.Label_C.Foreground = colorBlue;
            }
        }

        private void Selection_C_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_C.Foreground = Brushes.White;
                this.Label_C.Foreground = Brushes.White;
            }
        }

        private void Selection_C_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_C.Background = imageSelected;
                selection = "C";
            }
            clickflag = true;
        }

        private void Label_D_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_D.Foreground = colorBlue;
                this.Label_D.Foreground = colorBlue;
            }
        }

        private void Label_D_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_D.Foreground = Brushes.White;
                this.Label_D.Foreground = Brushes.White;
            }
        }

        private void Label_D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_D.Background = imageSelected;
                selection = "D";
            }
            clickflag = true;
        }

        private void Selection_D_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_D.Foreground = colorBlue;
                this.Label_D.Foreground = colorBlue;
            }
        }

        private void Selection_D_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!clickflag)
            {
                this.Selection_D.Foreground = Brushes.White;
                this.Label_D.Foreground = Brushes.White;
            }
        }

        private void Selection_D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickflag)
            {
                this.Label_D.Background = imageSelected;
                selection = "D";
            }
            clickflag = true;
        }


        int FinishNumber = 0;//记录完成的题数
        int Score = 0;
        bool ifConf_Answer = false;
        private void Conf_Answer_Click(object sender, RoutedEventArgs e)
        {

            if (this.FinishNumber == 25)
            {
                hint.ShowHint("全部答题，请提交试卷！");
            }
            else
            {
                if (this.FinishNumber == 24)
                {
                    /*选择答案不为空的时候才可以确认答案*/
                    if (selection != "")
                    {
                        if (selection == this.table.Rows[currentQuestionID]["正确答案"].ToString())
                        {
                            Score += 4;
                            this.TotalScores_Label.Content = Score.ToString();
                            this.QID_Nums[currentQuestionID].Background = imageCorrect_Blue;
                            hint.ShowHint("答对啦！");
                        }
                        else
                        {
                            //this.QID_Nums[currentQuestionID].Background = imageIncorrect_Red;
                            hint.ShowHint("哎呀！答错喽——");
                            this.ShowQAnswer(currentQuestionID);

                        }
                        //ifAnswer[currentQuestionID] = true;//记录该题已作答，不可再查看
                        FinishNumber++;
                        this.AnsweredNums_Label.Content = FinishNumber.ToString();
                        hint.ShowHint("全部答题，请提交试卷！");
                        //CheckAnswer();
                        //this.ShowNextQuestion(this.ifConf_Answer);
                    }
                    else
                    {
                        hint.ShowHint("您还未选择任何选项呢！");
                    }

                }
                else
                {
                    this.ifConf_Answer = true;
                    if (this.QID_Nums[currentQuestionID].Background == null || this.QID_Nums[currentQuestionID].Background == imageCurrent_Gray || this.QID_Nums[currentQuestionID].Background == imageCurrent_Blue)
                    {
                        /*选择答案不为空的时候才可以确认答案*/
                        if (selection != "")
                        {
                            if (selection == this.table.Rows[currentQuestionID]["正确答案"].ToString())
                            {
                                Score += 4;
                                this.TotalScores_Label.Content = Score.ToString();
                                this.QID_Nums[currentQuestionID].Background = imageCorrect_Blue;
                                hint.ShowHint("答对啦！");
                            }
                            else
                            {
                                this.QID_Nums[currentQuestionID].Background = imageIncorrect_Red;
                                hint.ShowHint("哎呀！答错喽——");
                                this.ShowQAnswer(currentQuestionID);

                            }
                            //ifAnswer[currentQuestionID] = true;//记录该题已作答，不可再查看
                            FinishNumber++;
                            this.AnsweredNums_Label.Content = FinishNumber.ToString();
                            //CheckAnswer();
                            this.ShowNextQuestion(this.ifConf_Answer);
                        }
                        else
                        {
                            hint.ShowHint("您还未选择任何选项呢！");
                        }
                    }
                    else
                    {
                        hint.ShowHint("已答题！");
                    }
                }
            }
            if (this.FinishNumber * 4 - this.Score > 10)
            {
                this.Conf_Answer_B.IsEnabled = false;
                END end = new END(this.IDNumber, this.name, this.photo, this.Score);
                end.Show();
                counter = 0;
                closeflag = true;
                opacityAnimation(1, 0, 1000);
            }


        }
        //string CorrectAnswer = "";

        private void NextOne_Label_MouseEnter(object sender, MouseEventArgs e)
        {
            this.NextOne_Label.Foreground = Brushes.Yellow;
        }

        private void NextOne_Label_MouseLeave(object sender, MouseEventArgs e)
        {
            this.NextOne_Label.Foreground = Brushes.White;
        }

        int currentQuestionID = 0;
        private void ShowNextQuestion(bool ifConf_Answer)
        {
            if (this.QID_Nums[currentQuestionID].Background == imageCurrent_Gray || this.QID_Nums[currentQuestionID].Background == imageCurrent_Blue)
            {
                this.QID_Nums[currentQuestionID].Background = null;
            }
            currentQuestionID++;
            while (true)
            {
                if (currentQuestionID == 25)
                {
                    currentQuestionID = 0;
                }
                if (this.QID_Nums[currentQuestionID].Background != null)
                { 
                    currentQuestionID++;
                }
                else
                {
                    break;
                }
            }
               

            if (currentQuestionID < 25)
            {
                this.ShowQuestion(currentQuestionID);
            }
            else
            {
                currentQuestionID = 0;
                this.ShowQuestion(0);
            }
            clickflag = false;
            setCursorflag = false;
            selection = "";
            if (currentQuestionID == 0)
            {
                if (!ifConf_Answer)
                {
                    if (this.QID_Nums[24].Background == imageCurrent_Gray|| this.QID_Nums[24].Background ==imageCurrent_Blue)
                    {
                        this.QID_Nums[24].Background = null;
                    }

                }
                
                this.QID_Nums[0].Background = imageCurrent_Gray;
            }
            else
            {
                if (!ifConf_Answer)
                {
                    if (this.QID_Nums[currentQuestionID - 1].Background == imageCurrent_Gray || this.QID_Nums[currentQuestionID - 1].Background == imageCurrent_Blue)
                    {
                        this.QID_Nums[currentQuestionID - 1].Background = null;
                    }
                    
                }
                
                this.QID_Nums[currentQuestionID].Background = imageCurrent_Gray;
            }


            this.Selection_A.Foreground = Brushes.White;
            this.Selection_B.Foreground = Brushes.White;
            this.Selection_C.Foreground = Brushes.White;
            this.Selection_D.Foreground = Brushes.White;
            this.Label_A.Foreground = Brushes.White;
            this.Label_B.Foreground = Brushes.White;
            this.Label_C.Foreground = Brushes.White;
            this.Label_D.Foreground = Brushes.White;

            //this.Selection_A.Background = imageUnSelected;
            //this.Selection_B.Background = imageUnSelected;
            //this.Selection_C.Background = imageUnSelected;
            //this.Selection_D.Background = imageUnSelected;
            this.Label_A.Background = imageUnSelected;
            this.Label_B.Background = imageUnSelected;
            this.Label_C.Background = imageUnSelected;
            this.Label_D.Background = imageUnSelected;
        }
        private void NextOne_Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.FinishNumber == 24)
            {
                hint.ShowHint("已经是最后一题啦！");
            }
            else
            {
                this.ifConf_Answer = false;
                this.ShowNextQuestion(this.ifConf_Answer);
            }
         
        }

        private void Analysis_Click(object sender, RoutedEventArgs e)
        {
            if (selection != "")
            {
                this.ShowQAnswer(currentQuestionID);
            }
            else
            {
                hint.ShowHint("还未答题，不可查看解析哦！");
            }
        }

        private void END_Click(object sender, RoutedEventArgs e)
        {
            END end = new END(this.IDNumber, this.name, this.photo, this.Score);
            end.Show();
            counter = 0;
            closeflag = true;
            opacityAnimation(1, 0, 1000);
        }
        //public void CheckAnswer()//核对答案
        //{
        //    CorrectAnswer = table.Rows[currentQuestionID]["正确答案"].ToString();

        //    if (CorrectAnswer == selection)
        //    {
        //        button[QuestionNumber].BackColor = Color.Yellow;//答案正确，题号显示黄色

        //        AheadSubmitScore += 5;//答对一题得5分

        //    }
        //    else
        //    {
        //        button[QuestionNumber].BackColor = Color.Red;//答案错误，题号显示红色

        //        Score -= 5;//错一题扣5分

        //        Question = ds.Tables[0].Rows[QuestionNumber][1].ToString();
        //        Analysis = ds.Tables[0].Rows[QuestionNumber][7].ToString();


        //        /*分数大于等于90分时出现错题显示解析，小于90分时不显示解析*/
        //        if (Score > 89)
        //        {
        //            Analysis analysis = new Analysis();
        //            analysis.Show(this);
        //        }
        //        else
        //        {

        //        }

        //    }

        //    selection = "";//选择的答案置空，避免影响下一道题的作答



        //    /*大于等于90分时，显示下一道题目*/
        //    if (Score > 89)
        //    {

        //        /*没有进行跳题时直接显示下一道题目*/
        //        if (LastQuestionNumber == 0)
        //        {
        //            QuestionNumber += 1;
        //            ShowQuestion(QuestionNumber, ds);
        //        }

        //        /*有跳题时读取下一道题目*/
        //        else
        //        {

        //            /*跳题后的题号大于等于跳题前的题号，则直接显示目前题号的下一题，反之，显示跳题前的题号对应的题目*/
        //            if (LastQuestionNumber < QuestionNumber || LastQuestionNumber == QuestionNumber)
        //            {
        //                QuestionNumber += 1;
        //                LastQuestionNumber = 0;
        //                ShowQuestion(QuestionNumber, ds);
        //            }
        //            else
        //            {
        //                QuestionNumber = LastQuestionNumber;
        //                LastQuestionNumber = 0;
        //                ShowQuestion(QuestionNumber, ds);
        //            }
        //        }
        //    }

        //    /*分数小于90，结束答题，显示成绩单*/
        //    else
        //    {
        //        ScoreReport ScoreReport = new ScoreReport();
        //        ScoreReport.Show(this);
        //        this.Hide();
        //    }
        //}
    }
}
