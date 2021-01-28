using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Spawner : MonoBehaviour
{
    [SerializeField] int wave_spawn_time = 5;
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loop = false;

    [SerializeField] int count_enemy =0;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int loop_counter = 0;
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
            loop_counter++;
        }
        while (wave_spawn_time > loop_counter );

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int wavenum = startingWave; wavenum < waveConfigs.Count; wavenum++)
        {
            var currentWave = waveConfigs[wavenum];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
            
        }
    }
    private IEnumerator SpawnAllEnemies(WaveConfig given_wave)
    {
        for (int enemycount = 0; enemycount < given_wave.GetNumberOfEnemies(); enemycount++)
        {
            var newEnemy = Instantiate(given_wave.GetEnemyPrefab(), given_wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(given_wave);
            yield return new WaitForSeconds(given_wave.GetTimeBetweenSpawns());
            
        }

    }
    public void KillEnemies()
    {
        count_enemy--;
    }
}
