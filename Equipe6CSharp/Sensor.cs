using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6
{
    public class Sensor
    {
        public Sensor(Robo robo, int fatorX, int fatorY)
        {
            this.Robo = robo;
            FatorX = fatorX;
            FatorY = fatorY;
        }

        public Robo Robo { get; set; }

        public bool VerSaida { get; set; }

        public bool Parede { get; set; }

        public bool JaPassou { get; set; }

        public int FatorX { get; set; }

        public int FatorY { get; set; }
    }
}
