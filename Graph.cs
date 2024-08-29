using static Task2.Exceptions;

namespace Task2
{
    internal class Graph
    {
        private const string POINT_NOT_FOUND = "Point {0} not found";
        private const string INDEX_POINT_NOT_FOUND = "Point at index {0} not found";
        private const int MIN_POINT_TO_CREATE_NODE = 2;

        private Dictionary<int, Point> _points = new();

        public void CreatePoint(Position position)
        {
            if (_points.Any(p => p.Value.Position.Equals(position)))
            {
                Console.WriteLine($"In current position {position} already created point");
                return;
            }

            var newPoint = new Point(position);
            _points.Add(newPoint.Id, newPoint);
        }

        public void CreateNode(Point point1, Point point2)
        {
            if (!_points.ContainsValue(point1))
            {
                Console.WriteLine(string.Format(POINT_NOT_FOUND, point1));
                return;
            }

            if (!_points.ContainsValue(point2))
            {
                Console.WriteLine(string.Format(POINT_NOT_FOUND, point2));
                return;
            }

            TryCreateNode(point1, point2);
        }

        public void CreateNode(int id1, int id2)
        {
            if (!_points.ContainsKey(id1))
            {
                Console.WriteLine(string.Format(POINT_NOT_FOUND, id1));
                return;
            }

            if (!_points.ContainsKey(id2))
            {
                Console.WriteLine(string.Format(POINT_NOT_FOUND, id2));
                return;
            }

            TryCreateNode(_points[id1], _points[id2]);
        }

        public void CreateNode(Position position1, Position position2)
        {
            try
            {
                try
                {
                    Point point1 = _points.Single(p => p.Value.Position.Equals(position1)).Value;

                    try
                    {
                        Point point2 = _points.Single(p => p.Value.Position.Equals(position2)).Value;

                        if (!TryCreateNode(point1, point2))
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is InvalidOperationException)
                        {
                            throw new NotCorrectPointPosition(position2);
                        }

                        throw;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidOperationException)
                    {
                        throw new NotCorrectPointPosition(position1);
                    }

                    throw;
                }
            }
            catch (Exception ex)
            {
                if (ex is NotCorrectPointPosition notCorrectPointPosition)
                {
                    Console.WriteLine(notCorrectPointPosition.Message);
                    return;
                }

                Console.WriteLine("Unknown exception: " + ex.Message);

                throw;
            }
        }

        public IEnumerable<Position> GetPositionNodesByPoint(Point point)
        {
            if (CheakValidPointCount())
            {
                if (!_points.ContainsValue(point))
                {
                    Console.WriteLine($"Point {point} not created");
                    yield break;
                }

                foreach (int nodeId in _points[point.Id].NodeIds)
                {
                    yield return _points[nodeId].Position;
                }
            }
        }

        public IEnumerable<Position> GetPositionNodesByPoint(int id)
        {
            if (CheakValidPointCount())
            {
                if (!_points.ContainsKey(id))
                {
                    Console.WriteLine($"Point at id {id} not created");
                    yield break;
                }

                foreach (int nodeId in _points[id].NodeIds)
                {
                    yield return _points[nodeId].Position;
                }
            }
        }

        public IEnumerable<Position> GetPositionNodesByPoint(Position position)
        {
            if (CheakValidPointCount())
            {
                Point point;

                try
                {
                    point = _points.Single(p => p.Value.Position.Equals(position)).Value;
                }
                catch (Exception ex)
                {
                    if (ex is InvalidOperationException)
                    {
                        Console.WriteLine(new NotCorrectPointPosition(position).Message);
                        yield break;
                    }

                    Console.WriteLine("Unknown exception: " + ex.Message);

                    throw;
                }

                foreach (int nodeId in _points[point.Id].NodeIds)
                {
                    yield return _points[nodeId].Position;
                }
            }
        }

        private bool TryCreateNode(Point point1, Point point2)
        {
            if (CheakValidPointCount())
            {
                if (point1.Id.Equals(point2.Id))
                {
                    Console.WriteLine("You can't noded a point to itself");
                    return false;
                }

                if (point1.HasNode(point2))
                {
                    Console.WriteLine("These points are already nodes");
                    return false;
                }

                point1.AddNode(point2);
                return true;
            }

            return false;
        }

        private bool CheakValidPointCount()
        {
            if (_points.Count < 2)
            {
                Console.WriteLine($"To create a node you need at least {MIN_POINT_TO_CREATE_NODE} points");
                return false;
            }

            return true;
        }
    }
}
