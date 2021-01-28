using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu (menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemy_prefab;
    [SerializeField] GameObject path_prefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float move_speed =2f;

    public GameObject GetEnemyPrefab() { return enemy_prefab;}
    public List<Transform> GetWaypoints(){

        var wave_waypoints = new List<Transform>();
        foreach(Transform child in path_prefab.transform)
        {
            wave_waypoints.Add(child);
        }
        return wave_waypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return move_speed; }
}
