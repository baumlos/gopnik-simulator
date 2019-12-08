Shader "Custom/FreezeEffect"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_Vingiette("Vingiette", 2D) = "white" {}
		_Freeze("Freeze", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Cutoff("Cutoff", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _Cutoff;

			sampler2D _Vingiette;
			sampler2D _Freeze;

			float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				if (tex2D(_Vingiette, i.uv).r * tex2D(_Freeze, i.uv).r < _Cutoff)
					return _Color;
				return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
