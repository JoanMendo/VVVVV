using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrow : MonoBehaviour
{
    public Animator animator;
    public GameObject arrowPrefab;

    void Start()
    {
        StartCoroutine(ThrowArrow());
    }

    public void spawnArrow()
    {

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().enabled = true;
    }
  

    public IEnumerator ThrowArrow()
    {
        while (true)
        {
            animator.Rebind();
            animator.Play("Load");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            spawnArrow();

            // Espera el tiempo especificado antes de reproducir de nuevo
            yield return new WaitForSeconds(6f);

        }
    }

   
    
}
