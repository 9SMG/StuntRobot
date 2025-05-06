using UnityEngine;

public class GunWeapon : MonoBehaviour, INormalRangedAttack, IRareRangedAttack//IOneShotable
{
    public GameObject _bulletPrefab;
    Transform _firePos;

    private void Awake()
    {
        _firePos = transform.GetChild(0).GetChild(2);
    }

    void FireSingleShot()
    {
        Debug.Log("GunWeapon.FireSingleShot()");
        //Instantiate<GameObject>(_bulletPrefab, transform.position, Quaternion.identity);
    }

    void FireSpreadShot()
    {
        Debug.Log("GunWeapon.FireSpreadShot()");
    }

    public void PowerRangedAttack()
    {
        FireSpreadShot();
    }

    public void RangedAttack()
    {
        FireSingleShot();
    }
}
