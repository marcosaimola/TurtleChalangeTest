using System.Collections.Generic;

namespace TurtleChallengeTest.Library
{
    public class Board
    {
        public Tile[,] BoardTiles { get; set; }
        public int SizeY { get; set; }
        public int SizeX { get; set; }

        public Board()
        { }

        public Board(int sizeX, int sizeY, List<BoardPosition> mines, BoardPosition exit)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            
            BoardTiles = new Tile[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    BoardTiles[x, y] = new Tile {Type = TileType.Normal};
                }
            }

            foreach (var tile in mines)
            {
                BoardTiles[tile.PosX, tile.PosY].Type = TileType.Mine;
            }

            BoardTiles[exit.PosX, exit.PosY].Type = TileType.Exit;

        }
    }
}
