using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Stats : MonoBehaviour
{
    public static int score;
    public float displayedMaxHeight;
    public int lives;
    public bool isDebugModeOn;
    public float maxHeight;
    public float previousMaxHeight;
    public float levelSpacing;
    public GameObject ball;
    public bool isTrackingBall;
    public GameObject gameCamera;
    public List<GameObject> levelPrefabs;
    public List<GameObject> levels;
    public GameObject scoreCounter;
    public GameObject lifeCounter;
    public GameObject maxHeightCounter;
    public GameObject gameOverText;
    public GameObject gameOverScore;
    public GameObject gameOverMaxHeight;
    public float sceneWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = (unitsPerPixel * Screen.height) /2f;
        gameCamera.GetComponent<Camera>().orthographicSize = desiredHalfHeight;

        lives = 3;
        maxHeight = ball.transform.position.y;
        previousMaxHeight = maxHeight;

        //Setup first level
        int rand = Random.Range(1, levelPrefabs.Count);
        levels.Add(Instantiate(levelPrefabs[rand], new Vector3(0, ball.transform.position.y + 12, ball.transform.position.z), Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            //Restart game when mouse is pressed
            {
                ball.SetActive(true);
                scoreCounter.SetActive(true);
                lifeCounter.SetActive(true);
                maxHeightCounter.SetActive(true);
                gameOverText.SetActive(false);
                gameOverScore.SetActive(false);
                gameOverMaxHeight.SetActive(false);
                displayedMaxHeight = 0;
                score = 0;
                
                lives = 3;
                ball.transform.SetLocalPositionAndRotation(new Vector3(-3, 5, 0), ball.transform.rotation);
                maxHeight = ball.transform.position.y;
                previousMaxHeight = maxHeight;

                foreach (GameObject level in levels)
                {
                    Destroy(level);
                }

                int rand = Random.Range(1, levelPrefabs.Count);
                levels.Add(Instantiate(levelPrefabs[rand], new Vector3(0, ball.transform.position.y + 12, ball.transform.position.z), Quaternion.identity));
            }
        }

        scoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        lifeCounter.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives;
        maxHeightCounter.GetComponent<TextMeshProUGUI>().text = "Max Height: " + Mathf.FloorToInt(displayedMaxHeight);

        //If the ball is out of bounds, decrease lives, give it a new position in play, and setup invincibility timer
        if (ball.transform.position.y < maxHeight - 31)
        {
            if (!isDebugModeOn)
            {
                lives--;

            }

            //Game Over
            if (lives <= 0)
            {
                ball.SetActive(false);
                scoreCounter.SetActive(false);
                lifeCounter.SetActive(false);
                maxHeightCounter.SetActive(false);
                gameOverText.SetActive(true);
                gameOverScore.SetActive(true);
                gameOverMaxHeight.SetActive(true);
                gameOverScore.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
                gameOverMaxHeight.GetComponent<TextMeshProUGUI>().text = "Max Height: " + Mathf.FloorToInt(displayedMaxHeight);
            }
            else
            //Respawn
            {
                Vector3 spawnTarget = new Vector3(-3, 3, 0); 
                GameObject[] currentPaddles =  GameObject.FindGameObjectsWithTag("Paddle");

                for (int i = 0; i < currentPaddles.Length; i++)
                {
                    GameObject g = currentPaddles[i];

                    if(g.transform.position.y > spawnTarget.y && g.transform.position.y <= maxHeight)
                    {
                        spawnTarget = g.transform.position;
                    }
                }

                ball.transform.SetLocalPositionAndRotation(new Vector3(spawnTarget.x, spawnTarget.y + 1.5f, spawnTarget.z), ball.transform.rotation);

                // make sure the ball does not keep previous velocity
                ball.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

                GameManager.Pause();
            }

        }

        //Set the score and max height if less than the ball's current position
        if (maxHeight < ball.transform.position.y)
        {
            displayedMaxHeight += ball.transform.position.y - maxHeight;

            maxHeight = ball.transform.position.y;
        }

        //Have the camera track the ball and stop tracking if fallen too far from max height
        if (isTrackingBall && ball.transform.position.y > maxHeight - 25)
        {
            gameCamera.transform.SetLocalPositionAndRotation(new Vector3(gameCamera.transform.position.x, ball.transform.position.y, gameCamera.transform.position.z), gameCamera.transform.rotation);
        }

        //Spawn new levels if close enough
        if (maxHeight > previousMaxHeight + levelSpacing)
        {
            previousMaxHeight = maxHeight;

            int rand = Random.Range(0, levelPrefabs.Count);

            levels.Add(Instantiate(levelPrefabs[rand], new Vector3(0, ball.transform.position.y + 12, ball.transform.position.z), Quaternion.identity));
        }
    }

    public static void bounceScore()
    {
        score += 100;
    }
}
