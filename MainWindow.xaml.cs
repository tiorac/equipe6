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
            //https://www.kaggle.com/xwolf12/malicious-and-benign-websites/data
        }

        double respostasErradas = 0;
        double erroTotal = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mlp = new MLP(9, 5, 9, 0.75);


            respostasErradas = 0;
            erroTotal = 0;


            for (int i = 0; i <= 1000; i++)
            {
                MaisUmteste(mlp, i < 1000);
            }
        }

        private void MaisUmteste(MLP mlp, bool treino)
        {
            mlp.teste_valoresParaEntradas(1, 0, 0, 1, 0, 0, 0, 0, 0);
            mlp.teste_valoresParaSaidas(1, 0, 0, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);


            mlp.teste_valoresParaEntradas(0, 1, 0, 1, 0, 0, 0, 0, 0);
            mlp.teste_valoresParaSaidas(0, 1, 0, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 1, 0, 1, 1, 0, 0, 0, 0);
            mlp.teste_valoresParaSaidas(0, 0, 1, 0, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 1, 0, 0, 0, 0, 0, 0);
            mlp.teste_valoresParaSaidas(0, 0, 0, 1, 0, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 1, 0, 1, 0, 0, 0, 0);
            mlp.teste_valoresParaSaidas(0, 0, 0, 0, 1, 0, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 0, 0, 0, 1, 1, 0, 0);
            mlp.teste_valoresParaSaidas(0, 0, 0, 0, 0, 1, 0, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 0, 0, 0, 1, 0, 0, 0);
            mlp.teste_valoresParaSaidas(0, 0, 0, 0, 0, 0, 1, 0, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 0, 0, 0, 0, 0, 1, 1);
            mlp.teste_valoresParaSaidas(0, 0, 0, 0, 0, 0, 0, 1, 0);
            mlp.FuncionarRede();
            if (treino) mlp.BackPropagation(); else testar(mlp);

            mlp.teste_valoresParaEntradas(0, 0, 0, 0, 0, 0, 0, 1, 0);
            mlp.teste_valoresParaSaidas(0, 0, 0, 0, 0, 0, 0, 0, 1);
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
    }
}
