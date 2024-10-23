using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrow : MonoBehaviour
{
    public Animator animator;
    private GameObject arrowPrefab;
    public Stack<GameObject> arrows = new Stack<GameObject>();

    void Start()
    {
        StartCoroutine(ThrowArrow());
        arrowPrefab = transform.GetChild(0).gameObject; //Coje de prefab a la flecha que tiene de hijo, simplemente para que spawnee en la posición exacta
    }
    public void spawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        arrow.SetActive(true);
        arrows.Push(arrow); //Esto lo mete en el stack
    }
  

    public IEnumerator ThrowArrow()
    {
       
            animator.Rebind();
            animator.Play("Load");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            if (arrows.Count > 0 && arrows.Peek().GetComponent<Arrow>().died)
            {
                    GameObject arrow = arrows.Pop();
                    arrows.Push(arrow); //Saco  y vuelvo a meter la flecha en el stack para que se quede al fondo de este
                    arrow.transform.position = transform.position;
                    arrow.SetActive(true);
                    arrow.GetComponent<Arrow>().died = false;

            }
            else
                spawnArrow();

            float randomTime = Random.Range(1f, 3.5f);
            yield return new WaitForSeconds(randomTime);
            yield return ThrowArrow();


    }

   
    
}
