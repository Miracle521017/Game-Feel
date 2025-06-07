using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner_1 : MonoBehaviour
{
    [Header("Koreographer设置")]
    [EventID] public string noteTypeEventID; // 音符类型事件ID
    [EventID] public string laneEventID;     // 轨道位置事件ID

    [Header("Note Prefabs")]
    [Tooltip("为每种音符类型分配预制体列表")]
    public List<GameObject> shellFoodPrefabs; // 海鲜类音符预制体
    public List<GameObject> regularVeggiePrefabs; // 普通果蔬类音符预制体
    public List<GameObject> cookwarePrefabs; // 厨具类音符预制体
    public List<GameObject> yamPrefabs; // 山药类音符预制体
    public List<GameObject> cupPrefabs; // 杯子类音符预制体

    [Header("Lanes")]
    public Transform[] lanes; // 轨道的位置

    private int currentLane = -1;
    private float currentNoteType = -1;

    void Start()
    {
        Koreographer.Instance.RegisterForEvents(noteTypeEventID, SetNoteType);
        Koreographer.Instance.RegisterForEvents(laneEventID, SetLane);
    }

    void SetNoteType(KoreographyEvent koreoEvent)
    {
        currentNoteType = koreoEvent.GetFloatValue();
        TrySpawnNote();
    }

    void SetLane(KoreographyEvent koreoEvent)
    {
        currentLane = koreoEvent.GetIntValue() % 4;
        TrySpawnNote();
    }
    void TrySpawnNote()
    {
        {
            if (currentLane != -1 && currentNoteType != -1)
            {
                List<GameObject> prefabsToUse = GetPrefabsByType(currentNoteType);

                if (prefabsToUse != null && prefabsToUse.Count > 0)
                {
                    int prefabIndex = Random.Range(0, prefabsToUse.Count);
                    GameObject prefab = prefabsToUse[prefabIndex];
                    prefab.GetComponent<Note>().track = currentLane;
                    prefab.GetComponent<Note>().SetNoteTypeByFloat(currentNoteType);
                    Instantiate(prefab, lanes[currentLane].position, Quaternion.identity, lanes[currentLane]);
                }
                else
                {
                    Debug.LogWarning("No prefabs available for the specified note type.");
                }

                currentLane = -1;
                currentNoteType = -1;
            }
        }


        List<GameObject> GetPrefabsByType(float noteTypeValue)
        {
            // 根据 noteTypeValue 返回相应的预制体列表
            int noteTypeIndex = Mathf.FloorToInt(noteTypeValue); // 假设 noteTypeValue 是整数，或者可以取整
            switch (noteTypeIndex)
            {
                case 0:
                    return shellFoodPrefabs;
                case 1:
                    return regularVeggiePrefabs;
                case 2:
                    return cookwarePrefabs;
                case 3:
                    return yamPrefabs;
                case 4:
                    return cupPrefabs;
                default:
                    return null;
            }
        }


        void OnDestroy()
        {
            if (Koreographer.Instance != null)
            {
                Koreographer.Instance.UnregisterForEvents(noteTypeEventID, SetNoteType);
                Koreographer.Instance.UnregisterForEvents(laneEventID, SetLane);
            }
        }
    }
}
