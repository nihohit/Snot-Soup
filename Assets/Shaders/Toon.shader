Shader "Roystan/Toon"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		_PopArtTex("Pop Art Texture", 2D) = "white" {}
		[Toggle] _PopArtTexInverted("Pop Art Texture Inverted", Float) = 0
		
		_ShadowBands("Shadow Bands", Int) = 3
		_ShadowBandsPower("Shadow Bands Power", Float) = 1.7

		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.85
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1

		[HDR]
		_Specular("Specular", Color) = (0.9, 0.9, 0.9, 1)
		_Glossiness("Glossiness", Float) = 32
	}
	SubShader
	{
		Pass
		{
			name "ToonShading"
			Tags 
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float3 normal : NORMAL;
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 worldNormal : NORMAL;
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				SHADOW_COORDS(3)
			};

			sampler2D _PopArtTex;
			bool _PopArtTexInverted;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			int _ShadowBands;
			float _ShadowBandsPower;

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;

			float4 _Specular;
			float _Glossiness;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float Quantize(float val)
			{
				float edge = 0.01;
				float curvedVal = pow((val + 1) / 2, _ShadowBandsPower) * _ShadowBands;
				float decimal = frac(curvedVal) > 1 - edge ? frac(curvedVal) - 1 : frac(curvedVal);
				if (abs(decimal) < edge)
				{
					float minBound = decimal < 0 ? ceil(curvedVal) / _ShadowBands : (max(ceil(curvedVal) - 1, 1)) / _ShadowBands;
					float maxBound = decimal < 0 ? (ceil(curvedVal) + 1) / _ShadowBands : ceil(curvedVal) / _ShadowBands;
					return min(minBound + smoothstep(-edge, edge, decimal) * (maxBound - minBound), 1);
				}
				return ceil(curvedVal) / _ShadowBands;
			}

			float4 frag (v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float lightIntensity = Quantize(NdotL * SHADOW_ATTENUATION(i));

				float3 viewDir = normalize(i.viewDir);
				float3 h = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = max(dot(normal, h), 0);
				float specularIntensity = smoothstep(0.001, 0.01, pow(NdotH, _Glossiness * _Glossiness));

				float4 rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot * pow(NdotL, _RimThreshold));

				float4 sample = tex2D(_MainTex, i.uv);

				float2 textureCoordinate = i.screenPos.xy / i.screenPos.w;
				float aspect = _ScreenParams.x / _ScreenParams.y;
				textureCoordinate.x = textureCoordinate.x * aspect;
				textureCoordinate = TRANSFORM_TEX(textureCoordinate, _MainTex);
				float textureSample = tex2D(_PopArtTex, textureCoordinate).x;
				float popArtSample = 0;
				popArtSample = _PopArtTexInverted ? textureSample < 0.5 ? 1.2 : 0.8 : max(textureSample, 0.6);

				return popArtSample *_Color * sample* _LightColor0* (lightIntensity + specularIntensity * _Specular + rimIntensity * rimDot * _RimColor);
			}

			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}