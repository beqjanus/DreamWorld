using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using OpenMetaverse;
using OpenMetaverse.Assets;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;
using OpenMetaverse.Rendering;
//using OpenSim.Region.PhysicsModules.SharedBase;

namespace eZombies {

  public partial class eZombie {
    private static readonly float m_floatToleranceInCastRay = 0.00001f;
    private static readonly float m_floatTolerance2InCastRay = 0.001f;
    private static readonly float m_primSafetyCoeffX = 2.414214f;
    private static readonly float m_primSafetyCoeffY = 2.414214f;
    private static readonly float m_primSafetyCoeffZ = 1.618034f;
    private static readonly int m_maxHitsInCastRay = 16;
    private static readonly int m_maxHitsPerPrimInCastRay = 16;
    private static readonly int m_maxHitsPerObjectInCastRay = 16;
    private static readonly bool m_detectExitsInCastRay = false;
    private static readonly float m_avatarHeightCorrection = 0.2f;
    //private static readonly bool m_useSimpleBoxesInGetBoundingBox = false;
    //private static readonly bool m_addStatsInGetBoundingBox = false;

    //LSL Avatar Bounding Box (lABB), lower (1) and upper (2),
    //standing (Std), Groundsitting (Grs), Sitting (Sit),
    //along X, Y and Z axes, constants (0) and coefficients (1)
    private static readonly float m_lABB1StdX0 = -0.275f;
    private static readonly float m_lABB2StdX0 = 0.275f;
    private static readonly float m_lABB1StdY0 = -0.35f;
    private static readonly float m_lABB2StdY0 = 0.35f;
    private static readonly float m_lABB1StdZ0 = -0.1f;
    private static readonly float m_lABB1StdZ1 = -0.5f;
    private static readonly float m_lABB2StdZ0 = 0.1f;
    private static readonly float m_lABB2StdZ1 = 0.5f;
    /*
    private static readonly float m_lABB1GrsX0 = -0.3875f;
    private static readonly float m_lABB2GrsX0 = 0.3875f;
    private static readonly float m_lABB1GrsY0 = -0.5f;
    private static readonly float m_lABB2GrsY0 = 0.5f;
    private static readonly float m_lABB1GrsZ0 = -0.05f;
    private static readonly float m_lABB1GrsZ1 = -0.375f;
    private static readonly float m_lABB2GrsZ0 = 0.5f;
    private static readonly float m_lABB2GrsZ1 = 0.0f;
    private static readonly float m_lABB1SitX0 = -0.5875f;
    private static readonly float m_lABB2SitX0 = 0.1875f;
    private static readonly float m_lABB1SitY0 = -0.35f;
    private static readonly float m_lABB2SitY0 = 0.35f;
    private static readonly float m_lABB1SitZ0 = -0.35f;
    private static readonly float m_lABB1SitZ1 = -0.375f;
    private static readonly float m_lABB2SitZ0 = -0.25f;
    private static readonly float m_lABB2SitZ1 = 0.25f;
    */

    private static readonly Dictionary<ulong, FacetedMesh> m_cachedMeshes = new Dictionary<ulong, FacetedMesh>();

    public struct RayHit {
      public UUID PartId;
      public UUID GroupId;
      //public int Link;
      public Vector3 Position;
      public Vector3 Normal;
      public float Distance;
    }

    public struct RayTrans {
      public UUID PartId;
      public UUID GroupId;
      //public int Link;
      public Vector3 ScalePart;
      public Vector3 PositionPart;
      public Quaternion RotationPart;
      public bool ShapeNeedsEnds;
      public Vector3 Position1Ray;
      public Vector3 Position1RayProj;
      public Vector3 VectorRayProj;
    }

    private struct Tri {
      public Vector3 p1;
      public Vector3 p2;
      public Vector3 p3;
    }

