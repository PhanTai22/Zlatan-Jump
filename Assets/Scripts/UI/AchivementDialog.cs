using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchivementDialog : Dialog
{
    public TextMeshProUGUI bestscoreText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestscoreText)
            bestscoreText.text = Prefs.bestScore.ToString();
    }
}
