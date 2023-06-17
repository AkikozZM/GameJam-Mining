using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class menu : MonoBehaviour
{
    public LeaderBoard leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameStart()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void gameQuit()
    {
        Application.Quit();
    }
    public void gameRank()
    {
        StartCoroutine(loadGlobalLeaderBoard());
    }
    private IEnumerator loadGlobalLeaderBoard()
    {
        yield return leaderboard.FetchTopHighscoreRoutine();
    }
}