    public List<UUID> CastRayV3( Vector3 start, Vector3 end, int maxHits ) {
      List<UUID> result = new List<UUID>();

      // Initialize
      List<RayHit> rayHits = new List<RayHit>();
      float tol = 0.00001f;
      Vector3 pos1Ray = start;
      Vector3 pos2Ray = end;

      // Calculate some basic parameters
      Vector3 vecRay = pos2Ray - pos1Ray;
      float rayLength = vecRay.Length();
      
      // Iterate over all objects/groups and prims/parts in region
      mScene.ForEachSOG( delegate ( SceneObjectGroup group ) {
        if (group.IsDeleted || group.RootPart == null) return;
        // we do not care about things outside parcel (arena)
        if (!IsInArena( group.AbsolutePosition )) return;

        // Check group filters unless part filters are configured
        bool isPhysical = (group.RootPart.PhysActor != null && group.RootPart.PhysActor.IsPhysical);
        bool isPhantom = group.IsPhantom || group.IsVolumeDetect;
        bool isAttachment = group.IsAttachment;
        if (isPhysical || isPhantom || isAttachment) return;

        // Parse object/group if passed filters
        // Iterate over all prims/parts in object/group
        foreach (SceneObjectPart part in group.Parts) {
          // ignore PhysicsShapeType.None as physics engines do
          // or we will get into trouble in future
          if (part.PhysicsShapeType == (byte)PhysicsShapeType.None) continue;

          isPhysical = (part.PhysActor != null && part.PhysActor.IsPhysical);
          isPhantom = ((part.Flags & PrimFlags.Phantom) != 0) || part.VolumeDetectActive;
          if (isPhysical || isPhantom) continue;

          // Parse prim/part and project ray if passed filters
          Vector3 scalePart = part.Scale;
          Vector3 posPart = part.GetWorldPosition();
          Quaternion rotPart = part.GetWorldRotation();
          Quaternion rotPartInv = Quaternion.Inverse( rotPart );
          Vector3 pos1RayProj = ((pos1Ray - posPart) * rotPartInv) / scalePart;
          Vector3 pos2RayProj = ((pos2Ray - posPart) * rotPartInv) / scalePart;

          // Filter parts by shape bounding boxes
          Vector3 shapeBoxMax = new Vector3( 0.5f, 0.5f, 0.5f );
          if (!part.Shape.SculptEntry)
            shapeBoxMax = shapeBoxMax * (new Vector3( m_primSafetyCoeffX, m_primSafetyCoeffY, m_primSafetyCoeffZ ));

          shapeBoxMax = shapeBoxMax + (new Vector3( tol, tol, tol ));

          if (rayIntersectsShapeBox( pos1RayProj, pos2RayProj, shapeBoxMax )) {
            // Prepare data needed to check for ray hits
            RayTrans rayTrans = new RayTrans();
            rayTrans.PartId = part.UUID;
            rayTrans.GroupId = part.ParentGroup.UUID;
            //rayTrans.Link = group.PrimCount > 1 ? part.LinkNum : 0;
            rayTrans.ScalePart = scalePart;
            rayTrans.PositionPart = posPart;
            rayTrans.RotationPart = rotPart;
            rayTrans.ShapeNeedsEnds = true;
            rayTrans.Position1Ray = pos1Ray;
            rayTrans.Position1RayProj = pos1RayProj;
            rayTrans.VectorRayProj = pos2RayProj - pos1RayProj;

            // Get detail level depending on type
            int lod = 0;
            // Mesh detail level
            if (part.Shape.SculptEntry && part.Shape.SculptType == (byte)SculptType.Mesh)
              lod = (int)DetailLevel.Highest;
            // Sculpt detail level
            else if (part.Shape.SculptEntry && part.Shape.SculptType == (byte)SculptType.Mesh)
              lod = (int)DetailLevel.Medium;
            // Shape detail level
            else if (!part.Shape.SculptEntry)
              lod = (int)DetailLevel.Medium;

            // Try to get cached mesh if configured
            ulong meshKey = 0;
            FacetedMesh mesh = null;
            meshKey = part.Shape.GetMeshKey( Vector3.One, (float)(4 << lod) );
            lock (m_cachedMeshes) { m_cachedMeshes.TryGetValue( meshKey, out mesh ); }

            // Create mesh if no cached mesh
            if (mesh == null) {
              // Make an OMV prim to be able to mesh part
              Primitive omvPrim = part.Shape.ToOmvPrimitive( posPart, rotPart );
              byte[] sculptAsset = null;
              if (omvPrim.Sculpt != null)
                sculptAsset = mScene.AssetService.GetData( omvPrim.Sculpt.SculptTexture.ToString() );

              // When part is mesh, get mesh
              if (omvPrim.Sculpt != null && omvPrim.Sculpt.Type == SculptType.Mesh && sculptAsset != null) {
                AssetMesh meshAsset = new AssetMesh( omvPrim.Sculpt.SculptTexture, sculptAsset );
                FacetedMesh.TryDecodeFromAsset( omvPrim, meshAsset, DetailLevel.Highest, out mesh );
                meshAsset = null;
              }
              // When part is sculpt, create mesh
              // Quirk: Generated sculpt mesh is about 2.8% smaller in X and Y than visual sculpt.
              else if (omvPrim.Sculpt != null && omvPrim.Sculpt.Type != SculptType.Mesh && sculptAsset != null) {
                IJ2KDecoder imgDecoder = mScene.RequestModuleInterface<IJ2KDecoder>();
                if (imgDecoder != null) {
                  Image sculpt = imgDecoder.DecodeToImage( sculptAsset );
                  if (sculpt != null) {
                    mesh = mMesherMod.GenerateFacetedSculptMesh( omvPrim, (Bitmap)sculpt, DetailLevel.Highest );
                    sculpt.Dispose();
                  }
                }
              }
              // When part is shape, create mesh
              else if (omvPrim.Sculpt == null) {
                if (
                          omvPrim.PrimData.PathBegin == 0.0 && omvPrim.PrimData.PathEnd == 1.0 &&
                          omvPrim.PrimData.PathTaperX == 0.0 && omvPrim.PrimData.PathTaperY == 0.0 &&
                          omvPrim.PrimData.PathSkew == 0.0 &&
                          omvPrim.PrimData.PathTwist - omvPrim.PrimData.PathTwistBegin == 0.0
                      )
                  rayTrans.ShapeNeedsEnds = false;
                mesh = mMesherMod.GenerateFacetedMesh( omvPrim, DetailLevel.Medium );
              }

              // Cache mesh if configured
              if (mesh != null) {
                lock (m_cachedMeshes) {
                  if (!m_cachedMeshes.ContainsKey( meshKey )) m_cachedMeshes.Add( meshKey, mesh );
                }
              }
            }
            // Check mesh for ray hits
            addRayInFacetedMesh( mesh, rayTrans, ref rayHits );
            mesh = null;
          }
        }
      } );

      // Iterate over all avatars in region      
      mScene.ForEachRootScenePresence( delegate ( ScenePresence sp ) {
        // we do not care about av/npc outside parcel (arena)
        if (!IsInArena( sp.AbsolutePosition )) return;

        // Get bounding box
        Vector3 lower;
        Vector3 upper;
        BoundingBoxOfScenePresence( sp, out lower, out upper );
        // Parse avatar
        Vector3 scalePart = upper - lower;
        Vector3 posPart = sp.AbsolutePosition;
        Quaternion rotPart = sp.GetWorldRotation();
        Quaternion rotPartInv = Quaternion.Inverse( rotPart );
        posPart = posPart + (lower + upper) * 0.5f * rotPart;
        // Project ray
        Vector3 pos1RayProj = ((pos1Ray - posPart) * rotPartInv) / scalePart;
        Vector3 pos2RayProj = ((pos2Ray - posPart) * rotPartInv) / scalePart;

        // Filter avatars by shape bounding boxes
        Vector3 shapeBoxMax = new Vector3( 0.5f + tol, 0.5f + tol, 0.5f + tol );
        if (rayIntersectsShapeBox( pos1RayProj, pos2RayProj, shapeBoxMax )) {
          // Prepare data needed to check for ray hits
          RayTrans rayTrans = new RayTrans();
          rayTrans.PartId = sp.UUID;
          rayTrans.GroupId = sp.ParentPart != null ? sp.ParentPart.ParentGroup.UUID : sp.UUID;
          //rayTrans.Link = sp.ParentPart != null ? UUID2LinkNumber( sp.ParentPart, sp.UUID ) : 0;
          rayTrans.ScalePart = scalePart;
          rayTrans.PositionPart = posPart;
          rayTrans.RotationPart = rotPart;
          rayTrans.ShapeNeedsEnds = false;
          rayTrans.Position1Ray = pos1Ray;
          rayTrans.Position1RayProj = pos1RayProj;
          rayTrans.VectorRayProj = pos2RayProj - pos1RayProj;

          // Try to get cached mesh if configured
          PrimitiveBaseShape prim = PrimitiveBaseShape.CreateSphere();
          int lod = (int)DetailLevel.Medium;
          ulong meshKey = prim.GetMeshKey( Vector3.One, (float)(4 << lod) );
          FacetedMesh mesh = null;
          lock (m_cachedMeshes) { m_cachedMeshes.TryGetValue( meshKey, out mesh ); }

          // Create mesh if no cached mesh
          if (mesh == null) {
            // Make OMV prim and create mesh
            prim.Scale = scalePart;
            Primitive omvPrim = prim.ToOmvPrimitive( posPart, rotPart );
            mesh = mMesherMod.GenerateFacetedMesh( omvPrim, DetailLevel.Medium );
            
            if (mesh != null) {
              lock (m_cachedMeshes) {
                if (!m_cachedMeshes.ContainsKey( meshKey )) m_cachedMeshes.Add( meshKey, mesh );
              }
            }
          }

          // Check mesh for ray hits
          addRayInFacetedMesh( mesh, rayTrans, ref rayHits );
          mesh = null;
        }
      } );

      // Sort hits by ascending distance
      rayHits.Sort( ( s1, s2 ) => s1.Distance.CompareTo( s2.Distance ) );

      // Check excess hits per part and group
      for (int t = 0; t < 2; t++) {
        int maxHitsPerType = 0;
        UUID id = UUID.Zero;
        if (t == 0) maxHitsPerType = m_maxHitsPerPrimInCastRay;
        else maxHitsPerType = m_maxHitsPerObjectInCastRay;

        // Handle excess hits only when needed
        if (maxHitsPerType < m_maxHitsInCastRay) {
          // Find excess hits
          Hashtable hits = new Hashtable();
          for (int i = rayHits.Count - 1; i >= 0; i--) {
            id = t == 0 ? rayHits[i].PartId : rayHits[i].GroupId;
            hits[id] = hits.ContainsKey( id ) ? (int)hits[id] + 1 : 1;
          }

          // Remove excess hits
          for (int i = rayHits.Count - 1; i >= 0; i--) {
            id = t == 0 ? rayHits[i].PartId : rayHits[i].GroupId;

            int hit = (int)hits[id];
            if (hit > m_maxHitsPerPrimInCastRay) {
              rayHits.RemoveAt( i );
              hit--;
              hits[id] = hit;
            }
          }
        }
      }

      // Parse hits into result list according to data flags
      int hitCount = rayHits.Count;
      if (hitCount > maxHits) hitCount = maxHits;
      for (int i = 0; i < hitCount; i++) {
        RayHit rayHit = rayHits[i];
        result.Add( rayHit.GroupId );
      }

      // Return hits
      return result;
    }

