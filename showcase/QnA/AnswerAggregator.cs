using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA
{
    public class AnswerAggregator
    {
        private readonly LotteryAnswerProvider lotteryProvider;
        private readonly NewsAnswerProvider newsProvider;
        private readonly RestaurantFinderAnswerProvider restaurantProvider;
        private readonly WeatherAnswerProvider weatherProvider;

        public AnswerAggregator(
            LotteryAnswerProvider lotteryAnswerProvider,
            NewsAnswerProvider newsAnswerProvider,
            RestaurantFinderAnswerProvider restaurantAnswerProvider,
            WeatherAnswerProvider weatherAnswerProvider)
        {
            lotteryProvider = lotteryAnswerProvider;
            newsProvider = newsAnswerProvider;
            restaurantProvider = restaurantAnswerProvider;
            weatherProvider = weatherAnswerProvider;
        }

        public async Task<IEnumerable<PotentialAnswer>> AskAsync(
            string question)
        {
            // Send the question to all answer providers in parallel but
            // do NOT wait for them to complete execution.

            var weatherTask = weatherProvider.AskAsync(question);
            var newsTask = newsProvider.AskAsync(question);
            var restaurantFinderTask = restaurantProvider.AskAsync(question);
            var lotteryTask = lotteryProvider.AskAsync(question);

            var tasks = new[]
            {
                weatherTask ,
                newsTask,
                restaurantFinderTask,
                lotteryTask
            };

            // Both the Weather and News answers are considered critical,
            // and as such, the aggregator can wait up to 300 ms for them
            // to respond. On the other hand, the less vital Lottery and
            // Restaurant Finder answers can block for no more than 200 ms
            // or till the completion of Weather and News tasks.

            // Waits for all answer providers to complete execution
            // within 200 ms
            Task.WaitAll(tasks, millisecondsTimeout: 200);

            // If needed, waits for Weather and News answer providers to
            // complete execution within an extra 100 ms
            if (!weatherTask.IsCompleted || !newsTask.IsCompleted)
                Task.WaitAll(tasks, millisecondsTimeout: 100);

            return tasks
                .Where(t => t.IsCompletedSuccessfully && t.Result?.ConfidenceScore > 0.65)
                .Select(t => t.Result!)
                .OrderByDescending(a => a.ConfidenceScore);
        }
    }
}
