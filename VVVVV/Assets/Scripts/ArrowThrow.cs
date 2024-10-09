using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrow : MonoBehaviour
{
    public Animator animator;
    private GameObject arrowPrefab;

    void Start()
    {
        StartCoroutine(ThrowArrow());
        arrowPrefab = transform.GetChild(0).gameObject;
    }

    public void spawnArrow()
    {

        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
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

            float randomTime = Random.Range(2f, 3f);
            yield return new WaitForSeconds(randomTime);

        }
    }

   
    
}