    /// <summary>
    /// Helper to calculate bounding box of an avatar.
    /// </summary>
    private void BoundingBoxOfScenePresence( ScenePresence sp, out Vector3 lower, out Vector3 upper ) {
      // Adjust from OS model
      // avatar height = visual height - 0.2, bounding box height = visual height
      // to SL model
      // avatar height = visual height, bounding box height = visual height + 0.2
      float height = sp.Appearance.AvatarHeight + m_avatarHeightCorrection;

      // According to avatar bounding box in SL 2015-04-18:
      // standing = <-0.275,-0.35,-0.1-0.5*h> : <0.275,0.35,0.1+0.5*h>
      // ZOMBIE ARE ALWAYS STANDING
      lower = new Vector3( m_lABB1StdX0, m_lABB1StdY0, m_lABB1StdZ0 + m_lABB1StdZ1 * height );
      upper = new Vector3( m_lABB2StdX0, m_lABB2StdY0, m_lABB2StdZ0 + m_lABB2StdZ1 * height );
    }

    private bool rayIntersectsShapeBox( Vector3 pos1RayProj, Vector3 pos2RayProj, Vector3 shapeBoxMax ) {
      // Skip if ray can't intersect bounding box;
      Vector3 rayBoxProjMin = Vector3.Min( pos1RayProj, pos2RayProj );
      Vector3 rayBoxProjMax = Vector3.Max( pos1RayProj, pos2RayProj );
      if (
          rayBoxProjMin.X > shapeBoxMax.X || rayBoxProjMin.Y > shapeBoxMax.Y || rayBoxProjMin.Z > shapeBoxMax.Z ||
          rayBoxProjMax.X < -shapeBoxMax.X || rayBoxProjMax.Y < -shapeBoxMax.Y || rayBoxProjMax.Z < -shapeBoxMax.Z
      )
        return false;

      // Check if ray intersect any bounding box side
      int sign = 0;
      float dist = 0.0f;
      Vector3 posProj = Vector3.Zero;
      Vector3 vecRayProj = pos2RayProj - pos1RayProj;

      // Check both X sides unless ray is parallell to them
      if (Math.Abs( vecRayProj.X ) > m_floatToleranceInCastRay) {
        for (sign = -1; sign <= 1; sign += 2) {
          dist = ((float)sign * shapeBoxMax.X - pos1RayProj.X) / vecRayProj.X;
          posProj = pos1RayProj + vecRayProj * dist;
          if (Math.Abs( posProj.Y ) <= shapeBoxMax.Y && Math.Abs( posProj.Z ) <= shapeBoxMax.Z)
            return true;
        }
      }

      // Check both Y sides unless ray is parallell to them
      if (Math.Abs( vecRayProj.Y ) > m_floatToleranceInCastRay) {
        for (sign = -1; sign <= 1; sign += 2) {
          dist = ((float)sign * shapeBoxMax.Y - pos1RayProj.Y) / vecRayProj.Y;
          posProj = pos1RayProj + vecRayProj * dist;
          if (Math.Abs( posProj.X ) <= shapeBoxMax.X && Math.Abs( posProj.Z ) <= shapeBoxMax.Z)
            return true;
        }
      }

      // Check both Z sides unless ray is parallell to them
      if (Math.Abs( vecRayProj.Z ) > m_floatToleranceInCastRay) {
        for (sign = -1; sign <= 1; sign += 2) {
          dist = ((float)sign * shapeBoxMax.Z - pos1RayProj.Z) / vecRayProj.Z;
          posProj = pos1RayProj + vecRayProj * dist;
          if (Math.Abs( posProj.X ) <= shapeBoxMax.X && Math.Abs( posProj.Y ) <= shapeBoxMax.Y)
            return true;
        }
      }

      // No hits on bounding box so return false
      return false;
    }

