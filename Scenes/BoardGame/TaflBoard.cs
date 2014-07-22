namespace Hnefatafl.Scenes.BoardGame
{
    public class TaflBoard
    {
        public Piece[,] Positions { get; set; }

        public TaflBoard()
        {
            Positions = new Piece[9, 9];

            Positions[0, 3] = new Attacker();
            Positions[0, 4] = new Attacker();
            Positions[0, 5] = new Attacker();
            Positions[1, 4] = new Attacker();
                                          
            Positions[3, 8] = new Attacker();
            Positions[4, 8] = new Attacker();
            Positions[5, 8] = new Attacker();
            Positions[4, 7] = new Attacker();
                                          
            Positions[8, 3] = new Attacker();
            Positions[8, 4] = new Attacker();
            Positions[8, 5] = new Attacker();
            Positions[7, 4] = new Attacker();
                                          
            Positions[3, 0] = new Attacker();
            Positions[4, 0] = new Attacker();
            Positions[5, 0] = new Attacker();
            Positions[4, 1] = new Attacker();
                                          
            Positions[2, 4] = new Defender();
            Positions[3, 4] = new Defender();
            Positions[5, 4] = new Defender();
            Positions[6, 4] = new Defender();
            Positions[4, 2] = new Defender();
            Positions[4, 3] = new Defender();
            Positions[4, 5] = new Defender();
            Positions[4, 6] = new Defender();

            Positions[4, 4] = new DefenderKing();
        }
    }
}