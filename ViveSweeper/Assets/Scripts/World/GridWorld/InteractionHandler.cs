using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public class InteractionHandler
    {
        private GridSpace Space { get; set; }

        public InteractionHandler(GridSpace space)
        {
            Space = space;
        }

        public void PlantMarker(GameObject marker)
        {
            Space.HasMarker = true;

            marker.transform.parent = Space.SpacePiece.transform;
            marker.transform.localPosition = Space.FlagPos;
            marker.transform.rotation = Quaternion.Euler(0, 180, 0);

            Space.SetColor(Color.blue);
        }

        public void Dig()
        {
            if (Space.HasMarker || Space.HasBeenDug)
                return;

            Space.HasBeenDug = true;
            WorldConstants.PreviouslyDugSpaces.Add(Space.Index);

            if (Space.IsMine)
            {
                MineInteraction();
            }
            else
            {
                EmptySpace();
            }
        }

        private void MineInteraction()
        {
            Space.SetColor(Color.black);
            // Lose
            Debug.Log("BOOM, you have lost");
        }

        public void EmptySpace()
        {
            Space.SetColor(Color.green);
            var neighbors = Space.Neighbors.GetListOfNeighborSpaces().Where(x => !x.HasBeenDug);
            var gridSpaces = neighbors as GridSpace[] ?? neighbors.ToArray();

            if (Space.NearbyMines > 0)
            {
                // Display a number. For now, just show a new color
                Space.SetColor(Color.yellow);
            }
            else
            {
                foreach (var space in gridSpaces)
                {
                    Debug.Log(string.Format("Undug neighbors of {0}: {1}", Space.Index, space.ToString()) );
                    Debug.Log("Auto Digging: " + space.Index);
                    space.Dig(); // Can be guaranteed to not be a mine
                }
            }

            if (WorldConstants.World.HasWon())
            {
                Debug.Log("Horray, you won!");
                // Win
            }
        }

        public void Grab()
        {
            Space.HasMarker = false;

            if (!Space.IsMine)
                Space.SetColor(Color.gray);
            else
                Space.SetColor(Color.red);
        }
    }
}
