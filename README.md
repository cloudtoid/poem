# NO as a Service (NOaaS)

**NOaaS** is a new cloud-native architecture with the help of AI techniques that removes the burden of designing cloud applications with microservices and containers. Simply put, application developers create a monolith backend in their chosen programming language, and a NOaaS implementation assumes all service decomposition, placement, deployment, and scaling responsibilities.

A NOaaS platform implementation frees engineering teams from mundane tasks such as:

- Definition of service boundaries
- Scaling of those services
- Exposing internal service endpoints
- Handling of secure and reliable communication between services with circuit breaking, load balancing, service discovery, and automated message schema versioning

A NOaaS platform continually monitors the usage of the monolith application to discover its usage patterns. Once these patterns are understood, and hot code paths, component dependencies, and resource usage are established, the system reconfigures the original application and creates a set of microservices. The monitoring never stops, and as the usage patterns evolve, the platform reshapes the physical architecture of the application.

NOaaS can also be described as an automated approach to converting a monolith into a set of microservices allowing engineering teams to focus on the logic of their application. Think of it as employing a software architect to draw up a microservices architecture and a DevOps team to monitor and scale the application.

NOaaS implementations typically employ sophisticated techniques to define service boundaries. However, some implementations may rely on developers to provide hints that can help with the automatic definition of the microservices architecture of the application.

NOaaS platforms can optimize for low latency by colocating chatty components, optimizing for hot code paths, and reducing the overhead of message serialization and deserialization. Such implementation can also increase the overall reliability of the application by decoupling the less reliable components into separate services. With sufficient monitoring of the application, these platforms can auto-scale the services when needed.

Such platforms abstract the underlying communication protocol and can choose the most appropriate technology and protocol for the task. Whether it is pure REST, gRPC, or a message queue technology, it is decided on the platform. Upgrades to the underlying communication stack are always transparent to the application developers.

These platforms typically rely on code generation and the recompilation of the monolith application to create a set of microservices. They integrate into the runtimes to fully understand the characteristics of an application and deploy deep learning models to decide on the next iteration of the microservices architecture of that application.

## What is in the name

NOaaS originates from "*No more microservices*" and is pronounced `no-aa-s`.