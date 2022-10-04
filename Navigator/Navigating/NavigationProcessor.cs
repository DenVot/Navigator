using Navigator.Triangulation;

namespace Navigator.Navigating;

public class NavigationProcessor
{
    public Grid Grid => _map.Grid;

    private readonly Map _map = new();

    private readonly WayNode[,] _nodes = new WayNode[Grid.Height, Grid.Width];

    private static Random _random = new();

    public void SetAnomaly(int x, int y, double value)
    {
        _map.SetAnomaly(x, y, value);
    }

    public void SearchWay(int xA, int yA, int xB, int yB)
    {
        // Обнуляем матрицу и ставим точку А и Б
        for (var x = 0; x < Grid.Width; x++)
        {
            for (var y = 0; y < Grid.Height; y++)
            {
                _nodes[y, x] = new WayNode(x, y, xA == x && yA == y, xB == x && yB == y);
            }
        }

        var node = GoThrough(xA, yA, xB, yB, 0); // Начинаем алгоритм с точки А и расстояния от главного узла 0

        while (!node?.IsStartPoint ?? false)
        {
            Grid.SetCellColor(node.X, node.Y, ConsoleColor.Cyan);
            node = node.PreviousNode;
        }
        
        Grid.SetCellColor(xA, yA, ConsoleColor.Magenta);
        Grid.SetCellColor(xB, yB, ConsoleColor.Magenta);
    }

    private WayNode? GoThrough(int x, int y, int xB, int yB, int distanceFromMain)
    {
        if (Grid.IsOutOfGrid(x, y)) return null; // Если вышли за границы поля, возвращаем null

        var currentNode = _nodes[y, x];
        var nearNodes = new List<WayNode>(); // Список с окружающими узлами

        for (var dx = -1; dx < 2; dx++)
        {
            for (var dy = -1; dy < 2; dy++)
            {
                if(dx == 0 && dy == 0) continue; // Тот же самый узел игнорим
                
                var absX = x + dx;
                var absY = y + dy;
                
                if((int)_map.Grid.GetValue(absX, absY) == 1) continue; // Аномалия

                if(Grid.IsOutOfGrid(absX, absY)) continue; // Если узел вне полня, пропускаем

                var nodesCount = (Math.Abs(xB - absX) + Math.Abs(yB - absY)) * 10; // Еврестическое приближеие
                var distFromCheckingNode = Math.Pow(Math.Pow(x - absX, 2) + Math.Pow(y - absY, 2), 0.5); // Растояние от точки проверки до конкретной
                var ctxDistanceFromMain = distanceFromMain + (int)Math.Ceiling(distFromCheckingNode);
                var weight = nodesCount + (int) (distFromCheckingNode * 10);
                var ctxNode = _nodes[absY, absX];

                // Если узел не "подсчитан" или расстояние от начальной точки меньше, чем конкретное, то сразу устанавливаем его вес и напрвление
                if (!ctxNode.IsCalculated || ctxDistanceFromMain < ctxNode.DistanceFromMainNode)  
                {
                    ctxNode.Weight = weight;
                    ctxNode.PreviousNode = currentNode;
                    ctxNode.DistanceFromMainNode = ctxDistanceFromMain;
                }
                
                nearNodes.Add(ctxNode);
            }   
        }

        var minWeight = nearNodes.Min(node => node.Weight); // Минимальный вес среди всех
        var nodesWithMinWeight = nearNodes.Where(node => node.Weight == minWeight).ToArray(); // Все узлы с минимальными весами
        
        var endPoint = nodesWithMinWeight.FirstOrDefault(node => node.IsEndPoint); // Проверка на точку назначения
        if (endPoint != null) return endPoint;
        
        // Выбираем рандомный узел и чекаем его дальше

        var randomMinNode = nodesWithMinWeight[_random.Next(0, nodesWithMinWeight.Length)];

        return GoThrough(randomMinNode.X, randomMinNode.Y, xB, yB, randomMinNode.DistanceFromMainNode);
    }
}