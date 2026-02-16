using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float score;
    public float maxHeight;
    public float previousMaxHeight;
    public float levelSpacing;
    public GameObject ball;
    public bool isTrackingBall;
    public GameObject gameCamera;
    public List<GameObject> levelPrefabs;
    public List<GameObject> levels;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHeight = ball.transform.position.y;
        previousMaxHeight = maxHeight;

        int rand = Random.Range(1, levelPrefabs.Count);
        levels.Add(Instantiate(levelPrefabs[rand], new Vector3(0, ball.transform.position.y + 12, ball.transform.position.z), Quaternion.identity));
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

        if(maxHeight > previousMaxHeight  + levelSpacing)
        {
            previousMaxHeight = maxHeight;

            int rand = Random.Range(0, levelPrefabs.Count);

            levels.Add(Instantiate(levelPrefabs[rand], new Vector3(0, ball.transform.position.y + 12, ball.transform.position.z), Quaternion.identity));
        }
    }
}
