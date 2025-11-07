using UnityEngine;

public class PickupWood : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: thêm vào inventory
            Debug.Log($"+{amount} wood");
            Destroy(gameObject);
        }
    }
}
