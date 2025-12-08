using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Referensi komponen untuk animasi dan arah sprite
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Jika pemain bergerak, aktifkan animasi berjalan dan cek arah sprite
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }

    }
    void SpriteDirectionChecker()
    {
        // Balik sprite jika bergerak ke kiri, normal jika ke kanan
        if (pm.lastHorizontalVector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
