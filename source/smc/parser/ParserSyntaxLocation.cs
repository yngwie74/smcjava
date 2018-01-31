namespace SMC.Parser
{
    using SMC.Builder;

    /// <summary>
    /// This class is used to represent the location of a syntactic element
    /// of the state machine being built.
    /// </summary>
    public class ParserSyntaxLocation : SyntaxLocation
    {
        #region Fields

        private readonly string fileName;
        private readonly int lineNumber;

        #endregion

        #region Constructors & Destructors

        public ParserSyntaxLocation(string theFileName, int theLineNumber)
        {
            this.fileName = theFileName;
            this.lineNumber = theLineNumber;
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{this.fileName} line {this.lineNumber}:";

        #endregion
    }
}