using Navigator.Triangulation;

var trianInfoA = new TriangulationInfo(
    new TriangulationPoint(12, 19, 5),
    new TriangulationPoint(16, 19, 5),
    new TriangulationPoint(14, 21, 5));
var trianInfoB = new TriangulationInfo(
    new TriangulationPoint(16, 1, 2),
    new TriangulationPoint(12, 9, 50),
    new TriangulationPoint(13, 13, 8));


var anomalies = AnomalyTriangulator.TriangulizeAnomalies(new List<TriangulationInfo>() 
    {
        trianInfoA, 
        trianInfoB
    });

foreach (var anomalyInfo in anomalies)
{
    Console.WriteLine($"{anomalyInfo.X} {anomalyInfo.Y} {anomalyInfo.Intensive}");
}
