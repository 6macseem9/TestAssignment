using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    protected Rigidbody rb;
    protected Collider bulletCollider;
    protected GameObject model;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();
        model = GetComponentInChildren<MeshRenderer>().gameObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;

        OnHit(collision.collider);
    }

    public void Shoot(Vector3 direction)
    {
        rb.velocity = direction * _speed;
    }

    protected void DealDamage(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy target))
            target.TakeDamage();
    }
    
    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected abstract void OnHit(Collider targetCollider);
}