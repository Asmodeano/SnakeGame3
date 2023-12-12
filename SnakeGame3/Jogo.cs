using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace SnakeGame3
{
    internal class Jogo
    {
        public Keys Direcao { get; set; }
        public Keys Flecha { get; set; }


        private Timer Frame { get; set; }
        private Label Lb { get; set; }
        private Panel Pntela { get; set; }

        private int Pontos = 0;

        private Comida comida;

        private Cobra cobra;

        private Bitmap screen;

        private Graphics bitgrap;

        private Graphics screengrap;

        public Jogo(ref Timer timer, ref Panel panel, ref Label label)
        {
            Pntela = panel;
            Frame = timer;
            Lb = label;
            screen = new Bitmap(428, 428);
            cobra = new Cobra();
            comida = new Comida();
            Direcao = Keys.Left;
            Flecha = Direcao;
        }
        public void Inicio()
        {
            cobra.Reset();
            comida.CriarComida();
            Direcao = Keys.Left;
            bitgrap = Graphics.FromImage(screen);
            screengrap = Pntela.CreateGraphics();
            Frame.Enabled = true;

        }

        public void Tick()
        {

            if (((Flecha == Keys.Left) && (Direcao != Keys.Right)) ||
             ((Flecha == Keys.Right) && (Direcao != Keys.Left) ||
             ((Flecha == Keys.Down) && (Direcao != Keys.Up)) ||
             ((Flecha == Keys.Up) && (Direcao != Keys.Down))))
            {

                Direcao = Flecha;
            }



            switch (Direcao)
            {
                case Keys.Left:
                    cobra.Esquerda();
                    break;
                case Keys.Right:
                    cobra.Direita();
                    break;
                case Keys.Up:
                    cobra.Cima();
                    break;
                case Keys.Down:
                    cobra.Baixo();
                    break;

            }

            bitgrap.Clear(Color.White);

            bitgrap.DrawImage(Properties.Resources.maca,(comida.Location.X * 15), (comida.Location.Y * 15), 15, 15);


            bool gameover = false;


            for (int i = 0; i < cobra.Len; i++)
            {
                if (i == 0)
                {
                    bitgrap.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (cobra.Location[i].X * 15), (cobra.Location[i].Y * 15), 15, 15);




                }
                else
                {

                    bitgrap.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4F4F4F")), (cobra.Location[i].X * 15), (cobra.Location[i].Y * 15), 15, 15);

                }

                if ((cobra.Location[i] == cobra.Location[0]) && (i > 0))
                {
                    gameover = true;
                }


            }

            screengrap.DrawImage(screen, 0, 0);

            CheckColision();

            if (gameover)
            {
                GameOver();
            }





        }

        public void CheckColision()
        {
            if (cobra.Location[0] == comida.Location)
            {
                cobra.Comeu();
                comida.CriarComida();
                Pontos += 1;
                Lb.Text = "PONTOS " + Pontos;
            }
        }

        public void GameOver()
        {
            Frame.Enabled = false;
            bitgrap.Dispose();
            screengrap.Dispose();
            Lb.Text = "PONTOS: 0";
            MessageBox.Show("SE FUDEU");
        }

    }
}
