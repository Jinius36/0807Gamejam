using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class EndigCredit : MonoBehaviour
{
    [SerializeField]GameObject Final;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ending()
    {
        Final.SetActive(true);
        StartCoroutine(ReturnToMain());
    }
    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(3.0f);
        Managers.Scene.LoadScene(Define.Scene.StartScene);
    }
}
