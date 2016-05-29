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

        public void StartDigging(GridSpace space)
        {
            StartCoroutine(EmptySpace(space));
        }

        public IEnumerator EmptySpace(GridSpace space)
        {
            space.SetColor(Color.green);

            for(int i = 0; i < frameDelay;i++)
                yield return new WaitForFixedUpdate();


            var neighbors = space.Neighbors.GetListOfNeighborSpaces().Where(x => !x.HasBeenDug);
            var gridSpaces = neighbors as GridSpace[] ?? neighbors.ToArray();

            if (space.NearbyMines > 0)
            {
                // Display a number. For now, just show a new color
                space.SetColor(Color.yellow);
            }
            else
            {
                foreach (var nearby in gridSpaces)
                {
                    Debug.Log(string.Format("Undug neighbors of {0}: {1}", space.Index, nearby.ToString()));
                    Debug.Log("Auto Digging: " + nearby.Index);
                    nearby.Dig(); // Can be guaranteed to not be a mine
                }
            }

            if (WorldConstants.World.HasWon())
            {
                Debug.Log("Horray, you won!");
                // Win
            }
        }
    }
}
