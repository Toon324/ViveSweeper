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

            SetColor(Color.blue);
        }

        public void PlantQuestionMark()
        {
            HasQuestion = true;

            SetColor(Color.white);
        }

        public void PlantFlag()
        {
            HasFlag = true;

            SetColor(Color.magenta);
        }

        public void Dig()
        {
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
