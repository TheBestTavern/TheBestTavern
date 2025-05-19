Shader "Custom/SpriteNightEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Saturation ("Saturation", Range(0, 1)) = 1
        _Lightness ("Lightness", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Saturation;
            float _Lightness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 RGBtoHSL(float3 color)
            {
                float r = color.r, g = color.g, b = color.b;
                float maxc = max(r, max(g, b));
                float minc = min(r, min(g, b));
                float l = (maxc + minc) * 0.5;

                float h = 0;
                float s = 0;

                if (maxc != minc)
                {
                    float d = maxc - minc;
                    s = l > 0.5 ? d / (2.0 - maxc - minc) : d / (maxc + minc);

                    if (maxc == r) h = (g - b) / d + (g < b ? 6.0 : 0.0);
                    else if (maxc == g) h = (b - r) / d + 2.0;
                    else if (maxc == b) h = (r - g) / d + 4.0;
                    h /= 6.0;
                }

                return float3(h, s, l);
            }

            float3 HSLtoRGB(float3 hsl)
            {
                float h = hsl.x, s = hsl.y, l = hsl.z;

                float3 rgb;

                if (s == 0)
                {
                    rgb = float3(l, l, l);
                }
                else
                {
                    float q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                    float p = 2 * l - q;

                    float3 t = float3(h + 1.0/3.0, h, h - 1.0/3.0);

                    for (int i = 0; i < 3; i++)
                    {
                        if (t[i] < 0) t[i] += 1;
                        if (t[i] > 1) t[i] -= 1;

                        if (t[i] < 1.0/6.0)
                            rgb[i] = p + (q - p) * 6.0 * t[i];
                        else if (t[i] < 0.5)
                            rgb[i] = q;
                        else if (t[i] < 2.0/3.0)
                            rgb[i] = p + (q - p) * (2.0/3.0 - t[i]) * 6.0;
                        else
                            rgb[i] = p;
                    }
                }

                return rgb;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 hsl = RGBtoHSL(col.rgb);

                float hue = hsl.x;

                // 파란색 계열 중심 0.6, ±0.15 범위
                float blueRange = 0.15;
                float blueCenter = 0.6;
                float blueDist = abs(hue - blueCenter);

                // 파란색일수록 1.0에 가까움
                float blueFactor = saturate(1.0 - blueDist / blueRange);

                // Saturation 보정: 파란색일수록 덜 줄어듦
                float saturationMultiplier = lerp(_Saturation, 1.0, blueFactor);
                hsl.y *= saturationMultiplier;

                // Lightness 그대로 적용
                hsl.z *= _Lightness;

                col.rgb = HSLtoRGB(hsl);
                return col;
            }
            ENDCG
        }
    }
}
