using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication.Models
{
    public class AnswerModel 
    {
        public string QuestionKey { get; set; }
        public double AnswerValue { get; set; }
    }

    public class QuestionModel
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public List<ChoiceModel> AnswerChoices { get; set; }
    }

    public class ChoiceModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
