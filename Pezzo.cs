using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    public enum Colore
    {
        Bianco,
        Nero
    }



    public abstract class Pezzo
    {

        public Image immagine;


        public Colore Colore { get; set; }

        public Pezzo(Colore colore)
        {
            Colore = colore;
        }




        public abstract List<int[]> CalcolaMosseValide(int x, int y, Pezzo[,] scacchiera);


    }

}
