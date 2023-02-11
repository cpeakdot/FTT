using cpeak.Tools;
using FTT.Consumable;
using System.Collections;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private float minMissionSpawnTime = 120f;
    [SerializeField] private float maxMissionSpawnTime = 600f;
    [SerializeField] private MissionItemUI missionItemPrefab;
    [SerializeField] private Transform missionsTransform;
    [SerializeField] private int startMissionAmount = 9;

    [Header("Canvas")]
    [SerializeField] private GameObject missionsCanvas;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private GameObject ShopCanvas;
    [SerializeField] private GameObject rewardCanvas;

    private void Start()
    {
        StartCoroutine(MissionSpawnRoutine());
    }

    public void Spawn()
    {
        for (int i = 0; i < startMissionAmount; i++)
        {
            var consumableToSpawn = ObjectProbability.GetObjectToSpawn();
            if (consumableToSpawn == null)
            {
                return;
            }
            
            SpawnNewMission(consumableToSpawn);
        }
    }

    IEnumerator MissionSpawnRoutine()
    {
        var spawnTime = Random.Range(minMissionSpawnTime , maxMissionSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        ConsumableSO consumableToSpawn = ObjectProbability.GetObjectToSpawn();
        if(consumableToSpawn != null)
        {
            SpawnNewMission(consumableToSpawn);
        }
        StartCoroutine(MissionSpawnRoutine());
    }
    
    private void SpawnNewMission(ConsumableSO consumable)
    {
        var missionItem = Instantiate(missionItemPrefab , Vector3.zero , Quaternion.identity, missionsTransform);
        if(missionItem.TryGetComponent(out MissionItemUI mItem))
        {
            var spawnAmount = consumable.marketMissionOrderMaxAmount > 2 ? Random.Range(1 , consumable.marketMissionOrderMaxAmount) : 1;
            mItem.SetUI(consumable.icon , spawnAmount + " " + consumable.id , (consumable.sellPrice * spawnAmount).ToString(), spawnAmount , consumable);
        }
    }

    public void OpenMissionCanvas()
    {
        missionsCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        rewardCanvas.SetActive(false);
    }
}
