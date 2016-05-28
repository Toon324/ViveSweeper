using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public class GridSpace
    {
        #region Properties
        public GameObject SpacePiece { get; set; }

        public int Index { get; set; }

        public bool IsMine { get; set; }

        public bool HasBeenDug { get; set; }

        public bool HasMarker { get; set; }

        public int NearbyMines { get; set; }

        protected NeighborSpaces Neighbors { get; set; }

        protected Vector3 flagPos = new Vector3(0,1.5f,0);
        #endregion

        public GridSpace(GameObject space, int index, int rowSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, rowSize);
            IsMine = false;
            HasBeenDug = false;
        }

        public void Grab()
        {
            HasMarker = false;

            if (!IsMine)
                SetColor(Color.gray);
            else
                SetColor(Color.red);
        }

        public void PlantMarker(GameObject marker)
        {
            HasMarker = true;

            marker.transform.parent = SpacePiece.transform;
            marker.transform.localPosition = flagPos;
            marker.transform.rotation = Quaternion.Euler(0, 180, 0);

            SetColor(Color.blue);
        }

        public void Dig()
        {
            if (HasMarker)
                return;

            HasBeenDug = true;

            Debug.Log("Digging Self: " + Index);

            if (IsMine)
            {
                MineInteraction();
            }
            else
            {
                EmptySpaceInteraction();
            }
        }

        private void MineInteraction()
        {
            SetColor(Color.black);
            // Lose
        }

        private void EmptySpaceInteraction()
        {
            SetColor(Color.green);
            var neighbors = Neighbors.GetListOfNeighborSpaces().Where(x => !x.HasBeenDug);
            var gridSpaces = neighbors as GridSpace[] ?? neighbors.ToArray();

            NearbyMines = gridSpaces.Count(x => x.IsMine);

            if (NearbyMines > 0)
            {
                // Display a number. For now, just show a new color
                SetColor(Color.yellow);
            }
            else
            {
                foreach (var space in gridSpaces)
                {
                    Debug.Log("Auto Digging: " + space.Index);
                    space.Dig(); // Can be guaranteed to not be a mine
                }
            }

            if (WorldConstants.World.HasWon())
            {
                // Win
            }
        }

        public Transform GetTransform()
        {
            return SpacePiece.transform;
        }

        public void SetColor(Color color)
        {
            SpacePiece.GetComponent<MeshRenderer>().material.color = color;
        }

    }
}
