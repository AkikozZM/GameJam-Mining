using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public LeaderBoard leaderboard;
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
