using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using WebApplication.Models;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class PredictController : Controller
    {
        public PredictController() { }

        private JObject RequestJson(string url, HttpMethod httpMethod, string jsonParameters = "")
        {
            var content = new StringContent(jsonParameters, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(url),
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

        [HttpGet]
        public IActionResult GetRiskFactors() 
        {
            JObject result = RequestJson("http://127.0.0.1:5000/risk-coefficients", HttpMethod.Get);

            Dictionary<string, double> values = JObject.FromObject(result).ToObject<Dictionary<string, double>>();

            return View("RiskFactors", values);
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
            JObject result = RequestJson("http://127.0.0.1:5000/predict", HttpMethod.Post, jsonStr);
            double probability = result.Value<double>("Probability_diabetes");

            return PartialView("_Predict", new PredictModel(probability));
        }
    }
}
