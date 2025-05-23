Shader "Unlit/RGB To Linear"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            half3 RGBToLinear(half3 col)
            {
                col.r = GammaToLinearSpaceExact(col.r);
                col.g = GammaToLinearSpaceExact(col.g);
                col.b = GammaToLinearSpaceExact(col.b);
                return col;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = RGBToLinear(col.rgb);
                return col;
            }
            ENDCG
        }
    }
}