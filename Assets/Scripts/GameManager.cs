using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject ball;
    public float lastPause;
    public int blinkNum;
    public float pauseTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
        pauseTime = 0;
        blinkNum = 0;
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            pauseTime -= Time.unscaledTime - lastPause;
            if(pauseTime < 0)
            {
                lastPause = Time.unscaledTime;
                pauseTime = 5.0f;
                blinkNum++;

                if (ball.activeSelf)
                {
                    ball.SetActive(false);
                }
                else
                {
                    ball.SetActive(true);
                }
            }

            if (blinkNum > 10)
            {
                blinkNum = 0;
                ball.SetActive(true);
                UnPause();
            }
        }
    }

    public static void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public static void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

}
