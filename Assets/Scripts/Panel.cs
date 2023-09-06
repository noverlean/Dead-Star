using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool state;

    [SerializeField] private Animator animator;

    private void Start()
    {
        state = false;
    }

    public void ShowPanel(bool state)
    {
        this.state = state;

        animator.SetBool("State", state);
    }
}
