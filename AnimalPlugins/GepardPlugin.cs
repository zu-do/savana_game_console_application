using Savanna.Models;

namespace AnimalPlugins
{
    public class GepardPlugin : IAnimalPlugin
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int VisionRange { get; set; }
        public bool IsAlive { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsPredator { get; set; } = false;
        public double Health { get; set; }
        public char CharRepresentation { get; set; }

        public GepardPlugin()
        {
            this.IsAlive = true;
            this.IsPredator = true;
            this.Health = 20;
            this.CharRepresentation = 'G';
            this.VisionRange = 2;
        }

        public override string ToString()
        {
            return "Gepard";
        }

        public void Die()
        {
            this.IsAlive = false;
        }
    }
}