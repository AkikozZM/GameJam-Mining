using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderBoard : MonoBehaviour
{
    int leaderboardID = 15214;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;

    void Start()
    {
        
    }
    public IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully upload");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public IEnumerator FetchTopHighscoreRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerName = "Name\n";
                string tempPlayerScore = "Score\n";
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerName += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerName += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerName += members[i].player.id;
                    }
                    tempPlayerScore += members[i].score + "\n";
                    tempPlayerName += "\n";
                }
                done = true;
                playerName.text = tempPlayerName;
                playerScore.text = tempPlayerScore;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

}
