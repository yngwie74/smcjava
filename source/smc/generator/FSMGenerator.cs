namespace SMC.Generator
{
    using System.Collections.Generic;
    using System.Linq;

    using SMC.FsmRep;

    using static System.IO.Path;

    public abstract class FSMGenerator
    {
        #region Constructors & Destructors

        public FSMGenerator()
        {
            this.FilePrefix = "";
            this.InputFileName = "";
        }

        #endregion

        #region Public Properties

        public string InputFileName { get; private set; }

        public string FilePrefix { get; private set; }

        public StateMap StateMap { get; private set; }

        public IEnumerable<ConcreteState> ConcreteStates => this.StateMap.OrderedStates.OfType<ConcreteState>();

        #endregion

        #region Public Methods

        public void FSMInit(StateMap map, string fileName)
        {
            this.StateMap = map;
            this.InputFileName = fileName;
            this.FilePrefix = GetFilePrefix(this.InputFileName);
        }

        public abstract void Initialize();

        public abstract string Generate();

        /// <summary>
        /// generate a hierarchy for the given state
        /// the order of states in the hierarchy is from the super state
        /// to the substate with the last element being the state s.
        /// </summary>
        public void GetStateHierarchy(IList<State> hierarchy, State s)
        {
            if (s is SubState)
            {
                var ss = (SubState)s;
                GetStateHierarchy(hierarchy, ss.SuperState);
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
        public void GetUnsharedHierarchy(IList<State> h1, IList<State> h2, State s1, State s2)
        {
            GetStateHierarchy(h1, s1);
            GetStateHierarchy(h2, s2);

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
        /// and returns just the prefix.
        /// </summary>
        /// <example>
        /// GetFilePrefix("c:/smc/draw.sm") => "draw"
        /// </example>
        private string GetFilePrefix(string theFileName) => GetFileNameWithoutExtension(theFileName);

        #endregion
    }
}