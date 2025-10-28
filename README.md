# Quotable by BOGsi

_adjective._ 
	suitable for or worth quoting.


## What is Quotable?

A massively, over-engineered, try-as-I-go-along project using dotnet. It's a CRUD interface for quotes and their respectable origins, nothing more, nothing less. 

It's a personal experiment where I take a deep dive in a lot of stuff I haven't gotten the chance to use a lot in my professional career. I'll be taking a deeper look at Docker, Valkey, Serilog, Seq, Mediatr, MassTransit, RabbitMQ, SonarQube, Keycloak, Test Containers and integration testing in general, AutoMapper, FluentValidation, FluentAssertions, and a couple of design patterns like Saga, Decorator, Result Monad, Flyweight, Builder, Request-Response, etc...

Afterwards I intend to use Quotable when trying new frontend frameworks, cloud platforms, or other stuff that might need a teeny tiny backend application laying around. 


## Prerequisites

* Docker
* WSL2+


## How to run Quotable

Simply use the following command in the terminal (when in the main project folder):

```
docker compose up -d
```


To stop Quotable use the following docker command.

```
docker compose down
```

## Port Mapping 

| Port | Application          | 
|------|----------------------|
|  5341 | SEQ log ingestion   |
|  5432 | PostgreSQL database |
|  5672 | RabbitMQ            |
|  6379 | Valkey caching      |
|  8080 | Quotable web HTTP   |
|  8081 | Quotable web HTTPS  |
|  8082 | SEQ GUI             |
| 15672 | RabbitMQ GUI        |


## Known Bugs 

* Unit Tests not working anymore. I'm in the midst of migrating from using self made requests to mediatr and mass transit, might fix this one day.


## Code Quality

* Using both StyleCop and SonarAnalyzer, with only a handful of rules disabled.
* Additional quality check done by SonarQube to maintain 0 issues.


## Considerations

* **Regarding Mediatr AND MassTransit AND Saga pattern**. To address the elephant in the room, yes, this was not needed and completely over-engineered at all. I just wanted to try it out, but this is definitely not the use case where this tech was appropriate.
* **Regarding unit tests for AutoMapper profiles**. I found quite a bit of "oh I didn't know that" because of them. Since mapping happens a lot I found it useful to unit test these mapping profiles. 
* **Regarding micro resolvers**. Inline would definitely suffice in most cases, but it's possible in other. Since I prefer a uniform look, I decided to put all resolvers in their own file, however small. 
* **Regarding cursor pagination instead of skip-take pagination**. Just a try-out after seeing a YouTube video about it. It did bring quite a bit of unforeseen issues with it, and opened up the id property to the user, but I like that it can enable infinite scrolling so I decided to stick with it. 
