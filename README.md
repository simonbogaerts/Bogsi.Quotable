# Quotable by BOGsi

_adjective._ 
	suitable for or worth quoting.


## What is Quotable?

A massively, over-engineered, best-practice-filled-solution using dotnet. It's a CRUD interface for quotes and their respectable origins, nothing more, nothing less. 

It's an experiment where I take a deep dive in some best practices regarding dotnet and REST. Further delve into Docker and Docker Compose. Set-up an entire environment using development containers, and test my code base using test containers. 

Afterwards I'll be using Quotable when trying new frontend frameworks, cloud platforms, or other stuff that might need a teeny tiny application laying around to test. 


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

* Create
* Delete
* Update 
* Parameters GetAll 
* Integration Test with Testcontainers
* Cashing with Redis
* Logging with SEQ
* code quality with sonarcube


## Additional

* **Health checks**. https://www.youtube.com/watch?v=4abSfjdzqms
* I chose to unit test my mappers because without them I wouldn't know about the resolver (since you can only have a single FromMember for a member).
* I chose to use resolver instead of inline mapping because this makes the profile easier to read and profile doesn't need to know the specific of the resolve. 

* have base model for getall model that has less info (no dates, tags)
* endpoint handle returning status coded and calling the handler
