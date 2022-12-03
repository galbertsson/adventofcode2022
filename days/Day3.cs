class Day3
{
    public void Start()
    {
        string[] backpacks = System.IO.File.ReadAllLines("./days/day3input.txt");
        this.Part1(backpacks);
        this.Part2(backpacks);
    }

    private HashSet<char> getCommonItems(string firstInput, string secondInput)
    {
        HashSet<char> returnSet = new HashSet<char>(firstInput);
        HashSet<char> secondComparmentSet = new HashSet<char>(secondInput);
        returnSet.IntersectWith(secondComparmentSet);

        return returnSet;
    }

    private int calculatePriorities(char[] items)
    {
        var proritySum = 0;
        foreach (var item in items)
        {
            var asciiValue = (int)item;
            var asciiToSumOffset = (int)'a' - 1;

            if ((int)'A' <= asciiValue && asciiValue <= (int)'Z')
            {
                asciiToSumOffset = (int)'A' - 27;
            }
            proritySum += asciiValue - asciiToSumOffset;
        }

        return proritySum;
    }

    private void Part1(string[] backpacks)
    {
        var sumOfPriorities = 0;
        foreach (var backpack in backpacks)
        {

            var badlyPlacedItems = getCommonItems(backpack.Substring(0, backpack.Length / 2), backpack.Substring(backpack.Length / 2)).ToArray();
            sumOfPriorities += calculatePriorities(badlyPlacedItems);
        }

        Console.WriteLine(sumOfPriorities);
    }

    private void Part2(string[] backpacks)
    {
        var sumOfPriorities = 0;

        for (var i = 0; i < backpacks.Length; i += 3)
        {
            var firstCommonItems = getCommonItems(backpacks[i], backpacks[i + 1]);
            var secondCommonItems = getCommonItems(backpacks[i], backpacks[i + 2]);

            var commonItem = firstCommonItems.Intersect(secondCommonItems).ToArray();
            sumOfPriorities += calculatePriorities(commonItem);
        }

        Console.WriteLine(sumOfPriorities);
    }
}