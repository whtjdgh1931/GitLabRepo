using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{
    public GameObject[] roads = new GameObject[30];

    public ObjectSpawner leftSpawner;
    public ObjectSpawner rightSpawner;

    public GameObject[] trees;
    public int treeNum;
    public List<int> treeBlock;

    public Mesh[] roadMesh;
    public int crossNum;

    public void  Awake()
    {
        for (int i = 0; i < 30; i++)
        {
            roads[i] = transform.GetChild(i).gameObject;
        }

        if (tag == "Road")
        {

            List<int> numList = new List<int>();
            for (int i = 0; i < crossNum; i++)
            {
                int RndNum = Random.Range(0, roads.Length);
                if (numList.Contains(RndNum)) --i;
                roads[RndNum].GetComponent<MeshFilter>().mesh = roadMesh[1];
                numList.Add(RndNum);

                int isLeft = Random.Range(0, 2);
                if (isLeft == 0)
                {
                    leftSpawner.enabled = true;
                    rightSpawner.enabled = false;
                }
                else
                {
                    leftSpawner.enabled = false;
                    rightSpawner.enabled= true;
                }
            }
        }
        else if (tag == "Grass")
        {
            for (int i = 0; i < treeNum; i++)
            {
                int RndNum = Random.Range(0, roads.Length);
                if (treeBlock.Contains(RndNum)) --i;
                else
                {
                    int rndTree = Random.Range(0, trees.Length);
                    GameObject tree = Instantiate(trees[rndTree], roads[RndNum].transform.position, Quaternion.identity);
                    tree.transform.SetParent(transform);
                    treeBlock.Add(RndNum);
                }

            }
        }
    }


}
