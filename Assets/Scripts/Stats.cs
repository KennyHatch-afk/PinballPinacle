using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stats : MonoBehaviour
{
    public float score;
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
    public GameObject gameOverText;
    public GameObject gameOverScore;
    public float invincibilityTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invincibilityTime = 0;
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
                gameOverText.SetActive(false);
                gameOverScore.SetActive(false);
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

        scoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + Mathf.FloorToInt(score);
        lifeCounter.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives;

        //Decrease invincibility timer and set the ball back to active if done
        // if (invincibilityTime > 0)
        // {
        //     invincibilityTime -= Time.deltaTime;

        //     if(invincibilityTime < 0)
        //     {
        //        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //     }
        // }

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
                gameOverText.SetActive(true);
                gameOverScore.SetActive(true);
                gameOverScore.GetComponent<TextMeshProUGUI>().text = "Score: " + Mathf.FloorToInt(score);
            }
            else
            //Respawn
            {
                ball.transform.SetLocalPositionAndRotation(new Vector3(ball.transform.position.x, maxHeight, ball.transform.position.z), ball.transform.rotation);
                // make sure the ball does not keep previous velocity
                ball.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

                //invincibilityTime = 3;      

                GameManager.Pause();
            }

        }

        //Set the score and max height if less than the ball's current position
        if (maxHeight < ball.transform.position.y)
        {
            Debug.Log(score);
            score += ball.transform.position.y - maxHeight;

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
}
