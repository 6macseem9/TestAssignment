using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _fireCooldownSec = 1;
    [Space(7)]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private ParticleSystem _shootParticles;

    private Animator _animator;

    private bool _canShoot = true;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _shootAction.action.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (!_canShoot) return;

        _animator.Play("Shoot");
        _shootParticles.Play();

        var bullet = Instantiate(_bulletPrefab,_shootPoint.position,Quaternion.LookRotation(_shootPoint.forward),null);
        bullet.Shoot(_shootPoint.forward);

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireCooldownSec);
        _canShoot = true;
    }

    public void SetBullet(Bullet bullet)
    {
        _bulletPrefab = bullet;

        _animator.Play("Reload");
    }

    private void OnDrawGizmosSelected()
    {
        if (_shootPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_shootPoint.position, _shootPoint.forward * 8);
    }
}