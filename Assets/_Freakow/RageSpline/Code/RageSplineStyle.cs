//RageSpline - Vector Graphics Renderer for Unity3D game engine
//Copyright (C) 2017 Freakow (www.freakow.com)
//You should have received a copy of the GNU Lesser General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using UnityEngine;

[System.Serializable]
public class RageSplineStyle : ScriptableObject {
    //public enum Outline { None = 0, Loop, Free };
    public RageSpline.Outline outline = RageSpline.Outline.Loop;
    public Color outlineColor1 = Color.black;
	public Color outlineColor2 = Color.black;
    public RageSpline.OutlineGradient outlineGradient = RageSpline.OutlineGradient.None;
    public float outlineNormalOffset = 1f;
    public RageSpline.Corner corners;
		
    //public enum Fill { None = 0, Solid, Gradient };
    public RageSpline.Fill fill = RageSpline.Fill.Solid;
    public Color fillColor1 = Color.gray;
    public Color fillColor2 = Color.blue;
    public float landscapeBottomDepth;
    public float landscapeOutlineAlign;

    //public enum UVMapping { None = 0, Fill, Outline, OutlineFree };
    public RageSpline.UVMapping UVMapping1 = RageSpline.UVMapping.Fill;
    public RageSpline.UVMapping UVMapping2 = RageSpline.UVMapping.None;

    public Vector2 gradientOffset = new Vector2(0f, 0f);
    public float gradientAngle = 0f;
    public float gradientScale = 10f;
    public Vector2 textureOffset = new Vector2(0f, 0f);
    public float textureAngle = 0f;
    public float textureScale = 10f;
    public Vector2 textureOffset2 = new Vector2(0f, 0f);
    public float textureAngle2 = 0f;
    public float textureScale2 = 10f;

    //public enum Emboss { None = 0, Sharp, Blurry };
    public RageSpline.Emboss emboss = RageSpline.Emboss.None;
    public Color embossColor1 = Color.white;
    public Color embossColor2 = Color.black;
    public float embossAngle = 180f;
    public float embossOffset = 0.5f;
    public float embossSize = 10f;
    public float embossCurveSmoothness = 3f;

    //public enum RageSpline.Physics { None = 0, Boxed, MeshCollider };
    public RageSpline.Physics physics = RageSpline.Physics.None;
    public bool createPhysicsInEditor = false;
    public PhysicMaterial physicsMaterial = null;

    public int vertexCount = 64;
    public int physicsColliderCount = 32;
    public float colliderZDepth = 100f;
    public float colliderNormalOffset = 0f;
    public float boxColliderDepth = 1f;
    public bool createConvexMeshCollider = false;

    public float antiAliasingWidth = 0.5f;
    public float outlineWidth = 1f;

    public bool optimize=false;
    public float optimizeAngle=5f;

    public float outlineTexturingScale = 0.1f;


    public void GetStyleFromRageSpline(RageSpline rageSpline)
    {
        this.antiAliasingWidth = rageSpline.antiAliasingWidth;
        this.colliderZDepth = rageSpline.colliderZDepth;
        this.createPhysicsInEditor = rageSpline.createPhysicsInEditor;
        this.emboss = rageSpline.emboss;
        this.embossAngle = rageSpline.embossAngle;
        this.embossColor1 = rageSpline.embossColor1;
        this.embossColor2 = rageSpline.embossColor2;
        this.embossCurveSmoothness = rageSpline.embossCurveSmoothness;
        this.embossOffset = rageSpline.embossOffset;
        this.embossSize = rageSpline.embossSize;
        this.fill = rageSpline.fill;
        this.fillColor1 = rageSpline.fillColor1;
        this.fillColor2 = rageSpline.fillColor2;
        this.gradientAngle = rageSpline.gradientAngle;
        this.gradientOffset = rageSpline.gradientOffset;
        this.gradientScale = rageSpline.gradientScale;
        this.outline = rageSpline.outline;
        this.outlineColor1 = rageSpline.outlineColor1;
        this.outlineColor2 = rageSpline.outlineColor2;
        this.outlineTexturingScale = rageSpline.outlineTexturingScale;
        this.outlineWidth = rageSpline.OutlineWidth;
        this.outlineGradient = rageSpline.outlineGradient;
        this.outlineNormalOffset = rageSpline.outlineNormalOffset;
        this.corners = rageSpline.corners;
        this.physics = rageSpline.physics;
        this.physicsColliderCount = rageSpline.physicsColliderCount;
        this.physicsMaterial = rageSpline.physicsMaterial;
        this.textureAngle = rageSpline.textureAngle;
        this.textureAngle2 = rageSpline.textureAngle2;
        this.textureOffset = rageSpline.textureOffset;
        this.textureOffset2 = rageSpline.textureOffset2;
        this.textureScale = rageSpline.textureScale;
        this.textureScale2 = rageSpline.textureScale2;
        this.UVMapping1 = rageSpline.UVMapping1;
        this.UVMapping2 = rageSpline.UVMapping2;
        this.vertexCount = rageSpline.vertexCount;
    }

