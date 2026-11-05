using ScottPlot;

public class DrawPlot
{
    public Plot visual = new();

    public void DrawLine(List<(float X, float Y)> path)
    {
        float[] xs = [.. path.Select(p => p.X)];
        float[] ys = [.. path.Select(p => p.Y)];

        visual.Add.Scatter(xs, ys);

        for (int i = 0; i < xs.Length; i++)
        {
            var text = visual.Add.Text(
                $"({xs[i]}, {ys[i]})",
                xs[i],
                ys[i]
            );

            text.LabelFontColor = Colors.White;
        }

        visual.FigureBackground.Color = Color.FromHex("#181818");
        visual.DataBackground.Color = Color.FromHex("#1f1f1f");
        visual.Axes.Color(Color.FromHex("#d7d7d7"));
        visual.Grid.MajorLineColor = Color.FromHex("#404040");
        
        visual.SavePng("Images/line.png", 1300, 800);
    }

    public void DrawGrid(Grid2D grid)
    {
        visual.FigureBackground.Color = Colors.Black;
        visual.DataBackground.Color = Colors.Black;

        int height = grid.cells.GetLength(0);
        int width = grid.cells.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var rect = visual.Add.Rectangle(
                    x, x + 1,
                    y, y + 1
                );

                rect.LineColor = Colors.White;
                rect.LineWidth = 1;
                rect.FillColor = Colors.Transparent;

                var text = visual.Add.Text(
                    $"({grid.GetValue(x, y)})",
                    x + 0.5,
                    y + 0.5
                );

                text.LabelFontColor = Colors.White;
                text.Alignment = Alignment.MiddleCenter;
                text.LabelFontSize = 16;
            }
        }

        visual.Axes.SetLimits(0, width, height, 0);
        visual.Axes.Margins(0, 0);

        visual.SavePng("Images/grid.png", 1300, 800);
    }
}