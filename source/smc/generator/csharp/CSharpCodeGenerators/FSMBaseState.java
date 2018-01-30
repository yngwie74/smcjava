package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;

import java.util.*;

public class FSMBaseState   extends  CSharpCodeGenerator
{
    public String generateCode(SMCSharpGenerator gen)
    {
        StringBuffer buff = new StringBuffer();
        buff.append(printSeparator(1));
        buff.append("/// <summary>\n");
        buff.append("/// This is the base State class\n");
        buff.append("/// </summary>\n");
        buff.append("public abstract class State\n");
        buff.append("{\n");
        buff.append("    #region Public Properties\n");
        buff.append("\n") ;
        buff.append("    public abstract string Name { get; }\n");
        buff.append("\n") ;
        buff.append("    #endregion\n");
        buff.append("\n") ;

        buff.append("    #region Default Event Functions\n");
        buff.append("\n") ;

        HashSet events = gen.getStateMap().getEvents();
        Iterator evi = events.iterator();
        while( evi.hasNext() )
        {
            String evName = (String)evi.next();

            buff.append("    public virtual void " + createMethodName(evName) + "(" +gen.getStateMap().getName() + " name)\n");
			buff.append("    {\n");
            if(gen.usesExceptions(gen.getStateMap()) )
            {
                buff.append("        throw new " + gen.getStateMap().getExceptionName() + "(\"" +
                        evName + "\", name.CurrentStateName);\n" );
            }
            else
            {
                buff.append("        name." + gen.getStateMap().getErrorFunctionName() + "(\"" +
                        evName + "\", name.CurrentState);\n" );
            }
			buff.append("    }\n" );
            buff.append("\n");
        }

        buff.append("    #endregion\n");
        buff.append("}\n");
        buff.append("\n");
        return buff.toString();
    }
}
