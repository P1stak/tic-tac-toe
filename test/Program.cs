using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    // Крестики нолики.
    // ТЗ: Реализовать консольную игру: крестики нолики для
    // двух игроков на одном и том же устройстве
    //

    //        X = 1
    //        0 = 2 
    public class Game
    {
        private int[,] array = new int[3, 3];
        private int player = 1;
        public void gameStart()
        {
            Console.WriteLine("Сделайте первый ход");

            while (true)
            {
                PrintArray();
                if (IsWin())
                {
                    player = (player == 1) ? 2 : 1;
                    Console.WriteLine($"Игрок {player} победил");
                    break;
                }
                if (IsDraw()) //выход
                {
                    Console.WriteLine("Ничья");
                    break;
                }
                
                var s = Console.ReadLine();
                int move = int.Parse(s);
                var t = Transform(move);

                while (!(char.IsDigit(s[0]) && s.Length == 1 && s[0] != '0' && array[t.Item1, t.Item2] == 0)) //проверка на правильность ввода
                {
                    Console.WriteLine("Не верный ход, ещё раз");
                    s = Console.ReadLine();
                    move = int.Parse(s);
                    t = Transform(move);
                }

                Console.WriteLine($"Игрок {player} делает ход: {s}");
                MakeMove(move);
                player = (player == 1) ? 2 : 1;
            }
        } 
        public int[,] MakeMove(int move)
        {

            var t = Transform(move);

            int i = t.Item1;
            int j = t.Item2;
            array[i, j] = player;
            return array;

        }
        public Tuple<int, int> Transform(int a) //трансформируем двумерный массив в одномерный с помощью кортежа
        {
            a--;
            int i = a / 3;
            int j = a % 3;

            return new Tuple<int, int>(i, j);
        }
        public bool IsDraw()
        {
            var count = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    if (array[i, k] == 0)
                    {
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                return true;
            }
            return false;
        }
        public bool IsWin()
        {
            for (int i = 0; i < array.GetLength(0); i++) //горизонталь
            {
                if (array[i, 0] == array[i, 1] && array[i, 1] == array[i, 2] && array[i, 0] != 0)
                {
                    return true;
                }
            }
            for (int j = 0; j < array.GetLength(1); j++) //вертикаль
            {
                if (array[0, j] == array[1, j] && array[1, j] == array[2, j] && array[0, j] != 0)
                {
                    return true;
                }
            }
            if (array[0,0] == array[1,1] && array[1,1] == array[2,2] && array[0,0] != 0) //диагональ 1(левый)
            {
                return true;
            }
            if (array[0, 2] == array[1, 1] && array[1, 1] == array[2, 0] && array[0, 2] != 0) //диагональ 2(правый)
            {
                return true;
            }
            
            
            return false;
        }

        public void PrintArray()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    if (array[i, k] == 0)
                        Console.Write(" - ");
                    if (array[i, k] == 1)
                        Console.Write(" X ");
                    if (array[i, k] == 2)
                        Console.Write(" O ");


                }
                Console.WriteLine();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.gameStart();
        }    
    }
}
