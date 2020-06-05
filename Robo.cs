using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Equipe6
{
    public class Robo
    {
        public const double TAXAAPRENDIZADO = 0.75;
        public const int SAIDAS = 5;
        public const int TAXATREINAMENTO = 1000;

        public Robo()
        {
            Historico = new List<Point>();
            PontoReferencia = new Point(0, 0);
            CriarSensores();
            CriarCerebro();
            TreinarCerebro();
            PonteiroVolta = -1;
        }

        public List<Sensor> Sensores { get; set; }

        public MLP Cerebro { get; set; }

        public double TaxaAprendizado { get; set; }

        public List<Point> Historico { get; set; }

        public Point PontoReferencia { get; set; }

        public int PonteiroVolta { get; set; }

        /// <summary>
        /// Cria os sensores do Robo
        /// </summary>
        private void CriarSensores()
        {
            this.Sensores = new List<Sensor>();

            this.Sensores.Add(new Sensor(this, -1, 0));
            this.Sensores.Add(new Sensor(this, 0, -1));
            this.Sensores.Add(new Sensor(this, 1, 0));
            this.Sensores.Add(new Sensor(this, 0, 1));
        }

        /// <summary>
        /// Cria o cérebro
        /// </summary>
        private void CriarCerebro()
        {
            var numEscondidos = this.Sensores.Count * 2;
            var numEntradas = this.Sensores.Count * 3;

            this.Cerebro = new MLP(numEntradas, numEscondidos, SAIDAS, TAXAAPRENDIZADO);
        }

        /// <summary>
        /// Treina o cérebro
        /// </summary>
        private void TreinarCerebro()
        {
            for (int i = 0; i < TAXATREINAMENTO; i++)
            {
                //Treino Saídas

                TreinarUmaInstancia(new double[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    new double[] { 1, 0, 0, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    new double[] { 0, 1, 0, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                                    new double[] { 0, 0, 1, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
                                    new double[] { 0, 0, 0, 1, 0 });


                TreinarUmaInstancia(new double[] { 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1 },
                                    new double[] { 1, 0, 0, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1 },
                                    new double[] { 0, 1, 0, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1 },
                                    new double[] { 0, 0, 1, 0, 0 });

                TreinarUmaInstancia(new double[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1 },
                                    new double[] { 0, 0, 0, 1, 0 });

                //Treino Paredes

                TreinarUmaInstancia(new double[] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        new double[] { 0, 1, 1, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                                        new double[] { 1, 0, 1, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                                        new double[] { 1, 1, 0, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                                        new double[] { 1, 1, 1, 0, 0 });

                //Treino Volta

                TreinarUmaInstancia(new double[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        new double[] { 0, 1, 1, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                                        new double[] { 1, 0, 1, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
                                        new double[] { 1, 1, 0, 1, 0 });

                TreinarUmaInstancia(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        new double[] { 1, 1, 1, 0, 0 });



                //Ctrl+Z

                TreinarUmaInstancia(new double[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 },
                                        new double[] { 0, 0, 0, 0, 1 });

                TreinarUmaInstancia(new double[] { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 },
                                        new double[] { 0, 0, 0, 0, 1 });

                TreinarUmaInstancia(new double[] { 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1 },
                                        new double[] { 0, 0, 0, 0, 1 });

                TreinarUmaInstancia(new double[] { 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0 },
                                        new double[] { 0, 0, 0, 0, 1 });

                TreinarUmaInstancia(new double[] { 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0 },
                                        new double[] { 0, 0, 0, 0, 1 });

                TreinarUmaInstancia(new double[] { 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1 },
                                        new double[] { 0, 0, 0, 0, 1 });

                /*
                TreinarUmaInstancia(new double[] { 0, 0, 0,   0, 0, 0,   0, 0, 0,   0, 0, 0 }, 
                                    new double[] { 0, 0, 0, 0, 0 });
                */
            }
        }

        private void TreinarUmaInstancia(IEnumerable<double> entradas, IEnumerable<double> saidas)
        {
            this.Cerebro.AdicionarValoresParaEntradas(entradas.ToArray());
            this.Cerebro.AdicionarValoresParaSaidas(saidas.ToArray());
            this.Cerebro.FuncionarRede();
            this.Cerebro.BackPropagation();
        }


        public void AtualizarSensor(int tamanhoRobo, int tamanhoPasso, Point posicaoRobo, Bitmap labirinto)
        {
            foreach (var sensor in Sensores)
            {
                sensor.Parede = VerificarParede(sensor, tamanhoRobo, tamanhoPasso, posicaoRobo, labirinto);
                sensor.VerSaida = VerificarSaida(sensor, tamanhoRobo, posicaoRobo, labirinto);
                sensor.JaPassou = VerificarHistorico(sensor);
            }
        }

        private bool VerificarParede(Sensor sensor, int tamanhoRobo, int tamanhoPasso, Point posicaoRobo, Bitmap labirinto)
        {
            int posX = posicaoRobo.X, posY = posicaoRobo.Y;
            int count = 0;

            if (sensor.FatorX > 0)
                posX += tamanhoRobo / 2;
            else if (sensor.FatorX < 0)
                posX -= tamanhoRobo / 2;

            if (sensor.FatorY > 0)
                posY += tamanhoRobo / 2;
            else if (sensor.FatorY < 0)
                posY -= tamanhoRobo / 2;

            while (posX >= 0 && posX < labirinto.Width
                && posY >= 0 && posY < labirinto.Height)
            {
                if (count > tamanhoPasso)
                    return false;

                var cor = labirinto.GetPixel(posX, posY);
                if (cor.R == 0 && cor.G == 0 && cor.B == 0)
                    return true;

                if (sensor.FatorX != 0)
                {
                    for (int i = posY - (tamanhoRobo / 2); i < (posY + tamanhoRobo) / 2; i++)
                    {
                        cor = labirinto.GetPixel(posX, i);
                        if (cor.R == 0 && cor.G == 0 && cor.B == 0)
                            return true;
                    }
                }

                if (sensor.FatorY != 0)
                {
                    for (int i = posX - (tamanhoRobo / 2); i < posX + (tamanhoRobo / 2); i++)
                    {
                        cor = labirinto.GetPixel(i, posY);
                        if (cor.R == 0 && cor.G == 0 && cor.B == 0)
                            return true;
                    }
                }

                count++;
                posX += sensor.FatorX;
                posY += sensor.FatorY;
            }

            return true;
        }

        private bool VerificarSaida(Sensor sensor, int tamanhoRobo, Point posicaoRobo, Bitmap labirinto)
        {
            int posX = posicaoRobo.X, posY = posicaoRobo.Y;

            if (sensor.FatorX > 0)
                posX += tamanhoRobo / 2;
            else if (sensor.FatorX < 0)
                posX -= tamanhoRobo / 2;

            if (sensor.FatorY > 0)
                posY += tamanhoRobo / 2;
            else if (sensor.FatorY < 0)
                posY -= tamanhoRobo / 2;

            while (posX >= 0 && posX < labirinto.Width
                && posY >= 0 && posY < labirinto.Height)
            {
                var cor = labirinto.GetPixel(posX, posY);
                if (cor.R == 0 && cor.G == 112 && cor.B == 192)
                {
                    if (sensor.FatorX != 0)
                    {
                        for (int i = posY - (tamanhoRobo / 2); i < posY + (tamanhoRobo / 2); i++)
                        {
                            cor = labirinto.GetPixel(posX, i);
                            if (cor.R != 0 || cor.G != 112 && cor.B != 192)
                                return false;
                        }
                    }

                    if (sensor.FatorY != 0)
                    {
                        for (int i = posX - (tamanhoRobo / 2); i < posX + (tamanhoRobo / 2); i++)
                        {
                            cor = labirinto.GetPixel(i, posY);
                            if (cor.R != 0 || cor.G != 112 || cor.B != 192)
                                return false;
                        }
                    }

                    return true;
                }

                //Está vendo uma parede, fim!!!!
                if (cor.R == 0 && cor.G == 0 && cor.B == 0)
                    return false;

                posX += sensor.FatorX;
                posY += sensor.FatorY;
            }

            return false;
        }

        private bool VerificarHistorico(Sensor sensor)
        {
            var novoPonto = new Point(sensor.FatorX + PontoReferencia.X, sensor.FatorY + PontoReferencia.Y);
            return Historico.Contains(novoPonto);
        }

        public Direcao ObterAcao()
        {
            List<double> entradas = new List<double>();

            foreach (var sensor in this.Sensores)
            {
                entradas.Add(Convert.ToDouble(sensor.VerSaida));
                entradas.Add(Convert.ToDouble(sensor.Parede));
                entradas.Add(Convert.ToDouble(sensor.JaPassou));
            }

            Cerebro.AdicionarValoresParaEntradas(entradas.ToArray());
            Cerebro.FuncionarRede();
            var resultado = Cerebro.ObterResultado();
            var maior = resultado.Max();
            var index = resultado.IndexOf(maior);
            var direcao = (Direcao)index;

            if (direcao == Direcao.CtrlZ)
                direcao = ObterDirecaoCtrlZ();
            else
                PonteiroVolta = -1;

            Historico.Add(PontoReferencia);

            switch (direcao)
            {
                case Direcao.Esquerda:
                    PontoReferencia = new Point(PontoReferencia.X - 1, PontoReferencia.Y);
                    break;

                case Direcao.Cima:
                    PontoReferencia = new Point(PontoReferencia.X, PontoReferencia.Y - 1);
                    break;

                case Direcao.Direita:
                    PontoReferencia = new Point(PontoReferencia.X + 1, PontoReferencia.Y);
                    break;
                case Direcao.Baixo:
                    PontoReferencia = new Point(PontoReferencia.X, PontoReferencia.Y + 1);
                    break;

                default:
                    break;
            }

            if (direcao == Direcao.Esquerda && Sensores[0].Parede)
                throw new Exception("Fuuuu!!");

            if (direcao == Direcao.Cima && Sensores[1].Parede)
                throw new Exception("Fuuuu!!");

            if (direcao == Direcao.Direita && Sensores[2].Parede)
                throw new Exception("Fuuuu!!");

            if (direcao == Direcao.Baixo && Sensores[3].Parede)
                throw new Exception("Fuuuu!!");

            return direcao;
        }

        private Direcao ObterDirecaoCtrlZ()
        {
            if (PonteiroVolta == -1)
                PonteiroVolta = Historico.Count - 1;

            var posicaoVoltar = Historico[PonteiroVolta];
            PonteiroVolta--;

            if (posicaoVoltar.X < PontoReferencia.X)
                return Direcao.Esquerda;
            else if (posicaoVoltar.X > PontoReferencia.X)
                return Direcao.Direita;
            else if (posicaoVoltar.Y < PontoReferencia.Y)
                return Direcao.Cima;
            else if (posicaoVoltar.Y > PontoReferencia.Y)
                return Direcao.Baixo;

            //Se chegou aqui, é pq fudeu!!!!
            return Direcao.Baixo;
        }
    }
}
