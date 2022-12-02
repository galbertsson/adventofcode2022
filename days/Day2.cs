class Day2
{
    private static Dictionary<string, int> scores = new Dictionary<string, int> {
        {"X", 1},
        {"Y", 2},
        {"Z", 3}
    };

    private static Dictionary<string, string> normalized = new Dictionary<string, string> {
        {"A", "X"},
        {"B", "Y"},
        {"C", "Z"}
    };

    private static Dictionary<string, string> findWinningMoveMap = new Dictionary<string, string> {
        {"Z", "X"},
        {"X", "Y"},
        {"Y", "Z"}
    };

    private static Dictionary<string, string> findLosingMoveMap = new Dictionary<string, string> {
        {"X", "Z"},
        {"Y", "X"},
        {"Z", "Y"}
    };

    private int getResultBonus(string opponent, string mine)
    {
        var normalizedOpponent = normalized[opponent];
        if (normalizedOpponent == mine)
        {
            return 3;
        }

        if (findLosingMoveMap[mine] == normalizedOpponent)
        {
            return 6;
        }

        return 0;
    }

    private int getRoundScore(string round)
    {
        var hands = round.Split(" ");
        return getResultBonus(hands[0], hands[1]) + scores[hands[1]];
    }

    public void Start()
    {
        string lines = System.IO.File.ReadAllText("./days/day2input.txt");
        var rounds = lines.Split("\n");

        this.part1(rounds);
        this.part2(rounds);
    }

    private string[] getMoveSets(string[] rounds)
    {
        return rounds.Select((round) =>
        {
            var moves = round.Split(" ");
            var normalizedOpponent = normalized[moves[0]];
            var move = normalizedOpponent;

            if (moves[1] == "X")
            {
                move = findLosingMoveMap[normalizedOpponent];
            }
            else if (moves[1] == "Z")
            {
                move = findWinningMoveMap[normalizedOpponent];
            }

            return moves[0] + " " + move;
        }).ToArray();
    }

    public void part1(string[] rounds)
    {
        Console.WriteLine(rounds.Sum((round) => getRoundScore(round)));
    }

    public void part2(string[] rounds)
    {
        var moveSets = this.getMoveSets(rounds);
        Console.WriteLine(moveSets.Sum((round) => getRoundScore(round)));
    }
}