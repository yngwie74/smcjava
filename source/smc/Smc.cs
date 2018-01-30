namespace SMC
{
    using System;
    using System.IO;

    using SMC.Builder;
    using SMC.Generator;
    using SMC.Generator.CSharp;
    using SMC.parser;

    public class Smc
    {
        #region Constructors & Destructors

        public Smc(string inputFilename)
        {
            this.InputFilename = inputFilename;
            this.FSMGeneratorName = string.Empty;
        }

        #endregion

        #region Public Properties

        public string InputFilename { get; private set; }

        public string FSMGeneratorName { get; set; }

        #endregion

        #region Public Methods

        public string Execute()
        {
            if (string.IsNullOrEmpty(this.InputFilename))
            {
                throw new InvalidOperationException("InputFilename not set.");
            }

            try
            {
                return ProcessInputFile();
            }
            catch (IOException ex)
            {
                var message = $"Cannot read state machine definition file: {this.InputFilename}.";
                throw new InvalidOperationException(message, ex);
            }
            catch (Exception ex)
            {
                var message = $"Aborting due to invalid state machine file: {this.InputFilename}.";
                throw new InvalidOperationException(message, ex);
            }
        }

        #endregion

        #region Methods

        private string ProcessInputFile()
        {
            var fsmbld = new FSMRepresentationBuilder();
            var parser = new FSMParser(fsmbld, this.InputFilename);
            var status = parser.parse();
            if (status == true)
            {
                return GenerateCode(fsmbld, parser);
            }
            return "";
        }

        private string GenerateCode(FSMRepresentationBuilder fsmbld, FSMParser parser)
        {
            var sm = fsmbld.StateMap;

            if (this.FSMGeneratorName.Length == 0)
            {
                this.FSMGeneratorName = parser.getFSMGeneratorName();
                if (this.FSMGeneratorName.Length == 0)
                {
                    Console.WriteLine("Using default C# Generator.");
                    this.FSMGeneratorName = typeof(SMCSharpGenerator).FullName;
                }
            }

            try
            {
                var generator = (FSMGenerator)Activator.CreateInstance(Type.GetType(this.FSMGeneratorName));
                generator.FSMInit(sm, this.InputFilename);

                try
                {
                    generator.Initialize();
                    return generator.Generate();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
            }
            catch (Exception cnf)
            {
                Console.WriteLine($"{this.FSMGeneratorName} is not a valid FSMGenerator");
                Console.WriteLine("Aborting due to invalid generator.");
                Console.WriteLine(cnf.StackTrace);
            }

            return "";
        }

        #endregion
    }
}