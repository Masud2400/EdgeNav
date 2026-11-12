public class EdgeNav(
    (int X, int Y) startPos, 
    (int X, int Y) endPos,
    Grid2D grid
)
{
    public int CalculateDistance((int x, int y) posOne, (int x, int y) posTwo)
    {
        //Chebyshev Distance
        int dx = Math.Abs(posTwo.x - posOne.x);
        int dy = Math.Abs(posTwo.y - posOne.y);
        return Math.Max(dx, dy);
    }

    public List<(float X, float Y)> Lerp(
        (int X, int Y) posOne,
        (int X, int Y) posTwo
    )
    {
        var increments = new List<(float X, float Y)>();

        float t = 0.1f;

        for(float i = 0f; i <= 1; i += t)
        {
            float x = posOne.X + (posTwo.X - posOne.X) * i;
            float y = posOne.Y + (posTwo.Y - posOne.Y) * i;

            increments.Add(
                (x, y)
            );
        }

        increments.Add((posTwo.X, posTwo.Y));

        return increments;
    }

    public List<(int X, int Y)> FindObstacles()
    {
        List<(float X, float Y)> increments = Lerp(startPos, endPos);        

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
        Dictionary<string, List<(int X, int Y)>> obstaclesEdges = [];

        (int X, int Y)[] directions = [(0, 1), (0, -1), (1, 0), (-1, 0)];

        int obstacleIndex = 1;

        foreach (var (X, Y) in foundObstacles)
        {
            (int X, int Y) max = 
                (int.MinValue, int.MinValue);
            (int X, int Y) min = 
                (int.MaxValue, int.MaxValue);

            foreach (var (dX, dY) in directions)
            {
                int x = X + dX;
                int y = Y + dY;

                int value = grid.GetValue(x, y);

                while (value > 0)
                {
                    if(x > max.X || y > max.Y)
                        max = (x, y);

                    if(x < min.X || y < min.Y)
                        min = (x, y);

                    x += dX;
                    y += dY;
                    value = grid.GetValue(x, y);
                }
            }

            string key = $"Obstacle_{obstacleIndex++}";
            obstaclesEdges[key] = [];
            obstaclesEdges[key].AddRange([max, min]);
        }

        return obstaclesEdges;
    }

    public Node CreateNode(
        (int X, int Y) position,
        int g,
        int d
    )
    {
        return new Node{
            position = position,
            g = g,
            d = d,
            f = g + d
        };
    }

    public List<Node> FindClosestEdge()
    {
        var obstaclesEdges = FindEdges();

        List<Node> nodes = [];
        var openList = new PriorityQueue<Node, int>();

        int currentDis = CalculateDistance(startPos, endPos);

        Node currentNode = CreateNode(
            startPos, 0, currentDis
        );

        nodes.Add(currentNode);

        foreach (var kvp in obstaclesEdges)
        {
            foreach (var val in kvp.Value)
            {
                int tentativeG = currentNode.g +
                    CalculateDistance(currentNode.position, val);

                currentDis = CalculateDistance(val, endPos);

                Node nextNode = CreateNode(
                    val, tentativeG, currentDis
                );

                openList.Enqueue(nextNode, nextNode.f);
            }

            if (openList.Count > 0)
            {
                currentNode = openList.Dequeue();
                nodes.Add(currentNode);
            }

            openList.Clear();
        }

        nodes.Add(CreateNode(endPos, 0, 0));
        return nodes;
    }
    
    public List<(float X, float Y)> ConnectNodes()
    {
        var nodes = FindClosestEdge();

        var path = new List<(float X, float Y)>();

        var increments = Lerp(startPos, nodes[0].position);
        path.AddRange(increments);

        var currentNode = nodes[0].position;

        foreach(var node in nodes)
        {
            var nodePos = node.position;
            if(nodePos == currentNode)
                continue;

            increments = Lerp(currentNode, nodePos);
            currentNode = nodePos;
            path.AddRange(increments);
        }

        increments = Lerp(nodes[^1].position, endPos);
        path.AddRange(increments);

        return path;
    }
}