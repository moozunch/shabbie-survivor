using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    
    public static CharacterSelector instance;             // Singleton untuk menyimpan pilihan karakter
    public CharacterScriptableObject characterData;       // Data karakter yang dipilih pemain

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
    }

    public static CharacterScriptableObject GetData()
    {
        return instance.characterData; // Ambil data karakter terpilih
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character; // Simpan pilihan karakter
    }

    public void DestroySingleton()
    {
        instance = null;       // Reset singleton setelah masuk game
        Destroy(gameObject);   // Hapus objek agar tidak menumpuk antar scene
    }
}
