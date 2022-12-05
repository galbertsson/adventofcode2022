class Day5
{
    public void Start()
    {
        var input = System.IO.File.ReadAllText("./days/day5input.txt").Split(Environment.NewLine + Environment.NewLine);

        var shipModel = getShipModel(input[0]);
        var instructions = getInstructions(input[1]);

        foreach (var instruction in instructions)
        {
            var createsToMove = shipModel[instruction.from].TakeLast(instruction.createsToMove);
            shipModel[instruction.to].AddRange(createsToMove); // Reverse for part1
            shipModel[instruction.from].RemoveRange(shipModel[instruction.from].Count() - instruction.createsToMove, instruction.createsToMove);
        }


        var topLayer = "";
        foreach (var stack in shipModel)
        {
            topLayer += stack.Value.Last();
        }
        Console.WriteLine(topLayer);
    }

    private List<(int createsToMove, char from, char to)> getInstructions(string input)
    {
        var instructions = new List<(int createsToMove, char from, char to)>();

        var commands = input.Split(Environment.NewLine);
        foreach (var command in commands)
        {
            var cratesAndRest = command.Substring("move ".Length).Split(" from ");
            var createsToMove = Int32.Parse(cratesAndRest[0]);

            var fromAndTo = cratesAndRest[1].Split(" to ");
            var from = fromAndTo[0][0];
            var to = fromAndTo[1][0];

            instructions.Add((createsToMove, from, to));
        }

        return instructions;
    }

    private Dictionary<char, List<char>> getShipModel(string input)
    {
        var ship = new Dictionary<char, List<char>>();
        var lines = input.Split(Environment.NewLine);

        var totalRows = lines.Length - 1;
        var totalColumns = lines[totalRows].Length;
        for (var column = 0; column < totalColumns; column++)
        {
            var columnNumber = lines[totalRows][column];
            if (columnNumber.Equals(' '))
            {
                continue;
            }

            var crateStack = new List<char>();
            for (var row = totalRows - 1; row >= 0; row--)
            {
                var createChar = lines[row][column];

                if (!createChar.Equals(' '))
                {
                    crateStack.Add(createChar);
                }
            }

            ship.Add(columnNumber, crateStack);
        }

        return ship;
    }
}