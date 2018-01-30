namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SMC.FsmRep;
    using SMC.Generator.CSharp;

    public class FSMStateClasses : CSharpCodeGenerator
    {
        #region Constants

        private const string ArgName = "name";

        #endregion

        #region Fields

        private SMCSharpGenerator gen;

        #endregion

        #region Public Methods

        public override string GenerateCode(SMCSharpGenerator gen)
        {
            this.gen = gen;
            var buff = new StringBuilder();
            var concreteStates = gen.ConcreteStates;

            foreach (var cs in concreteStates)
            {
                buff.AppendLine("/// <summary>")
                    .AppendLine($"/// Handles the {cs.Name} State and its events")
                    .AppendLine("/// </summary>")
                    .AppendLine($"public class {ClassNameFor(cs)} : State")
                    .AppendLine("{")
                    .AppendLine($"    public override string Name => \"{cs.Name}\";");

                gen.SourceState = cs;
                gen.ClearOverRiddenEvents();

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
            var transitions = s.Transitions;
            foreach (var t in transitions)
            {
                var _event = t.Event;
                if (!this.gen.IsOverRiddenEvent(_event))
                {
                    this.gen.AddOverRiddenEvent(_event);
                    var noResponse = true;

                    buff.AppendLine();
                    buff.AppendLine($"    public override void {CreateMethodName(_event)}({this.gen.StateMap.Name} {ArgName})");
                    buff.AppendLine("    {");

                    var actions = t.Actions;
                    if (actions.Any())
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
                buff.Append(generateTransitions(ss.SuperState));
            }

            return buff.ToString();
        }

        private string generateStateChange(ExternalTransition et)
        {
            var buff = new StringBuilder()
                .AppendLine("        // change the state")
                .AppendLine($"        {ArgName}.CurrentState = State.{CreateMethodName(et.NextState)};");

            var oldHierarchy = new List<State>();
            var newHierarchy = new List<State>();

            this.gen.GetUnsharedHierarchy(oldHierarchy, newHierarchy, this.gen.SourceState, et.NextState);

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
            var actions = state.ExitActions;
            if (actions.Any() == false)
            {
                return;
            }

            buff.AppendLine()
                .AppendLine($"  // {qualifier} functions for: {state.Name}");

            foreach (var action in actions)
            {
                buff.AppendLine($"      {ArgName}.{action}();");
            }
        }

        #endregion
    }
}
