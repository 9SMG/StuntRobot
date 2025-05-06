using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _speed = 10f;
    int _damage = 1;
    float _knockbackForce = 1f;

    private void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if(damageable != null)
        {
            Debug.Log("OnTriggerEnter2D(): " + collision.name);
            Vector2 knockbackDir = collision.transform.position - transform.position;
            damageable.Damaged(_damage, knockbackDir, _knockbackForce);
        }

        Destroy(gameObject);
    }
}
