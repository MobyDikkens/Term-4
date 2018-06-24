using System.Windows.Media;

namespace Gomoku.Model
{
    public static class PlayersInfo
    {
        public const byte Empty = 0;
        public const byte AI = 1;
        public const byte Player = 2;

        private static SolidColorBrush _aiColor = Brushes.Black;
        private static SolidColorBrush _playerColor = Brushes.Gray;

        public static SolidColorBrush GetColor(byte item)
        {
            switch(item)
            {
                case AI:
                    {
                        return _aiColor;
                    }
                case Player:
                    {
                        return _playerColor;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public static byte Inverse(byte player)
        {
            if(player == AI)
            {
                return Player;
            }
            return AI;
        }

    }
}
