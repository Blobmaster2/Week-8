using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnFuelEvent();
    public event OnFuelEvent OnFuel;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;

    public float SpinoutTimer;

    public bool isSpinningOut = false;
    public float Fuel { get; private set; }

    public float Speed = -1;

    private Rect cameraRect;

    private void Awake()
    {
        OnFuel += FuelPlayer;

        rb = GetComponent<Rigidbody2D>();

        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    private void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
            Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax));

        if (isSpinningOut)
        {
            if (SpinoutTimer > 3)
            {
                isSpinningOut = false;
            }

            GetComponent<SpriteRenderer>().color = Color.yellow;

            Speed -= Time.deltaTime;

            SpinoutTimer += Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Speed += Time.deltaTime;
        }

        Speed = Mathf.Clamp(Speed, -1, 1);
    }

    private void FuelPlayer()
    {
        Fuel = 100;
    }

    public void CallOnFuel()
    {
        OnFuel.Invoke();
    }

    private void FixedUpdate()
    {
        int inputs = 0;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * moveSpeed);
            inputs++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * moveSpeed);
            inputs++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveSpeed);
            inputs++;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveSpeed);
            inputs++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {

        }
    }
}
