/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

Shader "OneManEscapePlan/FlashingTexture"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color1("Color", Color) = (1,.2,0,1)
		_Color2("Color2", Color) = (.8,.8,.2,1)
		_Speed("Speed", Range(1, 64)) = 8
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _Strength;
			float _Speed;
			fixed4 _Color1;
			fixed4 _Color2;

			fixed4 frag(v2f i) : SV_Target
			{
				float t = (sin(_Time.y * _Speed) + 1) / 2;

				fixed4 flashingColor = (_Color1 * t) + (_Color2 * (1 - t));
				fixed4 texCol = tex2D(_MainTex, i.uv);
				return flashingColor * texCol;
			}
			ENDCG
		}
	}
}
