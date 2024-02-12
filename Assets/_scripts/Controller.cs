using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
public class Controller : MonoBehaviour
{
    [SerializeField] private Transform _spatula;
    [SerializeField] private GameObject _pancakePrefab;

    [SerializeField] public int _pancakeCount = 0;
    [SerializeField] private OOBTrigger _oobTriger;


    // Start is called before the first frame update
    void Start()
    {
        _oobTriger.PancakeFallen += PancakeFallen;
        SpatulaMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropPancake();
        }
    }

    void DropPancake()
    {
        Vector3 pancakePos = new Vector3(_spatula.position.x, _spatula.position.y + 0.75f, _spatula.position.z);
        GameObject pancake = Instantiate(_pancakePrefab, pancakePos, Quaternion.identity);
        _pancakeCount++;

        _spatula.gameObject.SetActive(false);
        StartCoroutine(TimerForNextPancake(3));
    }

    IEnumerator TimerForNextPancake(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spatula.gameObject.SetActive(true);
       
    }

    void SpatulaMove()
    {
        _spatula.DOMoveX(-6.5f, 3).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void PancakeFallen()
    {
        _pancakeCount--;
        Debug.Log("A pancake has fallen, current pancake count is :" + _pancakeCount);
    }
}

