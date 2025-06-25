using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Cavallo : Pezzo
    {

       

        public Cavallo(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }

        public Cavallo(Colore colore) : base(colore)
        {
        }


        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            // Controlla le otto possibili posizioni del cavallo e aggiungi quelle valide
            int[] offsetX = { -1, -2, -2, -1, 1, 2, 2, 1 };
            int[] offsetY = { -2, -1, 1, 2, 2, 1, -1, -2 };
            for (int i = 0; i < 8; i++)
            {
                int newX = x + offsetX[i];
                int newY = y + offsetY[i];
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    Pezzo pezzoDestinazione = scacchiera[newX, newY];
                    if (pezzoDestinazione == null || pezzoDestinazione.Colore != Colore)
                    {
                        mosseValide.Add(new int[] { newX, newY });
                    }
                }
            }

            return mosseValide;
        }












    }
}
