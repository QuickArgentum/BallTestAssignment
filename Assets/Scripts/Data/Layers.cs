using UnityEngine;

namespace Data
{
    public class Layers
    {
        public static readonly int Destination = LayerMask.NameToLayer("Destination");

        public static readonly int VictoryMask = LayerMask.GetMask("Obstacle", "Destination");
    }
}