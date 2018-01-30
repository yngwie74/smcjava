namespace smc.parser
{

    //--------------------------------------------
    // Parser Syntax Location Class
    //  This class is used to represent the location of a syntactic element
    //  of the state machine being built.
    //

    using smc.builder;

    public class ParserSyntaxLocation : SyntaxLocation
    {
        private string itsFileName;
        private int itsLineNumber;

        public ParserSyntaxLocation(string theFileName, int theLineNumber)
        {
            itsFileName = theFileName;
            itsLineNumber = theLineNumber;
        }

        public override string ToString()
        {
            string s = itsFileName + " line " + itsLineNumber + ":";
            return s;
        }
    }
}

