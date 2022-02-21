Shader "Unlit/Dice"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1)
        _Top ("Top", 2D) = "white" {}
        _Bottom ("Bottom", 2D) = "white" {}
        _Left ("Left", 2D) = "white" {}
        _Right ("Right", 2D) = "white"{}
        _Forward ("Forward", 2D) = "white" {}
        _Backward ("Backward", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members localPos)
            #pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL; //you don't need these semantics except for XBox360
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _MainTex)
                UNITY_DEFINE_INSTANCED_PROP(fixed4 , _Color)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Top)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Bottom)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Left)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Right)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Forward)
                UNITY_DEFINE_INSTANCED_PROP(sampler2D , _Backward)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v)
                UNITY_TRANSFER_INSTANCE_ID(v, o); // necessary only if you want to access instanced properties in the fragment Shader.

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = v.normal;
                return o;
            }

            bool checkDirection(float3 normal,float3 axis)
            {
                return normal.x == axis.x &&
                normal.y == axis.y &&
                normal.z == axis.z;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i); // necessary only if any instanced properties are going to be accessed in the fragment Shader.

                fixed4 color = UNITY_ACCESS_INSTANCED_PROP(Props,_Color);
                fixed4 main = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_MainTex), i.uv);
                fixed4 result = (main.a > 0) ? main:color;
                    
                if(checkDirection(i.normal,float3(1,0,0)))
                {
                    fixed4 faceRight = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Right), i.uv);
                    if(faceRight.a != 0)
                        result = faceRight;
                }
                if(checkDirection(i.normal,float3(-1,0,0)))
                {
                    fixed4 faceLeft = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Left), i.uv);
                    if(faceLeft.a != 0)
                        result = faceLeft;
                }
                if(checkDirection(i.normal,float3(0,1,0)))
                {
                    fixed4 faceTop = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Top), i.uv);
                    if(faceTop.a != 0)
                        result = faceTop;
                }
                if(checkDirection(i.normal,float3(0,-1,0)))
                {
                    fixed4 faceBottom = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Bottom), i.uv);
                    if(faceBottom.a != 0)
                        result = faceBottom;
                }
                if(checkDirection(i.normal,float3(0,0,1)))
                {
                    fixed4 faceForward = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Forward), i.uv);
                    if(faceForward.a != 0)
                        result = faceForward;
                }
                if(checkDirection(i.normal,float3(0,0,-1)))
                {
                    fixed4 faceBackward = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props,_Backward), i.uv);
                    if(faceBackward.a != 0)
                        result = faceBackward;
                }



                return result;
            }

            ENDCG
        }
    }
}
