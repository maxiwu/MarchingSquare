using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelMap : MonoBehaviour {

    [SerializeField]public float size = 2f;

    public int voxelResolution = 8;
    public int chunkResolution = 2;

    public VoxelGrid voxelGridPrefab;

    private VoxelGrid[] chunks;

    private float chunkSize, voxelSize, halfSize;

    private void Awake()
    {
        halfSize = size * 0.5f;
        chunkSize = size / chunkResolution;
        voxelSize = chunkSize / voxelResolution;

        chunks = new VoxelGrid[chunkResolution * chunkResolution];
        for (int i = 0, y = 0; y < chunkResolution; y++)
        {
            for (int x = 0; x < chunkResolution; x++, i++)
            {
                //Debug.LogFormat("Chunk {0} at x:{1} y:{2}", i, x, y);
                CreateChunk(i, x, y);
            }
        }

        //Debug.LogFormat("voxel rez:{0} voxelSize:{1}", voxelResolution, voxelSize);

        //catch mouse input
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(size, size);
    }

    private void CreateChunk(int i, float x, float y)
    {
        VoxelGrid chunk = Instantiate(voxelGridPrefab) as VoxelGrid;
        
        chunk.Initialize(voxelResolution, voxelSize);
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(x * chunkSize - halfSize, y * chunkSize - halfSize);
        //Debug.Log(chunk.transform.localPosition);
        chunks[i] = chunk;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("mouse clicked " + Camera.main.ScreenPointToRay(Input.mousePosition));
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                //Debug.Log("Raycast hit");
                //if (hitInfo.collider.gameObject == gameObject)
                //{
                //    Debug.Log("Raycast hit and found");
                //    EditVoxels(transform.InverseTransformPoint(hitInfo.point));
                //}
                //if (hitInfo.collider.gameObject is Quad)
                if (hitInfo.collider.gameObject != gameObject)
                {
                    //Debug.Log("Raycast hit and found");
                    //EditVoxels(transform.InverseTransformPoint(hitInfo.point));
                    EditVoxels(hitInfo.collider.gameObject.transform.position);

                    //GameObject.Destroy(hitInfo.collider.gameObject);
                    //Debug.Log(hitInfo.collider.gameObject.GetType());
                }
            }
        }
    }

    private void EditVoxels(Vector3 point)
    {
        //float voxelX = (point.x / voxelSize);
        //float voxelY = (point.y / voxelSize);
        //Debug.Log(voxelX + ", " + voxelY);

        //offset to set (0,0) at bottom left
        float voxelX = ((point.x + halfSize) / voxelSize);
        float voxelY = ((point.y + halfSize) / voxelSize);
        float chunkX = voxelX / voxelResolution;
        float chunkY = voxelY / voxelResolution;
        Debug.Log(voxelX + ", " + voxelY + " in chunk " + chunkX + ", " + chunkY);

        //set votex bit to true
        voxelX -= chunkX * voxelResolution;
        voxelY -= chunkY * voxelResolution;        
        chunks[(int)chunkY * chunkResolution + (int)chunkX].SetVoxel((int)voxelX, (int)voxelY, true);
    }
}
