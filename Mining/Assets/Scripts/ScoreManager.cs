using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI dirt;
    public TextMeshProUGUI green;
    public TextMeshProUGUI blue;
    static int dirtPoints = 0;
    static int greenPoints = 0;
    static int bluePoints = 0;
    void Start()
    {
        UpdatePointsUI();
    }
    private void UpdatePointsUI()
    {
        dirt.text = dirtPoints.ToString();
        green.text = greenPoints.ToString();
        blue.text = bluePoints.ToString();
    }

    public void addDirtPoints(int points)
    {
        dirtPoints += points;
        UpdatePointsUI();
    }
    public void addGreenPoints(int points)
    {
        greenPoints += points;
        UpdatePointsUI();
    }
    public void addBluePoints(int points)
    {
        bluePoints += points;
        UpdatePointsUI();
    }
}
