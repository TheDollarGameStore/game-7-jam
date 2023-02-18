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

    private int score;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
