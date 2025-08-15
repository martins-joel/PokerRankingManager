# PokerRankingManager
A manager for our semestral tournament poker ranking.

A tournament is composed of multiple steps, each week is a new tournament step.
A tournament has a name, a start date, and an end date, as well as the number of steps/weeks.

Each tournament has a tournament_configuration, which defines the the scoring system.
It also define the cut (how many scores will be cut from the ranking, the worse N scores are not used on the score total).

A tournament step has a date, and a list of players with their positions which is another table called tournament_step_result.


## Using Docker
### Generate an image using the Dockerfile
To be able to generate the image using the Dockerfile, you need to have Docker installed on your machine.
Then, from the src folder, you can build the image using the following command:
```bash
docker build -t pokermanagerapi:v0.0.4 -f ./PokerManager.Api/Dockerfile .
```
Important: It has to be run from the src folder, otherwise the context will not be correct.

### Run the image using Docker Compose
If you want to run the image using docker-compose, from the docker-compose folder, you can use the following command:
```bash
docker-compose up -d
```
Important: It has to be run from the docker-compose folder, otherwise the context will not be correct.