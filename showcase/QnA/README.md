# Poem platform explained using a sample

[*fork the repo*](showcase/QnA)

The sample here is a Question and Answer (QnA) application, which provides zero or more potential answers to a question. Each answer has a *score*, belongs to a *domain* such as Weather, News, Restaurant Finder, or Lottery, and can be provided by a separate microservice. However, it is the Poem platform and not the developer that decides which, if any, of these answer providers should become a separate microservice.

## Application structure

The QnA application has a single REST endpoint [`Question Controller`](Controllers/QuestionController.cs) to receive new questions. The rest of the app looks like one monolith application that is compiled and executed within the same process boundary.

Once the REST endpoint receives a question, it forwards it to the [`Answer Aggregator`](AnswerAggregator.cs), which in turn, dispatches the question in *parallel* to all these answer providers: [`Weather`](AnswerProviders/WeatherAnswerProvider.cs), [`News`](AnswerProviders/NewsAnswerProvider.cs), [`Restaurant Finder`](AnswerProviders/RestaurantFinderAnswerProvider.cs), and [`Lottery`](AnswerProviders/LotteryAnswerProvider.cs).

In this application, the developer has provided hints to the Poem platform as to what classes can potentially become entry points to new microservices. In this sample, we are using the [`PotentialPoemService`](Poem/PotentialPoemServiceAttribute.cs) code attribute to mark the four answer provider classes as the entry points to potential new microservices.

## From monolith to microservices

The Poem platform uses code analysis, developer hints, as well as online monitoring of user traffic and usage patterns, to reconfigure the architecture of the application. Poem can split and create new microservices and merge and delete previously created microservices.

As the usage patterns evolve, Poem's intelligent runtime will reconfigure the application to better meet the needs of these new patterns.

## Summary

In summary, the Poem platform removes the burden of designing cloud applications using microservices. Poem either eliminates or significantly reduces the need to:

- define service boundaries upfront,
- scale those services, expose internal service endpoints,
- securely and reliably communicate between services, and
- handle endpoint schema and message versioning

[*fork the repo*](showcase/QnA)