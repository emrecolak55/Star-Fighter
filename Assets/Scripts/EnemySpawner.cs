using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loop = false;
 
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
       while (loop) ;
 
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int wavenum=startingWave; wavenum < waveConfigs.Count; wavenum++)
        {
            var currentWave = waveConfigs[wavenum];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemies(WaveConfig given_wave)
    {
        for(int enemycount =0; enemycount < given_wave.GetNumberOfEnemies(); enemycount++)
        {
         var newEnemy =   Instantiate(given_wave.GetEnemyPrefab(), given_wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(given_wave);
            yield return new WaitForSeconds(given_wave.GetTimeBetweenSpawns());
        }
        
    }
}
