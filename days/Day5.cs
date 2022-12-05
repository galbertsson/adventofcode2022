class Day5 {
    public void Start()
    {
        var input = System.IO.File.ReadAllText("./days/day5input.txt").Split(Environment.NewLine+Environment.NewLine);

        var shipModel = getShipModel(input[0]);
        var instructions = getInstructions(input[1]);
    }

    private LinkedList<(string from, string to, int createsToMove)> getInstructions(string input) {
        var instructions = new LinkedList<(string from, string to, int createsToMove)>();
        return instructions;
    }

    private Dictionary<char, char[]> getShipModel(string input) {
        var ship = new Dictionary<char, char[]>();

        var lines = input.Split(Environment.NewLine);
        var lastLineIndex = lines.Length-1;
        var totalRows = lines[lastLineIndex].Length;

        for (var row = 0;row<totalRows;row++) {
            var columnNumber = lines[lastLineIndex][row];
            if (!columnNumber.Equals(' ')) {
                var crateStack = new LinkedList<char>();
                for (var lineNumber = lastLineIndex - 1; lineNumber>=0;lineNumber--) {
                    var createChar = lines[lineNumber][row];

                    if (!createChar.Equals(' ')) {
                        crateStack.Append(createChar);
                        Console.WriteLine(createChar);
                    }
                }
                
                ship.Add(columnNumber, crateStack.ToArray());
            }
        }

        return ship;
    }
}