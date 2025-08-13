# PokerRankingManager
A manager for our semestral tournament poker ranking.

A tournament is composed of multiple steps, each week is a new tournament step.
A tournament has a name, a start date, and an end date, as well as the number of steps/weeks.

Each tournament has a tournament_configuration, which defines the the scoring system.
It also define the cut (how many scores will be cut from the ranking, the worse N scores are not used on the score total).

A tournament step has a date, and a list of players with their positions which is another table called tournament_step_result.