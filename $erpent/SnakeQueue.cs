using System;
using System.Collections.Generic;
//using System.Linq;

namespace Serpent
{
    public class SnakeQueue : LinkedList<Coordonnees>
    {
        private const char caractere = '$';
        private const string tete = "QR";
        private const sbyte startLength = 5;
        private sbyte variationX, variationY;
        private int augmentation = 0;
        private HashSet<Coordonnees> corps;

        public SnakeQueue()
        {
            this.corps = new HashSet<Coordonnees>();

            for (sbyte i = 0; i < startLength; i++)
            {
                this.AddFirst(new Coordonnees((sbyte)(Console.WindowLeft + i), (sbyte)Console.WindowTop));
            }
            Console.Write(new String(caractere, 5));
            this.DirectionCourante = Direction.EAST;
            this.variationX = 1;
            this.variationY = 0;
        }

        public Direction DirectionCourante { get; private set; }

        public IEnumerable<Coordonnees> Corps { get { return this.corps; } }

        private new void AddFirst(Coordonnees coord)
        {
            base.AddFirst(coord);
            if (!this.corps.Add(coord))
            {
                throw new SnakeIsDeadException("Vous vous êtes mangé vous-même!");
            }
        }

        private new void RemoveLast()
        {
            this.corps.Remove(this.Last.Value);
            base.RemoveLast();
        }

        public bool MoveHead(Direction dir, Coordonnees cenne)
        {
            bool retour = false;
            if (((int)dir - (int)this.DirectionCourante) % 2 != 0)
            {
                this.DirectionCourante = dir;
                switch (this.DirectionCourante)
                {
                    case Direction.NORTH:
                        this.variationX = 0;
                        this.variationY = -1;
                        break;
                    case Direction.EAST:
                        this.variationX = 1;
                        this.variationY = 0;
                        break;
                    case Direction.SOUTH:
                        this.variationX = 0;
                        this.variationY = 1;
                        break;
                    case Direction.WEST:
                        this.variationX = -1;
                        this.variationY = 0;
                        break;
                    default:
                        break;
                }
            }
            Coordonnees newHead = this.First.Value;
            newHead.X += this.variationX;
            newHead.Y += this.variationY;

            if (newHead.X < Console.WindowLeft || newHead.X >= Console.WindowWidth ||
                newHead.Y < Console.WindowTop || newHead.Y >= Console.WindowHeight)
            {
                throw new SnakeIsDeadException("Vous avez touché à la cloture électrique!");
            }

            if (newHead.Equals(cenne))
            {
                this.augmentation += 2;
                retour = true;
            }

            if (this.augmentation == 0)
            {
                Console.SetCursorPosition(this.Last.Value.X, this.Last.Value.Y);
                Console.Write(" ");
                this.RemoveLast();
            }
            else
            {
                this.augmentation--;
            }

            this.AddFirst(newHead);
            var noeudCourant = this.First;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char item in tete)
            {
                Console.SetCursorPosition(noeudCourant.Value.X, noeudCourant.Value.Y);
                Console.Write(item);
                noeudCourant = noeudCourant.Next;
            }
            Console.ResetColor();
            Console.SetCursorPosition(noeudCourant.Value.X, noeudCourant.Value.Y);
            Console.Write(caractere);

            return retour;
        }
    }
}
