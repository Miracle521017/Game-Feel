using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Koreography")]
    public string eventID;

    [Header("Note Prefabs")]
    public GameObject shellFoodPrefab;
    public GameObject veggiePrefab;
    public GameObject cookwarePrefab;
    public GameObject yamPrefab;

    [Header("Lanes")]
    public Transform[] playerALanes; // 玩家A的轨道Transform数组
    public Transform[] playerBLanes; // 玩家B的轨道Transform数组

    void Start()
    {
        Koreographer.Instance.RegisterForEvents(eventID, SpawnNote);

        void SpawnNote(KoreographyEvent koreoEvent)
        {
            int noteTypeValue = koreoEvent.GetIntValue();
            NoteType noteType = (NoteType)noteTypeValue;

            int laneIndex = koreoEvent.GetIntValue() % 4;
            bool isPlayerA = laneIndex < 2;

            Transform spawnParent = isPlayerA ?
                playerALanes[laneIndex % 2] :
                playerBLanes[laneIndex % 2];

            GameObject prefab = GetPrefabByType(noteType);
            Note note = Instantiate(prefab, spawnParent.position, Quaternion.identity, spawnParent).GetComponent<Note>();

            // 设置音符的移动方向和轨道索引

        }

        GameObject GetPrefabByType(NoteType type)
        {
            switch (type)
            {
                case NoteType.ShellFood: return shellFoodPrefab;
                case NoteType.RegularVeggie: return veggiePrefab;
                case NoteType.Cookware: return cookwarePrefab;
                case NoteType.Yam: return yamPrefab;
                default: return veggiePrefab;
            }
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
