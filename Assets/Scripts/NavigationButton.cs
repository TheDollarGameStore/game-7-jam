using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NavigationButton : MonoBehaviour
{
    [SerializeField] private bool rightClick;

    private Wobble wobbler;

    [SerializeField] private string destination;

    [SerializeField] private AudioClip selectSound;

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
            if (Transitioner.Instance.CanTransition())
            {
                wobbler.DoTheWobble();
                Transitioner.Instance.TransitionToScene(destination);
                SoundManager.instance.PlayRandomized(selectSound);
            }
        }
    }
}
