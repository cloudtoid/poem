# Poem platform explained using a sample

[*fork the repo*]()

The sample here is a Question and Answer (QnA) application, that given a question, can respond with zero or more answers. Each answer has a *score*, belongs to a *domain* such as Weather, and can be provided by a separate microservice. However, it is the Poem platform and not the developer that decides which, if any, of these answer providers should become an independent microservice.

## Application structure

The QnA application has a single REST endpoint [`Question Controller`](Controllers/QuestionController.cs) to receive new questions. The rest of the app looks like one monolith application that is compiled and executed within the same process boundary.

Once the REST endpoint receives a question, it forwards it to the [`Answer Aggregator`](AnswerAggregator.cs), which in turn, dispatches the question in *parallel* to all these answer providers: [`Weather`](AnswerProviders/WeatherAnswerProvider.cs), [`News`](AnswerProviders/NewsAnswerProvider.cs), [`Restaurant Finder`](AnswerProviders/RestaurantFinderAnswerProvider.cs), and [`Lottery`](AnswerProviders/LotteryAnswerProvider.cs).

In this application, the developer has provided hints to the Poem platform as to what classes can potentially become entry points to new microservices. In this sample, we are using the [`PotentialPoemService`](Poem/PotentialPoemServiceAttribute.cs) code attribute to mark the four answer provider classes as the entry points to potential new microservices.

## From monolith to microservices

The Poem platform uses code analysis, developer hints, as well as online monitoring of user traffic and usage patterns, to reconfigure the architecture of the application. Poem can split and create new microservices and merge and delete previously created microservices.

In the QnA application, both the Weather and News answers are considered critical, and as such, the [`Answer Aggregator`](AnswerAggregator.cs) can wait up to `300 ms` for them to respond. On the other hand, the less vital Lottery and Restaurant Finder answers can block for no more than `200 ms` or till the completion of Weather and News.

It is worth noting that Poem's intelligent runtime is on the lookout for usage pattern changes and can adapt the application architecture to meet those needs.

## Summary

In summary, the Poem platform removes the burden of designing cloud applications using microservices. Poem either eliminates or significantly reduces the need to:

- define service boundaries upfront,
- scale those services, expose internal service endpoints,
- securely and reliably communicate between services, and
- handle endpoint schema and message versioning

[*fork the repo*]()