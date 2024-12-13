using UnityEngine;

public class NormalBullet : Bullet
{
    protected override void OnHit(Collider targetCollider)
    {
        DealDamage(targetCollider);

        DestroySelf();
    }

}