using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrow : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        StartCoroutine(ThrowArrow());
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ThrowArrow()
    {
        while (true)
        {
            animator.Play("ArrowThrow");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        }
    }

   
    
}
