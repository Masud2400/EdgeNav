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

    public Grid2D SetObstacles( 
        int rowStart, 
        int rowLen,
        int colStart,
        int colEnd
    )
    {
        Generate();
        
        for(int y = rowStart; y < rowLen; y++)
        {
            for(int x = colStart; x < colEnd; x++)
            {
                grid.SetValue(x, y, 1);
            }
        }

        return grid;
    }
}