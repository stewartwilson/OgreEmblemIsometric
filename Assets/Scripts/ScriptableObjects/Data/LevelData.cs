using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    public List<MapTile> map;
    [SerializeField]
    public TextAsset textFile;
    [SerializeField]
    public int endLine;
    [SerializeField]
    public string sceneName;



    /**  
     *  Will return the map tile fro mthe List that matches the
     *  x and y cooridnates
     *
     */
    public MapTile getMapTileFromXY(GridPosition pos)
    {
        foreach(MapTile mt in map)
        {
            if(mt.position.x == pos.x && mt.position.y == pos.y)
            {
                return mt;
            }
        }
        return null;
    }

    /**  
     *  Will return the largest X value found in the Tile List
     *
     */
    public int getMaxX()
    {
        int maxX = 0;
        foreach (MapTile mt in map)
        {
            if (mt.position.x > maxX)
            {
                maxX = mt.position.x;
            }
        }
        return maxX;
    }

    /**  
     *  Will return the largest Y value found in the Tile List
     *
     */
    public int getMaxY()
    {
        int maxY = 0;
        foreach (MapTile mt in map)
        {
            if (mt.position.y > maxY)
            {
                maxY = mt.position.y;
            }
        }
        return maxY;
    }

    /**  
     *  Will generate the list of tiles based on the text file
     *  attached to this asset 
     *
     */
    public void generateMapFromTextFile()
    {
        string[] textLines;
        if (textFile != null)
        {
            map = new List<MapTile>();
            textLines = textFile.text.Split('\n');
            endLine = textLines.Length - 1;
            for (int i = 0; i <= endLine; i++)
            {
                string newLine = textLines[i];
                string[] tileArray = newLine.Split('|');
                for (int x = 0; x < tileArray.Length; x++)
                {
                    string[] values = tileArray[x].Split(',');
                    int[] valuesInt = new int[values.Length];
                    for (int y = 0; y < values.Length; y++)
                    {
                        valuesInt[y] = Int32.Parse(values[y]);
                    }
                    map.Add(new MapTile(valuesInt));
                }

            }
        }
    }

    /**  
     *  Will generate the list of tiles based on the text file
     *  attached to this asset 
     *
     */
    public void generateBasicFlatMap(int width, int height)
    {
        map = new List<MapTile>();
        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                int[] values = new int[5];
                values[0] = 0;
                values[1] = i;
                values[2] = j;
                values[3] = 0;
                values[4] = 1;
                map.Add(new MapTile(values));
            }
        }
        
    }
}

