using UnityEngine;

public class Car : MonoBehaviour
{
    protected Player player;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetDistanceToPlayer() < 5)
        {
            if (Random.Range(0, 2) == 0)
            {
                rb.AddForce(Vector2.left * 3);
            }
            else
            {
                rb.AddForce(Vector2.right * 3);
            }
        }

        transform.position += Vector3.down * player.Speed / 100;
    }

    float GetDistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            player.isSpinningOut = true;
            player.SpinoutTimer = 0;
        }
    }
}
