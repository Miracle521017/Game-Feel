using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner_1 : MonoBehaviour
{
    // Koreographer设置
    [Header("Koreographer设置")]
    [EventID] public string eventID;          // 在Inspector面板中选择对应的事件ID

    // 音符预制体
    [Header("Note Prefabs")]
    public List<GameObject> veggiePrefabs; // 仅蔬菜类音符预制体

    // 轨道位置
    [Header("Lanes")]
    public Transform[] lanes; // 四个轨道的位置

    // 初始化事件监听
    void Start()
    {
        Koreographer.Instance.RegisterForEvents(eventID, SpawnNote);

        // 生成音符的方法
        void SpawnNote(KoreographyEvent koreoEvent)
        {
            // 计算轨道索引
            int laneIndex = koreoEvent.GetIntValue() % 4; // 轨道索引
            // 随机选择音符预制体
            int prefabIndex = Random.Range(0, veggiePrefabs.Count); // 随机选择蔬菜类音符预制体

            // 生成音符
            GameObject prefab = veggiePrefabs[prefabIndex];
            Note note = Instantiate(prefab, lanes[laneIndex].position, Quaternion.identity, lanes[laneIndex]).GetComponent<Note>();
            // 设置音符的轨道索引和生成时间
            note.track = laneIndex;
            note.spawnTime = Time.time;
        }

        // 销毁时取消事件监听
        void OnDestroy()
        {
            if (Koreographer.Instance != null)
            {
                Koreographer.Instance.UnregisterForEvents(eventID, SpawnNote);
            }
        }
    }
}
