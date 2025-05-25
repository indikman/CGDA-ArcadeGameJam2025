using DG.Tweening;
using UnityEngine;

public class PanVisual : MonoBehaviour
{
    [SerializeField] private Pan pan;

    [SerializeField] private Animator panAnimator;
    [SerializeField] private float targetScale;
    
    
    private void OnEnable()
    {
        pan.OnFlipping += OnFlip;
    }

    private void OnFlip(bool obj)
    {
        if(!obj) return;
        
        panAnimator.SetTrigger("Flip");
        transform.DOScale(targetScale, .35f).SetEase(Ease.InOutCubic).SetLoops(2, LoopType.Yoyo);

    }

    private void OnDisable()
    {
        
        pan.OnFlipping -= OnFlip;
    }
}
