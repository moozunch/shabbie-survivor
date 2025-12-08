using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        // Gerakkan pisau sesuai arah yang ditentukan oleh controller
        transform.position += direction * currentSpeed * Time.deltaTime;   
    }
}
