using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Re : Pezzo
    {

        

        public Re(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }


        public Re(Colore colore) : base(colore)
        {
        }


        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            int[] posizioniX = { x - 1, x, x + 1 };
            int[] posizioniY = { y - 1, y, y + 1 };

            // Scorri tutte le possibili posizioni
            foreach (int i in posizioniX)
            {
                foreach (int j in posizioniY)
                {
                    // Controllo che la posizione sia all'interno della scacchiera
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        // Controllo che la posizione non sia occupata da un pezzo dello stesso colore
                        if (scacchiera[i, j] == null || scacchiera[i, j].Colore != Colore)
                        {
                            mosseValide.Add(new int[] { i, j });
                        }
                    }
                }
            }

            return mosseValide;
        }








    }
}
