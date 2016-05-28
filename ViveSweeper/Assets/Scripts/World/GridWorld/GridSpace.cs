using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public class GridSpace: IGridSpace
    {
        #region Properties
        public GameObject SpacePiece { get; set; }

        public int Index { get; set; }

        protected NeighborSpaces Neighbors { get; set; }

        protected bool Interacting { get; set; }
        #endregion

        public GridSpace(GameObject space, int index, int worldSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, worldSize);
            Interacting = false;
        }

        public void Interact()
        {
            if (Interacting)
                return;

            Interacting = true;
              
            //Debug.Log("Interacting with:"+index);
            SpacePiece.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        public void DoneInteracting()
        {
            SpacePiece.GetComponent<MeshRenderer>().material.color = Color.white;
            Interacting = false;
        }

        public Transform GetTransform()
        {
            return SpacePiece.transform;
        }

    }

    public class NeighborSpaces
    {
        #region Properties
        public int Up { get; set; }
        public int UpRight { get; set; }
        public int UpLeft { get; set; }
        public int Down { get; set; }
        public int DownRight { get; set; }
        public int DownLeft { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }
        #endregion

        public NeighborSpaces(int index, int worldSize)
        {
            SetNeighbors(index, worldSize);
        }

        public void SetNeighbors(int index, int worldSize)
        {
            if (index < 0)
            {
                return;
            }

            Up = index - worldSize;
            UpRight = Up + 1;
            UpLeft = Up - 1;
            Down = index + worldSize;
            DownLeft = Down - 1;
            DownRight = Down + 1;
            Right = index + 1;
            Left = index - 1;
        }
    }
}
