package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;
import smc.fsmrep.ConcreteState;
import smc.fsmrep.State;

import java.util.Vector;
import java.util.Iterator;
import java.util.List;

public class FSMConstructor extends CSharpCodeGenerator
{
    public String generateCode(SMCSharpGenerator gen)
    {
        StringBuffer buff = new StringBuffer();
        buff.append("    #region Constructors & Destructors\n");
        buff.append("\n");
        buff.append("    public " + gen.getStateMap().getName() + "()\n");
        buff.append("    {\n");


        List states = gen.getConcreteStates();
        for(int i = 0; i!=states.size();i++)
        {
            ConcreteState cs = (ConcreteState)states.get(i);
            buff.append("        this." + createStateFieldName( cs ) + " = new " + createMethodName( cs ) + "();\n");
        }

        buff.append("\n");
        String iName = createStateFieldName(gen.getStateMap().getInitialState());
        buff.append("        this.currentState = this." + iName + ";\n") ;

        Vector initialHierarchy = new Vector();
        gen.getStateHierarchy( initialHierarchy,gen.getStateMap().getInitialState());

        Iterator i = initialHierarchy.iterator();
        while( i.hasNext() )
        {
            State newState = (State)i.next();
            Vector eactions = newState.getEntryActions();
            Iterator eai = eactions.iterator();

            if( eai.hasNext() )
            {
                buff.append("\n");
                buff.append( "        // Entry functions for: " + newState.getName() + "\n");
            }

            while( eai.hasNext() )
            {
                String action = (String)eai.next();
                buff.append( "        " + action + "();\n" );
            }
        }
        buff.append("    }\n");
        buff.append("\n");
        buff.append("    #endregion\n");
        buff.append("\n");

        return buff.toString();
    }
}
