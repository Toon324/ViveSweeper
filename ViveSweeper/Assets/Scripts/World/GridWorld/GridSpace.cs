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

        public int NearbyMines { get; set; }

        protected NeighborSpaces Neighbors { get; set; }

        protected bool Interacting { get; set; }
        #endregion

        public GridSpace(GameObject space, int index, int worldSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, worldSize);
            Interacting = false;
            IsMine = false;
        }

        public void Grab()
        {
            if (Interacting)
                return;

            Interacting = true;
            HasQuestion = false;
            HasFlag = false;

            SetColor(Color.gray);
        }

        public void PlantQuestionMark()
        {
            if (Interacting)
                return;

            Interacting = true;
            HasQuestion = true;

            SetColor(Color.white);
        }

        public void PlantFlag()
        {
            if (Interacting)
                return;

            Interacting = true;
            HasFlag = true;

            SetColor(Color.magenta);
        }

        public void Dig()
        {
            if (Interacting)
                return;

            Interacting = true;

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

        }

        public void DoneInteracting()
        {
            Interacting = false;
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

        public IEnumerable<GridSpace> GetListOfNeighborSpaces()
        {
            var world = WorldConstants.World;

            if (world == null)
            {
                return new List<GridSpace>();
            }

            return new List<GridSpace>
            {
                world.GetSpaceFromWorldIndex(Up),
                world.GetSpaceFromWorldIndex(UpRight),
                world.GetSpaceFromWorldIndex(UpLeft),
                world.GetSpaceFromWorldIndex(Down),
                world.GetSpaceFromWorldIndex(DownLeft),
                world.GetSpaceFromWorldIndex(DownRight),
                world.GetSpaceFromWorldIndex(Right),
                world.GetSpaceFromWorldIndex(Left)
            }.Where(x => x != null);
        }
    }
}
