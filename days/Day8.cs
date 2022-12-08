public class Day8
{
    public void Start()
    {
        var quadMap = System.IO.File.ReadAllText("./days/day8input.txt");
        var mapMatrix = this.getMapMatrix(quadMap); // Row, column

        var trees = this.GetAllSeenTrees(mapMatrix);

        Console.WriteLine(trees.Length);
        Console.WriteLine(trees.Select((data) => data.viewingUp * data.viewingDown * data.viewingRight * data.viewingleft).Max());
    }

    private (bool canBeSeen, int viewingUp, int viewingDown, int viewingleft, int viewingRight) CanBeeSeenFromEdge(int row, int column, int rowDimension, int columnDimension, int[,] mapMatrix)
    {
        if (row == 0 || column == 0 || row == rowDimension - 1 || column == columnDimension - 1)
        {
            return (true, 0, 0, 0, 0);
        }

        var maxHeightUp = -1;
        var maxHeightDown = -1;
        var maxHeightLeft = -1;
        var maxHeightRight = -1;

        var viewingUp = 0;
        var viewingDown = 0;
        var viewingLeft = 0;
        var viewingRight = 0;

        // Up
        for (var currRow = row - 1; currRow >= 0; currRow--)
        {
            if ((mapMatrix[currRow, column] >= mapMatrix[row, column] || currRow == 0) && viewingUp == 0)
            {
                viewingUp = Math.Abs(currRow - row);
            }

            maxHeightUp = Math.Max(maxHeightUp, mapMatrix[currRow, column]);
        }

        // Down
        for (var currRow = row + 1; currRow < rowDimension; currRow++)
        {
            if ((mapMatrix[currRow, column] >= mapMatrix[row, column] || currRow == rowDimension - 1) && viewingDown == 0)
            {
                viewingDown = Math.Abs(currRow - row);
            }

            maxHeightDown = Math.Max(maxHeightDown, mapMatrix[currRow, column]);
        }

        // Left
        for (var currColumn = column - 1; currColumn >= 0; currColumn--)
        {
            if ((mapMatrix[row, currColumn] >= mapMatrix[row, column] || currColumn == 0) && viewingLeft == 0)
            {
                viewingLeft = Math.Abs(currColumn - column);
            }

            maxHeightLeft = Math.Max(maxHeightLeft, mapMatrix[row, currColumn]);
        }

        // Right
        for (var currColumn = column + 1; currColumn < rowDimension; currColumn++)
        {
            if ((mapMatrix[row, currColumn] >= mapMatrix[row, column] || currColumn == rowDimension - 1) && viewingRight == 0)
            {
                viewingRight = Math.Abs(currColumn - column);
            }


            maxHeightRight = Math.Max(maxHeightRight, mapMatrix[row, currColumn]);
        }

        var canBeSeenFromUp = maxHeightUp < mapMatrix[row, column];
        var canBeSeenFromDown = maxHeightDown < mapMatrix[row, column];
        var canBeSeenFromLeft = maxHeightLeft < mapMatrix[row, column];
        var canBeSeenFromRight = maxHeightRight < mapMatrix[row, column];

        return (canBeSeen: canBeSeenFromUp || canBeSeenFromDown || canBeSeenFromLeft || canBeSeenFromRight, viewingUp, viewingDown, viewingLeft, viewingRight);
    }

    private (bool canBeSeen, int viewingUp, int viewingDown, int viewingleft, int viewingRight)[] GetAllSeenTrees(int[,] mapMatrix)
    {
        var allSeenTrees = new List<(bool canBeSeen, int maxHeightUp, int maxHeightDown, int maxHeightLeft, int maxHeightRight)>();

        var rowDimension = mapMatrix.GetLength(0);
        var columnDimension = mapMatrix.GetLength(1);

        for (var row = 0; row < rowDimension; row++)
        {
            for (var column = 0; column < columnDimension; column++)
            {
                var data = this.CanBeeSeenFromEdge(row, column, rowDimension, columnDimension, mapMatrix);
                if (true)
                {
                    allSeenTrees.Add(data);
                }
            }
        }

        return allSeenTrees.ToArray();
    }

    private int[,] getMapMatrix(string quadMap)
    {
        var rows = quadMap.Split(Environment.NewLine);
        var numberOfColumns = rows[0].Length;
        var mapMatrix = new int[rows.Length, numberOfColumns];

        for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            var currentRow = rows[rowIndex];
            for (int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
            {
                mapMatrix[rowIndex, columnIndex] = Int32.Parse(currentRow[columnIndex].ToString());
            }
        }

        return mapMatrix;
    }
}