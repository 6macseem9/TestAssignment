using System.Collections;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [Space(10)]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosionParticles;

    protected override void OnHit(Collider targetCollider)
    {
        var targets = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var target in targets)
            DealDamage(target);

        DestroySelf();
    }

    protected override void DestroySelf()
    {
        rb.isKinematic = true;
        bulletCollider.enabled = false;
        model.SetActive(false);

        _explosionParticles.Play();
        StartCoroutine(DelayDestroy(_explosionParticles.main.duration));

        IEnumerator DelayDestroy(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            base.DestroySelf();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}