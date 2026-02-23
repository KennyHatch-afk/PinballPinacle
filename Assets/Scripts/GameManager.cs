using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject ball;
    public float lastPause;
    public float pauseTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
        pauseTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
        {
            pauseTime -= (Time.unscaledTime - lastPause);
            if(pauseTime < 0)
            {
                lastPause = Time.unscaledTime;
                pauseTime = 2.0f;

                if (ball.activeSelf)
                {
                    ball.SetActive(false);
                }
                else
                {
                    ball.SetActive(true);
                }
            }

            if (Mouse.current.leftButton.isPressed)
            {
                ball.SetActive(true);
                UnPause();
            }
        }

        if (Mouse.current.leftButton.isPressed && isPaused) UnPause();
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
