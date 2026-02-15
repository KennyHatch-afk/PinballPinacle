using UnityEngine;

public class Stats : MonoBehaviour
{
    public float score;
    public float maxHeight;
    public GameObject ball;
    public bool isTrackingBall;
    public GameObject gameCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHeight = ball.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHeight < ball.transform.position.y)
        {
            score += ball.transform.position.y - maxHeight;

            maxHeight = ball.transform.position.y;
        }

        if (isTrackingBall)
        {
            gameCamera.transform.SetLocalPositionAndRotation(new Vector3(gameCamera.transform.position.x, ball.transform.position.y, gameCamera.transform.position.z), gameCamera.transform.rotation);
        }
    }
}
