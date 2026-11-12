public class MakeGrid(int row, int col)
{
    public Grid2D grid = new(row, col);

    public void Generate()
    {
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                grid.SetValue(i, j, 0);
            }
        }
    }

    public Grid2D SetObstacles(Dictionary<string, List<int>> obstacles)
    {
        Generate();
        
        foreach(var kvp in obstacles)
        {
            if(kvp.Value is [var num1, var num2, var num3, var num4])
            {
                for(int y = num1; y < num2; y++)
                {
                    for(int x = num3; x < num4; x++)
                    {
                        grid.SetValue(x, y, 1);
                    }
                }
            }
        }

        return grid;
    }
}