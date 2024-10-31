using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab, ghostPrefab;
    public static float currentDifficulty = 1;
    public static Vector3 screenBottomLeft, screenTopRight;
    public static float screenWidth, screenHeight;

    public static int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        // find (slightly expanded) screen corners and size, in world coordinates
        // for ViewportToWorldPoint, the z value specified is in world units from the camera
        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, -0.1f, 30f));
        screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, 30f));
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.z - screenBottomLeft.z;
        Debug.Log("BottomLeft: " + screenBottomLeft);
        Debug.Log("TopRight: " + screenTopRight);
        Debug.Log("Width: " + screenWidth);
        Debug.Log("Height: " + screenHeight);
        // instantiate some enemies near the edges of the screen
        for (int i = 0; i < 10; i++)
        {
            spawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesSpawned % 10 == 0) {
            currentDifficulty++; //every 10 enemies spawned increase speed
        }
    }

    public void spawnEnemy()
    {   
        enemiesSpawned++;
        GameObject go = Instantiate(ghostPrefab) as GameObject;
        float x, z;
        if (Random.Range(0f, 1f) < 0.5f)
            x = screenBottomLeft.x + Random.Range(0f, 0.15f) * screenWidth; // near the left edge
        else
            x = screenTopRight.x - Random.Range(0f, 0.15f) * screenWidth; // near the right edge
        if (Random.Range(0f, 1f) < 0.5f)
            z = screenBottomLeft.z + Random.Range(0f, 0.15f) * screenHeight; // near the bottom edge
        else
            z = screenTopRight.z - Random.Range(0f, 0.15f) * screenHeight; // near the top edge
        go.transform.position = new Vector3(x, 0f, z);

        Enemy enemy = go.GetComponent<Enemy>();
        enemy.SetMoveSpeed(currentDifficulty);
    }
}
