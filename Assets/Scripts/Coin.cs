using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int value;

    [SerializeField]
    private GameObject textPrefab;

    private Canvas canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        canvas = GameObject.FindGameObjectWithTag("WorldCanvas").GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1f);

            // Instantiate text as child of canvas
            GameObject textObj = Instantiate(textPrefab, canvas.transform);
            textObj.transform.position = transform.position;
            Stats.bounceScore();

            Destroy(gameObject);
        }
    }

}
