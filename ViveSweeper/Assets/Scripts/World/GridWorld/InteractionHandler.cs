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
            marker.transform.rotation = Quaternion.Euler(0, 0, 0);

            //Space.SetColor(Color.blue);
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

            Space.SpacePiece.transform.DetachChildren();
            Space.MinimapPiece.transform.DetachChildren();

            GameEngine.DestroyObject(Space.SpacePiece);
            Space.SpacePiece = null;

            GameEngine.DestroyObject(Space.MinimapPiece);
            Space.MinimapPiece = null;
        }

        private void MineInteraction()
        {
            //Space.NumDisplay.text = "!";
            var obj = (MineCoroutine) GetComponentFromEngine("MineCoroutine");
            obj.StartExplosion(Space);
        }

        public void EmptySpace()
        {
            var obj = (EmptySpaceCoroutine)GetComponentFromEngine("EmptySpaceCoroutine");
            obj.StartDigging(Space);
        }

        public void Grab()
        {
            Space.HasMarker = false;
            /*
            if (!Space.IsMine)
                Space.SetColor(Color.gray);
            else
                Space.SetColor(Color.red);
            */
        }

        private Component GetComponentFromEngine(string name)
        {
            var engine = GameObject.Find("GameEngine");

            return engine == null ? null : engine.GetComponent(name);
        }
    }
}
