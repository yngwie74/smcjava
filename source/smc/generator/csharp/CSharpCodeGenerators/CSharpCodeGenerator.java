package smc.generator.csharp.CSharpCodeGenerators;

import smc.generator.csharp.SMCSharpGenerator;
import smc.fsmrep.State;

public abstract class CSharpCodeGenerator
{
    public abstract String generateCode(SMCSharpGenerator gen);

    public  String printSeparator( int level )
    {
        return  "";
    }

    public String createStateFieldName( State s )
    {
        StringBuffer buff = new StringBuffer(s.getName());
		buff.append("State");
        if( buff.length() > 0 )
            buff.setCharAt(0, Character.toLowerCase( buff.charAt(0) ));
        return( buff.toString() );
    }

    public String createMethodName( State s )
    {
        StringBuffer buff = new StringBuffer( s.getName() );
        if( buff.length() > 0 )
            buff.setCharAt(0, Character.toUpperCase( buff.charAt(0) ));
        return( buff.toString() );
    }

    public String createMethodName( String event )
    {
        StringBuffer buff = new StringBuffer( event );
        if( buff.length() > 0 )
            buff.setCharAt(0, Character.toUpperCase( buff.charAt(0) ));
        return( buff.toString() );
    }
}
