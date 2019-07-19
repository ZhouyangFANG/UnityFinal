Shader "UnityChan/Eye"
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_ShadowColor ("Shadow Color", Color) = (0.8, 0.8, 1, 1)
		_CutOff ("Cut Off", Int) = 0
		_MainTex ("Diffuse", 2D) = "white" {}
		_FalloffSampler ("Falloff Control", 2D) = "white" {}
		_RimLightSampler ("RimLight Control", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
			"Queue"="Geometry"
			"LightMode"="ForwardBase"
		}

		Pass   
		{ 
			AlphaTest Greater [_CutOff]
			ZWrite On
			ColorMask 0
				SetTexture [_MainTex] 
				{
					ConstantColor [_Color]
					Combine Texture * constant
				}
		}


		Pass
		{
			Cull Back
		    ZWrite Off
    		Blend SrcAlpha OneMinusSrcAlpha
			ZTest LEqual
	
        		
CGPROGRAM
#pragma multi_compile_fwdbase
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "CharaSkin.cg"
ENDCG
		}

	}

	FallBack "Transparent/Cutout/Diffuse"
}
