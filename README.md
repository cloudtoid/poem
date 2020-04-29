# Poem Cloud Architecture

**Poem** is a proposal for a new cloud-native architecture that removes the burden of designing cloud applications with microservices. Simply put, application developers create a monolith backend in their chosen programming language, and a Poem implementation assumes all service decomposition, placement, deployment, and scaling responsibilities.

A Poem platform frees engineering teams from mundane tasks such as:

- Definition of service boundaries
- Scaling of those services
- Exposing internal service endpoints
- Handling of secure and reliable communication between services with circuit breaking, load balancing, service discovery, automated message schema versioning, and such features.

A Poem platform continually monitors the usage of the monolith application to discover its usage patterns. Once these patterns are understood, and hot code paths, component dependencies, and resource usages are established, the system reconfigures the original application and creates a set of microservices. The monitoring never stops, and as the usage patterns evolve, the platform reshapes the physical architecture of the application.

Poem can also be described as an automated approach to converting a monolith into a set of microservices allowing engineering teams to focus on the logic of their application. Think of it as employing a software architect to draw up a microservices architecture and a DevOps team to monitor and scale the application.

Poem platforms can optimize for low latency by colocating chatty components, optimizing for hot code paths, and reducing the overhead of message serialization and deserialization. Such implementations can also increase the overall reliability of the application by decoupling the less reliable components into separate services. With sufficient monitoring of the application, these platforms can also auto-scale the services when needed.

Poem platforms abstract the underlying communication protocol and can choose the most appropriate technology and protocol for the task. Whether it is pure REST, gRPC, or a message queue technology, these platforms can employ heuristics, machine learning, and deep learning techniques to decide on the best technology. Upgrades to the underlying communication stack are always transparent to the application developers.

We foresee these platforms relying on code generation and the recompilation of the monolith application to create a set of microservices. They integrate into the application runtimes to monitor and understand the characteristics of an application and deploy machine learning techniques to decide on the next iteration of the microservices architecture for the application.

Although Poem platforms employ sophisticated techniques to decide on service boundaries and the physical architecture of the application, they allow developers to provide hints to help this process. For example, code markers can indicate the entry point to a code component or mark potential service boundaries. They can also provide hints of the scale characteristics of these components and services. In other words, Poem abstracts away the underlying microservices architecture but gives full flexibility and customization to those that want them.

## Poem in action

Over the past 18 months, the Microsoft Bing engineering team has been working on an implementation of Poem to eventually replace the existing workflow that generates the Search Results page. This workflow is called *Bing Front-Page Result* or BFPR and runs mainly as a monolith to reduce latency. However, there are plenty of parallel workflows inside of BFPR that are not considered critical. If they complete within a set timeout, they can provide a potential small boost to the relevance, but they are not essential.

For instance, only a small fraction of Bing queries trigger the Lottery Answer. Therefore, it is best to ignore the result of this answer if it fails to complete in a set time.

In such a scenario, Poem can intelligently offload the lottery answer to a separate microservice and execute it in parallel to BFPR. However, a more popular component, such as the Weather Answer, should collocate with the main workflow.

## Serverless architecture

Poem platforms are [serverless](https://en.wikipedia.org/wiki/Serverless_computing) because they provide the resources needed to run an entire application. The main difference is that with today's compute serverless services such as [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview) and AWS Lambda, developers still need to define service/function boundaries and deal with lower-level communication constructs such HTTP requests and endpoint versioning.

## What is in the name

The word *Poem* is a play on "**Po**st-**M**icroservices **E**ra" that is indicative of the need to upscale cloud-native software development.

## Author

[Pedram Rezaei](https://www.linkedin.com/in/pedramrezaei/): Pedram is a software architect at Microsoft with years of experience building highly scalable and reliable cloud platforms and applications.
