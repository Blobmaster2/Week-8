using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] float spawnCooldown = .75f;
    float spawnTimer;

    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject fuelCarPrefab;
    [SerializeField] List<Transform> spawnLocations;

    List<GameObject> activeCars = new List<GameObject>();
    [SerializeField] int maxActiveCars;

    bool cullCarsDirtyFlag;

    void Update()
    {
        if (cullCarsDirtyFlag)
        {
            CullCars(activeCars.Count - maxActiveCars);
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnCooldown)
        {
            SpawnCar(Random.Range(0, spawnLocations.Count), Random.Range(0, 4));
            spawnTimer = 0;
        }

        if (activeCars.Count > maxActiveCars)
        {
            cullCarsDirtyFlag = true;
        }
    }

    private void SpawnCar(int location, int fuelCarChance)
    {
        if (fuelCarChance == 0)
        {
            var car = Instantiate(fuelCarPrefab, spawnLocations[location].position, Quaternion.identity);
            activeCars.Add(car);
        }
        else
        {
            var car = Instantiate(carPrefab, spawnLocations[location].position, Quaternion.identity);
            activeCars.Add(car);
        }
    }

    private void CullCars(int carsToCull)
    {
        for (int i = 0; i < carsToCull; i++)
        {
            Destroy(activeCars[i]);
            activeCars.RemoveAt(i);
        }

        cullCarsDirtyFlag = false;
    }
}
