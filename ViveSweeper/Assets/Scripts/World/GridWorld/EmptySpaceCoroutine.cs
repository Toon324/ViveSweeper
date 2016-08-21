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
        private EndGameHandler endGameHandler;

        [SerializeField]
        private int frameDelay;

        // These colors should be in order
        private static Color[] Colors = new Color[]
        {
                new Color(81f, 168f, 255f), // 1 Mine
                new Color(33f, 203f, 48f), // 2 Mines
                new Color(204, 0, 0), // 3 Mines
                new Color(0, 9, 178), // 4 Mines
                new Color(120, 4, 4), // 5 Mines
                new Color(96, 235, 255), // 6 Mines
                new Color(145, 145, 145), // 7 Mines
                new Color(0, 0, 0) // 8 Mines
        };

        public void StartDigging(GridSpace space)
        {
            StartCoroutine(EmptySpace(space));
        }

        public IEnumerator EmptySpace(GridSpace space)
        {
            for (int i = 0; i < frameDelay; i++)
                yield return new WaitForFixedUpdate();


            var neighbors = space.Neighbors.GetListOfNeighborSpaces().Where(x => !x.HasBeenDug);
            var gridSpaces = neighbors as GridSpace[] ?? neighbors.ToArray();

            if (space.NearbyMines > 0)
            {
                space.NumDisplay.text = space.NearbyMines.ToString();
                space.MinimapNumDisplay.text = space.NearbyMines.ToString();

                space.NumDisplay.color = Colors[space.NearbyMines - 1];
                space.MinimapNumDisplay.color = Colors[space.NearbyMines - 1];
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

            if (WorldConstants.World.HasWon())
            {
                Debug.Log("Horray, you won!");
                endGameHandler.WonGame();
            }
        }
    }
}
