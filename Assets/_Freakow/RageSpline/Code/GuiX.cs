//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System;

public static class GuiX {

    public static System.Action<System.Action> Horizontal = horizontalBlockActions => {
		GUILayout.BeginHorizontal();
		horizontalBlockActions();
		GUILayout.EndHorizontal();
	};

    public static System.Action<System.Action> Vertical = verticalBlockActions => {
  		GUILayout.BeginVertical();
  		verticalBlockActions();
  		GUILayout.EndVertical();
  	};

    public static Action<GUIStyle, System.Action> VerticalStyled = (blockStyle, verticalBlockActions) => {
		if (blockStyle==null) GUILayout.BeginVertical();
		else 
			GUILayout.BeginVertical(blockStyle);
		verticalBlockActions();
		GUILayout.EndVertical();
	};

}
