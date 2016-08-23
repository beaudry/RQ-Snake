using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Serpent
{
    class Program
    {
        const char cenneChar = '¢';
        private static Random aleatoire = new Random();

        private static Coordonnees GetCenne(ISet<Coordonnees> carte, SnakeQueue serpent)
        {
            HashSet<Coordonnees> carteTmp = new HashSet<Coordonnees>(carte);
            carteTmp.ExceptWith(serpent.Corps);
            Coordonnees newCenne = carteTmp.ToArray()[aleatoire.Next(carteTmp.Count)];
            Console.SetCursorPosition(newCenne.X, newCenne.Y);
            Console.Write(cenneChar);
            return newCenne;
        }

        static void Main(string[] args)
        {
            HashSet<Coordonnees> carte = new HashSet<Coordonnees>();

            for (int x = Console.WindowLeft; x < Console.WindowWidth; x++)
            {
                for (int y = Console.WindowTop; y < Console.WindowHeight; y++)
                {
                    carte.Add(new Coordonnees((sbyte)x, (sbyte)y));
                }
            }

            do
            {
                Console.Clear();

                SnakeQueue serpent = new SnakeQueue();
                Console.CursorVisible = false;
                bool cenneMangee = false;
                Coordonnees cenne = GetCenne(carte, serpent);
                Dictionary<ConsoleKey, Direction> controles = new Dictionary<ConsoleKey, Direction>()
                {
                    { ConsoleKey.DownArrow, Direction.SOUTH},
                    { ConsoleKey.LeftArrow, Direction.WEST},
                    { ConsoleKey.RightArrow, Direction.EAST},
                    { ConsoleKey.UpArrow, Direction.NORTH}
                };

                ConsoleKey curKey = ConsoleKey.RightArrow;
                ConsoleKey tmpKey = curKey;
                try
                {
                    while (true)
                    {
                        Thread.Sleep(200);
                        if (cenneMangee)
                        {
                            Console.SetCursorPosition(cenne.X, cenne.Y);
                            Console.Write(' ');
                            cenne = GetCenne(carte, serpent);
                        }

                        while (Console.KeyAvailable && (tmpKey = Console.ReadKey(false).Key) == curKey)
                        {

                        }

                        if (tmpKey != curKey && controles.ContainsKey(tmpKey))
                        {
                            curKey = tmpKey;
                        }

                        cenneMangee = serpent.MoveHead(controles[curKey], cenne);
                    }
                }
                catch (SnakeIsDeadException ex)
                {
                    Console.Clear();
                    Console.WriteLine("Le jeu est terminé,");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Appuyez sur une touche pour recommencer à jouer, ou sur Échap pour quitter.");
                }
            } while (Console.ReadKey(false).Key != ConsoleKey.Escape);
        }
    }
}
