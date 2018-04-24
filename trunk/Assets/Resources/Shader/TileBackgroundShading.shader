// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

///
///  Reference: 	Lake A, Marshall C, Harris M, et al. Stylized rendering techniques for scalable real-time 3D animation[C]
///						Proceedings of the 1st international symposium on Non-photorealistic animation and rendering. ACM, 2000: 13-20.
///
Shader "NPR/Tile Background Shading" {
	Properties {
		_Color ("Diffuse Color", Color) = (1, 1, 1, 1)
		_MainTex ("Paper Texture", 2D) = "white" {}
		_TileScale("TileScale", Int) = 1
		_Thickness("Line Thickness", Range(0, 0.1)) = 0.01
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			Tags { "LightMode"="ForwardBase" }
			
			Cull Back
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma multi_compile_fwdbase
			
			#pragma glsl
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "UnityShaderVariables.cginc"
			
			#define DegreeToRadian 0.0174533
			
			fixed4 _Color;
			sampler2D _MainTex;
			float _TileScale;
			float _Thickness;
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			}; 
			
			struct v2f {
				float4 pos : POSITION;
				float4 uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				SHADOW_COORDS(4)
			};
			
			v2f vert (a2v v) {
				v2f o;
				
				o.pos = UnityObjectToClipPos( v.vertex);
				o.uv.xy =  v.texcoord.xy * _TileScale;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				TRANSFER_SHADOW(o);

				return o;
			}
			
			float4 frag(v2f i) : COLOR {
				fixed3 fragColor;

				if( abs(i.worldPos.x % 1) < _Thickness)  
				{  
					fragColor = fixed3(0,0,0);  
				}  
				else if(abs(i.worldPos.z % 1) < _Thickness)  
				{  
					fragColor = fixed3(0,0,0);  
				}  
				else {
					fragColor = tex2D(_MainTex, i.uv);
				}

				UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
				fragColor = fragColor * _Color.rgb * atten;


				
				return fixed4(fragColor, 1.0);
			}
			
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}
