using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI copper;
    public TextMeshProUGUI iron;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI diamond;
    static int copperPoints = 0;
    static int ironPoints = 0;
    static int goldPoints = 0;
    static int diamondPoints = 0;
    void Start()
    {
        UpdatePointsUI();
    }
    private void UpdatePointsUI()
    {
        copper.text = copperPoints.ToString();
        iron.text = ironPoints.ToString();
        gold.text = goldPoints.ToString();
        diamond.text = diamondPoints.ToString();
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
}
