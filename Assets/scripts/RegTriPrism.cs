using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegTriPrism : MonoBehaviour
{
    [SerializeField] int size;
    // Start is called before the first frame update
    void Start()
    {
        // New mesh to store the object shape
        Mesh mesh = new Mesh();
        // Link to the mesh n the game object
        GetComponent<MeshFilter>().mesh = mesh;


        ///// GEOMETRY //////
        /// The vertices
        /// 4 vertices 
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(2.812f, -4.824f, 8.298f);
        verts[1] = new Vector3(1.062f, -4.824f, 5.266f);
        verts[2] = new Vector3(4.562F, -4.824f, 5.266f);
        verts[3] = new Vector3(2.812f, -1.694f, 6.277f);

        //// TOPOLOGY /////
        /// 12 indices de caaras
        // Orden contra la manecillas del reloj
        int[] faces = new int[12];
        faces[0] = 1;
        faces[1] = 2;
        faces[2] = 0;
        faces[3] = 1;
        faces[4] = 0;
        faces[5] = 3;
        faces[6] = 2;
        faces[7] = 3;
        faces[8] = 0;
        faces[9] = 2;
        faces[10] = 1;
        faces[11] = 3;
        

        /// ORDEN DE FACES
        // 1 2 4
        // 1 3 2
        /// 2 3 4
        /// 3 1 4

        
        
        ///// TRANSFORMS ////
        // get translation matrix

        Matrix4x4 OriginTranslation = MakeTranslate(-2.812f, 4.042f, -6.277f);
        Matrix4x4 CentroidTranslation = MakeTranslate(2.812f, -4.042f, 6.277f);
        Matrix4x4 RotationMatrix = MakeRotationY();
        Matrix4x4 Composite1 =  RotationMatrix * OriginTranslation;
        Matrix4x4 Composite2 = Composite1 * CentroidTranslation;

        Debug.Log(OriginTranslation);
        Debug.Log(CentroidTranslation);

        Debug.Log(Composite2);

        for (int i=0; i<verts.Length;i++){
            float x = verts[i][0];
            float y = verts[i][1];
            float z = verts[i][2];

            Vector4 temp = new Vector4(x, y, z, 1);
            verts[i] = Composite2 * temp;

            Debug.Log(temp);
        }

        mesh.vertices = verts;
        mesh.triangles = faces;
    }

   


    Matrix4x4 MakeTranslate(float tx, float ty, float tz)
    {
        Matrix4x4 mat = Matrix4x4.identity;
        mat[0, 3] = tx;
        mat[1, 3] = ty;
        mat[2, 3] = tz;
        

        return mat;
    }

    Matrix4x4 MakeRotationY()
    {
        Matrix4x4 mat = Matrix4x4.identity;
    
        mat[0, 0] = 0.9659f;
        mat[2, 0] = 0.2558f;
        mat[0, 2] = -0.2558f;
        mat[2, 2] = 0.9659f;

        return mat;
    }

}
