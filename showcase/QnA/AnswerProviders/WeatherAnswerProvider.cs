using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA
{
    public class WeatherAnswerProvider : IAnswerProvider
    {
        private static readonly ISet<string> WeatherQuestions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "What is the weather?",
            "How cold is it outside?"
        };

        public async Task<PotentialAnswer?> AskAsync(string question)
        {
            if (await IsWeatherQuestion(question))
            {
                return new PotentialAnswer(
                    confidenceScore: 1,
                    domain: "Weather",
                    answer: "Currently, in Seattle, it is 76 degrees. You can expect rain later today.");
            }

            return null;
        }

        private Task<bool> IsWeatherQuestion(string question)
        {
            // This is where we would run some potentially 
            // long-running natural language understanding models.

            return Task.FromResult(WeatherQuestions.Contains(question));
        }
    }
}
