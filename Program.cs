public class Program
{
    public static void Main()
    {
        (int X, int Y) startPos = (1, 5);
        (int X, int Y) endPos = (10, 10);
        var obstacles = new Dictionary<string, List<int>>()
        {
            ["ObstacleOne"] = [4, 10, 3, 4],
            ["ObstacleTwo"] = [2, 15, 8, 9]
        };

        MakeGrid makeGrid = new(20, 20);
        Grid2D grid = makeGrid.SetObstacles(obstacles);

        EdgeNav edgeNav = new(startPos, endPos, grid);
        List<(float X, float Y)> path = edgeNav.ConnectNodes();

        DrawPlot plot = new();
        plot.DrawLine(path);
        plot.DrawGrid(grid);
    }
}