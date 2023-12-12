using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame3
{
    internal class Cobra
    {
        public int Len { get; private set; }
        public Point[] Location { get; private set; }

        public Cobra()
        {
            Location = new Point[28 * 28];
        }
        public void Reset()
        {
            Len = 4;
            for (int i = 0; i < Len; i++)
            {
                Location[i].X = 12;
                Location[i].Y = 12;
            }
        }

        public void Junto()
        {
            for (int i = Len - 1; i > 0; i--)
            {
                Location[i] = Location[i - 1];

            }
        }
        public void Cima()
        {
            Junto();
            Location[0].Y--;
            if (Location[0].Y < 0)
            {
                Location[0].Y += 28;
            }
        }


        public void Baixo()
        {
            Junto();
            Location[0].Y++;
            if (Location[0].Y > 27)
            {
                Location[0].Y -= 28;
            }
        }

        public void Esquerda()
        {
            Junto();
            Location[0].X--;
            if (Location[0].X < 0)
            {
                Location[0].X += 28;
            }
        }

        public void Direita()
        {

            Junto();
            Location[0].X++;
            if (Location[0].X > 0)
            {
                Location[0].X -= 28;
            }
        }

        public void Comeu()
        {
            Len++;
        }
    }

}