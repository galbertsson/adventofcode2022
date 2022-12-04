class Day4
{
    public void Start()
    {
        string[] elfPairs = System.IO.File.ReadAllLines("./days/day4input.txt");
        var splittedElfPairs = elfPairs.Select((elfPair) => (elfPair.Split(",")));

        this.Part1(splittedElfPairs);
        this.Part2(splittedElfPairs);
    }

    private HashSet<int> getSectionSet(string elf)
    {
        var startAndEnd = elf.Split("-");
        var start = Int32.Parse(startAndEnd[0]);
        var end = Int32.Parse(startAndEnd[1]);

        return new HashSet<int>(Enumerable.Range(start, end - start + 1));
    }

    private void Part1(IEnumerable<string[]> elfPairs)
    {
        var overlappingPairs = 0;

        foreach (var elfs in elfPairs)
        {
            var firstElfSet = getSectionSet(elfs[0]);
            var secondElfSet = getSectionSet(elfs[1]);

            var isSubSet = firstElfSet.IsSubsetOf(secondElfSet);
            var isSuperSet = firstElfSet.IsSupersetOf(secondElfSet);

            if (isSubSet || isSuperSet)
            {
                overlappingPairs++;
            }
        }

        Console.WriteLine(overlappingPairs);
    }

    private void Part2(IEnumerable<string[]> elfPairs)
    {
        var overlappingPairs = 0;

        foreach (var elfs in elfPairs)
        {
            var firstElfSet = getSectionSet(elfs[0]);
            var secondElfSet = getSectionSet(elfs[1]);
            var hasOverlap = firstElfSet.Overlaps(secondElfSet);

            if (hasOverlap)
            {
                overlappingPairs++;
            }
        }

        Console.WriteLine(overlappingPairs);
    }
}