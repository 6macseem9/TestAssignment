using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _transformToShake;

    public void TakeDamage()
    {
        _transformToShake.DOComplete();
        _transformToShake.DOShakeRotation(0.3f, 20, 15);
    }
}
