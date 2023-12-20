namespace Savanna
{
    public class FieldMap
    {
        public int Height { get; set; }
        public int Width { get; set; }

        private bool[,] Occupancy;

        public FieldMap(int width, int height) 
        {
            this.Height = width;
            this.Width = height;
            Occupancy = new bool[this.Width, this.Height];
        }

        public bool IsPositionEmpty(int x, int y)
        {
            if (x >= 0 && x < Occupancy.GetLength(0) && y >= 0 && y < Occupancy.GetLength(1))
            {
                return !Occupancy[x, y];
            }
            return false; //out of field bounds
        }

        public void MarkPositionOccupied(int x, int y)
        {
            if (x >= 0 && x < Occupancy.GetLength(0) && y >= 0 && y < Occupancy.GetLength(1))
            {
                Occupancy[x, y] = true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Position is out of bounds");
            }
        }

        public void MarkPositionEmpty(int x, int y)
        {
            if (x >= 0 && x < Occupancy.GetLength(0) && y >= 0 && y < Occupancy.GetLength(1))
            {
                Occupancy[x, y] = false;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Position is out of bounds");
            }
        }
    }
}
