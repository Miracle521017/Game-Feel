using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner_1 : MonoBehaviour
{
    [Header("Koreographer设置")]
    [EventID] public string eventID;          // 在Inspector面板中选择对应的事件ID

    [Header("Note Prefabs")]
    public List<GameObject> veggiePrefabs; // 仅蔬菜类音符预制体

    [Header("Lanes")]
    public Transform[] lanes; // 四个轨道的位置

    void Start()
    {
        Koreographer.Instance.RegisterForEvents(eventID, SpawnNote);

        void SpawnNote(KoreographyEvent koreoEvent)
        {
            int laneIndex = koreoEvent.GetIntValue() % 4; // 轨道索引
            int prefabIndex = Random.Range(0, veggiePrefabs.Count); // 随机选择蔬菜类音符预制体

            GameObject prefab = veggiePrefabs[prefabIndex];
            Instantiate(prefab, lanes[laneIndex].position, Quaternion.identity, lanes[laneIndex]);
        }

        void OnDestroy()
        {
            if (Koreographer.Instance != null)
            {
                Koreographer.Instance.UnregisterForEvents(eventID, SpawnNote);
            }
        }
    }
}
