using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MapMgr : MonoBehaviour
{
    public BaseMap[] maps;
    public List<BaseMap> currentMapList;

    public int initMapCnt;
    public int score;

    public Vector2Int currentPlayerPos; 

    public PlayerInput player;

    public float deathTimer;
    public float currentTime;

    public GameObject GameOverPanel;
    public GameObject deadEffect;

    public void Start()
    {
        DOTween.Init();

        score = 0;
        for(int i = 0; i < initMapCnt; i++)
        {
            GenerateMap();
        }

        player.transform.SetParent(GetPosition(currentPlayerPos).transform);
        player.transform.localPosition = Vector3.zero;

        transform.DOMove(transform.position - Vector3.right*3, 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }


    public void GenerateMap()
    {
        int rndMap = Random.Range(0, maps.Length);
        if (score == currentPlayerPos.y) rndMap = 1;
        BaseMap map = Instantiate(maps[rndMap]);
        map.transform.position = transform.position + Vector3.right * score;
        map.transform.SetParent(transform);
        map.name = "Map" + score;
        currentMapList.Add(map);
        score++;
    }

    public GameObject GetPosition(Vector2Int position)
    {
        return currentMapList[position.y].roads[position.x].gameObject;
    }

    public void PlayerMove(Vector2Int inputVector)
    {
        currentPlayerPos += inputVector;

        if(currentMapList[currentPlayerPos.y].CompareTag("Grass"))
        {
            if (currentMapList[currentPlayerPos.y].treeBlock.Contains(currentPlayerPos.x))
            {
                currentPlayerPos-= Vector2Int.RoundToInt(inputVector);
                return;
            }
        }

        if (inputVector == Vector2Int.up) currentTime = 0f;
        player.transform.SetParent(GetPosition(currentPlayerPos).transform);
        player.SetAnimTrigger();
        player.isMove = true;
        player.transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(()=>player.isMove = false);
        CheckMapSize();

    }

public void CheckMapSize()
    {
        if (currentPlayerPos.y + 10 > score) GenerateMap();
        if(currentPlayerPos.y - 13 >= 0)
        {
            if (currentMapList[currentPlayerPos.y-13] !=null)
            {
             Destroy(currentMapList[currentPlayerPos.y - 13].gameObject);
            }
        }
    }

    public void GameOver()
    {
        Instantiate(deadEffect, player.transform);
        GameOverPanel.gameObject.SetActive(true);
        Invoke("Restart", 2f);
    }

    public void Restart()
    {

        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        if (deathTimer < currentTime) GameOver();
    }
}
