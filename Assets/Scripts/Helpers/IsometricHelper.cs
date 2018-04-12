using UnityEngine;

public static class IsometricHelper
{
    public const float XDELTA = .5f;
    public const float YDELTA = .25f;

    /**
     * Maps an xy coordinate to the position a tile shoudl be placed in
     * unity. Method also takes into accoutn elevation of the tile
     * 
     */
    public static Vector2 gridToGamePostion(GridPosition pos)
    {
        float posX = -(pos.y * XDELTA) + (pos.x * XDELTA);
        float posY = (pos.y * YDELTA) + (pos.x * YDELTA);
        posY += pos.elevation * YDELTA;
        return new Vector2(posX, posY);
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    public static int getTileSortingOrder(GridPosition pos)
    {
        int sortingOrder = -pos.x + -pos.y;
        return sortingOrder;
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    public static int distanceBetweenGridPositions(GridPosition one, GridPosition two)
    {
        int distanceX = Mathf.Abs(one.x - two.x);
        int distanceY = Mathf.Abs(one.y - two.y);
        return distanceX+distanceY;
    }


    /**
     * Maps an the position a unit has in a group and maps that to the world
     * position to be displayed
     * 
     */
    public static GridPosition battleCoordToPostion(int pos, bool isPlayer)
    {
        int x = 0;
        int y = 0;
        if (isPlayer) {
            switch(pos)
            {
                case 0:
                    x = 3;
                    y = 3;
                    break;
                case 1:
                    x = 3;
                    y = 2;
                    break;
                case 2:
                    x = 3;
                    y = 1;
                    break;
                case 3:
                    x = 2;
                    y = 3;
                    break;
                case 4:
                    x = 2;
                    y = 2;
                    break;
                case 5:
                    x = 2;
                    y = 1;
                    break;
                case 6:
                    x = 1;
                    y = 3;
                    break;
                case 7:
                    x = 1;
                    y = 2;
                    break;
                case 8:
                    x = 1;
                    y = 1;
                    break;
                default:
                    Debug.Log("Found unexpected player unit position: " + pos);
                    break;

            }
        }
        else
        {
            switch (pos)
            {
                case 0:
                    x = 6;
                    y = 1;
                    break;
                case 1:
                    x = 6;
                    y = 2;
                    break;
                case 2:
                    x = 6;
                    y = 3;
                    break;
                case 3:
                    x = 7;
                    y = 1;
                    break;
                case 4:
                    x = 7;
                    y = 2;
                    break;
                case 5:
                    x = 7;
                    y = 3;
                    break;
                case 6:
                    x = 8;
                    y = 1;
                    break;
                case 7:
                    x = 8;
                    y = 2;
                    break;
                case 8:
                    x = 8;
                    y = 3;
                    break;
                default:
                    Debug.Log("Found unexpected enemy unit position: " + pos);
                    break;
            }
        }
        
        return new GridPosition(x,y);
    }


}

