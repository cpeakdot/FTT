using FTT.Actions;
using FTT.Farm;
using FTT.Managers;
using UnityEngine;

namespace FTT.Tile
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance { get; private set; }

        [SerializeField] private PlantingAction plantingAction;

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private Dirt dirtObject;
        [SerializeField] private Tile[,] tiles;
        [SerializeField] private Tile[,] tempTileArray;

        [SerializeField] private int[] totalDirtPerLevel;
        [SerializeField] private Vector2[] areaPerLevel;
        private int level;

        private int totalDirt = 4;
        
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void InitTiles(int level = 1)
        {
            totalDirt = totalDirtPerLevel[level - 1];
            width = (int)areaPerLevel[level - 1].x;
            height = (int)areaPerLevel[level - 1].y;
            this.level = level;
            CreateTiles();
        }

        /// <summary>
        /// This creates tiles from save at the Start of the game.
        /// </summary>
        /// <param name="onRunTime"> If you create new tiles on runtime, set onRunTime to true for 
        /// prevent plant duplication. </param>
        private void CreateTiles(bool onRunTime = false)
        {
            if(!onRunTime)
            {
                tiles = new Tile[width , height];
            }
            var counter = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    counter++;
                    print("createTiles: " + i + " " + j);
                    if(tiles[i,j] == null)
                    {
                        var dirtObj = Instantiate(dirtObject, new Vector3(i, 0, j), Quaternion.identity);
                        var hasCropOn = PlayerPrefs.GetInt("tile" + i + j, 0) == 1;
                        var newTile = new Tile(dirtObj, true, hasCropOn, i, j);
                        tiles[i, j] = newTile;

                        if(hasCropOn && !onRunTime)
                        {
                            /// Get consumableIndex
                            var crop = PlayerPrefs.GetInt("xPosition" + i + "zPosition" + j , -1);
                            if(crop != -1)
                            {
                                print(crop);
                                var consumable = ConsumableManager.GetConsumableSO(crop).plant;
                                
                                if (consumable != null)
                                {
                                    var timer = newTile.GetTimer();
                                    var watered = timer > 0f;
                                    plantingAction.PlantCrop(newTile , consumable , watered , timer, true);
                                }
                            }
                            else
                            {
                                tiles[i , j].HarvestCrop();
                            }
                        }
                        

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

        private void ReadjustTiles(int count, int newWidth, int newHeight)
        {
            this.totalDirt = count;

            width = newWidth;
            height = newHeight;

            CreateTiles(true);
        }

        /// <summary>
        /// This creates tiles on runtime.
        /// </summary>
        /// <param name="count"> Total tile count. </param>
        /// <param name="newWidth"> Width of the tile. </param>
        /// <param name="newHeight"> Height of the tile. </param>
        public void SetTiles(int count, int newWidth, int newHeight)
        {
            // Clone the tiles array
            tempTileArray = tiles;
            Consumable.Consumable[,] plantArray = new Consumable.Consumable[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    print("width: " + i + " height: " + j);
                    if (tempTileArray[i, j].HasCropOn())
                    {
                        plantArray[i , j] = tempTileArray[i , j].GetCrop();
                    }
                    //Destroy(tiles[i , j].GetDirt().gameObject);
                }
            }

            tiles = new Tile[newWidth, newHeight];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(tempTileArray[i, j] == null)
                    {
                        continue;
                    }

                    tiles[i, j] = tempTileArray[i, j];
                    
                    if(plantArray[i, j] != null)
                    {
                        print(i + " " + j + " has plant");
                        tiles[i , j].SetCrop(plantArray[i,j]);
                    }
                }
            }

            height = newHeight;
            width = newWidth;
            ReadjustTiles(count, newWidth, newHeight);
        }

        public void LevelUpDirtExpansion(int level)
        {
            var totalDirt1 = totalDirtPerLevel[level - 1];
            var width1 = (int)areaPerLevel[level - 1].x;
            var height1 = (int)areaPerLevel[level - 1].y;
            SetTiles(totalDirt1 , width1 , height1);
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

