using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Pedone : Pezzo
    {

        

        public Pedone(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }


        public Pedone(Colore colore) : base(colore)
        {
        }


        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            // determina il colore del pedone
            int direzione = (Colore == Colore.Bianco) ? 1 : -1;

            // controlla la casella davanti al pedone
            if (scacchiera[x + direzione, y] == null)
            {
                mosseValide.Add(new int[] { x + direzione, y });

                // se il pedone non ha ancora mosso, può avanzare di due caselle
                if ((direzione == -1 && x == 6) || (direzione == 1 && x == 1))
                {
                    if (scacchiera[x + 2 * direzione, y] == null)
                    {
                        mosseValide.Add(new int[] { x + 2 * direzione, y });
                    }
                }
            }

            // controlla le caselle diagonali per poter vedere se è possibile catturare
            if (y > 0 && scacchiera[x + direzione, y - 1] != null && scacchiera[x + direzione, y - 1].Colore != Colore)
            {
                mosseValide.Add(new int[] { x + direzione, y - 1 });
            }

            if (y < 7 && scacchiera[x + direzione, y + 1] != null && scacchiera[x + direzione, y + 1].Colore != Colore)
            {
                mosseValide.Add(new int[] { x + direzione, y + 1 });
            }

            return mosseValide;
        }







    }
}
