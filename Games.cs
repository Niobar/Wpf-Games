namespace Games
{
    public abstract class State
    {
        internal abstract State Clone();
    }
    public abstract class Game
    {
        public Judge judge;
        public void Start(Player[] players)
        {
            State state = judge.SetStartState(players);
            while (judge.IsNotEnd(state))
			{
				Player player;
            while ((player = judge.NextPlayer(state)) != null)
            {
                State nextState;
                do
                {
                   nextState = player.NextAction(state);
                } while (judge.StateAllowed(nextState));
            }				
			}

        }
    }

    public abstract class Judge
    {
        public abstract State SetStartState(Player[] players);
        public abstract bool IsNotEnd(State game);
        public abstract bool StateAllowed(State action);
        internal abstract Player NextPlayer(State state);
    }

    public abstract class Player
    {
        public State NextAction(State state)
        {
            bool pass;
            State nextState = state.Clone();
            do
            {
                pass = NextMove(nextState);
            } while (!pass);
            return nextState;
        }

        internal abstract bool NextMove(State nextState);
    }
}