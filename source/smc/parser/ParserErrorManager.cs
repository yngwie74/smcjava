namespace smc.parser
{
    using System.Collections.Generic;

    using smc.builder;

    //----------------------------------------------
    // Name
    //  ParserErrorManager
    //
    // Description
    //  Error manager for the parser
    //
    public class ParserErrorManager : FSMBuilderErrorManager
    {
        private IList<string> errors;
        private bool isOutputing;
        public ParserErrorManager()
        {
            isOutputing=true;
            errors = new List<string>();
        }
        public ParserErrorManager(bool isOutputing)
        {
            this.isOutputing=isOutputing;
            errors = new List<string>();
        }
        public void error(SyntaxLocation loc, string s)
        {
            if( loc is ParserSyntaxLocation )
            {
                ParserSyntaxLocation l = (ParserSyntaxLocation)loc;
                if(isOutputing)
                    System.Console.WriteLine( l.ToString() + s );
                errors.Add(s);
            }
        }
        public void error(string s)
        {
            if(isOutputing)
                System.Console.WriteLine( s );
            errors.Add(s);
        }
        public IList<string> getErrors()
        {
            return errors;
        }
    }
}
