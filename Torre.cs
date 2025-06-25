using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Torre : Pezzo
    {
        

        public Torre(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }


        public Torre(Colore colore) : base(colore)
        {
        }



        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            // Controlla le mosse in orizzontale
            for (int i = x - 1; i >= 0; i--)
            {
                if (scacchiera[i, y] == null)
                {
                    mosseValide.Add(new int[] { i, y });
                }
                else
                {
                    if (scacchiera[i, y].Colore != Colore)
                    {
                        mosseValide.Add(new int[] { i, y });
                    }
                    break;
                }
            }

            for (int i = x + 1; i < 8; i++)
            {
                if (scacchiera[i, y] == null)
                {
                    mosseValide.Add(new int[] { i, y });
                }
                else
                {
                    if (scacchiera[i, y].Colore != Colore)
                    {
                        mosseValide.Add(new int[] { i, y });
                    }
                    break;
                }
            }

            // Controlla le mosse in verticale
            for (int j = y - 1; j >= 0; j--)
            {
                if (scacchiera[x, j] == null)
                {
                    mosseValide.Add(new int[] { x, j });
                }
                else
                {
                    if (scacchiera[x, j].Colore != Colore)
                    {
                        mosseValide.Add(new int[] { x, j });
                    }
                    break;
                }
            }

            for (int j = y + 1; j < 8; j++)
            {
                if (scacchiera[x, j] == null)
                {
                    mosseValide.Add(new int[] { x, j });
                }
                else
                {
                    if (scacchiera[x, j].Colore != Colore)
                    {
                        mosseValide.Add(new int[] { x, j });
                    }
                    break;
                }
            }

            return mosseValide;
        }



    }
}
