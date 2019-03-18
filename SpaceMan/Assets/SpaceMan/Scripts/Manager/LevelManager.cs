using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<LevelBlock> allLevelsBlock = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlock = new List<LevelBlock>();
    public Transform levelStartPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GenerateInitBlock();
    }

    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(0, allLevelsBlock.Count);
        LevelBlock block;
        Vector2 spawnPosition = Vector2.zero;
        if (currentLevelBlock.Count == 0)
        {
            block = Instantiate(allLevelsBlock[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allLevelsBlock[randomIdx]);
            spawnPosition = currentLevelBlock[currentLevelBlock.Count - 1].exitPoint.position;
        }

        block.transform.SetParent(this.transform, false);

        Vector2 correction = new Vector2(spawnPosition.x - block.startPoint.position.x, spawnPosition.y - block.startPoint.position.y);
        block.transform.position = correction;
        currentLevelBlock.Add(block);
    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlock[0];
        currentLevelBlock.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlock()
    {
        while (currentLevelBlock.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInitBlock()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
