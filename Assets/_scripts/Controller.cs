using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform _spatula;
    [SerializeField] private GameObject _pancakePrefab;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _dropPosition;

    [SerializeField] private int _minimumPancakeThreshold;
    [SerializeField] private float _heightChangeStep = 1f;
    [SerializeField] public int _pancakeCount = 0;
    [SerializeField] private OOBTrigger _oobTriger;

    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private Canvas _winPopUp;
    [SerializeField] private Canvas _backgroundCanvas;

    private bool _canDrop = true;


    // Start is called before the first frame update
    void Start()
    {
        _oobTriger.PancakeFallen += PancakeFallen;
        SpatulaMove();
    }

    // Update is called once per frame
    void Update()
    {

#if(UNITY_EDITOR) 
        if (_pancakeCount >= 1)
        {
            _backgroundCanvas.enabled = false;
            _winPopUp.enabled = true;
        }
#endif
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                DropPancake();
            }
        } 
       

    }

   
    void UpdateHeight(bool moveUp)
    {
            if (_pancakeCount >= _minimumPancakeThreshold)
            {
                float heightChange = moveUp ? _heightChangeStep : _heightChangeStep * -1;
                _cameraTransform.DOMoveY(_cameraTransform.position.y + heightChange, 0.5f);
                _spatula.DOMoveY(_spatula.position.y + heightChange, 0.5f);
            }
    }
    void DropPancake()
    {
        if (_canDrop == false) return;
        _canDrop = false;
        Vector3 pancakePos = _dropPosition.position;
        GameObject pancake = Instantiate(_pancakePrefab, pancakePos, Quaternion.identity);
        _pancakeCount++;
        _scoreUI.text = _pancakeCount.ToString();       

        _spatula.gameObject.SetActive(false);
        StartCoroutine(TimerForNextPancake(3));
        UpdateHeight(true);
        
    }

    IEnumerator TimerForNextPancake(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spatula.gameObject.SetActive(true);
        _canDrop = true;
       
    }

    void SpatulaMove()
    {
        _spatula.DOMoveX(-5f, 3).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void PancakeFallen()
    {
        UpdateHeight(false);
        _pancakeCount--;
        _scoreUI.text = _pancakeCount.ToString();
        Debug.Log("A pancake has fallen, current pancake count is :" + _pancakeCount);
        
    }
}

