namespace smc.builder
{
    using java.lang;

    public abstract class FSMBuilder : Object
    {

        private FSMBuilderErrorManager itsErrorManager;



        public FSMBuilder()
        {
        }



        public abstract void setName(string str);

        public abstract void setContextName(string str);

        public abstract void setException(string str);

        public abstract void setInitialState(string str);

        public abstract void setVersion(string str);

        public abstract void addPragma(string str);

        public abstract void addSuperSubState(string str1, string str2, SyntaxLocation sl);

        public abstract void addSuperState(string str, SyntaxLocation sl);

        public abstract void addSubState(string str1, string str2, SyntaxLocation sl);

        public abstract void addState(string str, SyntaxLocation sl);

        public abstract void addTransition(string str1, string str2, SyntaxLocation sl);

        public abstract void addInternalTransition(string str, SyntaxLocation sl);

        public abstract void addAction(string str);

        public abstract void addEntryAction(string str);

        public abstract void addExitAction(string str);

        public abstract bool build();

        public virtual void setErrorManager(FSMBuilderErrorManager fsmbem)
        {
            this.itsErrorManager = fsmbem;
        }

        public virtual void error(string str)
        {
            if (this.itsErrorManager != null)
            {
                this.itsErrorManager.error(str);
            }
        }

        public virtual void error(SyntaxLocation sl, string str)
        {
            if (this.itsErrorManager != null)
            {
                this.itsErrorManager.error(sl, str);
            }
        }

    }
}