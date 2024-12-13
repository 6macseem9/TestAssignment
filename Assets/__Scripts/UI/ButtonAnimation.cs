using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PressAnimation);
    }

    private void PressAnimation()
    {
        transform.DOComplete();
        transform.localScale = Vector3.one * 0.5f;
        transform.DOScale(1,0.3f).SetEase(Ease.OutCirc);
    }
}