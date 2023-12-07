using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverDialog : Dialog
{
    public TextMeshProUGUI bestscoreText;
    bool m_replayBtnClicked;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnScreenLoad;
    }    
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestscoreText)
            bestscoreText.text = Prefs.bestScore.ToString();
    }

    public void Replay()
    {
        m_replayBtnClicked = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    

    public void BackToHome()
    {
        GameGUIManager.Ins.ShowGameGui(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    

    void OnScreenLoad(Scene scene, LoadSceneMode mode)
    {
        if(m_replayBtnClicked)
        {
            GameGUIManager.Ins.ShowGameGui(true);
            GameManager.Ins.Playgame();
        }

        SceneManager.sceneLoaded -= OnScreenLoad;
    }    


}
