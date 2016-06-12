using UnityEngine;
using System.Collections;

public class EndGameHandler : MonoBehaviour {

    [SerializeField]
    private GameObject confettiObj;

    [SerializeField]
    private GameObject textObj;

    [SerializeField]
    private TextMesh endText1;
    [SerializeField]
    private TextMesh endText2;
    [SerializeField]
    private TextMesh endText3;
    [SerializeField]
    private TextMesh endText4;


    private Color winColor = Color.green;
    private Color loseColor = Color.red;

    private string winText = "YOU WIN!";
    private string loseText = "YOU LOSE!";

    public void WonGame()
    {
        confettiObj.SetActive(true);

        setTextWin(endText1);
        setTextWin(endText2);
        setTextWin(endText3);
        setTextWin(endText4);

        textObj.SetActive(true);
    }

    public void LostGame()
    {

        setTextLose(endText1);
        setTextLose(endText2);
        setTextLose(endText3);
        setTextLose(endText4);

        textObj.SetActive(true);

    }


    private void setTextWin(TextMesh tm)
    {
        tm.text = winText;
        tm.color = winColor;
    }

    private void setTextLose(TextMesh tm)
    {
        tm.text = loseText;
        tm.color = loseColor;
    }

}
