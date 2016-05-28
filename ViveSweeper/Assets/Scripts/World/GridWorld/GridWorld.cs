using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public class GridWorld {

        public int Size { get; set; }

        public int TotalSize
        {
            get { return Size * Size; }
        }

        private GridSpace[] World { get; set; }

        public GameObject GridSpace { get; set; }

        private const float WorldScaleFactor = 1f;

        private GameObject WorldObj { get; set; }

        private const int TransformYPosition = -1;

        public GridWorld(int size = 9)
        {    
            Size = size;
            World = new GridSpace[Size * Size];
            GridSpace = WorldConstants.GridSpace;

            WorldObj = new GameObject
            {
                name = "World"
            };

            GenerateWorld();
        }

        private void GenerateWorld()
        {
            for (var index = 0; index < Size*Size; index++)
            {
                var x = index / Size;
                var z = index % Size;

                var xPos = x * WorldScaleFactor;
                var zPos = z * WorldScaleFactor;


                var space = (GameObject)
                    Object.Instantiate(GridSpace, new Vector3(xPos, TransformYPosition, zPos), Quaternion.identity);

                space.name = "" + index;

                World[index] = new GridSpace(space, index, Size);

                space.transform.parent = WorldObj.transform;
            }
        }

        public GridSpace GetSpaceFromWorldIndex(int y)
        {
            return World[y];
        }


    }
}
