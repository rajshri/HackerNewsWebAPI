-----HackerNewsClient API -------

This is a sample .NET Core RESTful API application
The Web API returns best stories details sorted by their score in descending order from "Hacker News API" documented at https://github.com/HackerNews/API.
The IDs for the stories can be retrieved from this URI: https://hacker-news.firebaseio.com/v0/beststories.json
The details for an individual story ID can be retrieved from this URI:
https://hacker-news.firebaseio.com/v0/item/21233041.json (in this case for the story with ID 21233041)
The results are cached locally.

Framework: .NET Core 5.0 
Visual Studio : 2019
Entry point: `./HackerNewsAPI.sln`
Swagger URL: <base address>/swagger/index.html
Endpoint URL: <base address>/BestStories/200

------------Assumptions------------
1. API should be able to efficiently service large numbers of requests without risking overloading of the Hacker News API
    - "InMemoryCache" cache is added to achieve this.
2. No authorization required        
3. Application configurations to be done  in code
    - Hacker News API URLs in settings  
    - Cache expiration configured in settings
4.  Web API read all 200 stories returned by the endpoint and then fetch their details and sort it.

--------Testing the API--------
Open `HackerNewsAPI.sln` in Visual Studio and then run (F5) "HackerNewsClient.Api" project.

Integration test project is added

---------Next features--------
The main improvement points are:
Implementation of Global exception handler and logging.



