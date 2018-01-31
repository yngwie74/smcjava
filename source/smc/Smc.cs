namespace SMC
{
    using System;
    using System.IO;

    using SMC.Builder;
    using SMC.FsmRep;
    using SMC.Generator;
    using SMC.Generator.CSharp;
    using SMC.Parser;

    public class Smc
    {
        private string fsmGeneratorName;
        #region Constructors & Destructors

        public Smc(string inputFilename)
            : this(inputFilename, "")
        {
        }

        public Smc(string inputFilename, string fsmGeneratorName)
        {
            this.InputFilename = !string.IsNullOrEmpty(inputFilename)
                ? inputFilename
                : throw new ArgumentNullException(nameof(inputFilename));

            this.FSMGeneratorName = fsmGeneratorName;
        }

        #endregion

        #region Public Properties

        public string InputFilename { get; private set; }

        public string FSMGeneratorName
        {
            get => this.fsmGeneratorName;
            set => this.fsmGeneratorName = value ?? throw new ArgumentNullException(nameof(value));
        }

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
            var status = parser.Parse();
            if (status == true)
            {
                return GenerateCode(fsmbld, parser);
            }
            return "";
        }

        private string GenerateCode(FSMRepresentationBuilder fsmbld, FSMParser parser)
        {
            var generator = TryGetCodeGenerator(parser, fsmbld.StateMap);
            return TryExecuteGenerator(generator);
        }

        private static string TryExecuteGenerator(FSMGenerator generator)
        {
            try
            {
                return generator.Generate();
            }
            catch (Exception ex)
            {
                throw new Exception("Aborting due to invalid generator.", ex);
            }
        }

        private FSMGenerator TryGetCodeGenerator(FSMParser parser, StateMap sm)
        {
            FSMGenerator retVal;

            var generator = GetRequestedGenerator(parser);

            if (generator.Type == null)
            {
                throw new Exception($"{generator} not found");
            }

            try
            {
                retVal = (FSMGenerator)Activator.CreateInstance(generator.Type);
                retVal.FSMInit(sm, this.InputFilename);
                retVal.Initialize();
            }
            catch (Exception ex)
            {
                throw new Exception($"{generator} is not a valid FSMGenerator", ex);
            }

            return retVal;
        }

        private RequestedGenerator GetRequestedGenerator(FSMParser parser)
        {
            if (!string.IsNullOrEmpty(this.FSMGeneratorName))
            {
                return new RequestedGenerator(this.FSMGeneratorName, "instance property");
            }

            if (!string.IsNullOrEmpty(parser.FSMGeneratorName))
            {
                return new RequestedGenerator(parser.FSMGeneratorName, this.InputFilename);
            }

            return new RequestedGenerator(GetDefaultGeneratorName(), "(default)");
        }

        private static string GetDefaultGeneratorName() => typeof(SMCSharpGenerator).FullName;

        #endregion

        private class RequestedGenerator
        {
            private readonly string typeName;
            private readonly string location;

            public RequestedGenerator(string typeName, string location)
            {
                this.typeName = typeName;
                this.location = location;
            }

            public Type Type => Type.GetType(this.typeName);

            public override string ToString() => $"FSMGenerator '{this.typeName} in {this.location}";
        }
    }
}