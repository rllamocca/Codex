using System;

namespace Codex
{
    public static class Bases
    {
        public static readonly DateTime _TIMESTAMP = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime _ANVIZ = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc);

        public static readonly string[] _SUITS = new string[] { "♠", "♥", "♣", "♦", "Black JOKER", "Red JOKER" };
        public static readonly string[] _SQUAD = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    }
}
