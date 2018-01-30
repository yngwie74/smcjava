package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;
import smc.generator.csharp.CSharpCodeGeneratorBuilder;

import java.util.List;

public class FSMClass extends CSharpCodeGenerator
{
    public String generateCode(SMCSharpGenerator gen)
    {
        StringBuffer buff = new StringBuffer();
        String fsmName = gen.getStateMap().getName();
        buff.append(printSeparator(0));
        buff.append("/// <summary>\n");
        buff.append("/// This is the Finite State Machine class\n");
        buff.append("/// <summary>\n");
        buff.append("public class " + fsmName + " : " + gen.getStateMap().getContextName() + "\n");
        buff.append("{\n");

        buff.append("    #region Fields\n");
        buff.append("\n");
        buff.append("    private State currentState;\n");
        buff.append("    private static string version = \"" + gen.getStateMap().getVersion() + "\";\n");
        buff.append("\n");
        buff.append("    #endregion\n");
        buff.append("\n");

        try
        {
            List generators = CSharpCodeGeneratorBuilder.cSharpCode.cSharpFSMInstances();
            for(int i = 0; i !=generators.size(); i++)
            {
				System.out.println(generators.get(i).getClass());
                CSharpCodeGenerator code = (CSharpCodeGenerator)generators.get(i);
                buff.append(code.generateCode(gen));
            }
        }
        catch(Exception e )
        {}

        buff.append("}\n");
        buff.append("\n");
        
        try
        {
            List generators = CSharpCodeGeneratorBuilder.cSharpCode.cSharpFSMClassesInstances();
            for(int i = 0 ; i !=generators.size();i++)
            {
                CSharpCodeGenerator code = (CSharpCodeGenerator)generators.get(i);
                buff.append(code.generateCode(gen));
            }
        }
        catch(Exception e )
        {}

        return buff.toString();
    }
}
