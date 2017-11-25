//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
Shader "RageSpline/MultiTextured" {
	Properties {
		_MainTex ("Texture1 (RGB)", 2D) = "white" {}
		_MainTex2 ("Texture2 (RGB)", 2D) = "white" {}
	}

	Category {
		Tags {"RenderType"="Transparent" "Queue"="Transparent"}
		Lighting Off
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "texcoord", texcoord0
			Bind "texcoord1", texcoord1
		}
		
		SubShader {
			Pass {
				ZWrite Off
				Cull off
				Blend SrcAlpha OneMinusSrcAlpha
				SetTexture [_MainTex] {
					Combine texture * primary, primary
				}
				SetTexture [_MainTex2] {
					Combine texture * previous double, texture * primary
				}
			}
		}
	}
}
