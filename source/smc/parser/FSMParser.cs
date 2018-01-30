namespace smc.parser
{
    using System.IO;
    using smc.builder;
    using smc.parser.iface;


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
            itsBuilder.setErrorManager(itsErrorManager);
        }

        public void setFSMGenerator(string s)
        { itsFSMGeneratorName = s; }

        public string getFSMGeneratorName()
        { return itsFSMGeneratorName; }

        public void setFSMName(string s)
        { itsBuilder.setName(s); }

        public void setContextName(string s)
        { itsBuilder.setContextName(s); }

        public void setException(string s)
        { itsBuilder.setException(s); }

        public void setInitialState(string s)
        { itsBuilder.setInitialState(s); }

        public void setVersion(string s)
        { itsBuilder.setVersion(s); }

        public void addPragma(string s)
        { itsBuilder.addPragma(s); }

        public void addSuperSubState(string theName, string theSuperState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addSuperSubState(theName, theSuperState, l);
        }

        public void addSuperState(string theName, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addSuperState(theName, l);
        }

        public void addSubState(string theName, string theSuperState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addSubState(theName, theSuperState, l);
        }

        public void addState(string theName, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addState(theName, l);
        }

        public void addTransition(string theEvent, string theNextState, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addTransition(theEvent, theNextState, l);
        }

        public void addInternalTransition(string theEvent, int theLineNumber)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsBuilder.addInternalTransition(theEvent, l);
        }

        public void addAction(string theAction, int theLineNumber)
        { itsBuilder.addAction(theAction); }

        public void addEntryAction(string theAction, int theLineNumber)
        { itsBuilder.addEntryAction(theAction); }

        public void addExitAction(string theAction, int theLineNumber)
        { itsBuilder.addExitAction(theAction); }

        public void syntaxError(int theLineNumber, string theMessage)
        {
            ParserSyntaxLocation l = new ParserSyntaxLocation(itsFileName, theLineNumber);
            itsErrorManager.error(l, theMessage);
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
                    status = itsBuilder.build();
                }
                catch (ParseException pe)
                {
                    printSyntaxError(pe);
                    System.Console.WriteLine("Aborting due to syntax errors.");
                }
            }
            catch (FileNotFoundException fe)
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