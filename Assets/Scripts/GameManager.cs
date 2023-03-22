using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int gameStartScene;
    public int gameEndScene;
    public int credits;

    // Start is called before the first frame update
    void Start()
    {
        if(Player.instance != null)
        {
            Player.instance.playerHealth.playerDiesEvent.AddListener(GameOver);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void ExitGame()
    {
        Application.Quit(gameEndScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(credits);
    }

    void GameOver()
    {
        Debug.Log("GAME OVER ");
        SceneManager.LoadScene(gameStartScene);
    }
}
