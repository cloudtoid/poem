using Poem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA
{
    [PotentialPoemService]
    public class NewsAnswerProvider : IAnswerProvider
    {
        private static readonly ISet<string> NewsQuestions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "What is my daily briefing?",
            "What is the latest news?"
        };

        public async Task<PotentialAnswer?> AskAsync(string question)
        {
            if (await IsNewsQuestion(question))
            {
                return new PotentialAnswer(
                    confidenceScore: 0.8,
                    domain: "News",
                    answer: "A resident of Seattle donates smartphones to help elderly connect with families.");
            }

            return null;
        }

        private Task<bool> IsNewsQuestion(string question)
        {
            // This is where we would run some potentially 
            // long-running natural language understanding models.

            return Task.FromResult(NewsQuestions.Contains(question));
        }
    }
}
