using UnityEngine;
public interface IDamageable
{
    void Damaged(int damage, Vector2 knockbackDir, float knockbackForce);
}
