using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World.GridWorld
{
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

        public NeighborSpaces(int index)
        {
            SetNeighbors(index);
        }

        public void SetNeighbors(int index)
        {
            if (index < 0)
            {
                return;
            }

            var worldSize = WorldConstants.World.Size;

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