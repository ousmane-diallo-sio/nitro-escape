using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float healthAmount = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            carController.AddHealth(healthAmount);
            Destroy(gameObject);
        }
    }
}
