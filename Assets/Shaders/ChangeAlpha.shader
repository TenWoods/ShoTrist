Shader "Custom/ChangeAlpha"
{
	Properties{
		_Color("Main Tint", Color) = (1,1,1,1)
		_MainTex("Main Tex", 2D) = "white" {}
	// 用于在透明纹理的基础上控制整体的透明度
	_AlphaScale("Alpha Scale", Range(0,1)) = 1
	}
		SubShader{

		// RenderType标签可以让Unity
		// 把这个Shader归入到提前定义的组(Transparent)
		// 用于指明该Shader是一个使用了透明度混合的Shader

		// IgnoreProjector=True这意味着该Shader
		// 不会受到投影器(Projectors)的影响

		// 为了使用透明度混合的Shader一般都应该在SubShader
		// 中设置这三个标签

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass{

		// 向前渲染路径的方式
		Tags{ "LightMode" = "ForwardBase" }

		Cull Front

		// 深度写入设置为关闭状态
		ZWrite Off

		// 这是混合模式
		Blend SrcAlpha OneMinusSrcAlpha
	}

		Pass{

		// 向前渲染路径的方式
		Tags{ "LightMode" = "ForwardBase" }

		Cull Back

		// 深度写入设置为关闭状态
		ZWrite Off

		// 这是混合模式
		Blend SrcAlpha OneMinusSrcAlpha

	}

	}
	FallBack "Diffuse"
}
