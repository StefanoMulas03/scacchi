using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{

    


    public partial class Form1 : Form
    {
        Splash splashScreen = new Splash();

        private Pezzo[,] scacchiera;
        private PictureBox[,] celle;
        private Colore turnoGiocatore;
        private List<Pezzo> pezziCatturatiBianchi;
        private List<Pezzo> pezziCatturatiNeri;
        private bool ScaccoBianco;
        private bool ScaccoNero;
        private object[] Valori;
        private bool scaccoMatto;


        public Form1()
        {
            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start();
            Thread.Sleep(6000);
            InitializeComponent();
            t.Abort();


            scacchiera = new Pezzo[8, 8];
            celle = new PictureBox[8, 8];
            turnoGiocatore = Colore.Bianco;
            pezziCatturatiBianchi = new List<Pezzo>();
            pezziCatturatiNeri = new List<Pezzo>();
            ScaccoBianco = false;
            ScaccoNero = false;
            Valori = new object[3];
            scaccoMatto = false;
            

            creaScacchiera();
            AggiungiPezzi();

            
        }

        

        private void creaScacchiera()
        {
            // dimensioni di una casella
            int dimensioneCella = 83;

            // Calcola le coordinate del punto iniziale per centrare la scacchiera
            int startX = (this.ClientSize.Width - (8 * dimensioneCella)) / 2 - 10;
            int startY = (this.ClientSize.Height - (8 * dimensioneCella)) / 2 - 10;

          

            // Imposta lo spessore del bordino
            int spessoreBordo = 10;

            // Crea un pannello per il bordino
            Panel bordo = new Panel();
            bordo.Width = (8 * dimensioneCella) + (2 * spessoreBordo);
            bordo.Height = (8 * dimensioneCella) + (2 * spessoreBordo);
            bordo.Top = startY - spessoreBordo;
            bordo.Left = startX - spessoreBordo;

            // Crea un pannello interno alla cornice per la scacchiera
            Panel pannelloScacchiera = new Panel();
            pannelloScacchiera.Width = 8 * dimensioneCella;
            pannelloScacchiera.Height = 8 * dimensioneCella;
            pannelloScacchiera.Top = spessoreBordo;
            pannelloScacchiera.Left = spessoreBordo;
            bordo.Controls.Add(pannelloScacchiera);


            


            // Loop per creare le righe della scacchiera
            for (int righe = 0; righe < 8; righe++)
            {
                // Loop per creare le colonne della scacchiera
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    // Crea una nuova PictureBox per la casella corrente
                    celle[righe, colonne] = new PictureBox();

                    // Imposta le proprietà della PictureBox
                    celle[righe, colonne].Width = dimensioneCella;
                    celle[righe, colonne].Height = dimensioneCella;
                    celle[righe, colonne].Top = righe * dimensioneCella;
                    celle[righe, colonne].Left = colonne * dimensioneCella;
                    celle[righe, colonne].BorderStyle = BorderStyle.FixedSingle;

                    // Imposta il colore della casella in base alla sua posizione
                    if ((righe + colonne) % 2 == 0) // la casella è bianca
                    {
                        celle[righe, colonne].BackColor = Color.FromArgb(205, 204, 202);
                    }
                    else // la casella è nera
                    {
                        celle[righe, colonne].BackColor = Color.FromArgb(58, 57, 61);
                    }

                    // Associa il gestore di eventi all'evento Click della PictureBox
                    celle[righe, colonne].Click += new EventHandler(pictureBox_Click);

                    // Aggiungi la PictureBox alla scacchiera
                    pannelloScacchiera.Controls.Add(celle[righe, colonne]);
                }
            }

            
            // Aggiungi il bordino al form
            bordo.BorderStyle = BorderStyle.FixedSingle;
            bordo.BackColor = Color.Green;
            this.Controls.Add(bordo);


        }






        private void pictureBox_Click(object sender, EventArgs e)
        {
             
           
                AggiornaInterfacciaGrafica();


                // Ottieni la PictureBox cliccata
                PictureBox cella = (PictureBox)sender;
                int x = -1, y = -1;

                // Trova le coordinate della casella nella matrice scacchiera
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (scacchiera[i, j] != null && cella == celle[i, j])
                        {
                            x = i;
                            y = j;
                            break;
                        }
                    }
                }


                // Verifica che le coordinate siano all'interno dei limiti della matrice scacchiera
                if (x < 0 || x >= 8 || y < 0 || y >= 8)
                {
                    return;
                }

                Pezzo pezzo = scacchiera[x, y];


                if (pezzo != null && pezzo.Colore == turnoGiocatore)
                {

                    // Calcola le mosse valide per il pezzo selezionato
                    List<int[]> mosseValide = pezzo.CalcolaMosseValide(x, y, scacchiera);

                    // Colora le caselle in cui il pezzo può muovere
                    ColoraCaselle(mosseValide);


                    // Imposta gli eventi delle PictureBox della casella di destinazione
                    ImpostaEventiPictureBoxDestinazione(mosseValide, pezzo, x, y);


                }

        
            


        }
    


        private void ImpostaEventiPictureBoxDestinazione(List<int[]> mosseValide, Pezzo pezzo, int x, int y)
        {
            foreach (int[] mossa in mosseValide)
            {
                Valori[0] = pezzo;
                Valori[1] = x;
                Valori[2] = y;
                celle[mossa[0], mossa[1]].Click += pictureBoxDestinazione_Click;
            }
            
        }





        private void pictureBoxDestinazione_Click(object sender, EventArgs e)
            {
                
                Pezzo pezzo = (Pezzo)Valori[0];
                int x = (int)Valori[1];
                int y = (int)Valori[2];

                // Ottieni la PictureBox di destinazione cliccata
                PictureBox pictureBoxDestinazione = (PictureBox)sender;
                int xDestinazione = -1, yDestinazione = -1;

            


                // Trova le coordinate della casella di destinazione nella matrice 
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (celle[i, j] == pictureBoxDestinazione)
                        {
                            xDestinazione = i;
                            yDestinazione = j;
                            break;
                        }
                        else
                        {
                            celle[i, j].Click -= pictureBoxDestinazione_Click;
                        }
                }
                }
            


            
                // Esegui la mossa
                bool mossaEseguita = EseguiMossa(pezzo, x, y, xDestinazione, yDestinazione);

                





            if (mossaEseguita)
                {
                    
                    // Cambia il turno del giocatore
                    turnoGiocatore = turnoGiocatore == Colore.Bianco ? Colore.Nero : Colore.Bianco;

                    // Aggiorna l'interfaccia grafica
                    AggiornaInterfacciaGrafica();

                    

                }
                
                 // elimina evento cella destinazione
                this.BeginInvoke(new Action(() =>
                {
                    pictureBoxDestinazione.Click -= pictureBoxDestinazione_Click;
                }));

        }




        private bool EseguiMossa(Pezzo pezzo, int xPartenza, int yPartenza, int xDestinazione, int yDestinazione)
        {



            // calcola mosse valide
            List<int[]> mosseValide = pezzo.CalcolaMosseValide(xPartenza, yPartenza, scacchiera);




            // Controlla se la mossa selezionata è valida
            bool mossaValida = false;

            foreach (int[] mossa in mosseValide)
            {
                if (mossa[0] == xDestinazione && mossa[1] == yDestinazione)
                {
                    mossaValida = true;
                    break;
                }
            }

            if (!mossaValida)
            {
                return false;
            }


            //se re corrente è in scacco e si fa una mossa, ma re corrente continua a essere in scacco, non fare la mossa
            if ((ScaccoBianco == true || ScaccoNero == true) && turnoGiocatore == pezzo.Colore)
            {
                if (VerificaScacco(pezzo.Colore == Colore.Bianco ? Colore.Bianco : Colore.Nero, pezzo, xPartenza, yPartenza, xDestinazione, yDestinazione))                    
                {
                    MessageBox.Show("È scacco !!");
                    return false;
                }
                else // Altrimenti non è scacco
                {
                    ScaccoNero = false;
                    ScaccoBianco = false;
                }
            }
            else  //altrimenti se re corrente non è in scacco e si fa una mossa che mettere il re corrente in scacco, non fare la mossa
            {
                if (VerificaScacco(pezzo.Colore == Colore.Bianco ? Colore.Bianco : Colore.Nero, pezzo, xPartenza, yPartenza, xDestinazione, yDestinazione))
                {
                    MessageBox.Show("È scacco !!");
                    return false;
                }
            }





            // Esegue la mossa

            Pezzo pezzoDestinazione = scacchiera[xDestinazione, yDestinazione];


            if (pezzoDestinazione != null && pezzoDestinazione.Colore != pezzo.Colore)
            {
                // Cattura il pezzo avversario

                if (pezzoDestinazione.Colore == Colore.Bianco)
                {
                    pezziCatturatiBianchi.Add(pezzoDestinazione);
                    int posizione = pezziCatturatiBianchi.Count;
                    string pictureBoxName = "bianco" + posizione;
                    PictureBox pictureBox = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

                    if (pictureBox != null)
                    {
                        pictureBox.Image = pezziCatturatiBianchi[posizione - 1].immagine;
                        pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }

                }
                if (pezzoDestinazione.Colore == Colore.Nero)
                {
                    pezziCatturatiNeri.Add(pezzoDestinazione);
                    int posizione = pezziCatturatiNeri.Count;
                    string pictureBoxName = "nero" + posizione;
                    PictureBox pictureBox = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

                    if (pictureBox != null)
                    {
                        pictureBox.Image = pezziCatturatiNeri[posizione - 1].immagine;
                        pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }


                scacchiera[xDestinazione, yDestinazione] = null;
                celle[xDestinazione, yDestinazione].Image = null;

            }

            // Sposta il pezzo sulla casella di destinazione
            scacchiera[xDestinazione, yDestinazione] = pezzo;
            celle[xDestinazione, yDestinazione].Image = celle[xPartenza, yPartenza].Image;
            celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[xPartenza, yPartenza] = null;
            celle[xPartenza, yPartenza].Image = null;





            //verifica se la mossa effettuata mette il re avversario in scacco

            if (VerificaScacco(pezzo.Colore == Colore.Bianco ? Colore.Nero : Colore.Bianco, pezzo, xPartenza, yPartenza, xDestinazione, yDestinazione))
            {
                scaccoMatto = true;

                if (turnoGiocatore == Colore.Bianco)
                {
                    ScaccoNero = true;
                    
                    // verifica se c'è scacco matto
                    if (VerificaScaccoMatto())
                    {
                        creaPannelloGameOver("Ha vinto il giocatore bianco");
                        scaccoMatto = true;

                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                celle[i, j].Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("È scacco !!");
                    }
                    
                }

                if (turnoGiocatore == Colore.Nero)
                {
                    ScaccoBianco = true;

                    // verifica se c'è scacco matto
                    if (VerificaScaccoMatto())
                    {
                        creaPannelloGameOver("Ha vinto il giocatore nero");
                        scaccoMatto = true;

                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                celle[i, j].Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("È scacco !!");
                    }

                }
                

                scaccoMatto = false;
            }
            





            return true;
        }


        private void creaPannelloGameOver(string giocatore)
        {
            Panel panelGameOver = new Panel();
            panelGameOver.BackColor = Color.Yellow;
            panelGameOver.Size = new Size(600, 300);
            panelGameOver.Location = new Point((this.ClientSize.Width - panelGameOver.Width) / 2 - 10, (this.ClientSize.Height - panelGameOver.Height) / 2);

            Label labelGameOver = new Label();
            labelGameOver.Text = "Game Over";
            labelGameOver.Font = new Font("Segoe UI", 30, FontStyle.Bold);
            labelGameOver.AutoSize = true;
            labelGameOver.Location = new Point((panelGameOver.Width - labelGameOver.Width) / 2 - 60, (panelGameOver.Height / 4) - (labelGameOver.Height / 2));
            labelGameOver.Anchor = AnchorStyles.None;

            Label labelWinner = new Label();
            labelWinner.Text = giocatore;
            labelWinner.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            labelWinner.AutoSize = true;
            labelWinner.Location = new Point((panelGameOver.Width - labelWinner.Width) / 2 - 80, (panelGameOver.Height / 2) + (labelWinner.Height / 2 - 20));
            labelWinner.Anchor = AnchorStyles.None;

            panelGameOver.Controls.Add(labelGameOver);
            panelGameOver.Controls.Add(labelWinner);
            this.Controls.Add(panelGameOver);
            panelGameOver.BringToFront();
        }







        private bool VerificaScaccoMatto()
        {
            // per ogni pezzo avversario controlla se effettuando qualsiasi mossa possibile il re avversario sta ancora in scacco. Se sta ancora in scacco è scacco matto
            
            for (int x = 0; x < scacchiera.GetLength(0); x++)
            {
                for (int y = 0; y < scacchiera.GetLength(1); y++)
                {
                    Pezzo pezzo = scacchiera[x, y];
                    if (pezzo != null && pezzo.Colore != turnoGiocatore)
                    {
                        List<int[]> mosseValide = pezzo.CalcolaMosseValide(x, y, scacchiera);
                        foreach (int[] mossa in mosseValide)
                        {

                            if (VerificaScacco(pezzo.Colore, pezzo, x, y, mossa[0], mossa[1]) == false)
                            {
                                return false;
                            }


                        }
                    }
                }
            }

            return true;


        }




        private bool VerificaScacco(Colore colore, Pezzo pezzo, int xPartenza, int yPartenza, int xDestinazione, int yDestinazione)
        {

            if (scaccoMatto)
            {
                //simula mossa

                Pezzo pezzoDestinazione = scacchiera[xDestinazione, yDestinazione];
                Image immagineDestinazione = null;

                if (pezzoDestinazione != null && pezzoDestinazione.Colore != pezzo.Colore)
                {
                    immagineDestinazione = celle[xDestinazione, yDestinazione].Image;
                    // Cattura il pezzo avversario

                    scacchiera[xDestinazione, yDestinazione] = null;
                    celle[xDestinazione, yDestinazione].Image = null;

                }

                // Sposta il pezzo sulla casella di destinazione
                scacchiera[xDestinazione, yDestinazione] = pezzo;
                celle[xDestinazione, yDestinazione].Image = celle[xPartenza, yPartenza].Image;
                celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;
                scacchiera[xPartenza, yPartenza] = null;
                celle[xPartenza, yPartenza].Image = null;




                // Scorri tutti i pezzi del giocatore corrente per verificare se possono catturare il re avversario
                for (int x = 0; x < scacchiera.GetLength(0); x++)
                {
                    for (int y = 0; y < scacchiera.GetLength(1); y++)
                    {

                        Pezzo pezz = scacchiera[x, y];
                        if (pezz != null && pezz.Colore == turnoGiocatore)
                        {
                            List<int[]> mosseValide = pezz.CalcolaMosseValide(x, y, scacchiera);
                            foreach (int[] mossa in mosseValide)
                            {

                                Pezzo pezzoDestinazion = scacchiera[mossa[0], mossa[1]];

                                // Verifica se la mossa mette il re in scacco
                                bool scacco = false;
                                if (pezzoDestinazion is Re)
                                {
                                    
                                    scacco = true;
                                }
                                else
                                {
                                    // Altrimenti, non è scacco
                                    scacco = false;
                                }


                                // Se la mossa mette il re avversario in scacco, allora c'è scacco
                                if (scacco)
                                {
                                    //ripristina posizioni
                                    scacchiera[xPartenza, yPartenza] = pezzo;
                                    celle[xPartenza, yPartenza].Image = celle[xDestinazione, yDestinazione].Image;
                                    celle[xPartenza, yPartenza].SizeMode = PictureBoxSizeMode.CenterImage;
                                    scacchiera[xDestinazione, yDestinazione] = pezzoDestinazione;
                                    celle[xDestinazione, yDestinazione].Image = immagineDestinazione;
                                    celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;

                                    return true;
                                }

                            }
                        }
                    }
                }

                //ripristina posizioni

                scacchiera[xPartenza, yPartenza] = pezzo;
                celle[xPartenza, yPartenza].Image = celle[xDestinazione, yDestinazione].Image;
                celle[xPartenza, yPartenza].SizeMode = PictureBoxSizeMode.CenterImage;
                scacchiera[xDestinazione, yDestinazione] = pezzoDestinazione;
                celle[xDestinazione, yDestinazione].Image = immagineDestinazione;
                celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;

            }
            else
            {
                if (colore == turnoGiocatore)
                {

                    //simula mossa

                    Pezzo pezzoDestinazione = scacchiera[xDestinazione, yDestinazione];
                    Image immagineDestinazione = null;

                    if (pezzoDestinazione != null && pezzoDestinazione.Colore != pezzo.Colore)
                    {
                        immagineDestinazione = celle[xDestinazione, yDestinazione].Image;
                        // Cattura il pezzo avversario

                        scacchiera[xDestinazione, yDestinazione] = null;
                        celle[xDestinazione, yDestinazione].Image = null;

                    }

                    // Sposta il pezzo sulla casella di destinazione
                    scacchiera[xDestinazione, yDestinazione] = pezzo;
                    celle[xDestinazione, yDestinazione].Image = celle[xPartenza, yPartenza].Image;
                    celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;
                    scacchiera[xPartenza, yPartenza] = null;
                    celle[xPartenza, yPartenza].Image = null;




                    // Scorri tutti i pezzi del giocatore corrente per verificare se possono catturare il re avversario
                    for (int x = 0; x < scacchiera.GetLength(0); x++)
                    {
                        for (int y = 0; y < scacchiera.GetLength(1); y++)
                        {

                            Pezzo pezz = scacchiera[x, y];
                            if (pezz != null && pezz.Colore != turnoGiocatore)
                            {
                                List<int[]> mosseValide = pezz.CalcolaMosseValide(x, y, scacchiera);
                                foreach (int[] mossa in mosseValide)
                                {

                                    Pezzo pezzoDestinazion = scacchiera[mossa[0], mossa[1]];

                                    // Verifica se la mossa mette il re in scacco
                                    bool scacco = false;
                                    if (pezzoDestinazion is Re)
                                    {
                                        // Se il pezzo da catturare è il re avversario, allora è uno scacco
                                        scacco = true;
                                    }
                                    else
                                    {
                                        // Altrimenti, non è scacco
                                        scacco = false;
                                    }


                                    // Se la mossa mette il re avversario in scacco, allora c'è scacco
                                    if (scacco)
                                    {
                                        //ripristina posizioni

                                        scacchiera[xPartenza, yPartenza] = pezzo;
                                        celle[xPartenza, yPartenza].Image = celle[xDestinazione, yDestinazione].Image;
                                        celle[xPartenza, yPartenza].SizeMode = PictureBoxSizeMode.CenterImage;
                                        scacchiera[xDestinazione, yDestinazione] = pezzoDestinazione;
                                        celle[xDestinazione, yDestinazione].Image = immagineDestinazione;
                                        celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;

                                        return true;
                                    }

                                }
                            }
                        }
                    }

                    //ripristina posizioni

                    scacchiera[xPartenza, yPartenza] = pezzo;
                    celle[xPartenza, yPartenza].Image = celle[xDestinazione, yDestinazione].Image;
                    celle[xPartenza, yPartenza].SizeMode = PictureBoxSizeMode.CenterImage;
                    scacchiera[xDestinazione, yDestinazione] = pezzoDestinazione;
                    celle[xDestinazione, yDestinazione].Image = immagineDestinazione;
                    celle[xDestinazione, yDestinazione].SizeMode = PictureBoxSizeMode.CenterImage;



                }
                else
                {
                    // Scorri tutti i pezzi del giocatore avversario per verificare se possono catturare il re corrente
                    for (int x = 0; x < scacchiera.GetLength(0); x++)
                    {
                        for (int y = 0; y < scacchiera.GetLength(1); y++)
                        {

                            Pezzo pezz = scacchiera[x, y];
                            if (pezz != null && pezz.Colore == turnoGiocatore)
                            {
                                List<int[]> mosseValide = pezz.CalcolaMosseValide(x, y, scacchiera);
                                foreach (int[] mossa in mosseValide)
                                {

                                    Pezzo pezzoDestinazion = scacchiera[mossa[0], mossa[1]];

                                    // Verifica se la mossa mette il re in scacco
                                    bool scacco = false;
                                    if (pezzoDestinazion is Re)
                                    {
                                        // Se il pezzo da catturare è il re avversario, allora è uno scacco
                                        scacco = true;
                                    }
                                    else
                                    {
                                        // Altrimenti, non è scacco
                                        scacco = false;
                                    }


                                    // Se la mossa mette il re avversario in scacco, allora c'è scacco
                                    if (scacco)
                                    {
                                        return true;
                                    }

                                }
                            }
                        }
                    }


                }
            }
     
           
            

            // Se nessun pezzo del giocatore corrente può mettere il re avversario in scacco, allora non c'è scacco
            return false;
        }












        private void AggiornaInterfacciaGrafica()
        {
           
            // Aggiorna l'immagine delle PictureBox sulla scacchiera in base alla configurazione della matrice scacchiera
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0) // la casella è bianca
                    {
                        celle[i, j].BackColor = Color.FromArgb(205, 204, 202);
                    }
                    else // la casella è nera
                    {
                        celle[i, j].BackColor = Color.FromArgb(58, 57, 61);
                    }
                }
            }
            
        }





        private void ColoraCaselle(List<int[]> caselle)
        {
            foreach (int[] casella in caselle)
            {
                int x = casella[0];
                int y = casella[1];
                celle[x, y].BackColor = Color.Yellow;
            }
        }





        public void AggiungiPezzi()
        {
            // Aggiungi pedoni
            Image imgPedoneBianco = Properties.Resources.pedone_bianc;
            Image imgPedoneNero = Properties.Resources.pedone_ner;
            for (int i = 0; i < 8; i++)
            {
                scacchiera[1, i] = new Pedone(Colore.Bianco, imgPedoneBianco);
                celle[1, i].Image = imgPedoneBianco;
                celle[1, i].SizeMode = PictureBoxSizeMode.CenterImage;
                scacchiera[6, i] = new Pedone(Colore.Nero, imgPedoneNero);
                celle[6, i].Image = imgPedoneNero;
                celle[6, i].SizeMode = PictureBoxSizeMode.CenterImage;
            }



            // Aggiungi torri
            Image imgTorreBianca = Properties.Resources.torre_bianc;
            Image imgTorreNera = Properties.Resources.torre_ner;

            scacchiera[0, 0] = new Torre(Colore.Bianco, imgTorreBianca);
            celle[0, 0].Image = imgTorreBianca;
            celle[0, 0].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[0, 7] = new Torre(Colore.Bianco, imgTorreBianca);
            celle[0, 7].Image = imgTorreBianca;
            celle[0, 7].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 0] = new Torre(Colore.Nero, imgTorreNera);
            celle[7, 0].Image = imgTorreNera;
            celle[7, 0].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 7] = new Torre(Colore.Nero, imgTorreNera);
            celle[7, 7].Image = imgTorreNera;
            celle[7, 7].SizeMode = PictureBoxSizeMode.CenterImage;




            // Aggiungi cavalli
            Image imgCavalloBianco = Properties.Resources.cavallo_bianc;
            Image imgCavalloNero = Properties.Resources.cavallo_ner;

            scacchiera[0, 1] = new Cavallo(Colore.Bianco, imgCavalloBianco);
            celle[0, 1].Image = imgCavalloBianco;
            celle[0, 1].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[0, 6] = new Cavallo(Colore.Bianco, imgCavalloBianco);
            celle[0, 6].Image = imgCavalloBianco;
            celle[0, 6].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 1] = new Cavallo(Colore.Nero, imgCavalloNero);
            celle[7, 1].Image = imgCavalloNero;
            celle[7, 1].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 6] = new Cavallo(Colore.Nero, imgCavalloNero);
            celle[7, 6].Image = imgCavalloNero;
            celle[7, 6].SizeMode = PictureBoxSizeMode.CenterImage;




            // Aggiungi alfieri
            Image imgAlfiereBianco = Properties.Resources.alfiere_bianc;
            Image imgAlfiereNero = Properties.Resources.alfiere_ner;

            scacchiera[0, 2] = new Alfiere(Colore.Bianco, imgAlfiereBianco);
            celle[0, 2].Image = imgAlfiereBianco;
            celle[0, 2].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[0, 5] = new Alfiere(Colore.Bianco, imgAlfiereBianco);
            celle[0, 5].Image = imgAlfiereBianco;
            celle[0, 5].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 2] = new Alfiere(Colore.Nero, imgAlfiereNero);
            celle[7, 2].Image = imgAlfiereNero;
            celle[7, 2].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 5] = new Alfiere(Colore.Nero, imgAlfiereNero);
            celle[7, 5].Image = imgAlfiereNero;
            celle[7, 5].SizeMode = PictureBoxSizeMode.CenterImage;




            // Aggiungi regine
            Image imgReginaBianca = Properties.Resources.regina_bianc;
            Image imgReginaNera = Properties.Resources.regina_ner;

            scacchiera[0, 4] = new Regina(Colore.Bianco, imgReginaBianca);
            celle[0, 4].Image = imgReginaBianca;
            celle[0, 4].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 4] = new Regina(Colore.Nero, imgReginaNera);
            celle[7, 4].Image = imgReginaNera;
            celle[7, 4].SizeMode = PictureBoxSizeMode.CenterImage;





            // Aggiungi re
            Image imgReBianco = Properties.Resources.re_bianc;
            Image imgReNero = Properties.Resources.re_ner;

            scacchiera[0, 3] = new Re(Colore.Bianco, imgReBianco);
            celle[0, 3].Image = imgReBianco;
            celle[0, 3].SizeMode = PictureBoxSizeMode.CenterImage;
            scacchiera[7, 3] = new Re(Colore.Nero, imgReNero);
            celle[7, 3].Image = imgReNero;
            celle[7, 3].SizeMode = PictureBoxSizeMode.CenterImage;
        }



        public void SplashScreen()
        {
            Application.Run(new Splash());
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            splashScreen.Close();   
        }
    }
}