    public void RefreshAllRageSplinesWithThisStyle(RageSpline caller)
    {                 
        RageSpline[] allRageSplines = GameObject.FindObjectsOfType(typeof(RageSpline)) as RageSpline[];
        foreach (RageSpline rageSpline in allRageSplines)
        {
            if (rageSpline.style != null)
            {
                if (rageSpline.style.Equals(this))
                {
                    if (Application.isPlaying)
                    {
                        rageSpline.RefreshMesh();
                    }
                    else
                    {
                        rageSpline.RefreshMeshInEditor(true, true, true);
                    }
                }
            }
        }
    }
    
    public void SetOutline(RageSpline.Outline outline, RageSpline caller)
    {
        if (this.outline != outline)
        {
            this.outline = outline;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.Outline GetOutline()
    {
        return this.outline;
    }

    public void SetOutlineColor1(Color color, RageSpline caller)
    {
        if (outlineColor1 != color)
        {
            this.outlineColor1 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetOutlineColor1()
    {
        return this.outlineColor1;
    }

    public void SetOutlineColor2(Color color, RageSpline caller)
    {
        if (outlineColor2 != color)
        {
            this.outlineColor2 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetOutlineColor2()
    {
        return this.outlineColor2;
    }
	
    public RageSpline.OutlineGradient GetOutlineGradient()
    {
        return this.outlineGradient;
    }
    public void SetOutlineGradient(RageSpline.OutlineGradient outlineGradient, RageSpline caller)
    {
        if (this.outlineGradient != outlineGradient)
        {
            this.outlineGradient = outlineGradient;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetOutlineNormalOffset()
    {
        return this.outlineNormalOffset;
    }
    public void SetOutlineNormalOffset(float outlineNormalOffset, RageSpline caller)
    {
        if (this.outlineNormalOffset != outlineNormalOffset)
        {
            this.outlineNormalOffset = outlineNormalOffset;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public void SetCorners(RageSpline.Corner corners, RageSpline caller)
    {
        if (this.corners != corners)
        {
            this.corners = corners;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.Corner GetCorners()
    {
        return this.corners;
    }

    public void SetFill(RageSpline.Fill fill, RageSpline caller)
    {
        if (this.fill != fill)
        {
            this.fill = fill;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
	
    public RageSpline.Fill GetFill()
    {
        return this.fill;
    }

    public void SetFillColor1(Color color, RageSpline caller)
    {
        if (this.fillColor1 != color)
        {
            this.fillColor1 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetFillColor1()
    {
        return this.fillColor1;
    }

    public void SetFillColor2(Color color, RageSpline caller)
    {
        if (this.fillColor2 != color)
        {
            this.fillColor2 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetFillColor2()
    {
        return this.fillColor2;
    }

    public void SetLandscapeBottomDepth(float landscapeBottomDepth, RageSpline caller)
    {
        if (this.landscapeBottomDepth != landscapeBottomDepth)
        {
            this.landscapeBottomDepth = landscapeBottomDepth;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetLandscapeBottomDepth()
    {
        return this.landscapeBottomDepth;
    }

    public void SetLandscapeOutlineAlign(float landscapeOutlineAlign, RageSpline caller)
    {
        landscapeOutlineAlign = Mathf.Clamp01(landscapeOutlineAlign);

        if (this.landscapeOutlineAlign != landscapeOutlineAlign)
        {
            this.landscapeOutlineAlign = landscapeOutlineAlign;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetLandscapeOutlineAlign()
    {
        return this.landscapeOutlineAlign;
    }

    public void SetTexturing1(RageSpline.UVMapping texturing, RageSpline caller)
    {
        if (this.UVMapping1 != texturing)
        {
            this.UVMapping1 = texturing;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.UVMapping GetTexturing1()
    {
        return this.UVMapping1;
    }

    public void SetTexturing2(RageSpline.UVMapping texturing, RageSpline caller)
    {
        if (this.UVMapping2 != texturing)
        {
            this.UVMapping2 = texturing;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.UVMapping GetTexturing2()
    {
        return this.UVMapping2;
    }

    public void SetGradientOffset(Vector2 offset, RageSpline caller)
    {
        if (this.gradientOffset != offset)
        {
            this.gradientOffset = offset;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Vector2 GetGradientOffset()
    {
        return this.gradientOffset;
    }

    public void SetGradientAngleDeg(float angle, RageSpline caller)
    {
        if (this.gradientAngle != angle)
        {
            this.gradientAngle = Mathf.Clamp(angle, 0f, 360f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetGradientAngleDeg()
    {
        return this.gradientAngle;
    }

    public void SetGradientScaleInv(float scale, RageSpline caller)
    {
        if (this.gradientScale != scale)
        {
            this.gradientScale = Mathf.Clamp(scale, 0.00001f, 100f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetGradientScaleInv()
    {
        return this.gradientScale;
    }

    public void SetTextureOffset(Vector2 offset, RageSpline caller)
    {
        if (this.textureOffset != offset)
        {
            this.textureOffset = offset;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Vector2 GetTextureOffset()
    {
        return this.textureOffset;
    }

    public void SetTextureAngleDeg(float angle, RageSpline caller)
    {
        if (this.textureAngle != angle)
        {
            this.textureAngle = Mathf.Clamp(angle, 0f, 360f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetTextureAngleDeg()
    {
        return this.textureAngle;
    }

    public void SetTextureScaleInv(float scale, RageSpline caller)
    {
        if (this.textureScale != scale)
        {
            this.textureScale = Mathf.Clamp(scale, 0.00001f, 100f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetTextureScaleInv()
    {
        return this.textureScale;
    }

    public void SetTextureOffset2(Vector2 offset, RageSpline caller)
    {
        if (this.textureOffset2 != offset)
        {
            this.textureOffset2 = offset;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Vector2 GetTextureOffset2()
    {
        return this.textureOffset2;
    }

    public void SetTextureAngle2Deg(float angle, RageSpline caller)
    {
        if (this.textureAngle2 != angle)
        {
            this.textureAngle2 = Mathf.Clamp(angle, 0f, 360f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetTextureAngle2Deg()
    {
        return this.textureAngle2;
    }

    public void SetTextureScale2Inv(float scale, RageSpline caller)
    {
        if (this.textureScale2 != scale)
        {
            this.textureScale2 = Mathf.Clamp(scale, 0.00001f, 100f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetTextureScale2Inv()
    {
        return this.textureScale2;
    }

    public void SetEmboss(RageSpline.Emboss emboss, RageSpline caller)
    {
        if (this.emboss != emboss)
        {
            this.emboss = emboss;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.Emboss GetEmboss()
    {
        return this.emboss;
    }

    public void SetEmbossColor1(Color color, RageSpline caller)
    {
        if (this.embossColor1 != color)
        {
            this.embossColor1 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetEmbossColor1()
    {
        return this.embossColor1;
    }

    public void SetEmbossColor2(Color color, RageSpline caller)
    {
        if (this.embossColor2 != color)
        {
            this.embossColor2 = color;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public Color GetEmbossColor2()
    {
        return this.embossColor2;
    }

    public void SetEmbossAngle(float angle, RageSpline caller)
    {
        if (this.embossAngle != angle)
        {
            this.embossAngle = Mathf.Clamp(angle, 0f, 360f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetEmbossAngle()
    {
        return this.embossAngle;
    }

    public void SetEmbossOffset(float offset, RageSpline caller)
    {
        if (this.embossOffset != offset)
        {
            this.embossOffset = offset;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetEmbossOffset()
    {
        return this.embossOffset;
    }

    public void SetEmbossSize(float size, RageSpline caller)
    {
        if (this.embossSize != size)
        {
            this.embossSize = Mathf.Clamp(size, 0.00061f, 1000f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetEmbossSize()
    {
        return this.embossSize;
    }

    public void SetEmbossSmoothness(float smoothness, RageSpline caller)
    {
        if (this.embossCurveSmoothness != smoothness)
        {
            this.embossCurveSmoothness = Mathf.Clamp(smoothness, 0f, 100f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetEmbossSmoothness()
    {
        return this.embossCurveSmoothness;
    }

    public void SetPhysics(RageSpline.Physics physics, RageSpline caller)
    {
        if (this.physics != physics)
        {
            this.physics = physics;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public RageSpline.Physics GetPhysics()
    {
        return this.physics;
    }

    public void SetCreatePhysicsInEditor(bool createInEditor, RageSpline caller)
    {
        if (this.createPhysicsInEditor != createInEditor)
        {
            this.createPhysicsInEditor = createInEditor;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public bool GetCreatePhysicsInEditor()
    {
        return this.createPhysicsInEditor;
    }

    public void SetPhysicsMaterial(PhysicMaterial physicsMaterial, RageSpline caller)
    {
        if (this.physicsMaterial != physicsMaterial)
        {
            this.physicsMaterial = physicsMaterial;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public PhysicMaterial GetPhysicsMaterial()
    {
        return this.physicsMaterial;
    }

    public void SetVertexCount(int count, RageSpline caller)
    {
        if (this.vertexCount != count)
        {
            this.vertexCount = count;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public int GetVertexCount()
    {
        return this.vertexCount;
    }

    public void SetPhysicsColliderCount(int count, RageSpline caller)
    {
        if (this.physicsColliderCount != count)
        {
            this.physicsColliderCount = count;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public int GetPhysicsColliderCount()
    {
        return this.physicsColliderCount;
    }

    public void SetCreateConvexMeshCollider(bool createConvexMeshCollider, RageSpline caller)
    {
        if (this.createConvexMeshCollider != createConvexMeshCollider)
        {
            this.createConvexMeshCollider = createConvexMeshCollider;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public bool GetCreateConvexMeshCollider()
    {
        return this.createConvexMeshCollider;
    }

    public void SetPhysicsZDepth(float depth, RageSpline caller)
    {
        if (this.colliderZDepth != depth)
        {
            this.colliderZDepth = depth;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetPhysicsZDepth()
    {       
        return this.colliderZDepth;
    }

    public void SetPhysicsNormalOffset(float offset, RageSpline caller)
    {
        this.colliderNormalOffset = offset;
    }
    public float GetPhysicsNormalOffset()
    {
        return this.colliderNormalOffset;
    }

    public void SetBoxColliderDepth(float depth, RageSpline caller)
    {
        if (this.boxColliderDepth != depth)
        {
            this.boxColliderDepth = Mathf.Clamp(depth, 0.1f, 1000f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetBoxColliderDepth()
    {
        return this.boxColliderDepth;
    }

    public void SetAntialiasingWidth(float width, RageSpline caller)
    {
        if (this.antiAliasingWidth != width)
        {
            this.antiAliasingWidth = Mathf.Clamp(width, 0f, 1000f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetAntialiasingWidth()
    {
        return this.antiAliasingWidth;
    }

    public void SetOutlineWidth(float width, RageSpline caller)
    {
        if (this.outlineWidth != width)
        {
            this.outlineWidth = Mathf.Clamp(width, 0.0001f, 1000f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetOutlineWidth()
    {
        return this.outlineWidth;
    }

    public void SetOutlineTexturingScaleInv(float scale, RageSpline caller)
    {
        if (this.outlineTexturingScale != scale)
        {
            this.outlineTexturingScale = Mathf.Clamp(scale, 0.001f, 1000f);
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }
    public float GetOutlineTexturingScaleInv()
    {
        return this.outlineTexturingScale;
    }

    public void SetOptimizeAngle(float angle, RageSpline caller)
    {
        if(this.optimizeAngle != angle)
        {
            this.optimizeAngle = angle;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }

    public float GetOptimizeAngle()
    {
        return this.optimizeAngle;
    }

    public void SetOptimize(bool optimize, RageSpline caller)
    {
        if(this.optimize != optimize)
        {
            this.optimize = optimize;
            RefreshAllRageSplinesWithThisStyle(caller);
        }
    }

    public bool GetOptimize()
    {
        return this.optimize;
    }

}
