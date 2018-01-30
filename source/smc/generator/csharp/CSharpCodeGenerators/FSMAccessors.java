package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;
import smc.fsmrep.ConcreteState;
import java.util.List;

public class FSMAccessors extends CSharpCodeGenerator
{
    public String generateCode(SMCSharpGenerator gen)
    {
        StringBuffer buff = new StringBuffer();

        buff.append("    #region Public Properties\n");
        buff.append("\n");
        buff.append("    public string Version => version;\n");
        buff.append("\n");
        buff.append("    public string CurrentStateName => this.currentState.Name;\n");
        buff.append("\n");
        buff.append("    public State CurrentState\n");
        buff.append("    {\n");
        buff.append("        get { return this.currentState; }\n");
        buff.append("        set { this.currentState = value; }\n");
        buff.append("    }\n");
        buff.append("\n");

        List states = gen.getConcreteStates();
        for(int i = 0; i!=states.size();i++)
        {
            ConcreteState cs = (ConcreteState)states.get(i);
            buff.append("    public State " + createMethodName( cs ) + " => this." + createStateFieldName( cs ) + ";\n");
        }
        buff.append("\n");
        buff.append("    #endregion\n");
        buff.append("\n");

        return buff.toString();
    }
}
