using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication.Models
{

    public class QuestionModel
    {
        public string Key { get; set; }
        public string Text { get; set; }
        [XmlIgnore]
        public AnswerModel SelectedAnswer { get; set; }
        public List<AnswerModel> AnswerChoices { get; set; }
    }

    public class AnswerModel 
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
