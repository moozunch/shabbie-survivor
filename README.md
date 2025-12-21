# Shabbie Survivor

## Ringkasan
Shabbie Survivor adalah game aksi 2D dengan loop permainan bertahan hidup (survivor-like). Pemain menjelajah arena, menghindari/menaklukkan musuh, mengumpulkan gem untuk mendapatkan EXP, naik level, lalu memilih upgrade (weapon atau passive item) untuk memperkuat karakter dan bertahan selama mungkin.

- Tujuan: bertahan hidup selama mungkin sambil mengalahkan musuh dan mengumpulkan gem (EXP) untuk memperkuat build.
- Gaya: terinspirasi gameplay wave-based/arena 2D dengan fokus pada pergerakan, positioning, dan pemilihan upgrade.
- Komponen utama: Tilemap 2D, UI (UGUI + TextMesh Pro), Prefab musuh/senjata, dan sistem item pasif.

[VIDEO DEMO]()

## Versi Unity
- Editor: 2023.1.22f1 (lihat [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt))
- Disarankan membuka melalui Unity Hub untuk memastikan versi editor sesuai.

## Dependensi Paket (Packages/manifest.json)
Proyek ini menggunakan paket-paket berikut (utama):
- com.unity.feature.2d: 2.0.0 — paket fitur 2D
- com.unity.textmeshpro: 3.0.6 — teks UI berkualitas
- com.unity.timeline: 1.8.2 — sequencing cutscene/animasi
- com.unity.ugui: 1.0.0 — sistem UI lama (UGUI)
- com.unity.visualscripting: 1.8.0 — visual scripting (opsional)
- com.unity.test-framework: 1.3.9 — unit test PlayMode/EditMode
- Integrasi IDE: Rider (3.0.26), Visual Studio (2.0.22)

Detail lengkap paket dapat dilihat di [Packages/manifest.json](Packages/manifest.json).

## Cara Bermain
- Pergerakan: WASD atau Arrow Keys untuk menggerakkan karakter.
- Tujuan: kumpulkan gem (EXP) dari musuh yang dikalahkan untuk naik level.
- Level-up: pilih upgrade (senjata/skill/item pasif) saat naik level untuk memperkuat damage, area, kecepatan serang, dll.
- Bertahan: hindari serangan musuh, manfaatkan positioning, dan terus mengembangkan build.

## Fitur
- 2D Tilemap: pembuatan arena/world berbasis tile.
- Musuh & gelombang: prefab musuh dengan perilaku dan pola spawn.
- Senjata & item pasif: sistem upgrade yang mempengaruhi damage, area, cooldown, dan statistik lain.
- UI dengan TextMesh Pro: teks tajam dan dapat dikustom.
- Integrasi UGUI: health bar, notifikasi level-up, dan menu.
- Proyek modular: aset tersusun dalam folder Art, Prefabs, Scenes, Scripts, Shaders.

## Prasyarat
- Unity Hub terpasang
- Unity Editor 2023.1.22f1 terinstal
- Git (opsional) untuk clone repositori

## Memulai
1. Clone repositori:
   ```bash
   git clone <URL-repo-anda>
   cd shabbie-survivor
   ```
2. Buka Unity Hub, klik "Open", pilih folder proyek ini.
3. Tunggu proses import dan kompilasi selesai.
4. Buka scene utama dari folder `Assets/Scenes` dan klik Play.

## Struktur Proyek
Ringkasan direktori utama:
- `Assets/` — konten proyek (art, prefabs, scenes, scripts, shaders, dll)
  - `Art/` — aset grafis dan animasi
  - `Materials/` — material seperti `EnemyFlashMat.mat`
  - `Prefabs/` — prefab (Chunks, Enemies, Passive Items, dst.)
  - `Scenes/` — scene permainan
  - `Scripts/` — skrip C# (logic gameplay)
  - `Shaders/` — shader kustom 
  - `TextMesh Pro/` — aset TMP
- `Packages/` — manifest dan lock paket
- `ProjectSettings/` — konfigurasi proyek Unity
- `UserSettings/` — preferensi lokal editor

Catatan: Folder `Library/` dan `Logs/` adalah hasil build/import lokal dan tidak perlu dikomit.

## Menjalankan & Build
- Jalankan: buka scene di `Assets/Scenes` lalu tekan Play.
- Build:
  1. File → Build Settings…
  2. Pilih Platform (mis. PC, Mac & Linux Standalone atau WebGL)
  3. Add Open Scenes (pastikan scene utama ditambahkan)
  4. Player Settings… (opsional: resolusi, ikon, kualitas)
  5. Klik Build (atau Build And Run)

## Pengujian
Proyek menyertakan `com.unity.test-framework`. Untuk menjalankan test:
- Buka Window → General → Test Runner
- Pilih EditMode atau PlayMode
- Jalankan seluruh test atau test tertentu sesuai kebutuhan

## Tips Pengembangan
- Simpan aset baru ke dalam folder yang tepat (Art, Prefabs, Scripts).
- Gunakan TextMesh Pro untuk semua teks UI.
- Gunakan Tilemap untuk level 2D agar mudah diedit.
- Periksa `ProjectSettings/` sebelum mengubah konfigurasi global.

## Masalah Umum
- Versi Unity tidak cocok: pastikan menggunakan 2023.1.22f1.
- Paket tidak tersinkron: buka Package Manager, klik "Refresh" atau pastikan koneksi internet.
- Import lama: tunggu proses reimport selesai; hindari menutup editor secara paksa.

## Kredit
Belajar dan referensi utama: Terresquall — lihat kanal YouTube [https://www.youtube.com/@terresquall](https://www.youtube.com/@terresquall).

## Lisensi
Proyek ini dirilis di bawah lisensi GNU General Public License v3.0 (GPL-3.0). Lihat berkas [LICENSE](LICENSE) untuk teks lisensi lengkap.
