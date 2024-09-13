using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Player owner;

    public void TakeDamage(int damage, Vector3 direction)
    {
        owner.TakeDamage(damage);
        owner.car.GetComponent<Rigidbody>().AddForce(Vector3.up * 15000, ForceMode.Impulse);
        owner.car.GetComponent<Rigidbody>().AddForce(damage * direction * 10000, ForceMode.Impulse);
    }
}
