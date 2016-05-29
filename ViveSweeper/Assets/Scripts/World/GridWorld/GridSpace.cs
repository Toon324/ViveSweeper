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

        public NeighborSpaces Neighbors { get; set; }

        public InteractionHandler InteractionHandler { get; set; }

        public Vector3 FlagPos = new Vector3(0,.35f,0);
        #endregion

        public GridSpace(GameObject space, int index, int rowSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, rowSize);
            IsMine = false;
            HasBeenDug = false;
            InteractionHandler = new InteractionHandler(this);
        }

        public void Grab()
        {
            InteractionHandler.Grab();
        }

        public void PlantMarker(GameObject marker)
        {
            InteractionHandler.PlantMarker(marker);
        }

        public void Dig()
        {
            InteractionHandler.Dig();
        }

        public Transform GetTransform()
        {
            return SpacePiece.transform;
        }

        public void SetColor(Color color)
        {
            SpacePiece.GetComponent<MeshRenderer>().material.color = color;
        }

        public override string ToString()
        {
            return string.Format("Space {0}: IsMine ? {1} HasBeenDug ? {2} NearbyMines : {3}", Index, IsMine, HasBeenDug, NearbyMines);
        }
    }
}
