using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekMachlinskiLab2
{
    abstract class Creature
    {
        Random random = new Random();
        public int MaxSpeed { get; set; }

        public int GetCurrentSpeed()
        {
            return random.Next(MaxSpeed);
        }
    }
}
