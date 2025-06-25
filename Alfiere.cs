using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Alfiere : Pezzo
    {
        

        public Alfiere(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }


        public Alfiere(Colore colore) : base(colore)
        {
        }


        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            // Mosse valide in direzione nord-est
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
            {
                if (scacchiera[i, j] == null)
                {
                    mosseValide.Add(new int[] { i, j });
                }
                else if (scacchiera[i, j].Colore != Colore)
                {
                    mosseValide.Add(new int[] { i, j });
                    break;
                }
                else
                {
                    break;
                }
            }

            // Mosse valide in direzione nord-ovest
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (scacchiera[i, j] == null)
                {
                    mosseValide.Add(new int[] { i, j });
                }
                else if (scacchiera[i, j].Colore != Colore)
                {
                    mosseValide.Add(new int[] { i, j });
                    break;
                }
                else
                {
                    break;
                }
            }

            // Mosse valide in direzione sud-est
            for (int i = x - 1, j = y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (scacchiera[i, j] == null)
                {
                    mosseValide.Add(new int[] { i, j });
                }
                else if (scacchiera[i, j].Colore != Colore)
                {
                    mosseValide.Add(new int[] { i, j });
                    break;
                }
                else
                {
                    break;
                }
            }

            // Mosse valide in direzione sud-ovest
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (scacchiera[i, j] == null)
                {
                    mosseValide.Add(new int[] { i, j });
                }
                else if (scacchiera[i, j].Colore != Colore)
                {
                    mosseValide.Add(new int[] { i, j });
                    break;
                }
                else
                {
                    break;
                }
            }

            return mosseValide;
        }






    }
}
