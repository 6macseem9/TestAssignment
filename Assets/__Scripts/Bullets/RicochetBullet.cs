using System.Linq;
using UnityEngine;

public class RicochetBullet : Bullet
{
    [Space(10)]
    [SerializeField] private float _ricochetRange;
    [SerializeField] private LayerMask _enemyLayerMask;

    protected override void OnHit(Collider targetCollider)
    {
        DealDamage(targetCollider);
        Physics.IgnoreCollision(bulletCollider, targetCollider);

        var neighbors = Physics.OverlapSphere(transform.position, _ricochetRange, _enemyLayerMask);
        if (neighbors.Length == 0)
        {
            DestroySelf();
            return;
        }

        var target = neighbors.FirstOrDefault(x => !Physics.GetIgnoreCollision(bulletCollider, x));
        if (target == null)
        {
            DestroySelf();
            return;
        }

        var targetDirection = (target.transform.position - transform.position).normalized;
        targetDirection.y = 0;

        Shoot(targetDirection);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _ricochetRange);
    }
}