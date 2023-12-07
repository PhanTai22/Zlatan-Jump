using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;
    public TextMeshProUGUI scoreCountingText;
    public Image powerBarSlider;

    public Dialog achivementDialog;
    public Dialog helpDialog;
    public Dialog gameoverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui)
            gameGui.SetActive(isShow);

        if (homeGui)
            homeGui.SetActive(!isShow);
    }
    public void UpdateScoreCountingText(int score)
    {
        if (scoreCountingText)
            scoreCountingText.text = score.ToString();
    }

    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if (powerBarSlider)
            powerBarSlider.fillAmount = curVal / totalVal;
    }    

    public void ShowAchivementDialog()
    {
        if (achivementDialog)
            achivementDialog.Show(true);
    }
    public void ShowHelpDialog()
    {
        if (helpDialog)
            helpDialog.Show(true);
    }
    public void ShowGameOverDialog()
    {
        if (gameoverDialog)
            gameoverDialog.Show(true);
    }


}
