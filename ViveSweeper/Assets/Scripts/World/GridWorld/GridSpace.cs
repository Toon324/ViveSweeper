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

        public Vector3 FlagPos = new Vector3(0, .35f, 0);

        private TextMesh NumDisplay;

        private Color oneColor = new Color(81, 168, 255);
        private Color twoColor = new Color(33,203,48);
        private Color threeColor = new Color(204, 0, 0);
        private Color fourColor = new Color(0, 9, 178);
        private Color fiveColor = new Color(120, 4, 4);
        private Color sixColor = new Color(96, 235, 255);
        private Color sevenColor = new Color(145, 145, 145);
        private Color eightColor = new Color(0, 0, 0);


        #endregion

        public GridSpace(GameObject space, int index, int rowSize)
        {
            SpacePiece = space;
            Index = index;
            Neighbors = new NeighborSpaces(index, rowSize);
            IsMine = false;
            HasBeenDug = false;
            InteractionHandler = new InteractionHandler(this);

            NumDisplay = space.GetComponentInChildren<TextMesh>();

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

            if (HasMarker)
                return;

            if(NearbyMines != 0)
            {
                NumDisplay.text = "" + NearbyMines;

                if (NearbyMines == 1)
                    NumDisplay.color = oneColor;
                else if (NearbyMines == 2)
                    NumDisplay.color = twoColor;
                else if (NearbyMines == 3)
                    NumDisplay.color = threeColor;
                else if (NearbyMines == 4)
                    NumDisplay.color = fourColor;
                else if (NearbyMines == 5)
                    NumDisplay.color = fiveColor;
                else if (NearbyMines == 6)
                    NumDisplay.color = sixColor;
                else if (NearbyMines == 7)
                    NumDisplay.color = sevenColor;
                else if (NearbyMines == 8)
                    NumDisplay.color = eightColor;


            }

            if (IsMine)
                NumDisplay.text = "!";

            SpacePiece.transform.DetachChildren();
            GameObject engineObj = GameObject.Find("GameEngine");
            GameEngine engine = (GameEngine)engineObj.GetComponent("GameEngine");
            GameEngine.DestroyObject(SpacePiece);
            SpacePiece = null;
        }

        public Transform GetTransform()
        {
            return SpacePiece.transform;
        }

        public void SetColor(Color color)
        {
            if(SpacePiece != null)
                SpacePiece.GetComponent<MeshRenderer>().material.color = color;
        }

        public override string ToString()
        {
            return string.Format("Space {0}: IsMine ? {1} HasBeenDug ? {2} NearbyMines : {3}", Index, IsMine, HasBeenDug, NearbyMines);
        }
    }
}
