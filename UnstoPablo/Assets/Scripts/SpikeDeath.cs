using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDeath : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
         StartCoroutine(WaitAndPrint());
    }

     private IEnumerator WaitAndPrint()
    {
            yield return new WaitForSeconds(10);
            Destroy(gameObject);

    }
}
