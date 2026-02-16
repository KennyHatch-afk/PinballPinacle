using System;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    [SerializeField]
    float repelStrength;

    [SerializeField]
    float scaleSpeed;

    bool scalePad = false;    
    bool scaleMet = false;

    Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 desiredScale;
    Vector3 startScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
        desiredScale = startScale * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (scalePad)
        {
            if (scaleMet)
            {
                transform.localScale -= scaleChange * (Time.deltaTime * scaleSpeed);

                if (transform.localScale.magnitude <= startScale.magnitude)
                {
                    scalePad = false;
                    scaleMet = false;
                }
                return;
            }

            transform.localScale += scaleChange * (Time.deltaTime * scaleSpeed);

            if (transform.localScale.magnitude >= desiredScale.magnitude)
            {
                scaleMet = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        scalePad = true;

        Vector2 dir = collision.rigidbody.linearVelocity;
        dir.Normalize();

        // flip the direction (only in Y)
        dir.y *= -1;

        // apply force to other collider
        collision.rigidbody.AddForce(dir * repelStrength);
        Debug.Log("Applied Force " + dir);
    }

}
