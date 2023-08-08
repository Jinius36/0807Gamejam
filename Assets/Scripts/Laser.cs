using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator Blink()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Blink());
    }
}
