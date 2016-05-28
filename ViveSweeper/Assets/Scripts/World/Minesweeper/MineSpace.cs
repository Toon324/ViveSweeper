using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.World.GridWorld;
using UnityEngine;

namespace Assets.Scripts.World.Minesweeper
{
    public class MineSpace : GridSpace
    {
        public MineSpace(GameObject space, int index, int worldSize) : base(space, index, worldSize)
        {
            SpacePiece.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public new void Interact()
        {
            if (Interacting)
                return;

            Interacting = true;

            //Debug.Log("Interacting with:"+index);
            SpacePiece.GetComponent<MeshRenderer>().material.color = Color.black;
        }

        public new void DoneInteracting()
        {
            //SpacePiece.GetComponent<MeshRenderer>().material.color = Color.white;
            Interacting = false;
        }
    }
}
