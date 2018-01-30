namespace smc.generator
{
    using System.Collections.Generic;

    using smc.fsmrep;

    public abstract class FSMGenerator
    {
        #region Fields

        private StateMap itsStateMap;
        private string itsFilePrefix;
        private string itsFileName;
        private string itsDirectory;

        #endregion

        #region Constructors & Destructors

        public FSMGenerator()
        {
            itsFilePrefix = "";
            itsFileName = "";
        }

        #endregion

        #region Public Methods

        public void FSMInit(StateMap map, string fileName, string directory)
        {
            itsStateMap = map;
            itsFileName = fileName;
            itsDirectory = directory;
            itsFilePrefix = getFilePrefix(itsFileName);
        }

        public abstract void initialize();

        public abstract void generate();

        public abstract IEnumerable<string> getGeneratedFileNames();

        public string getInputFileName()
        {
            return itsFileName;
        }

        public string getFilePrefix()
        {
            return itsFilePrefix;
        }

        public string getDirectory()
        {
            return itsDirectory;
        }

        public StateMap getStateMap()
        {
            return itsStateMap;
        }

        public IList<ConcreteState> getConcreteStates()
        {
            var concreteStates = new List<ConcreteState>();
            var states = getStateMap().getOrderedStates();
            foreach (var s in states)
            {
                if (s is ConcreteState)
                {
                    ConcreteState cs = (ConcreteState)s;
                    concreteStates.Add(cs);
                }
            }
            return concreteStates;
        }

        /// <summary>
        /// generate a hierarchy for the given state
        /// the order of states in the hierarchy is from the super state
        /// to the substate with the last element being the state s.
        /// </summary>
        public void getStateHierarchy(IList<State> hierarchy, State s)
        {
            if (s is SubState)
            {
                var ss = (SubState)s;
                getStateHierarchy(hierarchy, ss.getSuperState());
            }
            hierarchy.Add(s);
        }

        /// <summary>
        /// generate a hierarchy for each state s1 snd s2 such that no member of the
        /// hierarchy of one state is a member of the heirarchy of the other - the 
        /// shared ancestors are not present in the vector hierarchy, only the 
        /// unshared ancestors of s1 are in the vector h1, and the unshared ancestors
        /// of s2 are in the vector h2
        /// </summary>
        public void getUnsharedHierarchy(IList<State> h1, IList<State> h2, State s1, State s2)
        {
            getStateHierarchy(h1, s1);
            getStateHierarchy(h2, s2);

            // Now prune the hierarchy from the top down, eliminating the
            // super states which are common to the hierarchy
            // Do not prune the last state since it is s1
            while (h1.Count > 1 && h2.Count > 1 &&
                   h1[0] == h2[0])
            {
                h1.RemoveAt(0);
                h2.RemoveAt(0);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Strips the path and the file extension from the file name
        /// and returns just the prefix. eg: c:/smc/draw.sm would return
        /// draw.
        /// </summary>
        private string getFilePrefix(string theFileName)
        {
            string retval = string.Copy(theFileName);
            int pos;
            // strip off leading directory path if any
            while ((pos = retval.IndexOf('/')) >= 0)
            {
                retval = retval.Substring(pos + 1);
            }

            while ((pos = retval.IndexOf('\\')) >= 0)
            {
                retval = retval.Substring(pos + 1);
            }

            while ((pos = retval.IndexOf(':')) >= 0)
            {
                retval = retval.Substring(pos + 1);
            }

            // Strip off the suffix.
            var dotPos = retval.IndexOf(".");
            if (dotPos >= 0)
            {
                retval = retval.Substring(0, dotPos);
            }

            return retval;
        }

        #endregion
    }
}
