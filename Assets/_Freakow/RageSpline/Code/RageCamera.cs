//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#if !UNITY_WEBPLAYER
using System.IO;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public partial class RageCamera : MonoBehaviour {

    [SerializeField]private Camera _camera;
	private bool _started;

    public void OnEnable() {
		if (_started) return;
		_camera = GetComponent<Camera>();
		_camera.transparencySortMode = TransparencySortMode.Orthographic;
		_started = true;
	}

}
