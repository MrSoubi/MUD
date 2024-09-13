using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    int baseDamage = 10;
    public Player owner;
    public GameObject damageVFX;


    private void OnTriggerEnter(Collider other)
    {
        DamageCollider victim = other.GetComponent<DamageCollider>();

        if (victim != null)
        {
            victim.TakeDamage(baseDamage * owner.GetSpeed() / 10, owner.car.transform.forward);
            Instantiate(damageVFX, other.ClosestPoint(transform.position), Quaternion.identity);
        }
    }
}
