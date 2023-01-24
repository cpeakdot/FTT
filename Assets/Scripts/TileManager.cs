using FTT.Farm;
using UnityEngine;

namespace FTT.Tile
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private Dirt dirtObject;
        private Tile[,] tiles;
        
        private void Awake()
        {
            InitTiles();
        }

        private void InitTiles()
        {
            tiles = new Tile[height, width];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var tileObj = Instantiate(dirtObject, new Vector3(i, 0, j), Quaternion.identity);
                    var hasCropOn = PlayerPrefs.GetInt("tile" + i + j, 0) == 1;
                    var newTile = new Tile(tileObj, true, hasCropOn);
                    tiles[i, j] = newTile;
                }
            }
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

