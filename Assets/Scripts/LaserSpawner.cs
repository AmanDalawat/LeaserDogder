using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
     public GameObject laserPrefab;
    public Transform[] spawnPoints;
    public float initialSpawnInterval = 2f;
    public float spawnIntervalReduction = 0.2f;
    public float initialLaserSpeed = 5f;
    public float speedIncrease = 1f;
    public int totalWaves = 5;

    private int currentWave = 0;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave() // Changed from private to public
    {
        if (currentWave < totalWaves)
        {
            currentWave++;
            float spawnInterval = initialSpawnInterval - (currentWave * spawnIntervalReduction);
            float laserSpeed = initialLaserSpeed + (currentWave * speedIncrease);
            InvokeRepeating(nameof(SpawnLaser), spawnInterval, spawnInterval);

            // Update laser speed for all existing lasers
            GameObject[] existingLasers = GameObject.FindGameObjectsWithTag("Laser");
            foreach (GameObject laser in existingLasers)
            {
                laser.GetComponent<LaserMovement>().speed = laserSpeed;
            }
        }
        else
        {
            GameManager.Instance.WinGame();
        }
    }

    private void SpawnLaser()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
            laser.GetComponent<LaserMovement>().speed = initialLaserSpeed + (currentWave * speedIncrease);
        }
    }
}
