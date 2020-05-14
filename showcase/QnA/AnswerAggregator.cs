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

            // Both the Weather and News answers are considered critical, and as such, the aggregator
            // can wait up to 300 ms for them to respond. On the other hand, the less vital Lottery and 
            // Restaurant Finder answers can block for no more than 200 ms or till the completion of 
            // Weather and News tasks.

            Task.WaitAll(tasks, millisecondsTimeout: 200);

            if (!weatherTask.IsCompleted || !newsTask.IsCompleted)
                Task.WaitAll(tasks, millisecondsTimeout: 100);

            return tasks
                .Where(t => t.IsCompletedSuccessfully && t.Result?.ConfidenceScore > 0.65)
                .Select(t => t.Result!)
                .OrderByDescending(a => a.ConfidenceScore);
        }
    }
}
