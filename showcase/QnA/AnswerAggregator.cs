using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA
{
    public class AnswerAggregator
    {
        private readonly LotteryAnswerProvider lotteryAnswerProvider;
        private readonly NewsAnswerProvider newsAnswerProvider;
        private readonly RestaurantFinderAnswerProvider restaurantFinderAnswerProvider;
        private readonly WeatherAnswerProvider weatherAnswerProvider;

        public AnswerAggregator(
            LotteryAnswerProvider lotteryAnswerProvider,
            NewsAnswerProvider newsAnswerProvider,
            RestaurantFinderAnswerProvider restaurantFinderAnswerProvider,
            WeatherAnswerProvider weatherAnswerProvider)
        {
            this.lotteryAnswerProvider = lotteryAnswerProvider;
            this.newsAnswerProvider = newsAnswerProvider;
            this.restaurantFinderAnswerProvider = restaurantFinderAnswerProvider;
            this.weatherAnswerProvider = weatherAnswerProvider;
        }

        public async Task<IEnumerable<PotentialAnswer>> AskAsync(string question)
        {
            var weatherTask = weatherAnswerProvider.AskAsync(question);
            var newsTask = newsAnswerProvider.AskAsync(question);
            var restaurantFinderTask = restaurantFinderAnswerProvider.AskAsync(question);
            var lotteryTask = lotteryAnswerProvider.AskAsync(question);

            var tasks = new[] { weatherTask, newsTask, restaurantFinderTask, lotteryTask };

            // Only the weather and news answer providers are critical, 
            // and we should wait for them a minimum of 200 ms to complete.
            // However, there is no need to block on Lottery or Restaurant 
            // Finder answer providers.

            Task.WaitAll(new[] { weatherTask, newsTask }, 200);

            return tasks
                .Where(t => t.IsCompletedSuccessfully && t.Result?.ConfidenceScore > 0.65)
                .Select(t => t.Result)!;
        }
    }
}
