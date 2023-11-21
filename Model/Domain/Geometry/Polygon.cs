using Model.Exception;
using Model.ObjectValue.Geometry;

namespace Model.Geometry
{
    public class Polygon
    {
        public List<Coordinate> Coordinates { get; private set; }
        public double Area { get; private set; }

        public Polygon(List<Coordinate> coordinates)
        {
            ValidateCoordinate(coordinates);
            this.Coordinates = coordinates;
            CalculateArea();
        }

        private static void ValidateCoordinate(List<Coordinate> coordinates)
        {
            if (coordinates.Count < 4)
            {
                throw new ArgumentException(Messages.LessCoordinatePolygon);
            }
        }

        private void CalculateArea()
        {
            this.Area = 0;
            int n = Coordinates.Count;

            for (int i = 0; i < n - 1; i++)
            {
                Area += (Coordinates[i].X * Coordinates[i + 1].Y) - (Coordinates[i + 1].X * Coordinates[i].Y);
            }

            Area += (Coordinates[n - 1].X * Coordinates[0].Y) - (Coordinates[0].X * Coordinates[n - 1].Y);

            Area = Math.Abs(Area) / 2.0;
        }

        public void SetCoordinates(List<Coordinate> newCoordinates)
        {
            Coordinates = newCoordinates;
            CalculateArea();
        }

        //Ray casting
        public bool IsPointInsidePolygon(Coordinate testPoint)
        {
            int count = this.Coordinates.Count;
            bool inside = false;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                if ((this.Coordinates[i].Y > testPoint.Y) != (this.Coordinates[j].Y > testPoint.Y) &&
                    testPoint.X < (this.Coordinates[j].X - this.Coordinates[i].X) * (testPoint.Y - this.Coordinates[i].Y) / (this.Coordinates[j].Y - this.Coordinates[i].Y) + this.Coordinates[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
