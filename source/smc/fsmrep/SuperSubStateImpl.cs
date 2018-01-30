namespace smc.fsmrep
{
    public class SuperSubStateImpl : StateImpl, SubState, State, SuperState
    {
        #region Fields

        private SuperState itsSuperState;

        #endregion

        #region Constructors & Destructors

        public SuperSubStateImpl(string str, SuperState ss)
            : base(str)
        {
            this.itsSuperState = ss;
        }

        #endregion

        #region Public Methods

        public virtual SuperState getSuperState()
        {
            return this.itsSuperState;
        }

        #endregion
    }
}