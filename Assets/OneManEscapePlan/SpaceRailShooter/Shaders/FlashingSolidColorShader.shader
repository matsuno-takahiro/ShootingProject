/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

Shader "OneManEscapePlan/FlashingSolidColor"
{
	Properties
	{
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

			float4 vert(float4 vertex : POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			float _Strength;
			float _Speed;
			fixed4 _Color1;
			fixed4 _Color2;

			// simple solid-color shader, no inputs needed
			fixed4 frag() : SV_Target
			{

				float t = (sin(_Time.y * _Speed) + 1) / 2;

				return (_Color1 * t) + (_Color2 * (1 - t));
			}
			ENDCG
		}
	}
}
