# Poem platform explained using a sample

> If you are new to Poem, read [this](../../) first. 

*See the [code](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/) for this sample on GitHub.*

The sample here is a Question and Answer (QnA) application, that given a question, can respond with zero or more answers. Each answer has a *score*, belongs to a *domain* such as Weather or News, and can be provided by a separate microservice. However, it is the Poem platform and not the developer that decides which, if any, of these answer providers should be an independent microservice.

## Application structure

The QnA application receives requests through the [`Question`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/Controllers/QuestionController.cs) REST endpoint and processes them within a single monolith service.

```csharp
[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase
{
    [HttpGet]
    public async Task<IList<PotentialAnswer>> GetAsync(
        [FromQuery(Name = "q")] string? question)
    {
        // This is the method that receives the HTTP Get request
        // with a question and returns a list of potential answers
    }
}
```

Once the REST endpoint receives a question, it forwards it to the [`Answer Aggregator`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerAggregator.cs), which in turn, dispatches the question in *parallel* to all the answer providers ([`Weather`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/WeatherAnswerProvider.cs), [`News`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/NewsAnswerProvider.cs), [`Restaurant Finder`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/RestaurantFinderAnswerProvider.cs), and [`Lottery`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/LotteryAnswerProvider.cs)).

It is possible for developers to provide hints of potential microservice boundaries. In the QnA app, we are using the [`PotentialPoemService`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/Poem/PotentialPoemServiceAttribute.cs) code attribute to mark the four answer provider classes as potential microservice entry points.

```csharp
[PotentialPoemService] // marks this class as a potential service boundary
public class WeatherAnswerProvider : IAnswerProvider
{
    public async Task<PotentialAnswer?> AskAsync(string question)
    {
        // Return an answer if this is a weather related question
        // that we can answer
    }
```

## From monolith to microservices

The Poem platform uses code analysis, developer hints, as well as online monitoring of user traffic and usage patterns to reconfigure the architecture of the application. Poem can split and create new microservices, and merge and delete previously created microservices.

In the QnA application, both the Weather and News answers are considered critical, and as such, the [`Answer Aggregator`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerAggregator.cs) can wait up to `300 ms` for them to respond. On the other hand, the less vital Lottery and Restaurant Finder answers can block for no more than `200 ms` or till the completion of Weather and News.

```csharp
public async Task<IList<PotentialAnswer>> AskAsync(string question)
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

    return ...;
}
```

It is therefore foreseeable that Poem could keep the [`Answer Aggregator`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerAggregator.cs) and [`Weather`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/WeatherAnswerProvider.cs) and [`News`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/NewsAnswerProvider.cs) answers within a single microservice and create 2 other microservices for [`Restaurant Finder`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/RestaurantFinderAnswerProvider.cs) and [`Lottery`](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/AnswerProviders/LotteryAnswerProvider.cs) answers. Although this might be a good initial configuration, the Poem platform is always on the lookout for usage pattern changes and can adapt the application architecture to meet those needs.

## Summary

In summary, the Poem platform removes the burden of designing cloud applications using microservices. Poem either eliminates or significantly reduces the need to:

- define service boundaries upfront,
- scale those services,
- expose internal service endpoints,
- securely and reliably communicate between services, and
- handle endpoint schema and message versioning

*See the [code](https://github.com/cloudtoid/poem/tree/master/showcase/QnA/) for this sample on GitHub.*

## Author

[Pedram Rezaei](https://www.linkedin.com/in/pedramrezaei/): Pedram is a software architect at Microsoft with years of experience building highly scalable and reliable cloud platforms and applications.
