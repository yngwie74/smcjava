/* Generated By:CSharpCC: Do not edit this line. SMParser.cs */
using System;

using SMC.parser.iface;

public class SMParser : SMParserConstants {
    private SMParserInterface pi;
    private int transactionAction = 1;
    private int entryAction = 2;
    private int exitAction = 3;

  public void parseFSM(SMParserInterface s) {
      pi = s;
    parseHeader();
    mcc_consume_token(16);
    parseStates();
    mcc_consume_token(17);
    mcc_consume_token(0);
        pi.processFSM();
  }

  public void parseHeader() {
   Token t;
    while (true) {
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case FSMNAME:
      case CONTEXT:
      case EXCEPTION:
      case INITIAL:
      case GENERATOR:
      case PRAGMA:
      case VERSION:
        ;
        break;
      default:
        mcc_la1[0] = mcc_gen;
        goto label_1;
      }
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case FSMNAME:
        mcc_consume_token(FSMNAME);
        t = mcc_consume_token(ID);
        pi.setFSMName( t.image );
        break;
      case CONTEXT:
        mcc_consume_token(CONTEXT);
        t = mcc_consume_token(ID);
        pi.setContextName( t.image );
        break;
      case EXCEPTION:
        mcc_consume_token(EXCEPTION);
        t = mcc_consume_token(ID);
        pi.setException( t.image );
        break;
      case INITIAL:
        mcc_consume_token(INITIAL);
        t = mcc_consume_token(ID);
        pi.setInitialState( t.image );
        break;
      case GENERATOR:
        mcc_consume_token(GENERATOR);
        switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
        case ID:
          t = mcc_consume_token(ID);
          break;
        case GENERATOR_ID:
          t = mcc_consume_token(GENERATOR_ID);
          break;
        default:
          mcc_la1[1] = mcc_gen;
          mcc_consume_token(-1);
          throw new ParseException();
        }
        pi.setFSMGenerator( t.image );
        break;
      case VERSION:
        t = mcc_consume_token(VERSION);
        string v = t.image;
        try
        {
            v = v.Substring(7);
            v = v.Trim();
            pi.setVersion( v );
        }
        catch (System.IndexOutOfRangeException e)
        { pi.setVersion(""); }
        break;
      case PRAGMA:
        t = mcc_consume_token(PRAGMA);
        string p = t.image;
        try
        {
            p = p.Substring(6);
            p = p.Trim();
            pi.addPragma( p );
        }
        catch (System.IndexOutOfRangeException e)
        {}
        break;
      default:
        mcc_la1[2] = mcc_gen;
        mcc_consume_token(-1);
        throw new ParseException();
      }
    }label_1: ;
    

  }

  public void parseStates() {
    while (true) {
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case ID:
      case 18:
        ;
        break;
      default:
        mcc_la1[3] = mcc_gen;
        goto label_2;
      }
      stateDefinition();
      while (true) {
        switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
        case 22:
        case 23:
          ;
          break;
        default:
          mcc_la1[4] = mcc_gen;
          goto label_3;
        }
        entryOrExitAction();
      }label_3: ;
      
      transitionSet();
    }label_2: ;
    

  }

  public void stateDefinition() {
  Token state1 = null;
  Token state2 = null;
    switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
    case 18:
      mcc_consume_token(18);
      state1 = mcc_consume_token(ID);
      mcc_consume_token(19);
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case 20:
        mcc_consume_token(20);
        state2 = mcc_consume_token(ID);
        break;
      default:
        mcc_la1[5] = mcc_gen;
        ;
        break;
      }
        if( state2 == null )
            pi.addSuperState( state1.image, state1.beginLine);
        else
            pi.addSuperSubState( state1.image, state2.image, state1.beginLine);
      break;
    case ID:
      state1 = mcc_consume_token(ID);
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case 20:
        mcc_consume_token(20);
        state2 = mcc_consume_token(ID);
        break;
      default:
        mcc_la1[6] = mcc_gen;
        ;
        break;
      }
        if( state2 == null )
            pi.addState( state1.image, state1.beginLine);
        else
            pi.addSubState( state1.image, state2.image, state1.beginLine);
      break;
    default:
      mcc_la1[7] = mcc_gen;
      mcc_consume_token(-1);
      throw new ParseException();
    }
  }

  public void transitionSet() {
    mcc_consume_token(16);
    while (true) {
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case ID:
        ;
        break;
      default:
        mcc_la1[8] = mcc_gen;
        goto label_4;
      }
      transition();
    }label_4: ;
    
    mcc_consume_token(17);
  }

  public void transition() {
  Token _event;
  Token state;
    if (mcc_2_1(2)) {
      _event = mcc_consume_token(ID);
      state = mcc_consume_token(ID);
        pi.addTransition( _event.image, state.image, _event.beginLine );
    } else {
      switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
      case ID:
        _event = mcc_consume_token(ID);
        mcc_consume_token(21);
        pi.addInternalTransition( _event.image, _event.beginLine );
        break;
      default:
        mcc_la1[9] = mcc_gen;
        mcc_consume_token(-1);
        throw new ParseException();
      }
    }
      actionSet(transactionAction);
  }

  public void entryOrExitAction() {
    switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
    case 22:
      mcc_consume_token(22);
        actionSet(entryAction);
      break;
    case 23:
      mcc_consume_token(23);
       actionSet(exitAction);
      break;
    default:
      mcc_la1[10] = mcc_gen;
      mcc_consume_token(-1);
      throw new ParseException();
    }
  }

  public void actionSet(int type) {
    switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
    case ID:
      action(type);
      break;
    case 16:
      mcc_consume_token(16);
      while (true) {
        switch ((mcc_ntk==-1)?mcc_mntk():mcc_ntk) {
        case ID:
          ;
          break;
        default:
          mcc_la1[11] = mcc_gen;
          goto label_5;
        }
        action(type);
      }label_5: ;
      
      mcc_consume_token(17);
      break;
    default:
      mcc_la1[12] = mcc_gen;
      mcc_consume_token(-1);
      throw new ParseException();
    }
  }

  public void action(int type) {
    Token t;
    t = mcc_consume_token(ID);
        if( type == transactionAction )
            pi.addAction(t.image, t.beginLine);
        else if( type == entryAction )
            pi.addEntryAction( t.image, t.beginLine);
        else if( type == exitAction )
            pi.addExitAction( t.image, t.beginLine);
  }

  private bool mcc_2_1(int xla) {
    mcc_la = xla; mcc_lastpos = mcc_scanpos = token;
    try { return !mcc_3_1(); }
    catch(LookaheadSuccess) { return true; }
    finally { mcc_save(0, xla); }
  }

  private bool mcc_3_1() {
    if (mcc_scan_token(ID)) return true;
    if (mcc_scan_token(ID)) return true;
    return false;
  }

  static private bool mcc_initialized_once = false;
  public SMParserTokenManager token_source;
  static SimpleCharStream mcc_input_stream;
  public Token token, mcc_nt;
  static private int mcc_ntk;
  static private Token mcc_scanpos, mcc_lastpos;
  static private int mcc_la;
  public bool lookingAhead = false;
  static private bool mcc_semLA;
  static private int mcc_gen;
  static private int[] mcc_la1 = new int[13];
  static private int[] mcc_la1_0;
  static SMParser() {
      mcc_gla1_0();
   }
   private static void mcc_gla1_0() {
      mcc_la1_0 = new int[] {16256,49152,16256,278528,12582912,1048576,1048576,278528,16384,16384,12582912,16384,81920,};
   }
  static private MccCalls[] mcc_2_rtns = new MccCalls[1];
  static private bool mcc_rescan = false;
  static private int mcc_gc = 0;

  public SMParser(System.IO.Stream stream) {
    if (mcc_initialized_once) {
      Console.Out.WriteLine("ERROR: Second call to constructor of static parser.  You must");
      Console.Out.WriteLine("       either use ReInit() or set the CSharpCC option STATIC to false");
      Console.Out.WriteLine("       during parser generation.");
      throw new Exception();
    }
    mcc_initialized_once = true;
    mcc_input_stream = new SimpleCharStream(stream, 1, 1);
    token_source = new SMParserTokenManager(mcc_input_stream);
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  public void ReInit(System.IO.Stream stream) {
    mcc_input_stream.ReInit(stream, 1, 1);
    token_source.ReInit(mcc_input_stream);
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  public SMParser(System.IO.TextReader stream) {
    if (mcc_initialized_once) {
      Console.Out.WriteLine("ERROR: Second call to constructor of static parser.  You must");
      Console.Out.WriteLine("       either use ReInit() or set the CSharpCC option STATIC to false");
      Console.Out.WriteLine("       during parser generation.");
      throw new Exception();
    }
    mcc_initialized_once = true;
    mcc_input_stream = new SimpleCharStream(stream, 1, 1);
    token_source = new SMParserTokenManager(mcc_input_stream);
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  public void ReInit(System.IO.TextReader stream) {
    mcc_input_stream.ReInit(stream, 1, 1);
    token_source.ReInit(mcc_input_stream);
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  public SMParser(SMParserTokenManager tm) {
    if (mcc_initialized_once) {
      Console.Out.WriteLine("ERROR: Second call to constructor of static parser.  You must");
      Console.Out.WriteLine("       either use ReInit() or set the CSharpCC option STATIC to false");
      Console.Out.WriteLine("       during parser generation.");
      throw new Exception();
    }
    mcc_initialized_once = true;
    token_source = tm;
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  public void ReInit(SMParserTokenManager tm) {
    token_source = tm;
    token = new Token();
    mcc_ntk = -1;
    mcc_gen = 0;
    for (int i = 0; i < 13; i++) mcc_la1[i] = -1;
    for (int i = 0; i < mcc_2_rtns.Length; i++) mcc_2_rtns[i] = new MccCalls();
  }

  private Token mcc_consume_token(int kind) {
    Token oldToken = null;
    if ((oldToken = token).next != null) token = token.next;
    else token = token.next = token_source.GetNextToken();
    mcc_ntk = -1;
    if (token.kind == kind) {
      mcc_gen++;
      if (++mcc_gc > 100) {
        mcc_gc = 0;
        for (int i = 0; i < mcc_2_rtns.Length; i++) {
          MccCalls c = mcc_2_rtns[i];
          while (c != null) {
            if (c.gen < mcc_gen) c.first = null;
            c = c.next;
          }
        }
      }
      return token;
    }
    token = oldToken;
    mcc_kind = kind;
    throw GenerateParseException();
  }

  private class LookaheadSuccess : System.Exception { }
  static private LookaheadSuccess mcc_ls = new LookaheadSuccess();
  private bool mcc_scan_token(int kind) {
    if (mcc_scanpos == mcc_lastpos) {
      mcc_la--;
      if (mcc_scanpos.next == null) {
        mcc_lastpos = mcc_scanpos = mcc_scanpos.next = token_source.GetNextToken();
      } else {
        mcc_lastpos = mcc_scanpos = mcc_scanpos.next;
      }
    } else {
      mcc_scanpos = mcc_scanpos.next;
    }
    if (mcc_rescan) {
      int i = 0; Token tok = token;
      while (tok != null && tok != mcc_scanpos) { i++; tok = tok.next; }
      if (tok != null) mcc_add_error_token(kind, i);
    }
    if (mcc_scanpos.kind != kind) return true;
    if (mcc_la == 0 && mcc_scanpos == mcc_lastpos) throw mcc_ls;
    return false;
  }

  public Token GetNextToken() {
    if (token.next != null) token = token.next;
    else token = token.next = token_source.GetNextToken();
    mcc_ntk = -1;
    mcc_gen++;
    return token;
  }

  public Token GetToken(int index) {
    Token t = lookingAhead ? mcc_scanpos : token;
    for (int i = 0; i < index; i++) {
      if (t.next != null) t = t.next;
      else t = t.next = token_source.GetNextToken();
    }
    return t;
  }

  private int mcc_mntk() {
    if ((mcc_nt=token.next) == null)
      return (mcc_ntk = (token.next=token_source.GetNextToken()).kind);
    else
      return (mcc_ntk = mcc_nt.kind);
  }

  static private System.Collections.ArrayList mcc_expentries = new System.Collections.ArrayList();
  static private int[] mcc_expentry;
  static private int mcc_kind = -1;
  static private int[] mcc_lasttokens = new int[100];
  static private int mcc_endpos;

  static private void mcc_add_error_token(int kind, int pos) {
    if (pos >= 100) return;
    if (pos == mcc_endpos + 1) {
      mcc_lasttokens[mcc_endpos++] = kind;
    } else if (mcc_endpos != 0) {
      mcc_expentry = new int[mcc_endpos];
      for (int i = 0; i < mcc_endpos; i++) {
        mcc_expentry[i] = mcc_lasttokens[i];
      }
      bool exists = false;
      for (System.Collections.IEnumerator e = mcc_expentries.GetEnumerator(); e.MoveNext();) {
        int[] oldentry = (int[])e.Current;
        if (oldentry.Length == mcc_expentry.Length) {
          exists = true;
          for (int i = 0; i < mcc_expentry.Length; i++) {
            if (oldentry[i] != mcc_expentry[i]) {
              exists = false;
              break;
            }
          }
          if (exists) break;
        }
      }
      if (!exists) mcc_expentries.Add(mcc_expentry);
      if (pos != 0) mcc_lasttokens[(mcc_endpos = pos) - 1] = kind;
    }
  }

  public ParseException GenerateParseException() {
    mcc_expentries.Clear();
    bool[] la1tokens = new bool[24];
    for (int i = 0; i < 24; i++) {
      la1tokens[i] = false;
    }
    if (mcc_kind >= 0) {
      la1tokens[mcc_kind] = true;
      mcc_kind = -1;
    }
    for (int i = 0; i < 13; i++) {
      if (mcc_la1[i] == mcc_gen) {
        for (int j = 0; j < 32; j++) {
          if ((mcc_la1_0[i] & (1<<j)) != 0) {
            la1tokens[j] = true;
          }
        }
      }
    }
    for (int i = 0; i < 24; i++) {
      if (la1tokens[i]) {
        mcc_expentry = new int[1];
        mcc_expentry[0] = i;
        mcc_expentries.Add(mcc_expentry);
      }
    }
    mcc_endpos = 0;
    mcc_rescan_token();
    mcc_add_error_token(0, 0);
    int[][] exptokseq = new int[mcc_expentries.Count][];
    for (int i = 0; i < mcc_expentries.Count; i++) {
      exptokseq[i] = (int[])mcc_expentries[i];
    }
    return new ParseException(token, exptokseq, tokenImage);
  }

  public void enable_tracing() {
  }

  public void disable_tracing() {
  }

  private void mcc_rescan_token() {
    mcc_rescan = true;
    for (int i = 0; i < 1; i++) {
      MccCalls p = mcc_2_rtns[i];
      do {
        if (p.gen > mcc_gen) {
          mcc_la = p.arg; mcc_lastpos = mcc_scanpos = p.first;
          switch (i) {
            case 0: mcc_3_1(); break;
          }
        }
        p = p.next;
      } while (p != null);
    }
    mcc_rescan = false;
  }

  private void mcc_save(int index, int xla) {
    MccCalls p = mcc_2_rtns[index];
    while (p.gen > mcc_gen) {
      if (p.next == null) { p = p.next = new MccCalls(); break; }
      p = p.next;
    }
    p.gen = mcc_gen + xla - mcc_la; p.first = token; p.arg = xla;
  }

  class MccCalls {
    public int gen;
    public Token first;
    public int arg;
    public MccCalls next;
  }

}
