using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public enum DiabetesRisk 
    {
        Low,
        Middle,
        Hight
    }

    public class PredictModel
    {
        public PredictModel(double probabilityDiabetes) 
        {
            if (probabilityDiabetes > 1 || probabilityDiabetes < 0)
                throw new ArgumentException("Значение вероятности должно быть в пределах от 0 до 1");

            ProbabilityDiabetes = probabilityDiabetes;
            DiabetesRisk = AnalyzeProbability(probabilityDiabetes);
            
        }

        public double ProbabilityDiabetes { get; }
        public DiabetesRisk DiabetesRisk { get; }

        private DiabetesRisk AnalyzeProbability(double probabilityDiabetes) 
        {
            DiabetesRisk diabetesRisk;
            switch (probabilityDiabetes) 
            {
                case double value when (value <= 0.3):
                    diabetesRisk = DiabetesRisk.Low;
                    break;
                case double value when (value <= 0.6):
                    diabetesRisk = DiabetesRisk.Middle;
                    break;
                default:
                    diabetesRisk = DiabetesRisk.Hight;
                    break;
            }
            return diabetesRisk;
        }
    }
}
