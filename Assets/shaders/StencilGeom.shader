//credit:  Daniel Ilett (https://www.youtube.com/@danielilett) (https://youtu.be/EzM8LGzMjmc?si=R59sjHm8UXifOvIJ)

Shader "Examples/Stencil"
{
	Properties

	{
		[IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0

	}
	SubShader

	{
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
			"RenderPipeline" = "UniversalPipeline"
		}

		Pass

		{
			Blend Zero One
			ZWrite Off

			Stencil
			{
				Ref[_StencilID]
				Comp Always
				Pass Replace
				Fail Keep
			}
		}
	}
}