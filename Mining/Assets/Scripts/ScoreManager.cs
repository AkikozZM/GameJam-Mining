using System.Collections;
using TMPro;
using UnityEngine;
using LootLocker.Requests;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI copper;
    public TextMeshProUGUI iron;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI diamond;
    public TextMeshProUGUI currentAxe;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI displayScore;
    public TextMeshProUGUI inputName;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI dur;

    static int copperPoints = 0;
    static int ironPoints = 0;
    static int goldPoints = 0;
    static int diamondPoints = 0;
    static string currentAxeName = "Starter";
    private int scorePoints = 0;
    public int copperValue;
    public int ironValue;
    public int goldValue;
    public int diamondValue;
    public GameObject gameover_UI;
    public GameObject leaderboard_UI;
    public LeaderBoard leaderboard_obj;

    void Start()
    {
        resetAllScores();
        UpdatePointsUI();
    }
    private void Update()
    {
        updateScore();
    }
    public void setPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputName.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Cannot set player name" + response.Error);
            }
        });
    }
    private void resetAllScores()
    {
        copperPoints = 0;
        ironPoints = 0;
        goldPoints = 0;
        diamondPoints = 0;
    }
    private void updateScore()
    {
        scorePoints = 
            copperPoints * copperValue + 
            ironPoints * ironValue + 
            goldPoints * goldValue + 
            diamondPoints * diamondValue;
        gameOverScore.text = scorePoints.ToString();
        displayScore.text = scorePoints.ToString();
    }
    private void UpdatePointsUI()
    {
        copper.text = copperPoints.ToString();
        iron.text = ironPoints.ToString();
        gold.text = goldPoints.ToString();
        diamond.text = diamondPoints.ToString();
        currentAxe.text = getCurrentAxe();
    }

    public void displayGameOver()
    {
        gameover_UI.SetActive(true);
        //submit player score
        StartCoroutine(submitScore());
        
    }
    private IEnumerator submitScore()
    {
        yield return leaderboard_obj.SubmitScoreRoutine(scorePoints);
    }
    public void setCurrentAxeName(string name)
    {
        currentAxeName = name;
        UpdatePointsUI();
    }
    public string getCurrentAxe()
    {
        return currentAxeName;
    }
    public int getCopperPoints()
    {
        return copperPoints;
    }
    public int getIronPoints()
    {
        return ironPoints;
    }
    public int getGoldPoints()
    {
        return goldPoints;
    }
    public int getDiamondPoints()
    {
        return diamondPoints;
    }

    public void addCopperPoints(int points)
    {
        copperPoints += points;
        UpdatePointsUI();
    }
    public void subtractCopperPoints(int points)
    {
        copperPoints -= points;
        UpdatePointsUI();
    }
    public void addIronPoints(int points)
    {
        ironPoints += points;
        UpdatePointsUI();
    }
    public void subtractIronPoints(int points)
    {
        ironPoints -= points;
        UpdatePointsUI();
    }
    public void addGoldPoints(int points)
    {
        goldPoints += points;
        UpdatePointsUI();
    }
    public void subtractGoldPoints(int points)
    {
        goldPoints -= points;
        UpdatePointsUI();
    }
    public void addDiamondPoints(int points)
    {
        diamondPoints += points;
        UpdatePointsUI();
    }
    public void subtractDiamondPoints(int points)
    {
        diamondPoints -= points;
        UpdatePointsUI();
    }

    public void displayLeaderBoard()
    {
        gameover_UI.SetActive(false);
        leaderboard_UI.SetActive(true);
        StartCoroutine(loadGlobalLeaderBoard());
    }
    private IEnumerator loadGlobalLeaderBoard()
    {
        yield return leaderboard_obj.FetchTopHighscoreRoutine();
    }
    public void updateAtkAndDur(int atk, int dur)
    {
        this.atk.text = atk.ToString();
        this.dur.text = dur.ToString();
    }
}
