using System;

namespace Serpent
{
    public struct Coordonnees: IEquatable<Coordonnees>
    {
        public Coordonnees(sbyte x, sbyte y)
        {
            this.X = x;
            this.Y = y;
        }

        public sbyte X;
        public sbyte Y;

        public override int GetHashCode()
        {
            int hash = 17;
            int premier = 23;
            hash = hash * premier + this.X.GetHashCode();
            hash = hash * premier + this.Y.GetHashCode();
            return hash;
        }

        public bool Equals(Coordonnees other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
