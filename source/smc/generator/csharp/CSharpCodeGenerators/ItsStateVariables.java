package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;
import smc.fsmrep.ConcreteState;
import java.util.List;

public class ItsStateVariables extends CSharpCodeGenerator
{
    public String generateCode(SMCSharpGenerator gen)
    {
        StringBuffer buff = new StringBuffer();

        buff.append("    #region Instance Variables For Each State\n");
        buff.append("\n");

        List states = gen.getConcreteStates();
        for(int i=0; i != states.size(); i++)
        {
            ConcreteState cs = (ConcreteState)states.get(i);
            buff.append("    private " + createMethodName( cs ) + " " + createStateFieldName( cs ) + ";\n");
        }

        buff.append("\n");
        buff.append("    #endregion\n");
        buff.append("\n");

        return buff.toString();
    }
}
