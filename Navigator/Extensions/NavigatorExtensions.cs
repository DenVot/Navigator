using Navigator.Navigating;
using Navigator.Triangulation;

namespace Navigator.Extensions;

public static class NavigatorExtensions
{
    public static void SetAnomalies(this NavigationProcessor processor, ICollection<TriangulationInfo> infos)
    {
        var anomalies = AnomalyTriangulator.TriangulizeAnomalies(infos);

        foreach (var (x, y, intensive) in anomalies)
        {
            processor.SetAnomaly(x, y, intensive);
        }
    }
}