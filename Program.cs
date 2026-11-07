public class Program
{
    public static void Main()
    {
        (int X, int Y) startPos = (1, 5);
        (int X, int Y) endPos = (10, 10);

        MakeGrid makeGrid = new(20, 20);
        Grid2D grid = makeGrid.SetObstacles(4, 10, 3, 4);

        EdgeNav edgeNav = new(startPos, endPos, grid);
        List<(float X, float Y)> path = edgeNav.ConnectNodes();

        DrawPlot plot = new();
        plot.DrawLine(path);
        plot.DrawGrid(grid);
    }
}