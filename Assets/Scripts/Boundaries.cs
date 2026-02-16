using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Boundaries : MonoBehaviour
{
    public GameObject ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(gameObject.transform.position.x, ball.transform.position.y, gameObject.transform.position.z), transform.rotation);
    }
}
