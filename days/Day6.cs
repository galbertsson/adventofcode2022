class Day6
{
    public void Start()
    {
        var dataStream = System.IO.File.ReadAllText("./days/day6input.txt");

        this.Parts(dataStream, 4);
        this.Parts(dataStream, 14);
    }

    public void Parts(string dataStream, int characters)
    {
        var windowStart = 0;
        for (var index = characters - 1; index < dataStream.Length; index++)
        {
            var slice = dataStream[windowStart..(index + 1)];
            var set = new HashSet<char>(slice);
            if (set.Count() == characters)
            {
                Console.WriteLine(index + 1);
                return;
            }

            windowStart++;
        }
    }
}