using UnityEngine;

public class NitroController : MonoBehaviour
{
    public float nitroAmount = 50f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            carController.AddNitro(nitroAmount);
            Destroy(gameObject);
        }
    }
}
