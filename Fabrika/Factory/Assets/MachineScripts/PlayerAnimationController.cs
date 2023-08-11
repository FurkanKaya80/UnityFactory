using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    bool wTusunaBasildi = false;
    bool spaceTusunaBasildi = false;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            wTusunaBasildi = true;

        }
        else if (Input.GetKey("space"))
        {
            spaceTusunaBasildi = true;

        }
    


    }
    private void FixedUpdate()
    {
        if (wTusunaBasildi == true)
        {

            animator.SetBool("isRunning", true);
            wTusunaBasildi = false;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        
        
        if (spaceTusunaBasildi == true)
        {
            animator.SetBool("isJumping", true);
            spaceTusunaBasildi = false;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
}
