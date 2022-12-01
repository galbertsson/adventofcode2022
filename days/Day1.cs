class Day1
{
    public void Start()
    {
        string lines = System.IO.File.ReadAllText("./days/day1input.txt");

        var inventories = lines.Split("\n\n").Select(inventory => inventory.Split("\n")).ToArray();

        this.Part1(inventories);
        this.Part2(inventories);
    }

    private int CalculateInventoryValue(string[] inventory)
    {
        var currentInventoryValue = 0;

        foreach (string line in inventory)
        {
            currentInventoryValue += Int32.Parse(line);
        }

        return currentInventoryValue;
    }

    private void Part1(string[][] inventories)
    {
        var inventoryValues = inventories.Select((inventory) => this.CalculateInventoryValue(inventory)).OrderBy((value) => -value).ToArray();
        Console.WriteLine(inventoryValues[0]);
    }

    private void Part2(string[][] inventories)
    {
        var inventoryValues = inventories.Select((inventory) => this.CalculateInventoryValue(inventory)).OrderBy((value) => -value).ToArray();
        var threeMostValueable = inventoryValues[0] + inventoryValues[1] + inventoryValues[2];

        Console.WriteLine(threeMostValueable);
    }
}