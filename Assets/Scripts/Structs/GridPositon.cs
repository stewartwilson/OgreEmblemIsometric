[System.Serializable]
public struct GridPosition
{
    public int x;
    public int y;
    public int elevation;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.elevation = 0;
        
    }

    public GridPosition(int x, int y, int elevation)
    {
        this.x = x;
        this.y = y;
        this.elevation = elevation;
    }
}