namespace TurtleChallengeTest.Library
{
    public enum TileType
    {
        Mine,
        Exit,
        Normal
    }

    public enum Action
    {
        Rotate,
        Move
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public enum ActionResult
    {
        SuccessMoved,
        HitTheWall,
        MineExploded,
        ExitFound
    }
}
