using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Transform _target;
    Rigidbody2D _rigidbody;
    public int _health = 3;
    float _moveSpeed = 5f;

    void Start()
    {
        SelectTarget();
    }

    void SelectTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Die()
    {
        _target = null;
        Destroy(gameObject, 1.0f);
    }

    public void Knockback(Vector2 hitPoint)
    {
    }

    private void FixedUpdate()
    {
        TrackingTarget();
    }

    void TrackingTarget()
    {
        if (_target == null)
        {
            return;
        }

        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(_rigidbody.position + direction * _moveSpeed * Time.fixedDeltaTime);

    }

    public void Damaged(int damage, Vector2 knockbackDir, float knockbackForce)
    {
        if (damage < 0)
            damage = 0;
        _health -= damage;

        if (_health <= 0)
        {
            Die();
            return;
        }

        _rigidbody.AddForce((knockbackDir.normalized) * knockbackForce, ForceMode2D.Impulse);
    }
}
