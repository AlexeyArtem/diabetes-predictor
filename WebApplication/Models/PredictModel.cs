using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class PredictModel
    {
        public PredictModel(double probabilityDiabetes) 
        {
            ProbabilityDiabetes = probabilityDiabetes;
        }

        public double ProbabilityDiabetes { get; }
    }
}
