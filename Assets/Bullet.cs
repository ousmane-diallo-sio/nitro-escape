using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private void OnEnable()
    {
        Invoke("Deactivate", lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void Deactivate()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Deactivate();
    }
}
