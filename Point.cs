using System.Linq;

namespace Task2
{
    internal class Point
    {
        private static int _countPoint = 0;

        public int Id => _id;
        public Position Position => _position;
        public List<int> NodeIds => _nodeIds;

        private readonly int _id;
        private readonly Position _position;
        private List<int> _nodeIds = new();

        public Point(Position position)
        {
            _id = _countPoint;
            _position = position;
            _countPoint++;
        }

        public void AddNode(Point point)
        {
            if (!_nodeIds.Contains(point.Id))
            {
                _nodeIds.Add(point.Id);
                point.AddNode(this);
            }
        }

        public bool HasNode(Point point) => _nodeIds.Contains(point.Id);

        public override string ToString()
        {
            return $"id: {_id}, position {_position}";
        }
    }
}
