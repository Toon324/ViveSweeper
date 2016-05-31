using UnityEngine;
using System.Collections;

public class EndGameHandler : MonoBehaviour {

    [SerializeField]
    private GameObject confettiObj;

    [SerializeField]
    private GameObject winTextObj;

    public void WonGame()
    {
        confettiObj.SetActive(true);
        winTextObj.SetActive(true);
    }

    public void LostGame()
    {

    }


}
