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
                    else h = (r - g) / d + 4.0;
                    h /= 6.0;
                }
                return float3(h, s, l);
            }
            float HueToRGB(float p, float q, float t)
            {
                if (t < 0.0) t += 1.0;
                if (t > 1.0) t -= 1.0;
                if (t < 1.0/6.0) return p + (q - p) * 6.0 * t;
                if (t < 0.5) return q;
                if (t < 2.0/3.0) return p + (q - p) * (2.0/3.0 - t) * 6.0;
                return p;
            }
            float3 HSLtoRGB(float3 hsl)
            {
                float h = hsl.x;
                float s = hsl.y;
                float l = hsl.z;
                float r, g, b;
                if (s == 0.0)
                {
                    r = g = b = l;
                }
                else
                {
                    float q = l < 0.5 ? l * (1.0 + s) : l + s - l * s;
                    float p = 2.0 * l - q;
                    r = HueToRGB(p, q, h + 1.0/3.0);
                    g = HueToRGB(p, q, h);
                    b = HueToRGB(p, q, h - 1.0/3.0);
                }
                return float3(r, g, b);
            }
            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float3 hsl = RGBtoHSL(col.rgb);
                float hue = hsl.x;
                float blueRange = 0.15;
                float blueCenter = 0.6;
                float blueDist = abs(hue - blueCenter);
                float blueFactor = saturate(1.0 - blueDist / blueRange);
                float saturationMultiplier = lerp(_Saturation, 1.0, blueFactor);
                hsl.y *= saturationMultiplier;
                hsl.z *= _Lightness;
                col.rgb = HSLtoRGB(hsl);
                return col;
            }
            ENDCG
        }
    }
}