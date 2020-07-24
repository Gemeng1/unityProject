// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/DefaultColorFlash"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        //-------------------Add---------------------------------
        _FlashColor("Color",Color) = (1,1,1,1)
        _FlashAmount("Flash Amount",Range(0.0,1.0)) = 1.0
        //-------------------Add---------------------------------

    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha


        Pass
        {
        CGPROGRAM
        // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members pos)
        #pragma exclude_renderers d3d11
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            //#include "UnitySprites.cginc"
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 position:POSITION;
                float4 color:COLOR;
                float2 texcoord:TEXCOORD0;
                
            };
            struct v2f
            {
                float4 pos:SV_POSITION;
                fixed4 color:COLOR;
                fixed2 texcoord:TEXCOORD0; 
                
            };
            fixed4 _Color;
            fixed4 _FlashColor;
            float  _FlashAmount;
            sampler2D  _MainTex;
            v2f SpriteVert(appdata_t v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.position);
                o.color = v.color*_Color;
                o.texcoord = v.texcoord;
                #if PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif

                return  o;
            }

            fixed4 SpriteFrag(v2f i):SV_TARGET{
                fixed4 c = tex2D(_MainTex,i.texcoord)*i.color;
                c.rgb = lerp(c.rgb,_FlashColor.rgb,_FlashAmount);
                c.rgb *= c.a;
                return c;
            }

        ENDCG
        }
    }
}
