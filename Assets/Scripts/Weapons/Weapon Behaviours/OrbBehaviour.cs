using UnityEngine;

// INHERIT DARI SCRIPT DI ATAS (OrbWeaponBehaviour)
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
                // Panggil fungsi damage dari script Base (Induk)
                enemy.TakeDamage(GetCurrentDamage());
            }

            // 2. KNOCKBACK (Dorong Musuh)
            // Ini penting buat senjata tipe orbit biar musuh gak nempel terus
            Rigidbody2D enemyRb = col.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 direction = (col.transform.position - transform.parent.position).normalized;
                enemyRb.AddForce(direction * currentKnockback, ForceMode2D.Impulse);
            }

            // PENTING: Kita TIDAK panggil ReducePierce() atau Destroy()
            // Karena Orb sifatnya menembus semua musuh tanpa hilang.
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