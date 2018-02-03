namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using NUnit.Framework;

    using SMC.Builder;
    using SMC.Generator.CSharp;

    public abstract class CSharpCodeGeneratorTest<T>
        where T : CSharpCodeGenerator, new()
    {
        #region Constants

        protected const string Indent = "    ";

        #endregion

        #region Fields

        protected T gen;

        #endregion

        #region Public Methods

        [SetUp]
        public void SetUp() => this.gen = new T();

        #endregion

        #region Methods

        protected string GenerateUsing(FSMRepresentationBuilder fsmbld)
        {
            var fsm = InitWithBuilder(fsmbld);
            return this.gen.GenerateCode(fsm);
        }

        protected static SMCSharpGenerator InitWithBuilder(FSMRepresentationBuilder fsmbld)
        {
            var retval = new SMCSharpGenerator();
            retval.FSMInit(fsmbld.StateMap, "fileName");
            retval.Initialize();
            return retval;
        }

        #endregion
    }
}