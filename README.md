# Poem Cloud Architecture

**Poem** is a proposal for a new cloud-native architecture that removes the burden of designing cloud applications with microservices. Simply put, application developers create a monolith backend in their chosen programming language, and a Poem implementation assumes all service decomposition, placement, deployment, and scaling responsibilities.

A Poem platform frees engineering teams from mundane tasks such as:

- Definition of service boundaries
- Scaling of those services
- Exposing internal service endpoints
- Handling of secure and reliable communication between services with circuit breaking, load balancing, service discovery, automated message schema versioning, and such like features

A Poem platform continually monitors the usage of the monolith application to discover its usage patterns. Once these patterns are understood, and hot code paths, component dependencies, and resource usages are established, the system reconfigures the original application and creates a set of microservices. The monitoring never stops, and as the usage patterns evolve, the platform reshapes the physical architecture of the application.

Poem can also be described as an automated approach to converting a monolith into a set of microservices allowing engineering teams to focus on the logic of their application. Think of it as employing a software architect to draw up a microservices architecture and a DevOps team to monitor and scale the application.

Poem platforms can optimize for low latency by colocating chatty components, optimizing for hot code paths, and reducing the overhead of message serialization and deserialization. Such implementations can also increase the overall reliability of the application by decoupling the less reliable components into separate services. With sufficient monitoring of the application, these platforms can also auto-scale the services when needed.

Poeams platforms abstract the underlying communication protocol and can choose the most appropriate technology and protocol for the task. Whether it is pure REST, gRPC, or a message queue technology, these platforms can employ heuristics, machine learning, and deep learning techniques to decide on the best technology. Upgrades to the underlying communication stack are always transparent to the application developers.

We foresee these platforms relying on code generation and the recompilation of the monolith application to create a set of microservices. They integrate into the application runtimes to monitor and understand the characteristics of an application and deploy machine learning techniques to decide on the next iteration of the microservices architecture of the application.

Although Poem platforms employ sophisticated techniques to decide on service boundaries and the physical architecture of the application, they can rely on developers to provide hints that can help this process. For example, code markers can indicate the entry point to a code component or mark potential service boundaries. They can also provide hints of the scale characteristics of these components and services. In other words, Poem abstracts away the underlying microservices architecture but should give full flexibility and customization to those that want them.

## What is in the name

The word *Poem* is a play on "*_P_ost-_M_icroservices _E_ra*" and indicates that underlying microservice concepts should not be exposed to the developers.
