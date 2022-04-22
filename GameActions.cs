using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using wpfIm = System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SnakeAndLadder
{
    public class GameActions
    {
        public static int RollDice(wpfIm.Image im)
        {
            Random r1 = new Random();
            int diceval = r1.Next(1, 7);
            im.Source =
                new BitmapImage(new Uri(@"..\..\..\assets\" + diceval +
                                            ".png",UriKind.Relative));


            return diceval;
        }

        public static void Move(int dice, ref int x, ref int y, ref int pos, wpfIm.Image player, ref int flag)
        {
            if (pos + dice > 100)
            {
                flag = 1;

                return;
            }

            for (int i = 0; i < dice; i++)

            {

                if (pos % 10 == 0)
                {
                    y -= 84;

                }

                else if ((pos / 10) % 2 == 0)
                {
                    x += 120;

                }

                else
                {
                    x -= 120;
                }

                SoundPlayer coinmove = new SoundPlayer(@"..\..\..\assets\coinmove.wav");
                coinmove.Play();


                player.RenderTransform = new TranslateTransform(x, y);
                pos++;
            }


        }

        public static void Snake(ref int x, ref int y, ref int pos, wpfIm.Image player, ref int sflag)
        {
            switch (pos)
            {
                case 8:
                    x = 120 * (3);
                    y = 0;
                    pos = 4;
                    sflag = 1;
                    break;
                case 18:
                    x = 0;
                    y = 0;
                    pos = 1;
                    sflag = 1;
                    break;
                case 26:
                    x = 120 * 9;
                    y = 0;
                    pos = 10;
                    sflag = 1;
                    break;
                case 39:
                    x = 120 * 4;
                    y = 0;
                    pos = 5;
                    sflag = 1;
                    break;
                case 51:
                    x = 120 * 5;
                    y = 0;
                    pos = 6;
                    sflag = 1;
                    break;
                case 54:
                    x = 120 * 4;
                    y = -84 * 3;
                    pos = 36;
                    sflag = 1;
                    break;
                case 56:
                    x = 0;
                    y = 0;
                    pos = 1;
                    sflag = 1;
                    break;
                case 60:
                    x = 120 * 2;
                    y = -84 * 2;
                    pos = 23;
                    sflag = 1;
                    break;
                case 75:
                    x = 120 * 7;
                    y = -84 * 2;
                    pos = 28;
                    sflag = 1;
                    break;
                case 83:
                    x = 120 * 4;
                    y = -84 * 4;
                    pos = 45;
                    sflag = 1;
                    break;
                case 85:
                    x = 120 * 1;
                    y = -84 * 5;
                    pos = 59;
                    sflag = 1;
                    break;
                case 90:
                    x = 120 * 7;
                    y = -84 * 4;
                    pos = 48;
                    sflag = 1;
                    break;
                case 92:
                    x = 120 * 4;
                    y = -84 * 2;
                    pos = 25;
                    sflag = 1;
                    break;
                case 97:
                    x = 120 * 6;
                    y = -84 * 8;
                    pos = 87;
                    sflag = 1;
                    break;
                case 99:
                    x = 120 * 2;
                    y = -84 * 6;
                    pos = 63;
                    sflag = 1;
                    break;


            }
            player.RenderTransform = new TranslateTransform(x, y);

        }
        public static void Ladder(ref int x, ref int y, ref int pos, wpfIm.Image player, ref int lflag)
        {
            switch (pos)
            {
                case 3:
                    x = 0;
                    y = -84;
                    pos = 20;
                    lflag = 1;
                    break;
                case 6:
                    x = 120 * 6;
                    y = -84;
                    pos = 14;
                    lflag = 1;
                    break;
                case 11:
                    x = 120 * 7;
                    y = -84 * 2;
                    pos = 28;
                    lflag = 1;
                    break;
                case 15:
                    x = 120 * 6;
                    y = -84 * 3;
                    pos = 34;
                    lflag = 1;
                    break;
                case 17:
                    x = 120 * 6;
                    y = -84 * 7;
                    pos = 74;
                    lflag = 1;
                    break;
                case 22:
                    x = 120 * 3;
                    y = -84 * 3;
                    pos = 37;
                    lflag = 1;
                    break;
                case 38:
                    x = 120;
                    y = -84 * 5;
                    pos = 59;
                    lflag = 1;
                    break;
                case 49:
                    x = 120 * 6;
                    y = -84 * 6;
                    pos = 67;
                    lflag = 1;
                    break;
                case 57:
                    x = 120 * 4;
                    y = -84 * 7;
                    pos = 76;
                    lflag = 1;
                    break;
                case 61:
                    x = 120 * 2;
                    y = -84 * 7;
                    pos = 78;
                    lflag = 1;
                    break;
                case 73:
                    x = 120 * 5;
                    y = -84 * 8;
                    pos = 86;
                    lflag = 1;
                    break;
                case 81:
                    x = 120 * 2;
                    y = -84 * 9;
                    pos = 98;
                    lflag = 1;
                    break;
                case 88:
                    x = 120 * 9;
                    y = -84 * 9;
                    pos = 91;
                    lflag = 1;
                    break;


            }
            player.RenderTransform = new TranslateTransform(x, y);

        }
    }
}
