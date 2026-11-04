public class Grid2D(int height, int width)
{
    public int[,] cells = new int[height, width];

    public void SetValue(int x, int y, int value)
    {
        cells[y, x] = value;
    }

    public int GetValue(int x, int y)
    {
        return cells[y, x];
    }
}