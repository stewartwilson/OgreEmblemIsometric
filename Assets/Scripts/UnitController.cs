using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public bool isSelected;
    public UnitGroup unitgroup;
    public Facing facing;
    public bool canMove;
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
