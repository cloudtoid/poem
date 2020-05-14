# Poem platform explained using a sample

[*fork the repo*]()

The sample here is a Question and Answer (QnA) application, that given a question, can respond with zero or more answers. Each answer has a *score*, belongs to a *domain* such as Weather, and can be provided by a separate microservice. However, it is the Poem platform and not the developer that decides which, if any, of these answer providers should be an independent microservice.

## Application structure

The QnA application receives new questions through the [`Question`](Controllers/QuestionController.cs) REST endpoint. The rest of the app looks like a monolith application that is compiled and executed within the same process boundary.

Once the REST endpoint receives a question, it forwards it to the [`Answer Aggregator`](AnswerAggregator.cs), which in turn, dispatches the question in *parallel* to all the answer providers ([`Weather`](AnswerProviders/WeatherAnswerProvider.cs), [`News`](AnswerProviders/NewsAnswerProvider.cs), [`Restaurant Finder`](AnswerProviders/RestaurantFinderAnswerProvider.cs), and [`Lottery`](AnswerProviders/LotteryAnswerProvider.cs)).

It is possible for developers to provide hints of potential microservice boundaries. In the QnA app, we are using the [`PotentialPoemService`](Poem/PotentialPoemServiceAttribute.cs) code attribute to mark the four answer provider classes as potential microservice entry points.

## From monolith to microservices

The Poem platform uses code analysis, developer hints, as well as online monitoring of user traffic and usage patterns to reconfigure the architecture of the application. Poem can split and create new microservices, and merge and delete previously created microservices.

In the QnA application, both the Weather and News answers are considered critical, and as such, the [`Answer Aggregator`](AnswerAggregator.cs) can wait up to `300 ms` for them to respond. On the other hand, the less vital Lottery and Restaurant Finder answers can block for no more than `200 ms` or till the completion of Weather and News.

It is therefore foreseeable that Poem would keep the aggregator and weather and news answers within a single microservice and create 2 microservices for restaurant finder and lottery answers.

It is worth noting that theh Poem platform is on the lookout for usage pattern changes and can adapt the application architecture to meet those needs.

## Summary

In summary, the Poem platform removes the burden of designing cloud applications using microservices. Poem either eliminates or significantly reduces the need to:

- define service boundaries upfront,
- scale those services,
- expose internal service endpoints,
- securely and reliably communicate between services, and
- handle endpoint schema and message versioning

[*fork the repo*]()