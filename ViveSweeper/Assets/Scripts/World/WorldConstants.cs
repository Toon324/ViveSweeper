using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class WorldConstants {

        //World Objects
        public static GameObject GridSpace { get; set; }

        public static GridWorld.GridWorld World { get; set; }

        public enum Difficulties { Easy = 9, Medium = 39, Expert = 80}

        public static Difficulties CurrentDifficulty { get; set; }

        public static List<int> PreviouslyDugSpaces = new List<int>();

        public static GameObject MinePrefab { get; set; }

        public static void LoadObjects()
        {
            GridSpace = Resources.Load<GameObject>("GridSpace");
        }
    }
}
