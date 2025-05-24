using System;
using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    private PanManager _panManager;

    private void Awake()
    {
        _panManager = ServiceLocator.instance.GetService<PanManager>();
    }

    private void OnEnable()
    {
        _panManager.OnPanSelected += OnPanSelected;
    }

    private void OnPanSelected(Pan pan)
    {
        transform.DOMoveX(pan.transform.position.x, .5f).SetEase(Ease.OutBack);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
