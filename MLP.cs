using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6
{
    public class MLP
    {
		public MLP(int numEntradas, int numEscondidos, int numSaidas, double taxaAprendizado)
		{
			A = numEntradas;
			B = numEscondidos;
			C = numSaidas;
			N = taxaAprendizado;

			/** inicialização e criação da rede conforme os números fornecidos **/

			x = new double[A];
			h = new double[B]; s = new double[B]; f = new double[B];
			o = new double[C];
			y = new double[C]; u = new double[C];

			w = new double[A, B]; deltaW = new double[A, B];
			q = new double[B, C]; deltaQ = new double[B, C];

			/** inicialização das descrições **/

			descEnt = new string[A];
			descSai = new string[C];

			InitRede();

			//d = new BufferedReader(new InputStreamReader(System.in));
			//textOut = new JTextArea(30, 80);
		}


		public double[] x, h, o, y;
		public double[,] w, q;

		public int A, B, C;

		public int i, j, k;

		public double[] u, s, f;
		public double[,] deltaQ, deltaW;

		public double N;

		public string[] descEnt, descSai;

		public string nomeArquivo = "";

		/// <summary>
		/// Escrever "zero" nas entradas e saidas desejadas (para apresentar um novo conjunto de teste)
		/// </summary>
		public void ZeraEntradasSaidasDesejadas()
		{
			for (int i = 0; i < A; i++)
				x[i] = 0;

			for (int k = 0; k < C; k++)
				y[k] = 0;
		}

		/// <summary>
		/// Escrever "zero" nas ativacoes da camada escondida e nas saidas (para poder funcionar a rede)
		/// </summary>
		public void ZeraEscondidosSaidas()
		{
			for (int j = 0; j < B; j++)
				h[j] = 0;

			for (int k = 0; k < C; k++)
				o[k] = 0;
		}

		public void InitRede()
		{
			ZeraEntradasSaidasDesejadas();

			/** inicializar sinapses com valores entre -0.1 e 0.1 **/
			var random = new Random();


			for (int i = 0; i < A; i++)
				for (int j = 0; j < B; j++)
					w[i, j] = ( random.NextDouble() / 5) - 0.1;

			for (int j = 0; j < B; j++)
				for (int k = 0; k < C; k++)
					q[j, k] = (random.NextDouble() / 5) - 0.1;

			for (int i = 0; i < A; i++)
				descEnt[i] = "";

			for (int k = 0; k < C; k++)
				descSai[k] = "";
		}

		public double Sigmoide(double x)
		{
			return (1 / (1 + Math.Exp(-x)));
		}

		public void FuncionarRede()
		{
			ZeraEscondidosSaidas();

			for (i = 0; i < A; i++)
				for (j = 0; j < B; j++)
					h[j] = h[j] + x[i] * w[i, j];

			for (j = 0; j < B; j++)
				h[j] = Sigmoide(h[j]);

			for (j = 0; j < B; j++)
				for (k = 0; k < C; k++)
					o[k] = o[k] + h[j] * q[j, k];

			for (k = 0; k < C; k++)
				o[k] = Sigmoide(o[k]);

		}

		public void BackPropagation()
		{
			/** Calculo dos erros nas saidas **/

			for (k = 0; k < C; k++)
				u[k] = o[k] * (1.0 - o[k]) * (y[k] - o[k]);

			/** Calculo dos erros na camada escondida **/

			for (j = 0; j < B; j++)
			{
				s[j] = 0.0;

				for (k = 0; k < C; k++)
					s[j] = s[j] + u[k] * q[j, k];

				f[j] = h[j] * (1.0 - h[j]) * s[j];
			}

			/** Delta nas sinapses entre camada escondida e saida **/

			for (j = 0; j < B; j++)
				for (k = 0; k < C; k++)
					deltaQ[j, k] = N * u[k] * h[j];

			/** Delta nas sinapses entre entradas e camanda escondida **/

			for (i = 0; i < A; i++)
				for (j = 0; j < B; j++)
					deltaW[i, j] = N * f[j] * x[i];

			/** Execucao de ajustes **/

			for (j = 0; j < B; j++)
				for (k = 0; k < C; k++)
					q[j, k] = q[j, k] + deltaQ[j, k];

			for (i = 0; i < A; i++)
				for (j = 0; j < B; j++)
					w[i, j] = w[i, j] + deltaW[i, j];

		}

		//treinaArquivo

		public void teste_valoresParaEntradas(params double[] values)
		{
			for (int g = 0; g < A; g++)
			{
				x[g] =  values[g];
			}
		}

		public void teste_valoresParaSaidas(params double[] values)
		{
			for (int g = 0; g < C; g++)
			{
				y[g] = values[g];
			}
		}
	}
}