    /// <summary>
    /// Helper to parse FacetedMesh for ray hits.
    /// </summary>
    private void addRayInFacetedMesh( FacetedMesh mesh, RayTrans rayTrans, ref List<RayHit> rayHits ) {
      if (mesh != null) {
        foreach (Face face in mesh.Faces) {
          for (int i = 0; i < face.Indices.Count; i += 3) {
            Tri triangle = new Tri();
            triangle.p1 = face.Vertices[face.Indices[i]].Position;
            triangle.p2 = face.Vertices[face.Indices[i + 1]].Position;
            triangle.p3 = face.Vertices[face.Indices[i + 2]].Position;
            AddRayInTri( triangle, rayTrans, ref rayHits );
          }
        }
      }
    }

    /// <summary>
    /// Helper to parse Tri (triangle) List for ray hits.
    /// </summary>
    private void AddRayInTris( List<Tri> triangles, RayTrans rayTrans, ref List<RayHit> rayHits ) {
      foreach (Tri triangle in triangles) {
        AddRayInTri( triangle, rayTrans, ref rayHits );
      }
    }

    /// <summary>
    /// Helper to add ray hit in a Tri (triangle).
    /// </summary>
    private void AddRayInTri( Tri triProj, RayTrans rayTrans, ref List<RayHit> rayHits ) {
      // Check for hit in triangle
      Vector3 posHitProj;
      Vector3 normalProj;
      if (HitRayInTri( triProj, rayTrans.Position1RayProj, rayTrans.VectorRayProj, out posHitProj, out normalProj )) {
        // Hack to circumvent ghost face bug in PrimMesher by removing hits in (ghost) face plane through shape center
        if (Math.Abs( Vector3.Dot( posHitProj, normalProj ) ) < m_floatToleranceInCastRay && !rayTrans.ShapeNeedsEnds)
          return;

        // Transform hit and normal to region coordinate system
        Vector3 posHit = rayTrans.PositionPart + (posHitProj * rayTrans.ScalePart) * rayTrans.RotationPart;
        Vector3 normal = Vector3.Normalize( (normalProj * rayTrans.ScalePart) * rayTrans.RotationPart );

        // Remove duplicate hits at triangle intersections
        float distance = Vector3.Distance( rayTrans.Position1Ray, posHit );
        for (int i = rayHits.Count - 1; i >= 0; i--) {
          if (rayHits[i].PartId != rayTrans.PartId)
            break;
          if (Math.Abs( rayHits[i].Distance - distance ) < m_floatTolerance2InCastRay)
            return;
        }

        // Build result data set
        RayHit rayHit = new RayHit();
        rayHit.PartId = rayTrans.PartId;
        rayHit.GroupId = rayTrans.GroupId;
        //rayHit.Link = rayTrans.Link;
        rayHit.Position = posHit;
        rayHit.Normal = normal;
        rayHit.Distance = distance;
        rayHits.Add( rayHit );
      }
    }

