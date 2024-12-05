using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private int currentWave = -1;
    void Start()
    {
        foreach (Wave wave in waves)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                enemy.SetActive(false);
            }
        }
    }
    public void NextWave()
    {
        currentWave += 1;
        waves[currentWave].SpawnEnemies();
    }
    public void NextWave(int wave)
    {
        waves[wave].SpawnEnemies();
    }
    
    [System.Serializable]
    private class Wave
    {
        [SerializeField] internal GameObject[] enemies;
        internal void SpawnEnemies()
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}
