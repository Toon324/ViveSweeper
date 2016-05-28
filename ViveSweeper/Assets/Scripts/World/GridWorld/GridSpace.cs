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

        public bool HasFlag { get; set; }

        public bool HasQuestion { get; set; }

        public bool HasMarker { get; set; }

        public int NearbyMines { get; set; }

        protected NeighborSpaces Neighbors { get; set; }

        protected Vector3 flagPos = new Vector3(0,.52f,0);
        #endregion

        public GridSpace(GameObject space, int index, int worldSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, worldSize);
            IsMine = false;
        }

        public void Grab()
        {
            HasQuestion = false;
            HasFlag = false;

            SetColor(Color.gray);
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
            if (HasFlag || HasQuestion || HasMarker)
                return;

            //Interacting = true;

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
            var neighbors = Neighbors.GetListOfNeighborSpaces();
            NearbyMines = neighbors.Count(x => x.IsMine);

            if (NearbyMines > 0)
            {
                // Display a number. For now, just show a new color
                SetColor(Color.yellow);
            }
            else
            {
                foreach (var space in neighbors)
                {
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
