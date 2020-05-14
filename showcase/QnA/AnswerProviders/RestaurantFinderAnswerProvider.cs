using Poem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA
{
    [PotentialPoemService]
    public class RestaurantFinderAnswerProvider : IAnswerProvider
    {
        private static readonly ISet<string> RestaurantQuestions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "What are the restaurants near me?",
            "Where can I eat tonight?"
        };

        public async Task<PotentialAnswer?> AskAsync(string question)
        {
            if (await IsRestaurantQuestion(question))
            {
                return new PotentialAnswer(
                    confidenceScore: 0.5,
                    domain: "RestaurantFinder",
                    answer: "Here are a few nearby restaurants: Canlis, 0.6 miles away; Manolin, 0.7 miles away.");
            }

            return null;
        }

        private Task<bool> IsRestaurantQuestion(string question)
        {
            // This is where we would run some potentially 
            // long-running natural language understanding models.

            return Task.FromResult(RestaurantQuestions.Contains(question));
        }
    }
}
