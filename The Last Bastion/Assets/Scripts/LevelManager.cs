using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// A prefab for creating a single tile
    /// </summary>


    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Creates the level
    /// </summary>
    private void CreateLevel()
    {
        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(),x,y, worldStart);
            }
        }
    }

    private void PlaceTile(string TileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(TileType);

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x + TileSize* x, worldStart.y - TileSize * y, 0);
    }


    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
