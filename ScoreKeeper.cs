using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
    }

    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    public static void ResetScore()
    {
        score = 0;
    }

}
