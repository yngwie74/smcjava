namespace SMC.Parser
{
    using System.Collections.Generic;

    using SMC.Builder;

    /// <summary>
    /// Error manager for the parser
    /// </summary>
    public class ParserErrorManager : FSMBuilderErrorManager
    {
        #region Fields

        private readonly IList<string> errors;

        #endregion

        #region Constructors & Destructors

        public ParserErrorManager()
        {
            this.errors = new List<string>();
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Errors => this.errors;

        #endregion

        #region Public Methods

        public void Error(SyntaxLocation loc, string s) => this.errors.Add(s);

        public void Error(string s) => this.errors.Add(s);

        #endregion
    }
}