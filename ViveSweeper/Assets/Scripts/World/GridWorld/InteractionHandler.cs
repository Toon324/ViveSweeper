using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            var engine = GameObject.Find("GameEngine");
            var obj = (EmptySpaceCoroutine)engine.GetComponent("EmptySpaceCoroutine");
            obj.StartDigging(Space);
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
