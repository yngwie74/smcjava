namespace SMC.parser
{
    using System.IO;
    using SMC.Builder;
    using SMC.parser.iface;


    public class FSMParser : SMParserInterface
    {
        private FSMBuilder itsBuilder;
        private string itsFSMGeneratorName;
        private string itsFileName;
        private ParserErrorManager itsErrorManager = new ParserErrorManager();

        public FSMParser(FSMBuilder theBuilder, string theFileName)
        {
            itsBuilder = theBuilder;
            itsFileName = theFileName;
            itsFSMGeneratorName = "";
            itsBuilder.ErrorManager = itsErrorManager;
        }

        public void setFSMGenerator(string s)
        { itsFSMGeneratorName = s; }

        public string getFSMGeneratorName()
        { return itsFSMGeneratorName; }

        public void setFSMName(string s)
        { itsBuilder.SetName(s); }

        public void setContextName(string s)
        { itsBuilder.SetContextName(s); }

        public void setException(string s)
        { itsBuilder.SetException(s); }

        public void setInitialState(string s)
        { itsBuilder.SetInitialState(s); }

        public void setVersion(string s)
        { itsBuilder.SetVersion(s); }

        public void addPragma(string s)
        { itsBuilder.AddPragma(s); }

        public void addSuperSubState(string theName, string theSuperState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddSuperSubState(theName, theSuperState, l);
        }

        public void addSuperState(string theName, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddSuperState(theName, l);
        }

        public void addSubState(string theName, string theSuperState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddSubState(theName, theSuperState, l);
        }

        public void addState(string theName, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddState(theName, l);
        }

        public void addTransition(string theEvent, string theNextState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddTransition(theEvent, theNextState, l);
        }

        public void addInternalTransition(string theEvent, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.AddInternalTransition(theEvent, l);
        }

        public void addAction(string theAction, int theLineNumber)
        { itsBuilder.AddAction(theAction); }

        public void addEntryAction(string theAction, int theLineNumber)
        { itsBuilder.AddEntryAction(theAction); }

        public void addExitAction(string theAction, int theLineNumber)
        { itsBuilder.AddExitAction(theAction); }

        public void syntaxError(int theLineNumber, string theMessage)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsErrorManager.Error(l, theMessage);
        }

        public void processFSM()
        { }

        public bool parse()
        {
            bool status = false;
            try
            {
                FileStream ifile = new FileStream(itsFileName, FileMode.Open);
                SMParser parser = new SMParser(ifile);
                try
                {
                    parser.parseFSM(this);
                    status = itsBuilder.Build();
                }
                catch (ParseException pe)
                {
                    printSyntaxError(pe);
                    System.Console.WriteLine("Aborting due to syntax errors.");
                }
            }
            catch (FileNotFoundException)
            {
                System.Console.WriteLine("Could not open input file: " + itsFileName);
            }
            return status;
        }

        private void printSyntaxError(ParseException pe)
        {
            int prevLine = pe.currentToken.beginLine;
            int errLine = pe.currentToken.next.beginLine;

            System.Console.WriteLine("Syntax Error: " + itsFileName + " line " + errLine);

            if (prevLine != errLine)
                System.Console.WriteLine("   Unknown field \"" +
                                pe.currentToken.next.image + "\" in header");
            else
                System.Console.WriteLine("   Field \"" + pe.currentToken.image +
                  "\" has invalid value \"" + pe.currentToken.next.image + "\"");
        }
    }
}