    /// <summary>
    /// Helper to find ray hit in triangle
    /// </summary>
    bool HitRayInTri( Tri triProj, Vector3 pos1RayProj, Vector3 vecRayProj, out Vector3 posHitProj, out Vector3 normalProj ) {
      float tol = m_floatToleranceInCastRay;
      posHitProj = Vector3.Zero;

      // Calculate triangle edge vectors
      Vector3 vec1Proj = triProj.p2 - triProj.p1;
      Vector3 vec2Proj = triProj.p3 - triProj.p2;
      Vector3 vec3Proj = triProj.p1 - triProj.p3;

      // Calculate triangle normal
      normalProj = Vector3.Cross( vec1Proj, vec2Proj );

      // Skip if degenerate triangle or ray parallell with triangle plane
      float divisor = Vector3.Dot( vecRayProj, normalProj );
      if (Math.Abs( divisor ) < tol)
        return false;

      // Skip if exit and not configured to detect
      if (divisor > tol && !m_detectExitsInCastRay)
        return false;

      // Skip if outside ray ends
      float distanceProj = Vector3.Dot( triProj.p1 - pos1RayProj, normalProj ) / divisor;
      if (distanceProj < -tol || distanceProj > 1 + tol)
        return false;

      // Calculate hit position in triangle
      posHitProj = pos1RayProj + vecRayProj * distanceProj;

      // Skip if outside triangle bounding box
      Vector3 triProjMin = Vector3.Min( Vector3.Min( triProj.p1, triProj.p2 ), triProj.p3 );
      Vector3 triProjMax = Vector3.Max( Vector3.Max( triProj.p1, triProj.p2 ), triProj.p3 );
      if (
          posHitProj.X < triProjMin.X - tol || posHitProj.Y < triProjMin.Y - tol || posHitProj.Z < triProjMin.Z - tol ||
          posHitProj.X > triProjMax.X + tol || posHitProj.Y > triProjMax.Y + tol || posHitProj.Z > triProjMax.Z + tol
      )
        return false;

      // Skip if outside triangle
      if (
          Vector3.Dot( Vector3.Cross( vec1Proj, normalProj ), posHitProj - triProj.p1 ) > tol ||
          Vector3.Dot( Vector3.Cross( vec2Proj, normalProj ), posHitProj - triProj.p2 ) > tol ||
          Vector3.Dot( Vector3.Cross( vec3Proj, normalProj ), posHitProj - triProj.p3 ) > tol
      )
        return false;

      // Return hit
      return true;
    }
  }
}

