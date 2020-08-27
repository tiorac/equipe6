using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using Brushes = System.Windows.Media.Brushes;

namespace Equipe6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //
            //
        }

        public MLP mlp { get; set; }
        public System.Drawing.Image Labirinto { get; set; }

        public System.Drawing.Point PosicaoRobo { get; set; }

        public Robo RoboAtual { get; set; }
        
        public Thread PlayRobo { get; set; }

        public bool Parar { get; set; }

        double respostasErradas = 0;
        double erroTotal = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var posicao = BuscarInicio();
            /*var robo = new Robo();

            robo.Sensores[0].VerSaida = false;
            robo.Sensores[0].Parede =false;
            robo.Sensores[0].JaPassou = true;

            robo.Sensores[1].VerSaida = false;
            robo.Sensores[1].Parede = false;
            robo.Sensores[1].JaPassou = false;

            robo.Sensores[2].VerSaida = false;
            robo.Sensores[2].Parede = true;
            robo.Sensores[2].JaPassou = false;

            robo.Sensores[3].VerSaida = true;
            robo.Sensores[3].Parede = false;
            robo.Sensores[3].JaPassou = false;

            var valor = robo.ObterAcao();

            var x = 1;*/



            /*var mlp = new MLP(42, 32, 2, 0.75);
            //var mlp = new MLP(9, 5, 9, 0.75);
            var listaTempo = new List<DateTime>();

            for (int i = 0; i < 10; i++)
            {
                listaTempo.Add(DateTime.Now);
                ProcessarCSV.LerArquivo(mlp);
                listaTempo.Add(DateTime.Now);
            }

            listaTempo.Add(DateTime.Now);
            ProcessarCSV.TestarArquivo(mlp);
            listaTempo.Add(DateTime.Now);*/



            //ProcessarCSV.LerArquivo(mlp, true);
            /*respostasErradas = 0;
            erroTotal = 0;


            for (int i = 0; i <= 1000; i++)
            {
                MaisUmteste(mlp, i < 1000);
            }*/
        }

        private void MaisUmteste(MLP mlp, bool treino)
        {
            mlp.AdicionarValoresParaEntradas(1, 0, 0, 1, 0, 0, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(1, 0, 0, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);


            mlp.AdicionarValoresParaEntradas(0, 1, 0, 1, 0, 0, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 1, 0, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 1, 0, 1, 1, 0, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 1, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 1, 0, 0, 0, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 1, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 1, 0, 1, 0, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 0, 1, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 0, 0, 0, 1, 1, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 0, 0, 1, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 0, 0, 0, 1, 0, 0, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 0, 0, 0, 1, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 0, 0, 0, 0, 0, 1, 1);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 0, 0, 0, 0, 1, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.AdicionarValoresParaEntradas(0, 0, 0, 0, 0, 0, 0, 1, 0);
            mlp.AdicionarValoresParaSaidas(0, 0, 0, 0, 0, 0, 0, 0, 1);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);
        }

        private void testar(MLP mlp)
        {
            for (int t = 0; t < mlp.C; t++)
            {
                if (mlp.y[t] == 1 && mlp.o[t] < 0.5) respostasErradas++;
                if (mlp.y[t] == 0 && mlp.o[t] > 0.5) respostasErradas++;

                erroTotal += Math.Abs(mlp.y[t] - mlp.o[t]);
            }
        }

        private void CarregarImagem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog().GetValueOrDefault())
            {

                Labirinto = Bitmap.FromFile(dialog.FileName);
                imagem.Source = new BitmapImage(new Uri(dialog.FileName));
                PosicaoRobo = BuscarInicio();
                MoverRobo();
                RoboAtual = new Robo();
            }
        }

        private void MoverRobo()
        {
            desenhoRobo.Margin = new Thickness(PosicaoRobo.X - (desenhoRobo.ActualWidth / 2), PosicaoRobo.Y - (desenhoRobo.ActualHeight / 2), 0, 0);
        }

        private void Play()
        {
            Parar = false;

            PlayRobo = new Thread(() =>
            {
                while (!Parar)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        var tamanhoRobo = (int)(desenhoRobo.ActualWidth);
                        //var tamanhoPasso = tamanhoRobo;
                        //var tamanhoPasso = tamanhoRobo / 2;
                        var tamanhoPasso = 1;

                        RoboAtual.AtualizarSensor(tamanhoRobo, tamanhoPasso, PosicaoRobo, (Bitmap)Labirinto);
                        var acao = RoboAtual.ObterAcao();
                        MostrarStatusSensor(acao);

                        switch (acao)
                        {
                            case Direcao.Esquerda:
                                PosicaoRobo = new System.Drawing.Point(PosicaoRobo.X - tamanhoPasso, PosicaoRobo.Y);
                                break;

                            case Direcao.Cima:
                                PosicaoRobo = new System.Drawing.Point(PosicaoRobo.X, PosicaoRobo.Y - tamanhoPasso);
                                break;

                            case Direcao.Direita:
                                PosicaoRobo = new System.Drawing.Point(PosicaoRobo.X + tamanhoPasso, PosicaoRobo.Y);
                                break;

                            case Direcao.Baixo:
                                PosicaoRobo = new System.Drawing.Point(PosicaoRobo.X, PosicaoRobo.Y + tamanhoPasso);
                                break;

                            case Direcao.CtrlZ:
                            default:
                                break;
                        }

                        MoverRobo();
                    });

                    //Thread.Sleep(10);
                }
            });

            PlayRobo.Start();
        }

        private void MostrarStatusSensor(Direcao direcao)
        {
            Sensor1_Saida.Fill = RoboAtual.Sensores[0].VerSaida ? Brushes.Blue : Brushes.Red;
            Sensor1_Parede.Fill = RoboAtual.Sensores[0].Parede ? Brushes.Blue : Brushes.Red;
            Sensor1_Volta.Fill = RoboAtual.Sensores[0].JaPassou ? Brushes.Blue : Brushes.Red;

            Sensor2_Saida.Fill = RoboAtual.Sensores[1].VerSaida ? Brushes.Blue : Brushes.Red;
            Sensor2_Parede.Fill = RoboAtual.Sensores[1].Parede ? Brushes.Blue : Brushes.Red;
            Sensor2_Volta.Fill = RoboAtual.Sensores[1].JaPassou ? Brushes.Blue : Brushes.Red;

            Sensor3_Saida.Fill = RoboAtual.Sensores[2].VerSaida ? Brushes.Blue : Brushes.Red;
            Sensor3_Parede.Fill = RoboAtual.Sensores[2].Parede ? Brushes.Blue : Brushes.Red;
            Sensor3_Volta.Fill = RoboAtual.Sensores[2].JaPassou ? Brushes.Blue : Brushes.Red;

            Sensor4_Saida.Fill = RoboAtual.Sensores[3].VerSaida ? Brushes.Blue : Brushes.Red;
            Sensor4_Parede.Fill = RoboAtual.Sensores[3].Parede ? Brushes.Blue : Brushes.Red;
            Sensor4_Volta.Fill = RoboAtual.Sensores[3].JaPassou ? Brushes.Blue : Brushes.Red;

            Decisao_Esq.Fill = direcao == Direcao.Esquerda ? Brushes.Blue : Brushes.Red;
            Decisao_Cima.Fill = direcao == Direcao.Cima ? Brushes.Blue : Brushes.Red;
            Decisao_Dir.Fill = direcao == Direcao.Direita ? Brushes.Blue : Brushes.Red;
            Decisao_Baixo.Fill = direcao == Direcao.Baixo ? Brushes.Blue : Brushes.Red;
        }

        private System.Drawing.Point BuscarInicio()
        {
            for (int x = 0; x < Labirinto.Width; x++)
            {
                for (int y = 0; y < Labirinto.Height; y++)
                {
                    System.Drawing.Color cor = ((Bitmap)Labirinto).GetPixel(x, y);

                    if (cor.R == 0 && cor.G == 176 && cor.B == 80)
                        return new System.Drawing.Point(x, y);
                }
            }

            return new System.Drawing.Point(0, 0);
        }

        private void StartMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void RestartMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Parar = true;
            PosicaoRobo = BuscarInicio();
            MoverRobo();
            RoboAtual = new Robo();
        }
    }
}
