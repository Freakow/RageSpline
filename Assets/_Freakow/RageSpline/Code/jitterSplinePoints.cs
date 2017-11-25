//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using UnityEngine;

public class jitterSplinePoints : MonoBehaviour
{
    public RageSpline rageSpline;

	void Start (){
	    rageSpline = GetComponent<RageSpline>();
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.J))
            for (int i = 0; i < rageSpline.spline.points.Length-1; i++) {
                float rnd = Random.Range(-0.25f, 0.25f);
                var newPos = rageSpline.GetPosition(i);
                rageSpline.SetPoint(i, new Vector3(newPos.x+rnd, newPos.y+rnd));
            }
        rageSpline.RefreshMesh();

	}
}
