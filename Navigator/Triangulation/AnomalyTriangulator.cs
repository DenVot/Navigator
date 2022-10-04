using Navigator.Navigating;

namespace Navigator.Triangulation;

/// <summary>
/// Триангулирует аномалии
/// </summary>
public static class AnomalyTriangulator
{
    /// <summary>
    /// Триангулирует местоположение аномалии
    /// </summary>
    /// <param name="infos">Данные для триангуляции конкретной аномалии</param>
    /// <returns>Перечисление аномалий</returns>
    public static IEnumerable<AnomalyInfo> TriangulizeAnomalies(ICollection<TriangulationInfo> infos)
    {
        /*
         * Суть алгоритма
         * Т.к. поле у нас дискретизировано по секторам, можно пройтись по ним посчитав в каждой точке значения int0.
         * Если у всех трех точек совпадает int0 во втором приближении в конкретной точке, то считаем этот сектор центром аномалии 
         */
        
        for (var x = 0; x < Grid.Width; x++)
        {
            for (var y = 0; y < Grid.Height; y++)
            {
                double CalculateHypot(TriangulationPoint point) => Hypot(point.X - x, point.Y - y);
                var infosToRemove = new List<TriangulationInfo>();
                
                foreach (var triangulationInfo in infos)
                {
                    // Точки, где были зарегистрированы аномалии
                    var a = triangulationInfo.A;
                    var b = triangulationInfo.B;
                    var c = triangulationInfo.C;
                    
                    // С помощью теоремы Пифагора считаем растояние до предполагаемой аномалии
                    var da = CalculateHypot(a);
                    var db = CalculateHypot(b);
                    var dc = CalculateHypot(c);

                    // Считаем предполагаемый int0 для каждой точки 
                    var aint0 = CalculateInt0(da, a.Int);
                    var bint0 = CalculateInt0(db, b.Int);
                    var cint0 = CalculateInt0(dc, c.Int);

                    // Если не совпали, то идем дальше
                    if (Math.Abs(aint0 - bint0) > 0.01 || Math.Abs(bint0 - cint0) > 0.01) continue;
                    
                    // Иначе выкидываем эти точки и возвращаем аномалию
                    infosToRemove.Add(triangulationInfo);
                    yield return new AnomalyInfo(x, y, aint0);
                }

                // Удаляем ненужные данные для оптимизации производительности
                foreach (var infoToDelete in infosToRemove)
                {
                    infos.Remove(infoToDelete);
                }
            }
        }
    }

    private static double Hypot(double dx, double dy) => Math.Pow(Math.Pow(dx, 2) + Math.Pow(dy, 2), 0.5);
    private static double CalculateInt0(double r, double ctxAnomaly) => ctxAnomaly * r * r;
}