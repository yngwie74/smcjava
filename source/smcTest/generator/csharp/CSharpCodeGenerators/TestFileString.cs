namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System;
    using System.IO;

    using SMC;

    public class TestFileString
    {
        #region Constants

        private const string TurnSMContents = "Context TurnStyleContext     // the name of the context class\n"
        + "FSMName TurnStyle            // the name of the FSM to create\n"
        + "Initial Locked               // the name of the initial state\n"
        + "                             // for C# output\n"
        + "pragma namespace TurnStyleExample\n"
        + "{\n"
        + "    Locked\n"
        + "    {\n"
        + "        Coin     Unlocked    Unlock\n"
        + "        Pass     Locked      Alarm\n"
        + "    }\n"
        + "    Unlocked\n"
        + "    {\n"
        + "	    Coin    Unlocked    Thankyou\n"
        + "	    Pass    Locked      Lock\n"
        + "    }\n"
        + "}\n";

        #endregion

        #region Fields

        private static Lazy<TestFileString> OurInstance = new Lazy<TestFileString>();

        private string fileContents;

        #endregion

        #region Constructors & Destructors

        public TestFileString()
        {
            var fileName = CreateSMFile();

            this.fileContents = CompilerFor(fileName).Execute();

            DeleteSMFile(fileName);
        }

        #endregion

        #region Public Properties

        public static TestFileString Instance => OurInstance.Value;

        public string FileContents => this.fileContents;

        #endregion

        #region Methods

        private static void DeleteSMFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (IOException)
            {
            }
        }

        private static Smc CompilerFor(string fileName)
        {
            return new Smc(fileName)
            {
                FSMGeneratorName = "SMC.Generator.CSharp.SMCSharpGenerator"
            };
        }

        private static string CreateSMFile()
        {
            var fileName = Path.GetTempFileName();
            new FileInfo(fileName).Attributes |= FileAttributes.Temporary;
            File.AppendAllText(fileName, TurnSMContents, System.Text.Encoding.ASCII);
            return fileName;
        }

        #endregion
    }
}