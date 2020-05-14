using Poem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA
{
    [PotentialPoemService]
    public class LotteryAnswerProvider : IAnswerProvider
    {
        private static readonly ISet<string> LotteryQuestions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "What are the winning lottery numbers?",
            "Did I win the lottery?"
        };

        public async Task<PotentialAnswer?> AskAsync(string question)
        {
            if (await IsLotteryQuestion(question))
            {
                return new PotentialAnswer(
                    confidenceScore: 0.7,
                    domain: "Lottery",
                    answer: "1, 2, 4, 8, 16, 32");
            }

            return null;
        }

        private Task<bool> IsLotteryQuestion(string question)
        {
            // This is where we would run some potentially 
            // long-running natural language understanding models.

            return Task.FromResult(LotteryQuestions.Contains(question));
        }
    }
}
