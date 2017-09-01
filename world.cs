using System;
using System.Collections.Generic;
namespace Magus
{
    /*
    ---------------------------------------
        The Game-world
    ---------------------------------------
    */
    class World
    {

        private const int WORLD_X_MAX = 200;
        private const int WORLD_Y_MAX = 320;
        private short gGateX, gGateY;

        private object sprites;
        private object gCharacters = null;
        private object gThings = null;


        public World()
        {
            Initialize();
        }

        private void Initialize()
        {

        }

        protected List<Character> Opponents;
        protected List<Person> Players;

        public void AddCharacter(Character pc)
        {
            Opponents.Add(pc);
        }

        public void DisposeOpponents(Character pc)
        {
        }

        public void DisposeCharacter(Character c)
        {

        }


        public Character FindPlayer(int x, int y)
        {
            foreach (Character c in Players)
            {
                if (c.x == x && c.y == y && c.health <= 0)
                {
                    return c;
                }
            }
            return null;
        }
        public Character FindOpponent(int x, int y)
        {
            foreach (Character c in Opponents)
            {
                if (c.x == x && c.y == y && c.health <= 0)
                {
                    return c;
                }
            }
            return null;
        }

    }
}