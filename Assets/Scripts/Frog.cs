﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        //Transition from Jump to Fall
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Transition from Fall to Idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                //Make sure sprite is facing tight location, and if it not, then face the right direction
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                
                //test to see if I am on the ground. if so jump
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else 
            {
                facingLeft = false;
            }
        }

        else 
        {
            if(transform.position.x < rightCap)
            {
                //Make sure sprite is facing tight location, and if it not, then face the right direction
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                
                //test to see if I am on the ground. if so jump
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else 
            {
                facingLeft = true;
            }
        }
    }

    

    
}
