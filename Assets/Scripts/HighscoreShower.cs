using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreShower : MonoBehaviour
{
    private Text highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        highscoreText = GetComponent<Text>();
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}
