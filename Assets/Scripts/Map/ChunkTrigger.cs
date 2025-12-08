using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;            // Pengendali peta dan manajemen chunk
    public GameObject targetMap; // Chunk yang ditandai oleh trigger ini

    void Start()
    {
        mc = FindObjectOfType<MapController>(); // Cari MapController di scene
    }

    // Saat pemain masuk area trigger, set chunk aktif ke target
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    // Saat pemain keluar area, kosongkan chunk jika yang aktif adalah target ini
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}
