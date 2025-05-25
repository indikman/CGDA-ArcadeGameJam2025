using System;
using DG.Tweening;
using UnityEngine;

public class PanCakeVisual : MonoBehaviour
{
    [SerializeField] private Food food;

    [SerializeField] private SpriteRenderer mainVisual;
    [SerializeField] private SpriteRenderer secondaryVisual;
    
    [Header("Sprite Elements")]
    [SerializeField] private Sprite[] mainVisualSpritesCooking;
    [SerializeField] private Sprite[] secondaryVisualSpritesCooking;
    
    [SerializeField] private Sprite[] mainVisualSpritesCooked;
    [SerializeField] private Sprite[] secondaryVisualSpritesCooked;
    
    [SerializeField] private Sprite[] mainVisualSpritesBurning;
    [SerializeField] private Sprite[] secondaryVisualSpritesBurning;
    
    [SerializeField] private Sprite mainVisualSpritesBurned;
    [SerializeField] private Sprite secondaryVisualSpritesBurned;
    

    private float _minCookRatio;
    private float _maxCookRatio;

    private float _yScale;
    
    private void OnEnable()
    {
        food.OnFoodBeingCooked += OnFoodBeingCooked;
        food.OnFoodFlipped += OnFoodFlipped;
    }

    private void OnFoodFlipped()
    {
        // food flip animation
        transform.DOJump(transform.position, 2, 1, 1f);
        transform.DOScaleY(_yScale * -1, 0.5f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
    }

    private void Start()
    {
        _minCookRatio = food.cookThresholdMin / food.timeToBurn;
        _maxCookRatio = food.cookThresholdMax / food.timeToBurn;

        _yScale = transform.localScale.y;
    }

    private void OnDisable()
    {
        food.OnFoodBeingCooked -= OnFoodBeingCooked;
        food.OnFoodFlipped -= OnFoodFlipped;
    }

    private void OnFoodBeingCooked(float foodCookRatio) 
    {
        if(foodCookRatio < _minCookRatio)
        {
            mainVisual.sprite =
                mainVisualSpritesCooking[(int)Math.Floor(mainVisualSpritesCooking.Length * foodCookRatio)];
            secondaryVisual.sprite =
                secondaryVisualSpritesCooking[(int)Math.Floor(secondaryVisualSpritesCooking.Length * foodCookRatio)];
        }else if (foodCookRatio >= _minCookRatio && foodCookRatio <= _maxCookRatio)
        {
            mainVisual.sprite = mainVisualSpritesCooked[(int)Math.Floor(mainVisualSpritesCooked.Length * foodCookRatio)];
            secondaryVisual.sprite = secondaryVisualSpritesCooked[(int)Math.Floor(secondaryVisualSpritesCooked.Length * foodCookRatio)];
        } else if (foodCookRatio > _maxCookRatio && foodCookRatio < 1)
        {
            //burning
            mainVisual.sprite = mainVisualSpritesBurning[(int)Math.Floor(mainVisualSpritesBurning.Length * foodCookRatio)];
            secondaryVisual.sprite = secondaryVisualSpritesBurning[(int)Math.Floor(secondaryVisualSpritesBurning.Length * foodCookRatio)];
        }
        else if (foodCookRatio >= 1)
        {   
            mainVisual.sprite = mainVisualSpritesBurned;
            secondaryVisual.sprite = secondaryVisualSpritesBurned;
        }
    }
}
