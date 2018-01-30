namespace smc.generator.csharp.CSharpCodeGenerators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using smc.fsmrep;
    using smc.generator.csharp;

    public class FSMStateClasses : CSharpCodeGenerator
    {
        #region Constants

        private const string ArgName = "name";

        #endregion

        #region Fields

        private SMCSharpGenerator gen;

        #endregion

        #region Public Methods

        public override string generateCode(SMCSharpGenerator gen)
        {
            this.gen = gen;
            var buff = new StringBuilder();
            var concreteStates = gen.getConcreteStates();

            foreach (var cs in concreteStates)
            {
                buff.AppendLine("/// <summary>")
                    .AppendLine($"/// Handles the {cs.getName()} State and its events")
                    .AppendLine("/// </summary>")
                    .AppendLine($"public class {classNameFor(cs)} : State")
                    .AppendLine("{")
                    .AppendLine($"    public override string Name => \"{cs.getName()}\";");

                gen.setItsSourceState(cs);
                gen.clearItsOverRiddenEvents();

                buff.Append(generateTransitions(cs))
                    .AppendLine("}")
                    .AppendLine();
            }

            return buff.ToString();
        }

        #endregion

        #region Methods

        private string generateTransitions(State s)
        {
            var buff = new StringBuilder();
            var transitions = s.getTransitions();
            foreach (var t in transitions)
            {
                var _event = t.getEvent();
                if (this.gen.getItsOverriddenEvents().Contains(_event) == false)
                {
                    this.gen.getItsOverriddenEvents().Add(_event);
                    var noResponse = true;

                    buff.AppendLine();
                    buff.AppendLine($"    public override void {createMethodName(_event)}({this.gen.getStateMap().getName()} {ArgName})");
                    buff.AppendLine("    {");

                    var actions = t.getActions();
                    if (actions.Count > 0)
                    {
                        noResponse = false;
                    }

                    foreach (var aName in actions)
                    {
                        buff.AppendLine($"        {ArgName}.{aName}();");
                    }

                    if (t is ExternalTransition)
                    {
                        var et = (ExternalTransition)t;
                        buff.AppendLine();
                        noResponse = false;
                        buff.Append(generateStateChange(et));
                    }

                    if (noResponse == true)
                    {
                        buff.AppendLine("}");
                    }
                    else
                    {
                        buff.AppendLine("    }");
                    }
                }
            }

            if (s is SubState)
            {
                var ss = (SubState)s;
                buff.Append(generateTransitions(ss.getSuperState()));
            }

            return buff.ToString();
        }

        private string generateStateChange(ExternalTransition et)
        {
            var buff = new StringBuilder()
                .AppendLine("        // change the state")
                .AppendLine($"        {ArgName}.CurrentState = State.{createMethodName(et.getNextState())};");

            var oldHierarchy = new List<State>();
            var newHierarchy = new List<State>();

            this.gen.getUnsharedHierarchy(oldHierarchy, newHierarchy, this.gen.getItsSourceState(), et.getNextState());

            var n = oldHierarchy.Count;
            for (n--; n >= 0; n--)
            {
                AddStateActions("Exit", oldHierarchy[n], buff);
            }

            foreach (var newState in newHierarchy)
            {
                AddStateActions("Entry", newState, buff);
            }

            return buff.ToString();
        }

        private static void AddStateActions(string qualifier, State state, StringBuilder buff)
        {
            var actions = state.getExitActions();
            if (actions.Any() == false)
            {
                return;
            }

            buff.AppendLine()
                .AppendLine($"  // {qualifier} functions for: {state.getName()}");

            foreach (var action in actions)
            {
                buff.AppendLine($"      {ArgName}.{action}();");
            }
        }

        #endregion
    }
}
