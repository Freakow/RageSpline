//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;

public interface IRageTriangulator {
    int[] Triangulate(UnityEngine.Vector2[] verts);
}
