using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QnA
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AnswerAggregator answerAggregator;

        public QuestionController(AnswerAggregator answerAggregator)
        {
            this.answerAggregator = answerAggregator;
        }

        [HttpGet]
        public async Task<IEnumerable<PotentialAnswer>> GetAsync(
            [FromQuery(Name = "q")] string? question)
        {
            return string.IsNullOrEmpty(question)
                ? Array.Empty<PotentialAnswer>()
                : await answerAggregator.AskAsync(question);
        }
    }
}
