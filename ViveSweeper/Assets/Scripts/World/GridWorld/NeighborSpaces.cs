using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace Assets.Scripts.World.GridWorld
{
    public class NeighborSpaces
    {
        #region Properties
        private int UpIndex { get; set; }
        private int UpRightIndex { get; set; }
        private int UpLeftIndex { get; set; }
        private int DownIndex { get; set; }
        private int DownRightIndex { get; set; }
        private int DownLeftIndex { get; set; }
        private int RightIndex { get; set; }
        private int LeftIndex { get; set; }

        public GridSpace Up { get; set; }
        public GridSpace UpRight { get; set; }
        public GridSpace UpLeft { get; set; }
        public GridSpace Down { get; set; }
        public GridSpace DownRight { get; set; }
        public GridSpace DownLeft { get; set; }
        public GridSpace Right { get; set; }
        public GridSpace Left { get; set; }
        #endregion

        public NeighborSpaces(int index, int rowSize)
        {
            SetNeighborIndexes(index, rowSize);
        }

        public void SetNeighborIndexes(int index, int rowSize)
        {
            if (index < 0)
            {
                return;
            }

            UpIndex = index - rowSize;
            UpRightIndex = UpIndex + 1;
            UpLeftIndex = UpIndex - 1;
            DownIndex = index + rowSize;
            DownLeftIndex = DownIndex - 1;
            DownRightIndex = DownIndex + 1;
            RightIndex = index + 1;
            LeftIndex = index - 1;

            CheckForBadIndexes(index, rowSize);
        }

        public void CheckForBadIndexes(int index, int rowSize)
        {
            // Up and Down are already checked for in other places

            if (index % rowSize == 0)
            {
                LeftIndex = -1;
                UpLeftIndex = -1;
                DownLeftIndex = -1;
            }

            if (index % rowSize == 8)
            {
                RightIndex = -1;
                UpRightIndex = -1;
                DownRightIndex = -1;
            }
        }

        public void SetNeighbors(GridWorld world)
        {
            Up = world.GetSpaceFromWorldIndex(UpIndex);
            UpRight = world.GetSpaceFromWorldIndex(UpRightIndex);
            UpLeft = world.GetSpaceFromWorldIndex(UpLeftIndex);
            Down = world.GetSpaceFromWorldIndex(DownIndex);
            DownRight = world.GetSpaceFromWorldIndex(DownLeftIndex);
            DownLeft = world.GetSpaceFromWorldIndex(DownRightIndex);
            Right = world.GetSpaceFromWorldIndex(RightIndex);
            Left = world.GetSpaceFromWorldIndex(LeftIndex);
        }

        public IEnumerable<GridSpace> GetListOfNeighborSpaces()
        {
            return new List<GridSpace>
            {
                Up, UpRight, UpLeft,
                Down, DownRight, DownLeft,
                Right, Left
            }.Where(x => x != null && !WorldConstants.PreviouslyDugSpaces.Contains(x.Index));
        }
    }
}