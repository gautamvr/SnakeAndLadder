using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeAndLadder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _blue, _green;
        private int x1 = 0, y1 = 0, pos1 = 0, flag1 = 0, diceValue, flagblue;
        private int x2 = 0, y2 = 0, pos2 = 0, flag2 = 0, flaggreen = 0;
        private int snakeflag = 0, ladderflag = 0;
        private int timer1flag = 0;
        private static int count = 0, timcount = 0, rollrandom;
        private DispatcherTimer timer1, timer2;

        public MainWindow()
        {
            InitializeComponent();
            timer2 = new DispatcherTimer();
            timer1 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromMilliseconds(10);
            timer1.Interval = TimeSpan.FromMilliseconds(100);
            timer2.Tick += timer_tick;
            timer1.Tick += timer_tick;
            timer1.Stop();
            timer2.Stop();
            SoundPlayer bgsound = new SoundPlayer(@"..\..\..\assets\bgsound.wav");
            bgsound.Play();
            //MediaPlayer bgmusic = new MediaPlayer(); 
            //bgmusic.Open(new Uri(@"..\assets\bgsound.wav")); 
            //bgmusic.Play();
        }

        public void DiceRolling()
        {
            SoundPlayer rolldice = new SoundPlayer(@"..\..\..\assets\rolldicex.wav");
            rolldice.Play();
        }

        public void StopSound()
        {
            SoundPlayer stoproll = new SoundPlayer(@"..\..\..\assets\empty.wav");
            stoproll.Play();

        }

        public void CantMoveSound()
        {
            SoundPlayer cantmovealert = new SoundPlayer(@"..\..\..\assets\cantmove.wav");
            cantmovealert.Play();

        }

        public void EnterSound()
        {
            SoundPlayer enteralert = new SoundPlayer(@"..\..\..\assets\enter.wav");
            enteralert.Play();

        }

        public void WinningSound()
        {
            SoundPlayer winner = new SoundPlayer(@"..\..\..\assets\winner.wav");
            winner.Play();
        }

        public void SnakeSound()
        {
            SoundPlayer snake = new SoundPlayer(@"..\..\..\assets\snake.wav");
            snake.Play();

        }

        public void LadderSound()
        {
            SoundPlayer ladder = new SoundPlayer(@"..\..\..\assets\ladder.wav");
            ladder.Play();

        }

        private void Pageloaded(object sender, RoutedEventArgs e)
        {
            player1.Visibility = Visibility.Hidden;
            RollButton2.IsEnabled = false;
            RollButton1.IsEnabled = true;
        }

        private void Reset()
        {

            Ypos.Content = "Player 2 :  ";
            Xpos.Content = "Player 1 :  ";
            DiceImage.Source = new BitmapImage(new Uri(@"..\..\..\assets\roll.jpg",UriKind.Relative));
            DiceVal.Content = "Dice Value :  ";
            actions.Text = string.Empty;
            actions.Text = "A new game has begun!";
            x1 = y1 = flag1 = x2 = y2 = flag2 = pos1 = pos2 = 0;
            _blue = false;
            _green = false;
            player1.RenderTransform = new TranslateTransform(x1, y1);
            player2.RenderTransform = new TranslateTransform(x2, y2);
            player1.Visibility = Visibility.Hidden;
            player2.Visibility = Visibility.Hidden;
            Player1Show.Visibility = Visibility.Visible;
            Player2Show.Visibility = Visibility.Visible;
            RollButton1.IsEnabled = true;
            RollButton2.IsEnabled = false;
            Msg.Content = "        A new game is started!";
        }


        private void timer_tick(object sender, EventArgs e)
        {

            RollButton2.IsEnabled = false;
            RollButton1.IsEnabled = false;
            rollImage.Visibility = Visibility.Visible;
            DiceImage.Visibility = Visibility.Hidden;
            if (timer1flag == 1)
            {
                count++;
            }
            else
            {
                timcount++;
            }

            Random r1 = new Random();
            rollrandom = r1.Next(1, 7);
            rollImage.Source =
                new BitmapImage(new Uri((@"..\..\..\assets\" + rollrandom +
                                        ".png"), UriKind.Relative));
            DiceVal.Content = "Dice Value : " + rollrandom + " ";
            if (timcount == 30)
            {
                timcount = 0;
                timer1.Start();
                timer2.Stop();
                timer1flag = 1;
            }
            if (count == 100)
            {
                timer1flag = 0;
                count = 0;
                Stoprolling();

                if (flagblue == 1)
                {
                    Action_blue();
                    flagblue = 0;
                }
                else if (flaggreen == 1)
                {
                    Action_green();
                    flaggreen = 0;
                }
            }
        }

        private void LadderEffect(int p, int pos)
        {
            LadderSound();
            Msg.Content = "Great! Player " + p + " reached a ladder!";
            Msg.Background = new SolidColorBrush(Colors.Green);
            actions.AppendText("\n\tPlayer " + p + " has got a ladder.\n\tPlayer " + p + " has climbed to " + pos + ".");
            ladderflag = 0;
        }

        private void SnakeEffect(int p, int pos)
        {
            SnakeSound();
            Msg.Content = "Oops! Player " + p + " bitten by a snake!";
            Msg.Background = new SolidColorBrush(Colors.Red);
            actions.AppendText("\n\tPlayer " + p + " is bit by a snake.\n\tPlayer " + p + " is dropped to " + pos + ".");
            snakeflag = 0;
        }

        private void CantMove(int player, ref int flag)
        {
            CantMoveSound();
            actions.AppendText("\nPlayer " + player + " can't move!");
            Msg.Content = "                Can't move!";
            Msg.Background = new SolidColorBrush(Colors.Red);
            flag = 0;
            actions.ScrollToEnd();
        }

        private void WinGame(int player, Color color)
        {
            WinningSound();
            Msg.Content = "             Player " + player + " WINS!!";
            Msg.Background = new SolidColorBrush(color);
            actions.AppendText("\nPLAYER " + player + " WINS THE GAME");
            actions.ScrollToEnd();
            MessageBoxResult msg = MessageBox.Show("Player " + player + " HAS WON THE GAME!!\n\nDo you want to play again?", "Congratulations!", MessageBoxButton.YesNoCancel);
            if (msg == MessageBoxResult.Yes)
            {
                Reset();
            }
            else if (msg == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else
            {
                RollButton2.IsEnabled = false;
                RollButton1.IsEnabled = false;
            }

        }

        private void Entergame(int player, ref bool bg, ref int pos, Color clr)
        {
            EnterSound();
            bg = true;
            pos = 1;
            actions.AppendText("\nPlayer " + player + " has started his game!");
            Msg.Content = "               Player " + player + " is in!";
            Msg.Background = new SolidColorBrush(clr);

        }

        private void Action_blue()
        {
            diceValue = GameActions.RollDice(DiceImage);
            DiceVal.Content = "Dice Value : " + diceValue + " ";

            if (_blue)
            {
                GameActions.Move(diceValue, ref x1, ref y1, ref pos1, player1, ref flag1);


                if (flag1 == 0)
                {
                    actions.AppendText("\nPlayer 1 is moved to " + pos1 + ".");

                }

                if (pos1 == 100)
                {
                    Xpos.Content = "Player 1  : " + pos1 + " ";
                    WinGame(1, Colors.Blue);
                    return;
                }
            }


            //else if(_blue == false)
            else if (diceValue == 6 && _blue == false)
            {
                Entergame(1, ref _blue, ref pos1, Colors.DodgerBlue);
                Player1Show.Visibility = Visibility.Hidden;
                player1.Visibility = Visibility.Visible;
                Xpos.Content = "Player 1 : " + pos1 + " ";
            }


            //if(true)
            if (diceValue == 1 || diceValue == 5 || diceValue == 6)
            {
                RollButton2.IsEnabled = false;
                RollButton1.IsEnabled = true;
            }

            else
            {
                Msg.Content = "             Player 2 's turn";
                Msg.Background = new SolidColorBrush(Colors.LimeGreen);
                RollButton2.IsEnabled = true;
                RollButton1.IsEnabled = false;
            }

            GameActions.Snake(ref x1, ref y1, ref pos1, player1, ref snakeflag);
            GameActions.Ladder(ref x1, ref y1, ref pos1, player1, ref ladderflag);

            if (snakeflag == 1) SnakeEffect(1, pos1);

            if (ladderflag == 1) LadderEffect(1, pos1);

            if (_blue) Xpos.Content = "Player 1 : " + pos1 + " ";

            if (flag1 == 1)
            {
                CantMove(1, ref flag1);
                return;
            }

            actions.ScrollToEnd();

        }

        private void Action_green()
        {

            diceValue = GameActions.RollDice(DiceImage);
            DiceVal.Content = "Dice Value : " + diceValue + " ";

            if (_green)
            {
                GameActions.Move(diceValue, ref x2, ref y2, ref pos2, player2, ref flag2);

                if (flag2 == 0)
                {
                    actions.AppendText("\nPlayer 2 is moved to " + pos2 + ".");
                }

                if (pos2 == 100)
                {
                    Ypos.Content = "Player 2 : " + pos2 + " ";
                    WinGame(2, Colors.LimeGreen);
                    return;
                }

            }

            //else if(green == false)
            else if (diceValue == 6 && _green == false)
            {
                Entergame(2, ref _green, ref pos2, Colors.LimeGreen);
                Player2Show.Visibility = Visibility.Hidden;
                player2.Visibility = Visibility.Visible;
                Ypos.Content = "Player 2 : " + pos2 + " ";
            }


            if (diceValue == 1 || diceValue == 5 || diceValue == 6)
            {
                RollButton1.IsEnabled = false;
                RollButton2.IsEnabled = true;
            }

            else
            {
                Msg.Content = "             Player 1 's turn";
                Msg.Background = new SolidColorBrush(Colors.DodgerBlue);
                RollButton1.IsEnabled = true;
                RollButton2.IsEnabled = false;
            }

            GameActions.Snake(ref x2, ref y2, ref pos2, player2, ref snakeflag);
            GameActions.Ladder(ref x2, ref y2, ref pos2, player2, ref ladderflag);

            if (snakeflag == 1) SnakeEffect(2, pos2);

            if (ladderflag == 1) LadderEffect(2, pos2);

            if (_green) Ypos.Content = "Player 2 : " + pos2 + " ";

            if (flag2 == 1)
            {
                CantMove(2, ref flag2);
                return;
            }


            actions.ScrollToEnd();
        }


        private void RollButton1_OnClick(object sender, RoutedEventArgs e)
        {
            timer1flag = 0;
            timer2.Start();
            DiceRolling();
            flagblue = 1;
            RollButton1.Visibility = Visibility.Hidden;
            StopButton1.Visibility = Visibility.Visible;
        }

        private void RollButton2_OnClick(object sender, RoutedEventArgs e)
        {
            timer1flag = 0;
            timer2.Start();
            DiceRolling();
            flaggreen = 1;
            RollButton2.Visibility = Visibility.Hidden;
            StopButton2.Visibility = Visibility.Visible;
        }

        private void Stoprolling()
        {
            timer2.Stop();
            timer1.Stop();
            DiceVal.Content = "Dice Value : " + diceValue + " ";
            rollImage.Visibility = Visibility.Hidden;
            DiceImage.Visibility = Visibility.Visible;
            RollButton1.Visibility = Visibility.Visible;
            StopButton1.Visibility = Visibility.Hidden;
            RollButton2.Visibility = Visibility.Visible;
            StopButton2.Visibility = Visibility.Hidden;
            StopSound();
        }




        private void Stopbutton1_OnClick(object sender, RoutedEventArgs e)
        {
            Stoprolling();
            Action_blue();
        }

        private void StopButton2_OnClick(object sender, RoutedEventArgs e)
        {
            Stoprolling();
            Action_green();
        }
    }
}
