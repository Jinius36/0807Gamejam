using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] public GameObject[] PC;
    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindGameObjectsWithTag("PC");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
