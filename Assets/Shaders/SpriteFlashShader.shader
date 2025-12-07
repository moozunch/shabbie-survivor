Shader "Custom/SpriteFlashShader"
{
    Properties
    {
        // Gambar asli sprite (otomatis diisi Unity)
        _MainTex ("Sprite Texture", 2D) = "white" {}
        // Warna dasar (biasanya putih)
        _Color ("Tint", Color) = (1,1,1,1)
        
        // --- PROPERTI CUSTOM UNTUK ANIMASI ---
        // Warna kedap-kedip (Kita set default Merah)
        _FlashColor ("Flash Color", Color) = (1,0,0,1)
        // Seberapa kuat flash-nya (0 = normal, 1 = full merah)
        _FlashAmount ("Flash Amount", Range(0,1)) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _FlashColor;
            float _FlashAmount;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // 1. Ambil warna asli pixel dari gambar sprite
                fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                
                // 2. Campur warna asli dengan warna flash (Merah)
                // Lerp (Linear Interpolation) mencampur warna berdasarkan _FlashAmount
                c.rgb = lerp(c.rgb, _FlashColor.rgb, _FlashAmount);
                
                // 3. Pastikan transparansi (alpha) tetap terjaga, dikali alpha gambar asli
                c.rgb *= c.a;

                return c;
            }
            ENDCG
        }
    }
}