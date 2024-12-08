using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    // Spawn the enemies in waves
    [SerializeField] private Wave[] waves; // Array of waves
    private int currentWave = -1; // Current wave
    void Start()
    {
        // Deactivate all the enemies
        foreach (Wave wave in waves)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                enemy.SetActive(false);
            }
        }
    }
    public void NextWave() // Go to the next wave
    {
        currentWave += 1;
        waves[currentWave].SpawnEnemies();
    }
    public void NextWave(int wave) // Go to a specific wave
    {
        waves[wave].SpawnEnemies();
    }
    
    [System.Serializable]
    private class Wave // Wave class
    {
        [SerializeField] internal GameObject[] enemies; // Array of enemies
        internal void SpawnEnemies() // Spawn the enemies
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true); // Activate the enemy
            }
        }
    }
}
