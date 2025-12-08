using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData; // Data senjata jarak jauh (proyektil)
    protected PlayerStats player;             // Referensi pemain untuk kalkulasi damage

    protected Vector3 direction;
    public float destroyAfterSeconds;         // Waktu hidup proyektil sebelum dihancurkan

    // Statistik saat ini (turunan dari weaponData)
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCoolDownDuration;
    protected int currentPierce;

    void Awake(){
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCoolDownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
        // Pastikan proyektil hidup cukup lama untuk bertabrakan; default 5 detik jika belum diisi
        if (destroyAfterSeconds <= 0f)
        {
            destroyAfterSeconds = 5f;
        }
    }

    public float GetCurrentDamage()
    {
        // Damage mengikuti pengganda kekuatan (might) dari pemain
        return currentDamage * player.currentMight;
    }

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        Destroy(gameObject, destroyAfterSeconds); // Hancurkan proyektil setelah durasi
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir; // Simpan arah gerak proyektil

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // kiri
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) // bawah
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) // atas
        {
            scale.x = scale.x * -1;
        }
        else if (dirx > 0 && diry > 0) // kanan atas
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // kanan bawah
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) // kiri atas
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0) // kiri bawah
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);    //Can't simply set the vector because cannot convert
    }

    protected virtual void OnTriggerEnter2D(Collider2D col){
        // Tabrakan dengan musuh: berikan damage lalu kurangi pierce
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
        // Tabrakan dengan prop yang bisa pecah
        else if (col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce(){
        // Kurangi jumlah tembus; jika habis, hancurkan proyektil
        currentPierce --;
        if(currentPierce <= 0){
            Destroy(gameObject);
        }
    }
}
