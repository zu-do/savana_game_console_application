namespace Savanna.Models
{
    public interface IAnimalPlugin
    {
        string ToString();
        double Health { get; set; }
        bool IsAlive { get; set; }
        bool IsNew { get; set; }
        bool IsPredator { get; set; }
        int VisionRange { get; set; }
        int X { get; set; }
        int Y { get; set; }
        char CharRepresentation { get; set; }

        void Die();
    }
}