namespace Savanna.Models
{
    public class Lion : IAnimalPlugin
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int VisionRange { get; set; }
        public bool IsAlive { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsPredator { get; set; } = false;
        public double Health { get; set; }
        public char CharRepresentation { get; set; }

        public Lion()
        {
            this.IsAlive = true;
            this.Health = 20;
            this.CharRepresentation = 'L';
            this.VisionRange = 1;
            this.IsPredator = true;
        }

        public override string ToString()
        {
            return "Lion";
        }

        public void Die()
        {
            this.IsAlive = false;
        }
    }
}
