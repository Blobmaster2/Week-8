using UnityEngine;

public class FuelCar : Car
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.CallOnFuel();
        }
    }
}


