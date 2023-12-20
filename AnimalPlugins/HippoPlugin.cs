using Savanna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalPlugins
{
    public class HippoPlugin : IAnimalPlugin
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int VisionRange { get; set; }
        public bool IsAlive { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsPredator { get; set; } = false;
        public double Health { get; set; }
        public char CharRepresentation { get; set; }

        public HippoPlugin()
        {
            this.IsAlive = true;
            this.Health = 20;
            this.CharRepresentation = 'H';
            this.VisionRange = 2;
        }

        public override string ToString()
        {
            return "Hippo";
        }

        public void Die()
        {
            this.IsAlive = false;
        }
    }
}
