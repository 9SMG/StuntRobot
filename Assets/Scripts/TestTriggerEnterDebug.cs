using System.Collections;
using UnityEngine;

public class TestTriggerEnterDebug : MonoBehaviour
{
    Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();

    }

    [ContextMenu("CheckAttackArea()")]
    void CheckAttackArea()
    {
        collider.enabled = true;
        StartCoroutine(CheckAttackAreaCoroutine());
    }

    IEnumerator CheckAttackAreaCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerEnter: {collision.name}");
    }
}
