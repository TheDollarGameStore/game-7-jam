using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsDisplay : MonoBehaviour
{
    private Text text;

    [SerializeField] private string prefsName;

    [SerializeField] private string prefix;

    [SerializeField] private string suffix;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = prefix + PlayerPrefs.GetInt(prefsName, 0).ToString() + suffix;
    }
}
