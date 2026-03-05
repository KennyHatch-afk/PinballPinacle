using UnityEngine;

public class Tunnel : MonoBehaviour
{

    [SerializeField]
    private Tunnel exitPoint;

    [SerializeField]
    private float exitForce;

    private Rigidbody2D storedBall;

    private bool isActive;
    private bool isHoldingBall;

    public float teleportTime;
    private float waitTime;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitTime = teleportTime;
        isActive = true;
        isHoldingBall = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHoldingBall)
        {
            timer += Time.deltaTime;

            if (timer >= teleportTime)
            {
                timer = 0f;
                isHoldingBall = false;
                Teleport(storedBall);
            }
        }
    }

    void Teleport(Rigidbody2D rb)
    {
        // disable exit temporarily so infinite teleport doesnt occur
        exitPoint.isActive = false;

        // move ball
        rb.position = exitPoint.transform.position;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(exitPoint.transform.up * exitForce);
        rb.transform.localScale = Vector2.one;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        if (collision.CompareTag("Player"))
        {
            storedBall = collision.GetComponent<Rigidbody2D>();
            storedBall.transform.localScale = Vector2.zero;

            isHoldingBall = true;

            Invoke(nameof(Reenable), waitTime);
        }
    }

    void Reenable()
    {
        exitPoint.isActive = true;
    }

}
