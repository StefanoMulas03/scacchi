using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    class Regina : Pezzo
    {
        

        public Regina(Colore colore, Image immagine) : base(colore)
        {
            this.immagine = immagine;
        }



        public Regina(Colore colore) : base(colore)
        {
        }


        public override List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera)
        {
            List<int[]> mosseValide = new List<int[]>();

            // Mosse valide dell'Alfiere
            Alfiere alfiere = new Alfiere(Colore);
            List<int[]> mosseAlfiere = alfiere.CalcolaMosseValide(x, y, scacchiera);
            mosseValide.AddRange(mosseAlfiere);

            // Mosse valide della Torre
            Torre torre = new Torre(Colore);
            List<int[]> mosseTorre = torre.CalcolaMosseValide(x, y, scacchiera);
            mosseValide.AddRange(mosseTorre);

            return mosseValide;
        }


    }
}
