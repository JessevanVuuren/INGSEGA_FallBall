using UnityEngine;

public class FanBoost : MonoBehaviour
{
    [SerializeField] private float boostForce = 10f;  // Strength of the fan boost
    [SerializeField] private Vector2 boostDirection = Vector2.up;  // Direction of the force

    private void OnTriggerEnter2D(Collider2D other)
    {

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply force in the specified direction
            rb.AddForce(boostDirection.normalized * boostForce, ForceMode2D.Impulse);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(boostDirection.normalized * boostForce * Time.deltaTime, ForceMode2D.Force);
        }

    }

    // Visualize the fan force in the Unity Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)boostDirection.normalized * 2);
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 0));
    }
}
