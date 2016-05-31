using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public class EmptySpaceCoroutine : MonoBehaviour
    {

        [SerializeField]
        private int frameDelay;

        private static Color oneColor = new Color(81f, 168f, 255f);
        private static Color twoColor = new Color(33f, 203f, 48f);
        private static Color threeColor = new Color(204, 0, 0);
        private static Color fourColor = new Color(0, 9, 178);
        private static Color fiveColor = new Color(120, 4, 4);
        private static Color sixColor = new Color(96, 235, 255);
        private static Color sevenColor = new Color(145, 145, 145);
        private static Color eightColor = new Color(0, 0, 0);

        public void StartDigging(GridSpace space)
        {
            StartCoroutine(EmptySpace(space));
        }

        public IEnumerator EmptySpace(GridSpace space)
        {
            for(int i = 0; i < frameDelay;i++)
                yield return new WaitForFixedUpdate();


            var neighbors = space.Neighbors.GetListOfNeighborSpaces().Where(x => !x.HasBeenDug);
            var gridSpaces = neighbors as GridSpace[] ?? neighbors.ToArray();

            if (space.NearbyMines > 0)
            {
                space.NumDisplay.text = "" + space.NearbyMines;
                space.MinimapNumDisplay.text = "" + space.NearbyMines;

                switch (space.NearbyMines)
                {
                    case 1:
                        space.NumDisplay.color = oneColor;
                        space.MinimapNumDisplay.color = oneColor;
                        break;
                    case 2:
                        space.NumDisplay.color = twoColor;
                        space.MinimapNumDisplay.color = twoColor;
                        break;
                    case 3:
                        space.NumDisplay.color = threeColor;
                        space.MinimapNumDisplay.color = threeColor;
                        break;
                    case 4:
                        space.NumDisplay.color = fourColor;
                        space.MinimapNumDisplay.color = fourColor;
                        break;
                    case 5:
                        space.NumDisplay.color = fiveColor;
                        space.MinimapNumDisplay.color = fiveColor;
                        break;
                    case 6:
                        space.NumDisplay.color = sixColor;
                        space.MinimapNumDisplay.color = sixColor;
                        break;
                    case 7:
                        space.NumDisplay.color = sevenColor;
                        space.MinimapNumDisplay.color = sevenColor;
                        break;
                    case 8:
                        space.NumDisplay.color = eightColor;
                        space.MinimapNumDisplay.color = eightColor;
                        break;
                }
            }
            else
            {
                foreach (var nearby in gridSpaces)
                {
                    // Debug.Log(string.Format("Undug neighbors of {0}: {1}", space.Index, nearby.ToString()));
                    // Debug.Log("Auto Digging: " + nearby.Index);
                    nearby.Dig(); // Can be guaranteed to not be a mine
                }
            }

            //space.SpacePiece.transform.DetachChildren();
            //GameObject engineObj = GameObject.Find("GameEngine");
            //GameEngine engine = (GameEngine)engineObj.GetComponent("GameEngine");
            //GameEngine.DestroyObject(space.SpacePiece);

            Destroy(space.SpacePiece);
            space.SpacePiece = null;

            Destroy(space.MinimapPiece);
            space.MinimapPiece = null;

            if (WorldConstants.World.HasWon())
            {
                Debug.Log("Horray, you won!");
                // Win
            }
        }
    }
}
