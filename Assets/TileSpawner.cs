using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {

    [SerializeField] int mapWide = 3;
    [SerializeField] int mapHeight = 3;
    //[SerializeField] GameObject tile;
    [SerializeField] Dirt tile;
    float tileCenterOffset = 0.5f;

    [SerializeField] int tileScale = 2;

    Dirt[] tiles;
    MouseControl finger;

    private void Awake()
    {
        Debug.Log("awake");
        tiles = new Dirt[mapWide * mapHeight];
        GenerateTileContent(mapWide, mapHeight);

        finger = GameObject.Find("Finger").GetComponent<MouseControl>();
    }

    private void GenerateTileContent(int mapWide, int mapHeight)
    {
        float halfWide = (mapWide / 2f);
        float halfHeight = (mapHeight / 2f);
        float left = (tileCenterOffset - halfWide);
        float bottom = (tileCenterOffset - halfHeight);

        //Debug.LogFormat(" map x/2:{0} map y/2:{1}", halfWide, halfHeight);
        for (float i = 0; i < mapWide; i++)
        {
            for (float j = 0; j < mapHeight; j++)
            {
                //Vector3 pos = new Vector3(left +i, bottom+j, 0);
                float posX = (left + i) * tileScale;
                float posY = (bottom + j) * tileScale;
                Vector3 pos = new Vector3(posX, posY , 0);
                //Debug.LogFormat(" pos x:{0} y/2:{1}", left + i, bottom + j);
                Dirt newtile = Instantiate(tile, pos, Quaternion.identity);
                newtile.transform.localScale = new Vector3(tileScale, tileScale, 0f);

                int seq = (int)(i * mapWide + j);
                //Debug.LogFormat("seq {0}", seq);
                tiles[seq] = newtile;
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //MouseControl finger = GetComponent<MouseControl>();

        if (Input.GetMouseButton(0))
        {
            if (finger != null)
            {
                CalculateMeatball(finger);
            }
            else
            {
                Debug.Log("finger not found");
            }
        }
    }

    private void CalculateMeatball(MouseControl finger)
    {
        float r = finger.radius;
        Vector3 center = finger.transform.position;
        float rsquare = r * r;
        for (int n = 0; n < tiles.Length; n++)
        {
            Dirt target = tiles[n];
            Vector3 tileCenter = target.transform.position;
            Vector3 posC = new Vector3(tileCenter.x-tileCenterOffset, tileCenter.y-tileCenterOffset, tileCenter.z);
            Vector3 posA = new Vector3(posC.x, posC.y + 1, tileCenter.z);
            Vector3 posB = new Vector3(posC.x + 1, posC.y + 1, tileCenter.z);
            Vector3 posD = new Vector3(posC.x + 1, posC.y , tileCenter.z);

            //Debug.LogFormat("A:{0} B:{1} C:{2} D:{3} M:{4}", posA, posB, posC, posD, center);

            float scoreA = CalcScoreFunc(rsquare, center, posA);
            float scoreB = CalcScoreFunc(rsquare, center, posB);
            float scoreC = CalcScoreFunc(rsquare, center, posC);
            float scoreD = CalcScoreFunc(rsquare, center, posD);

            if ((scoreA >= 1) || (scoreB >= 1) || (scoreC >= 1) || (scoreD >= 1))
            {
                Debug.LogFormat("A:{0} B:{1} C:{2} D:{3} ", scoreA, scoreB, scoreC, scoreD);
                var rdr = target.GetComponent<Renderer>();     
                if (rdr != null)
                {
                    rdr.material.color = Color.black;
                }
            }

            //if ((scoreA < 1) || (scoreB < 1) || (scoreC < 1) || (scoreD < 1))
            //{
            //    var rdr = target.GetComponent<Renderer>();
            //    if (rdr != null)
            //    {
            //        rdr.material.color = Color.black;
            //    }
            //}
        }
    }

    private float CalcScoreFunc(float rsquare, Vector3 center, Vector3 posC)
    {
        float diffx = Mathf.Pow((center.x - posC.x), 2f);
        float diffy = Mathf.Pow((center.y - posC.y), 2f);
        float score = rsquare / (diffx + diffy);
        //return score >= 1 ? true : false;
        return score;
    }
}

