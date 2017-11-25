//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
Shader "RageSpline/Basic" {
	Properties {

	}

	Category {
		Tags {"RenderType"="Transparent" "Queue"="Transparent"}
		Lighting Off
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
		}
		
		SubShader {
			Pass {
				ZWrite Off
				Cull Off
				Blend SrcAlpha OneMinusSrcAlpha
			}
		}
	}
}
/*Shader "RageSpline/Basic" {
Properties {
	/*_EmisColor ("Emissive Color", Color) = (.2,.2,.2,0)*/
	_MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off ZWrite On
	
	Lighting On
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
	}
	/*Material { Emission [_EmisColor] }*/
	ColorMaterial AmbientAndDiffuse

	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine texture * primary
			}
		}
	}
}
}*/

