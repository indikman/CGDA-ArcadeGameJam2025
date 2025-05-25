using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float panPositionOffset;
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
        transform.DOMoveX(pan.transform.position.x + panPositionOffset, .5f).SetEase(Ease.OutBack);
    }

}
