public class Program
{
    public static void Main()
    {
        (int X, int Y) startPos = (1, 1);
        (int X, int Y) endPos = (10, 10);

        MakeGrid makeGrid = new(20, 20);
        Grid2D grid = makeGrid.SetObstacles(1, 10, 3, 4);

        EdgeNav edgeNav = new(startPos, endPos, grid);
        Dictionary<string, List<(int X, int Y)>> obstacles = edgeNav.FindEdges();
        foreach(var (X, Y) in obstacles["One"])
        {
            Console.WriteLine($"{X}, {Y}");
        }

        DrawPlot plot = new();
        plot.DrawGrid(grid);
    }
}