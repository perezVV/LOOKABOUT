// Created with help on understanding how to write a shader from Perplexity AI

Shader "Custom/Scanlines" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _NumLines ("Number of Lines", Range(1, 100)) = 5
        _LineThickness ("Line Thickness", Range(0.001, 0.1)) = 0.01
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _NumLines;
            float _LineThickness;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float lineSpacing = 1.0 / _NumLines;
                float lineThickness = _LineThickness;
                float lineY = floor(i.uv.y / lineSpacing) * lineSpacing;
                float lineTop = lineY + lineThickness / 2.0;
                float lineBottom = lineY - lineThickness / 2.0;
                if (i.uv.y > lineTop || i.uv.y < lineBottom) {
                    discard;
                }
                return float4(0, 0, 0, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}