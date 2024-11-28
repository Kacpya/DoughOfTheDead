using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab, ghostPrefab;
    public TextMeshProUGUI waveCountText;

    public TextMeshProUGUI pointsText;
    public static int currentWave = 0;
    public static int currentPoints = 0;

    public float enemySpeed = 3f;
    public int enemiesInWave = 5; 

    public static Vector3 screenBottomLeft, screenTopRight;
    public static float screenWidth, screenHeight;

    public static int enemiesSpawned = 0;

    public AudioSource startupMusic;

    public float timeBetweenWaves = 0f; //updated in start()

    private static int enemiesRemaining = 0;
    private bool currentlyInWave = false;

    private List<GameObject> activeEnemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
    
        //set resolution
        Screen.SetResolution(1920, 1080, false);

        if (startupMusic != null) {
            startupMusic.Play();
        }

        screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, -0.1f, 30f));
        screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, 30f));
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.z - screenBottomLeft.z;
        

        StartCoroutine(startNewWave());
        
        timeBetweenWaves = 5f; //add some time between waves
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyInWave && enemiesRemaining <= 0)
        {
            currentWave++;
            //add enemy and make them faster every five waves
            UpdateWaveCounter();
            if (currentWave > 0 && currentWave % 5 == 0) {
                enemiesInWave++;
                SpeedUpEnemies();
            }
            StartCoroutine(startNewWave());
        }
    }

    public void spawnEnemy()
    {

        GameObject go = Instantiate(ghostPrefab) as GameObject;

        float x = 0, y = 0;

        //spawn enemy at random edge
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0: //left
                x = screenBottomLeft.x;
                y = Random.Range(screenBottomLeft.y, screenTopRight.y);
                break;
            case 1: //right
                x = screenTopRight.x;
                y = Random.Range(screenBottomLeft.y, screenTopRight.y);
                break;
            case 2: //bottom
                x = Random.Range(screenBottomLeft.x, screenTopRight.x);
                y = screenBottomLeft.y;
                break;
            case 3: //top
                x = Random.Range(screenBottomLeft.x, screenTopRight.x);
                y = screenTopRight.y;
                break;
        }

        go.transform.position = new Vector3(x, y, screenBottomLeft.z);

        Enemy enemy = go.GetComponent<Enemy>();
        enemy.SetMoveSpeed(enemySpeed);
        activeEnemies.Add(go); //add to list of active enemies
    }

    public IEnumerator startNewWave()
    {
        currentlyInWave = true;

        enemiesRemaining = enemiesInWave;

        yield return new WaitForSeconds(timeBetweenWaves);

        //instantiate some enemies near the edges of the screen
        for (int i = 0; i < enemiesInWave; i++)
        {
            spawnEnemy();
        }

        currentlyInWave = false;  //wave over
    }

    public void UpdateWaveCounter()
    {
        if (waveCountText != null) {
            waveCountText.text = $"Wave: {currentWave}";
        }
    }

    public void UpdatePoints()
    {
        if (pointsText != null)
        {
            pointsText.text = $"Points: {currentPoints}";
        }   
    }

    public static void GhostKilled()
    {
        enemiesRemaining--;
        currentPoints++;
        GameManager instance = FindObjectOfType<GameManager>();
        if (instance != null)
        {
            instance.UpdatePoints();
        }
    }

    private void SpeedUpEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.SetMoveSpeed(enemySpeed + 1);
            }
        }
    }
} 