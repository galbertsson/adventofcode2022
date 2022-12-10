class Day10
{
    private static Dictionary<string, int> executionTimes = new Dictionary<string, int>()
    {
        {"noop", 0},
        {"addx", 1}
    };

    public void Start()
    {
        var inscructions = System.IO.File.ReadAllLines("./days/day10input.txt");
        var ops = this.ParseInput(inscructions);

        var signalStrengths = this.ExecuteOps(ops);
        var sumOfSignals = signalStrengths.Sum();
        Console.WriteLine(sumOfSignals);
    }

    private char getDisplayCharacter(int spriteMiddle, int row)
    {
        if (spriteMiddle - 1 <= row && row <= spriteMiddle + 1)
        {
            return '#';
        }

        return ' ';
    }

    private List<int> ExecuteOps((string op, string[] args)[] ops)
    {
        var display = new char[6, 40];
        var signalStrengths = new List<int>();

        var xRegistry = 1;

        (string op, string[] args)? curentOp = ops[0];
        var curentOpIndex = 0;

        var currentOpStartCycle = 1;

        var currentCycle = 1;
        while (curentOp != null)
        {
            var opData = curentOp.GetValueOrDefault();
            var instructionType = opData.op;

            var displayColumn = (currentCycle - 1) % 40;
            var displayRow = (currentCycle - 1) / 40;

            display[displayRow, displayColumn] = this.getDisplayCharacter(xRegistry, displayColumn);

            if (currentOpStartCycle + executionTimes[instructionType] == currentCycle)
            {
                curentOpIndex++;

                if (instructionType == "addx")
                {
                    xRegistry += Int32.Parse(opData.args[0]);
                }

                if (curentOpIndex == ops.Length)
                {
                    curentOp = null;
                }
                else
                {
                    curentOp = ops[curentOpIndex];
                    currentOpStartCycle = currentCycle + 1;
                }
            }

            if (currentCycle == 20 || (currentCycle - 20) % 40 == 0)
            {
                signalStrengths.Add(currentCycle * xRegistry);
            }

            currentCycle++;
        }

        this.RenderDisplay(display);

        return signalStrengths;
    }

    private void RenderDisplay(char[,] display)
    {
        for (var x = 0; x < display.GetLength(0); x++)
        {
            for (int y = 0; y < display.GetLength(1); y++)
            {
                Console.Write(display[x, y]);
            }
            Console.WriteLine("");
        }
    }

    private (string op, string[] args)[] ParseInput(string[] inscructions)
    {
        var inputs = new List<(string op, string[] args)>();

        foreach (var instruction in inscructions)
        {
            var parts = instruction.Split(" ");
            inputs.Add((parts[0], parts.Skip(1).ToArray()));
        }

        return inputs.ToArray();
    }
}