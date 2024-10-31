using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject cookiePrefab;
    public Transform throwPoint;
    public float throwForce = 20f;
    
    public void Fire() {
        GameObject bullet = Instantiate(cookiePrefab, throwPoint.position, throwPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(throwPoint.up * throwForce, ForceMode2D.Impulse);
    }
}
