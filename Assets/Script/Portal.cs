using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destinationPortal;  // Assign the destination portal in Inspector
    public GameObject player;
    [SerializeField] private string portalTag = "Portal";  // Tag for identifying portals

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = destinationPortal.position;
    }
}
