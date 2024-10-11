# Quotable by BOGsi

_adjective._ 
	suitable for or worth quoting.


## What is Quotable?

A massively, over-engineered, best-practice-filled-solution using dotnet. It's a CRUD interface for quotes and their respectable origins, nothing more, nothing less. 

It's a personal experiment where I take a deep dive in a lot of stuff I haven't gotten the chance to use a lot in my professional career. I'll be taking a deeper look at Docker, Valkey, Serilog, Seq, SonarQube, Keycloak, Test Containers and integration testing in general, AutoMapper, FluentValidation, FluentAssertions, and a couple of design patterns like Decorator, Result, Flyweight, Builder, etc...

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


## Roadmap

### V1.0

- [x] Setup project base and add Docker support.
- [x] Add endpoint discovery through reflection.
- [x] Setup Entity Framework and create DbContext. 
- [x] CRUD functionality for base Quote model. 
- [x] Implement Result monad pattern. 
- [x] Integration tests with Test Containers. 
- [x] Add logging (Serilog) and SEQ.
- [x] Add static code analyzers and setup SonarQube. 
- [ ] Use in-memory Cashing (Valkey)
- [ ] Clean-Up for release V1.0. 

### V1.1

- [ ] Head and Options endpoints 
- [ ] Update data model
- [ ] Add security (bearer)
- [ ] Clean-Up for release V1.1. 


## Known Bugs 

* When creating a quote, the Created and updated fields are a default date value. 


## Code Quality

* Using both StyleCop and SonarAnalyzer, with only a handful of rules disabled.
* Additional quality check done by SonarQube to maintain 0 issues.


## Considerations

* **Regarding a lack of Mediatr**. I originally reasoned not to use it because I would not be using the Pipeline behavior. At second thought it would have made integration testing even easier so I might add it along the line. 
* **Regarding unit tests for AutoMapper profiles**. I found quite a bit of "oh I didn't know that" because of them. Since mapping happens a lot I found it useful to unit test these mapping profiles. 
* **Regarding micro resolvers**. Inline would definitely suffice in most cases, but it's possible in other. Since I prefer a uniform look, I decided to put all resolvers in their own file, however small. 
* **Regarding cursor pagination instead of skip-take pagination**. Just a try-out after seeing a YouTube video about it. It did bring quite a bit of unforeseen issues with it, and opened up the id property to the user, but I like that it can enable infinite scrolling so I decided to stick with it. 