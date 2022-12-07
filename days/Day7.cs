class Day7
{
    public void Start()
    {
        var terminalOutput = System.IO.File.ReadAllText("./days/day7input.txt");
        var all = terminalOutput.Split("$");
        var fileTree = this.interpeterCommands(all);

        var avaliableSpace = 70000000 - fileTree.getTotalSize();
        var updateSize = 30000000;
        var neededSpace = -(avaliableSpace - updateSize);
        var dirSizes = getDirectoriesWithSize(fileTree.getChildren());

        var bestMatch = -1;
        foreach (var dirSize in dirSizes)
        {
            if (dirSize >= neededSpace)
            {
                bestMatch = bestMatch == -1 ? dirSize : Math.Min(bestMatch, dirSize);
            }
        }

        Console.WriteLine(bestMatch);
    }

    private int[] getDirectoriesWithSize(Node[] baseDirectory)
    {
        var dirsToSearch = new List<Node>(baseDirectory);
        var outData = new List<int>();

        while (dirsToSearch.Count() > 0)
        {
            var node = dirsToSearch[0];
            var children = node.getChildren();
            if (children.Length > 0)
            {
                dirsToSearch.AddRange(children);
                var nodeSize = node.getTotalSize();
                outData.Add(nodeSize);
            }

            dirsToSearch.RemoveAt(0);
        }

        return outData.ToArray();
    }

    private int countDirectoriesWithMaxSize(Node[] baseDirectory)
    {
        var dirsToSearch = new List<Node>(baseDirectory);
        var totalSizes = 0;

        while (dirsToSearch.Count() > 0)
        {
            var node = dirsToSearch[0];
            var children = node.getChildren();
            if (children.Length > 0)
            {
                dirsToSearch.AddRange(children);

                var nodeSize = node.getTotalSize();
                if (nodeSize <= 100000)
                {
                    totalSizes += nodeSize;
                }
            }

            dirsToSearch.RemoveAt(0);
        }

        return totalSizes;
    }

    private Node interpeterCommand(Node fileSystemRoot, Node currentDirectory, string command, string? argument, string[] commandOutput)
    {
        if (command == "cd")
        {
            if (argument == "/")
            {
                return fileSystemRoot;
            }
            if (argument == "..")
            {
                var parent = currentDirectory.GetParent();

                if (parent == null)
                {
                    Console.WriteLine("Node not found, exiting");
                    return currentDirectory;
                }
                return parent;
            }

            var newCurentDir = currentDirectory.GetChild(argument);
            if (newCurentDir == null)
            {
                Console.WriteLine("Node not found, exiting");
                return currentDirectory;
            }

            return newCurentDir;
        }
        else if (command == "ls")
        {
            foreach (var outputLine in commandOutput)
            {
                var lineData = outputLine.Split(" ");
                var name = lineData[1];

                if (currentDirectory.GetChild(name) != null)
                {
                    continue;
                }

                if (lineData[0] == "dir")
                {
                    currentDirectory.addChild(new Node(name, 0, currentDirectory));
                }
                else
                {
                    currentDirectory.addChild(new Node(name, Int32.Parse(lineData[0]), currentDirectory));
                }
            }
        }

        return currentDirectory;
    }

    private Node interpeterCommands(string[] terminalOutput)
    {
        Node fileSystemRoot = new Node("/", 0, null);
        Node currentDirectory = fileSystemRoot;

        foreach (var outputLine in terminalOutput.Skip(1))
        {
            var splitted = outputLine.Trim().Split(Environment.NewLine);
            var commandAndArguments = splitted[0].Split(" ");
            var command = commandAndArguments[0];
            var argument = commandAndArguments.Length > 1 ? commandAndArguments[1] : null;

            currentDirectory = this.interpeterCommand(fileSystemRoot, currentDirectory, command, argument, splitted.Skip(1).ToArray());
        }

        return fileSystemRoot;
    }
}

public class Node
{
    string name;
    int size;
    Node? parent;
    List<Node> children = new List<Node>(); // Maybe make this a set instead? Not sure

    public Node(string name, int size, Node? parent)
    {
        this.name = name;
        this.size = size;
        this.parent = parent;
    }


    public void addChild(Node child)
    {
        this.children.Add(child);
    }

    public Node? GetChild(string name)
    {
        return children.Find((child) => child.name == name);
    }

    public Node? GetParent()
    {
        return this.parent;
    }

    public int getTotalSize()
    {
        return size + children.Sum((child) => child.getTotalSize());
    }

    public Node[] getChildren()
    {
        return this.children.ToArray();
    }
}