using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    float spawnCooldown = .75f;
    float spawnTimer;

    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject fuelCarPrefab;
    [SerializeField] List<Transform> spawnLocations;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnCooldown)
        {
            SpawnCar(Random.Range(0, spawnLocations.Count), Random.Range(0, 4));
            spawnTimer = 0;
        }
    }

    private void SpawnCar(int location, int fuelCarChance)
    {
        if (fuelCarChance == 0)
        {
            Instantiate(fuelCarPrefab, spawnLocations[location].position, Quaternion.identity);
        }
        else
        {
            Instantiate(carPrefab, spawnLocations[location].position, Quaternion.identity);
        }
    }
}
