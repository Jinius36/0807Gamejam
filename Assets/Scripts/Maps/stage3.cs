using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage3 : MonoBehaviour
{
    [SerializeField] GameObject[] LaserList;
    [SerializeField] float time;
    // Start is called before the first frame update
    void Start()
    {
        LaserList = GameObject.FindGameObjectsWithTag("Laser");
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch ((int)time % 2)
        {
            case 0:
                for(int i = 0; i < LaserList.Length; i++)
                {
                    LaserList[i].SetActive(true);
                }
                break;
            case 1:
                for (int i = 0; i < LaserList.Length; i++)
                {
                    LaserList[i].SetActive(false);
                }
                break;
        }
    }
}
