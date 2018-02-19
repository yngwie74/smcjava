namespace SMC.Generator.CSharp.CSharpCodeGenerators
{
    using System;
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

            foreach (var cs in this.gen.ConcreteStates)
            {
                GenerateConcreteStateClass(cs, buff);
            }

            var length = Environment.NewLine.Length;
            buff.Remove(buff.Length - length - 1, length);

            return buff.ToString();
        }

        #endregion

        #region Methods

        private void GenerateConcreteStateClass(ConcreteState cs, StringBuilder buff)
        {
            ClassHeader(cs, buff);
            PublicProperties(cs, buff);

            this.gen.SourceState = cs;
            this.gen.ClearOverRiddenEvents();

            GenerateTransitionsOf(cs, buff);

            CloseClass(buff);
        }

        private void ClassHeader(ConcreteState cs, StringBuilder buff)
        {
            buff.AppendLine("/// <summary>")
                .AppendLine($"/// This class handles the \"{cs.Name}\" state and its events")
                .AppendLine("/// </summary>")
                .AppendLine($"internal class {ClassNameFor(cs)} : State")
                .AppendLine("{");
        }

        private static void PublicProperties(ConcreteState cs, StringBuilder buff) => buff.AppendLine($"    public override string Name => \"{cs.Name}\";");

        private StringBuilder GenerateTransitionsOf(State s, StringBuilder buff)
        {
            var transitions = s.Transitions;
            foreach (var t in transitions)
            {
                GenerateTransitionFor(t, buff);
            }

            if (s is SubState ss)
            {
                GenerateTransitionsOf(ss.SuperState, buff);
            }

            return buff;
        }

        private void GenerateTransitionFor(Transition t, StringBuilder buff)
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

                if (t is ExternalTransition et)
                {
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

        private static void CloseClass(StringBuilder buff) => buff.AppendLine("}").AppendLine();

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
            // TODO add unit-test coverage for entry/exit state actions.
            var actions = GetActions(qualifier, state);

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

        private static IEnumerable<string> GetActions(string qualifier, State state)
        {
            switch (qualifier)
            {
                case "Entry":
                    return state.EntryActions;
                case "Exit":
                    return state.ExitActions;
                default:
                    throw new ArgumentOutOfRangeException(nameof(qualifier), qualifier, "Solo se permite Entry/Exit");
            }
        }

        #endregion
    }
}