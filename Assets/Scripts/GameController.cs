using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private float _timeToSpawnEnemy = 10f;
    private float _currentTimeToSpawnEnemy = 0f;
    public GameObject enemy = null;
    public EnemyPoolController enemyPoolController = null;
    public Image arrowKick = null;
    public Image arrowMove = null;
    public TMP_Text textEnemy = null;
    public TMP_Text textMove = null;
    public TMP_Text textKick = null;
    public TMP_Text textGameover = null;
    // Start is called before the first frame update
    void Start()
    {
        textGameover.enabled = false;
        Debug.Log("To disable first time start happening every time you open a game, remove next code string from GameController.");
        PlayerPrefs.DeleteAll();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            Tutorial();
        }
        _currentTimeToSpawnEnemy -= Time.deltaTime;
        if (_currentTimeToSpawnEnemy <= 0) {
            if (enemyPoolController.IsEmpty() != true) {
                EnemyController enemyController = enemyPoolController.Get();
                enemyController.Init();
            }
            else
            {
                GameObject.Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-19f, 19f), 0, UnityEngine.Random.Range(-4f, 4f)), new Quaternion(0, 0, 0, 0));
            }
            _currentTimeToSpawnEnemy = _timeToSpawnEnemy;
        }
    }

    public void Tutorial() {
        StartCoroutine(DoneTutorial());
        //Time.timeScale = 0;
        arrowKick.enabled = true;
        arrowMove.enabled = true;
        textEnemy.enabled = true;
        textKick.enabled = true;
        textMove.enabled = true;
    }

    public IEnumerator DoneTutorial()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1;
        arrowKick.enabled = false;
        arrowMove.enabled = false;
        textEnemy.enabled = false;
        textKick.enabled = false;
        textMove.enabled = false;
    }

    internal void Gameover()
    {
        textGameover.enabled = true;
        Time.timeScale = 0;
    }
}
