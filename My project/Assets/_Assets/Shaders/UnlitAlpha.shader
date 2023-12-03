Shader "Game/UnlitAlpha" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Color ("Base Color", Color) = (1,1,1,1)
}

CGINCLUDE
    #include "UnityCG.cginc"

    struct v2fShadow {
        V2F_SHADOW_CASTER;
        UNITY_VERTEX_OUTPUT_STEREO
    };
 
    v2fShadow vertShadow( appdata_base v )
    {
        v2fShadow o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
        return o;
    }
 
    float4 fragShadow( v2fShadow i ) : SV_Target
    {
        SHADOW_CASTER_FRAGMENT(i)
    }

    ENDCG
SubShader {
    Tags { "RenderType"="Opaque" "Queue"="Transparent" }
    LOD 100
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                col *= _Color;
                UNITY_APPLY_FOG(i.fogCoord, col);
                // UNITY_OPAQUE_ALPHA(col.a);
                return col;
            }
        ENDCG
    }
     Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
            LOD 80
            Cull [_Culling]
            Offset [_Offset], [_Offset]
            ZWrite [_ZWrite]
            ZTest [_ZTest]
           
            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #pragma target 2.0
            #pragma multi_compile_shadowcaster
            ENDCG
        }
}
}