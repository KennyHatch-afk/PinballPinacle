using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed && isPaused) UnPause();
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
