namespace Hnefatafl.Scenes.BoardGame
{
    public class TaflBoard
    {
        public BoardTile[,] Positions { get; set; }

        public TaflBoard()
        {
            Positions = new BoardTile[9, 9];
            for (var x = 0; x < Positions.GetLength(0); x++)
            for (var y = 0; y < Positions.GetLength(1); y++)
            {
                Positions[x, y] = new BoardTile();
            }

            Positions[0, 3].Occupant = new Attacker();
            Positions[0, 4].Occupant = new Attacker();
            Positions[0, 5].Occupant = new Attacker();
            Positions[1, 4].Occupant = new Attacker();

            Positions[3, 8].Occupant = new Attacker();
            Positions[4, 8].Occupant = new Attacker();
            Positions[5, 8].Occupant = new Attacker();
            Positions[4, 7].Occupant = new Attacker();

            Positions[8, 3].Occupant = new Attacker();
            Positions[8, 4].Occupant = new Attacker();
            Positions[8, 5].Occupant = new Attacker();
            Positions[7, 4].Occupant = new Attacker();

            Positions[3, 0].Occupant = new Attacker();
            Positions[4, 0].Occupant = new Attacker();
            Positions[5, 0].Occupant = new Attacker();
            Positions[4, 1].Occupant = new Attacker();

            Positions[2, 4].Occupant = new Defender();
            Positions[3, 4].Occupant = new Defender();
            Positions[5, 4].Occupant = new Defender();
            Positions[6, 4].Occupant = new Defender();
            Positions[4, 2].Occupant = new Defender();
            Positions[4, 3].Occupant = new Defender();
            Positions[4, 5].Occupant = new Defender();
            Positions[4, 6].Occupant = new Defender();

            Positions[4, 4].Occupant = new DefenderKing();
        }
    }
}