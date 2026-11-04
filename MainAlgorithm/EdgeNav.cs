public class EdgeNav(
    (int X, int Y) startPos, 
    (int X, int Y) endPos,
    Grid2D grid
)
{
    public List<(float X, float Y)> Lerp()
    {
        var increments = new List<(float X, float Y)>();

        float t = 0.1f;

        for(float i = 0f; i <= 1; i += t)
        {
            float x = startPos.X + (endPos.X - startPos.X) * i;
            float y = startPos.Y + (endPos.Y - startPos.Y) * i;

            increments.Add(
                (x, y)
            );
        }

        increments.Add((endPos.X, endPos.Y));

        return increments;
    }

    public List<(int X, int Y)> FindObstacles()
    {
        List<(float X, float Y)> increments = Lerp();        

        List<(int X, int Y)> foundObstacles = [];

        foreach(var (X, Y) in increments)
        {
            int x = (int)Math.Floor(X);
            int y = (int)Math.Floor(Y);

            int value = grid.GetValue(x, y);

            if(value > 0)
                foundObstacles.Add(
                    (x, y)
                );
        }

        return foundObstacles;
    }

    public Dictionary<string, List<(int X, int Y)>> FindEdges()
    {
        List<(int X, int Y)> foundObstacles = FindObstacles();

        Dictionary<string, List<(int X, int Y)>> obstacles = [];

        foreach(var (X, Y) in foundObstacles)
        {
            int x = X;
            int y = Y;

            int value = grid.GetValue(x, y);

            while(value > 0)
            {
                obstacles["One"].Add((x, y));
                y += 1;
                value = grid.GetValue(x, y);
            }
        }

        return obstacles;
    }
}