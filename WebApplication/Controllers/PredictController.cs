using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PredictController : Controller
    {
        private string apiUrl;

        public PredictController() 
        {
            apiUrl = "http://127.0.0.1:5000/predict";
        }

        private JObject RequestPredict(string jsonParameters)
        {
            var content = new StringContent(jsonParameters, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl),
                Content = content
            };

            using (HttpClient client = new HttpClient())
            {
                var response = client.SendAsync(request);

                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string resultStr = response.Result.Content.ReadAsStringAsync().Result;
                    JObject resultJson = JObject.Parse(resultStr);
                    return resultJson;
                }
                else
                {
                    // возвратить ошибку
                    return null;
                }
            }
        }

        [HttpPost]
        public IActionResult GetPredict([FromBody]AnswerModel[] data)
        {
            //TODO: сделать проверку data на NULL

            JObject jsonObject = new JObject();
            foreach (var answer in data)
            {
                jsonObject[answer.QuestionKey] = answer.AnswerValue;
            }
            string jsonStr = JsonConvert.SerializeObject(jsonObject);
            JObject result = RequestPredict(jsonStr);
            double probability = result.Value<double>("Probability_diabetes");

            return PartialView("_Predict", new PredictModel(probability));
        }
    }
}
