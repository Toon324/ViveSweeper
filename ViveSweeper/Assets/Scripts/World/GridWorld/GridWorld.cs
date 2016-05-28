﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.World.GridWorld
{
    public class GridWorld {

        public int Size { get; set; }

        public int TotalSize
        {
            get { return Size * Size; }
        }

        private GridSpace[] World { get; set; }

        public GameObject GridSpace { get; set; }

        private const float WorldScaleFactor = 1f;

        private GameObject WorldObj { get; set; }

        private const int TransformYPosition = -1;

        public GridWorld(int size = 9)
        {    
            Size = size;
            World = new GridSpace[Size * Size];
            GridSpace = WorldConstants.GridSpace;

            WorldObj = new GameObject
            {
                name = "World"
            };

            GenerateWorld();
        }

        private void GenerateWorld()
        {
            GenerateEmptyWorld();

            PlaceMines();

            foreach (var space in World)
            {
                // Prefetch spaces and mine Counts for performance
                space.Neighbors.SetNeighbors(this);
                space.NearbyMines = space.Neighbors.GetListOfNeighborSpaces().Count(x => x.IsMine);
            }
        }

        private void PlaceMines()
        {
            var numOfMines = (int) WorldConstants.CurrentDifficulty;

            var usedIndexes = new List<int>();

            for (var x = numOfMines; x > 0; x--)
            {
                var random = new Random();
                var randomNumber = -1;

                while (randomNumber == -1 || usedIndexes.Contains(randomNumber))
                {
                    randomNumber = random.Next(Size*Size);
                }

                usedIndexes.Add(randomNumber);

                World[randomNumber].IsMine = true;
                World[randomNumber].SetColor(Color.red);
            }
        }

        private void GenerateEmptyWorld()
        {
            for (var index = 0; index < TotalSize; index++)
            {
                var x = index/Size;
                var z = index%Size;

                var xPos = x*WorldScaleFactor;
                var zPos = z*WorldScaleFactor;

                var space = (GameObject)
                    UnityEngine.Object.Instantiate(GridSpace, new Vector3(xPos, TransformYPosition, zPos), Quaternion.identity);

                space.name = "" + index;

                World[index] = new GridSpace(space, index, Size);
                World[index].SetColor(Color.gray);

                space.transform.parent = WorldObj.transform;
            }
        }

        public bool HasWon()
        {
            return World.All(x => x.HasBeenDug || x.IsMine);
        }

        public GridSpace GetSpaceFromWorldIndex(int y)
        {
            try
            {
                if (y < 0 || y > TotalSize)
                { 
                    return null;
                }
                return World[y];
            }
            catch
            {
                return null;
            }
        }
    }
}
