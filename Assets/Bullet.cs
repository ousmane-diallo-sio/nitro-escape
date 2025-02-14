using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 0.5f;

    private void OnEnable()
    {
        RestartLifetime();
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void Deactivate()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    public void RestartLifetime()
    {
        CancelInvoke();
        Invoke("Deactivate", lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CarController>().RemoveHealth(2f);
            Deactivate();
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().RemoveHealth(50f);
            Deactivate();
            return;
        }

    }
}
