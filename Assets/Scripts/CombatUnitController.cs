using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public Facing facing;
    protected Animator animator;

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        animator.SetInteger("Facing", (int)facing);
    }
}
