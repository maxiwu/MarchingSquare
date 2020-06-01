using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VoxelGrid : MonoBehaviour {

    //[SerializeField]public int resolutionX;
    //[SerializeField]public int resolutionY;
    [SerializeField] public int resolution;
    private float voxelSize;
    private bool[] voxels;
    [SerializeField] private GameObject voxelPrefab;

    private Color colorStart;
    private Color colorEnd;

    private void Awake()
    {
        voxelSize = 1f / resolution;
        //Initialize(resolution, voxelSize);
        Debug.LogFormat("grid rez:{0} gridSize:{1}", resolution, voxelSize);
    }

    public void Initialize(int resolution, float voxelSize)
    {
        colorStart = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colorEnd = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //voxels = new bool[resolutionX * resolutionY];
        //voxelSize = 1f / resolution;
        //Debug.Log("voxel size " + voxelSize);
        voxels = new bool[resolution * resolution];

        //create voxel
        for (int i = 0, y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {                
                CreateVoxel(i, x, y);
            }
        }
    }

    private void CreateVoxel(int i, int x, int y)
    {        
        GameObject o = Instantiate(voxelPrefab) as GameObject;
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * voxelSize, (y + 0.5f) * voxelSize);
        
        o.transform.localScale = Vector3.one * voxelSize;        
        changeColor(o);
        //Debug.Log("create voxel at " + o.transform.localPosition);
    }

    private GameObject changeColor(GameObject pf)
    {
        var cubeRenderer = pf.GetComponent<Renderer>();

        //Debug.Log(cubeRenderer == null ? "no render" : "yes render");

        if (cubeRenderer != null)
        {
            
            //float duration = 1.0f;
            float duration = Random.RandomRange(0f,1f);

            //Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.red);
            //float lerp = Mathf.PingPong(Time.time, duration) / duration;
            cubeRenderer.material.color = Color.Lerp(colorStart, colorEnd, duration);
        }
        return pf;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


