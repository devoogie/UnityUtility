Shader "Custom/HP"
{
    Properties
    {
        _Repeat ("Repeat Texture", Float) = 1
        _MainTex ("Texture", 2D) = "white" {}
        _LineTex ("Line", 2D) = "black" {}
        _ColorLine ("Line Color", Color) = (1,1,1,1)
        _ColorBG ("BackGround", Color) = (1,1,1,1)
        _ColorShield ("Shield", Color) = (1,1,1,1)
        _ColorHP ("HP", Color) = (1,1,1,1)
        _ColorDamage ("Damage", Color) = (1,1,1,1)
        _ColorHeal ("Heal", Color) = (1,1,1,1)
        _RatioHealth ("Health", Float) = .2 
        _RatioShield ("Shield", Float) = .3 
        _RatioPivot ("Pivot", Float) = 1 
        _RatioCurrent ("Current", Float) = 0.5 

    }
    SubShader
    {
        Tags
        {
            "Queue"="Overlay"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        

        Pass
        {
            CGPROGRAM
            #pragma vertex vert keepalpha
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID // necessary only if you want to access instanced properties in fragment Shader.
            };
                sampler2D _MainTex;
                float4 _MainTex_ST;
                sampler2D _LineTex;
                float4 _LineTex_ST;
                float4 _ColorLine;
                float4 _ColorBG;
                float4 _ColorShield;
                float4 _ColorDamage;
                float4 _ColorHeal;
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _ColorHP);
                UNITY_DEFINE_INSTANCED_PROP(float, _Repeat);
                UNITY_DEFINE_INSTANCED_PROP(float, _RatioHealth);
                UNITY_DEFINE_INSTANCED_PROP(float, _RatioShield);
                UNITY_DEFINE_INSTANCED_PROP(float, _RatioPivot);
                UNITY_DEFINE_INSTANCED_PROP(float, _RatioCurrent);
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o); // necessary only if you want to access instanced properties in the fragment Shader.

                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i); // necessary only if any instanced properties are going to be accessed in the fragment Shader.
                // sample the texture
                fixed2 uv = i.uv;
                fixed4 baseColor = tex2D(_MainTex, i.uv);
                fixed4 lineColor = tex2D(_LineTex, i.uv * UNITY_ACCESS_INSTANCED_PROP(Props, _Repeat));
                fixed4 resultColor = _ColorBG;
                // resultColor.rgb *= _ColorBG.rgba;
                float animation = UNITY_ACCESS_INSTANCED_PROP(Props,_RatioPivot) - UNITY_ACCESS_INSTANCED_PROP(Props,_RatioCurrent);
                float pivotMax = min(UNITY_ACCESS_INSTANCED_PROP(Props,_RatioCurrent),UNITY_ACCESS_INSTANCED_PROP(Props,_RatioPivot));
                
                if(uv.x  < UNITY_ACCESS_INSTANCED_PROP(Props,_RatioHealth) + UNITY_ACCESS_INSTANCED_PROP(Props,_RatioShield) &&uv.x  > UNITY_ACCESS_INSTANCED_PROP(Props,_RatioHealth))
                    resultColor += _ColorShield;
                if(uv.x  < UNITY_ACCESS_INSTANCED_PROP(Props,_RatioHealth))
                    resultColor += UNITY_ACCESS_INSTANCED_PROP(Props,_ColorHP);
                if(uv.x  < pivotMax + abs(animation) &&uv.x  > pivotMax)
                    if(animation != 0)
                        resultColor = animation> 0 ?_ColorDamage:_ColorHeal;
                resultColor.rgba = resultColor.rgba * (1- lineColor.r)* baseColor.rgba + _ColorLine * lineColor.r * baseColor.rgba;

                return resultColor;
            }
            ENDCG
        }
    }
}
