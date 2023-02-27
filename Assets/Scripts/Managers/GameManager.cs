using FTT.Tile;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int gold;

    public delegate void GoldAdjusted(int gold);
    public GoldAdjusted OnGoldAdjusted;

    public TileManager tileManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("There is more than one GameManager in the scene!");
        }
        gold = PlayerPrefs.GetInt("gold" , 0);
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        OnGoldAdjusted?.Invoke(gold);
    }

    public bool TrySpendGold(int amount)
    {
        if(amount <= gold)
        {
            gold -= amount;
            PlayerPrefs.SetInt("gold" , gold);
            OnGoldAdjusted?.Invoke(gold);
            return true;
        }
        return false;
    }

    public void AdjustGold(int amount)
    {
        if (amount < 0)
            return;
        gold += amount;
        PlayerPrefs.SetInt("gold" , gold);
        OnGoldAdjusted?.Invoke(gold);
    }

    [ContextMenu("Test")]
    public void Testing()
    {
        tileManager.SetTiles(9 , 3 , 3);
    }
}
