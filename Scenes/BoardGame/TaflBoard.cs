namespace Hnefatafl.Scenes.BoardGame
{
    public class TaflBoard
    {
        public Piece?[,] Positions { get; set; }

        public TaflBoard()
        {
            Positions = new Piece?[9, 9];

            Positions[0, 3] = Piece.Attacker;
            Positions[0, 4] = Piece.Attacker;
            Positions[0, 5] = Piece.Attacker;
            Positions[1, 4] = Piece.Attacker;

            Positions[3, 8] = Piece.Attacker;
            Positions[4, 8] = Piece.Attacker;
            Positions[5, 8] = Piece.Attacker;
            Positions[4, 7] = Piece.Attacker;

            Positions[8, 3] = Piece.Attacker;
            Positions[8, 4] = Piece.Attacker;
            Positions[8, 5] = Piece.Attacker;
            Positions[7, 4] = Piece.Attacker;

            Positions[3, 0] = Piece.Attacker;
            Positions[4, 0] = Piece.Attacker;
            Positions[5, 0] = Piece.Attacker;
            Positions[4, 1] = Piece.Attacker;

            Positions[2, 4] = Piece.Defender;
            Positions[3, 4] = Piece.Defender;
            Positions[5, 4] = Piece.Defender;
            Positions[6, 4] = Piece.Defender;
            Positions[4, 2] = Piece.Defender;
            Positions[4, 3] = Piece.Defender;
            Positions[4, 5] = Piece.Defender;
            Positions[4, 6] = Piece.Defender;

            Positions[4, 4] = Piece.DefenderKing;
        }
    }
}