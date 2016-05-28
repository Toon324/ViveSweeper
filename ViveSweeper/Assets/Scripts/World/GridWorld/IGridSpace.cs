using UnityEngine;

namespace Assets.Scripts.World.GridWorld
{
    public interface IGridSpace
    {
        void Interact();
        void DoneInteracting();
        Transform GetTransform();
    }
}