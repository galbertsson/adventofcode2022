
public class Day9
{

    public void Start()
    {
        var lines = System.IO.File.ReadAllLines("./days/day9input.txt");

        var firstKnotTrail = this.CreatePath(lines);
        var knotTrailBefore = firstKnotTrail.ends;

        for (var knotNumber = 1; knotNumber < 10; knotNumber++)
        {
            // (int x, int y) headPosition = (0, 0);
            var headTrail = new List<(int, int)>();
            headTrail.Add((0, 0));
            var tailTrail = new List<(int, int)>();
            tailTrail.Add((0, 0));
            var tailTrailEnds = new List<(int, int)>();

            for (var move = 0; move < knotTrailBefore.Count(); move++)
            {
                var trail = this.CalculateTailPositions(headTrail.Last(), knotTrailBefore[move], tailTrail.Last());

                headTrail.Add(knotTrailBefore[move]);
                tailTrail.AddRange(trail);

                if (trail.Length > 0)
                {
                    tailTrailEnds.Add(trail[trail.Length - 1]);
                }

            }

            knotTrailBefore = tailTrailEnds;

            if (knotNumber == 9)
            {
                var allPlaces = new HashSet<string>(tailTrail.Select((position) => position.Item1 + ":" + position.Item2));
                Console.WriteLine(allPlaces.Count());
            }
        }
    }

    private (int x, int y)[] CalculateTailPositions((int x, int y) oldHeadPosition, (int x, int y) newHeadPosition, (int x, int y) currentTailPosition)
    {
        var trail = new List<(int x, int y)>();
        var isNegativeX = oldHeadPosition.x > newHeadPosition.x;
        var isNegativeY = oldHeadPosition.y > newHeadPosition.y;

        var newTailX = currentTailPosition.x;
        for (var xStep = 1; xStep <= Math.Abs(newHeadPosition.x - oldHeadPosition.x); xStep++)
        {
            // if Moved left or right diagonal to tail
            var isYSame = oldHeadPosition.y == currentTailPosition.y;
            var currentHeadX = oldHeadPosition.x + (isNegativeX ? -xStep : xStep);
            var xDistance = Math.Abs(currentTailPosition.x - currentHeadX);
            if ((!isYSame && xDistance <= 1) || (xDistance <= 1 && isYSame))
            {
                //   Do nothing
                continue;
            }

            newTailX += (isNegativeX ? -1 : 1);
            trail.Add((newTailX, newHeadPosition.y));
        }

        var newTailY = currentTailPosition.y;
        for (var yStep = 1; yStep <= Math.Abs(newHeadPosition.y - oldHeadPosition.y); yStep++)
        {
            // if Moved up or down diagonal to tail
            var isXSame = oldHeadPosition.x == currentTailPosition.x;
            var currentHeadY = oldHeadPosition.y + (isNegativeY ? -yStep : yStep);
            var yDistance = Math.Abs(currentTailPosition.y - currentHeadY);
            if ((!isXSame && yDistance <= 1) || (yDistance <= 1 && isXSame))
            {
                //   Do nothing
                continue;
            }

            newTailY += (isNegativeY ? -1 : 1);
            trail.Add((newHeadPosition.x, newTailY));
        }

        return trail.ToArray();
    }

    public (List<(int, int)> full, List<(int, int)> ends) CreatePath(string[] moves)
    {
        (int x, int y) headPosition = (0, 0);

        var headTrail = new List<(int, int)>();
        headTrail.Add((0, 0));
        var tailTrail = new List<(int, int)>();
        tailTrail.Add((0, 0));
        var tailTrailEnds = new List<(int, int)>();

        foreach (var move in moves)
        {
            var moveData = move.Split(" ");
            var direction = moveData[0];
            var steps = Int32.Parse(moveData[1]);

            var xSteps = 0;
            var ySteps = 0;

            if (direction == "U")
            {
                ySteps = steps;
            }
            if (direction == "D")
            {
                ySteps = -steps;
            }
            if (direction == "L")
            {
                xSteps = -steps;
            }
            if (direction == "R")
            {
                xSteps = steps;
            }

            headPosition = (headPosition.x + xSteps, headPosition.y + ySteps);
            Console.WriteLine(headPosition);

            var trail = this.CalculateTailPositions(headTrail.Last(), headPosition, tailTrail.Last());

            headTrail.Add(headPosition);
            tailTrail.AddRange(trail);

            if (trail.Length > 0)
            {
                tailTrailEnds.Add(trail[trail.Length - 1]);
            }
        }

        // var positions = new HashSet<string>(tailTrail.Select((position) => position.Item1 + ":" + position.Item2));

        return (tailTrail, tailTrailEnds);
    }
}