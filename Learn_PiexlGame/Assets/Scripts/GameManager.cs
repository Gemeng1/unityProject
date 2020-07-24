using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Text time;
    public GameObject gameOverUI;
    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        time.text = Time.timeSinceLevelLoad.ToString("00");
    }

    public static void GameOver(bool isdead)
    {
        if (isdead)
        {
            _instance.gameOverUI.gameObject.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
