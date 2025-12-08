using UnityEngine;

// Turunan dari OrbWeaponBehaviour: menangani tabrakan dan efeknya
public class OrbBehaviour : OrbWeaponBehaviour
{
    // Kita override OnTriggerEnter2D untuk logika tabrakan
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            // 1. DAMAGE
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                // Berikan damage memakai kalkulasi dari induk
                enemy.TakeDamage(GetCurrentDamage());
            }

            // 2. KNOCKBACK (Dorong Musuh)
            // Penting agar musuh tidak menempel saat terkena orb
            Rigidbody2D enemyRb = col.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 direction = (col.transform.position - transform.parent.position).normalized;
                enemyRb.AddForce(direction * currentKnockback, ForceMode2D.Impulse);
            }

            // Orb tidak berkurang pierce dan tidak hancur saat tabrakan
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}