namespace SMC.Generator.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SMC.Generator.CSharp.CSharpCodeGenerators;

    public class CSharpCodeGeneratorBuilder
    {
        #region Fields

        private static Type[] CSharpCodeGenerators = new Type[]
        {
            typeof(InitialComments),
            typeof(NamespaceStatement),
            typeof(UsingStatements),
            typeof(FSMClass)
        };

        private static Type[] CSharpFSMCodeGenerators = new Type[]
        {
            typeof(FSMConstructor),
            typeof(FSMAccessors),
            typeof(FSMEvents)
        };

        private static Type[] CSharpFSMClassesCodeGenerators = new Type[]
        {
            typeof(FSMBaseState),
            typeof(FSMStateClasses)
        };

        public static readonly CSharpCodeGeneratorBuilder Instance = new CSharpCodeGeneratorBuilder();

        #endregion

        #region Constructors & Destructors

        private CSharpCodeGeneratorBuilder()
        {
        }

        #endregion

        #region Public Methods

        public IEnumerable<CSharpCodeGenerator> StateMachineGenerators() => Instantiate(CSharpFSMCodeGenerators);

        public IEnumerable<CSharpCodeGenerator> StateGenerators() => Instantiate(CSharpFSMClassesCodeGenerators);

        public IEnumerable<CSharpCodeGenerator> TopLevelGenerators() => Instantiate(CSharpCodeGenerators);

        #endregion

        #region Methods

        private IEnumerable<CSharpCodeGenerator> Instantiate(Type[] types)
        {
            return types
                .Select(Activator.CreateInstance)
                .Cast<CSharpCodeGenerator>()
                .ToList();
        }

        #endregion
    }
}