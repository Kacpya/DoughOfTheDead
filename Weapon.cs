using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public GameObject cookiePrefab;
    public Transform throwPoint;
    public TextMeshProUGUI CookiesRemainingText;
    public float throwForce = 20f;

    public int numCookies = 5;
    public int maxCookies = 20;
    
    void Start() {
        CookiesRemainingText.text = $"Cookies: {numCookies}";
    }

    public void Fire() {
        if (numCookies > 0) {
            GameObject bullet = Instantiate(cookiePrefab, throwPoint.position, throwPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(throwPoint.up * throwForce, ForceMode2D.Impulse);
            numCookies--;
            UpdateCookies();
        }
        else if(numCookies == 0) {
            CookiesRemainingText.text = $"NO MORE COOKIES. GO TO THE KITCHEN!!";
        }
        else {
            CookiesRemainingText.text = $"Cookies: {numCookies} (YOUR POCKETS ARE FULL!)";
        }
    }

    public void incrementCookies() {
        if (numCookies + 5 < maxCookies) {
            numCookies = numCookies + 5;
        }
        else {
            numCookies = 20;
        }
        UpdateCookies();
    }

    public void UpdateCookies()
    {
        if (CookiesRemainingText != null)
        {
            CookiesRemainingText.text = $"Cookies: {numCookies}";
        }   
    }
}
