using UnityEngine;

public class FloatingText : MonoBehaviour
{

    private float moveSpeed = 1f;
    private float destroyTime = 0.75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
