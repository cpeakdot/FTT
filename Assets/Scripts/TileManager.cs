using FTT.Farm;
using UnityEngine;
using System;

namespace FTT.Tile
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private Dirt dirtObject;
        [SerializeField] private Tile[,] tiles;

        private int totalDirt = 4;
        
        private void Awake()
        {
            InitTiles();
        }

        private void InitTiles()
        {
            tiles = new Tile[height, width];
            totalDirt = PlayerPrefs.GetInt("totalDirtCount", 4);
            CreateTiles();
        }

        private void CreateTiles()
        {
            var counter = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    counter++;
                    if(tiles[i,j] == null)
                    {
                        var tileObj = Instantiate(dirtObject, new Vector3(i, 0, j), Quaternion.identity);
                        var hasCropOn = PlayerPrefs.GetInt("tile" + i + j, 0) == 1;
                        var newTile = new Tile(tileObj, true, hasCropOn);
                        tiles[i, j] = newTile;

                        if (counter >= totalDirt)
                        {
                            break;
                        }
                    }
                }
                if (counter >= totalDirt)
                {
                    break;
                }
            }
        }

        private void ReadjustTiles(int count)
        {
            this.totalDirt = count;
            PlayerPrefs.SetInt("totalDirtCount", totalDirt);
            CreateTiles();
        }

        [ContextMenu("Test")]
        public void SetTiles()
        {
            var tempArray = tiles;
            var newHeight = 3;
            var newWidth = 3;
            tiles = new Tile[newHeight, newWidth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(tempArray[i, j] == null)
                    {
                        continue;
                    }

                    tiles[i, j] = tempArray[i, j];
                }
            }
            height = newHeight;
            width = newWidth;
            ReadjustTiles(10);
        }

        public Dirt GetDirt(Vector3 position)
        {
            var x = Mathf.FloorToInt(position.x);
            var y = Mathf.FloorToInt(position.z);
            return tiles[x, y].GetDirt();
        }

        public Tile GetTile(Dirt dirt)
        {
            var position = dirt.transform.position;
            var x = Mathf.FloorToInt(position.x);
            var y = Mathf.FloorToInt(position.z);
            return tiles[x, y];
        }

        public Tile GetTile(Vector3 position)
        {
            var x = Mathf.FloorToInt(position.x);
            var y = Mathf.FloorToInt(position.z);
            return tiles[x, y];
        }
        
    }
}

