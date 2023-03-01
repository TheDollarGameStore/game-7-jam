using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : MonoBehaviour
{
    [SerializeField] private bool rightClick;

    private Wobble wobbler;

    // Start is called before the first frame update
    void Start()
    {
        wobbler = GetComponent<Wobble>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(rightClick ? 1 : 0))
        {
            wobbler.DoTheWobble();
        }
    }
}
