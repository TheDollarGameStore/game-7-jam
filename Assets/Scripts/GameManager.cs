using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public CameraBehaviour cameraBehaviour;

    public GameObject playerObject;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text multiplierText;

    private int score;

    private int multiplier;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1;
        score = 0;
    }

    // Update is called once per frame
    public void AddScore(int value)
    {
        score += value * multiplier;
        scoreText.text = score.ToString();
    }

    public void AddMultiplier(int value)
    {
        multiplier += value;
        multiplierText.text = "X" + multiplier.ToString();
    }
}
