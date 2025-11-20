using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text fuel;

    float fuelTickTimer = 1;
    float fuelTimer = 0;

    private UpdateUICommand prevCommand;

    private void Update()
    {
        if (fuelTimer >= fuelTickTimer)
        {
            fuelTimer = 0;
            GameObject.Find("Player").GetComponent<Player>().CallFuelTick();
        }

        fuelTimer += Time.deltaTime;
    }

    public void FueledCar(UpdateUICommand command)
    {
        prevCommand = command;
    }
}

public class UpdateUICommand : ICommand
{
    float prevFuelValue;

    public void Execute(float currentFuel)
    {
        prevFuelValue = currentFuel;
    }
}

public interface ICommand
{
    void Execute(float value);
}
