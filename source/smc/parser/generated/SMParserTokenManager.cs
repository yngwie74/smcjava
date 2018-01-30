/* Generated By:CSharpCC: Do not edit this line. SMParserTokenManager.cs */
using System;
using smc.parser.iface;

public  class SMParserTokenManager : SMParserConstants {
  public  System.IO.TextWriter debugStream = Console.Out;
  public  void SetDebugStream(System.IO.TextWriter ds) { debugStream = ds; }
private int mccStopAtPos(int pos, int kind)
{
   mccmatchedKind = kind;
   mccmatchedPos = pos;
   return pos + 1;
}
private int mccMoveStringLiteralDfa0_0()
{
   switch((int)curChar) {
      case 9:
         mccmatchedKind = 2;
         return mccMoveNfa_0(2, 0);
      case 10:
         mccmatchedKind = 3;
         return mccMoveNfa_0(2, 0);
      case 13:
         mccmatchedKind = 4;
         return mccMoveStringLiteralDfa1_0(32L);
      case 32:
         mccmatchedKind = 1;
         return mccMoveNfa_0(2, 0);
      case 40:
         mccmatchedKind = 18;
         return mccMoveNfa_0(2, 0);
      case 41:
         mccmatchedKind = 19;
         return mccMoveNfa_0(2, 0);
      case 42:
         mccmatchedKind = 21;
         return mccMoveNfa_0(2, 0);
      case 58:
         mccmatchedKind = 20;
         return mccMoveNfa_0(2, 0);
      case 60:
         mccmatchedKind = 22;
         return mccMoveNfa_0(2, 0);
      case 62:
         mccmatchedKind = 23;
         return mccMoveNfa_0(2, 0);
      case 67:
         return mccMoveStringLiteralDfa1_0(256L);
      case 69:
         return mccMoveStringLiteralDfa1_0(512L);
      case 70:
         return mccMoveStringLiteralDfa1_0(2176L);
      case 73:
         return mccMoveStringLiteralDfa1_0(1024L);
      case 99:
         return mccMoveStringLiteralDfa1_0(256L);
      case 101:
         return mccMoveStringLiteralDfa1_0(512L);
      case 102:
         return mccMoveStringLiteralDfa1_0(2176L);
      case 105:
         return mccMoveStringLiteralDfa1_0(1024L);
      case 123:
         mccmatchedKind = 16;
         return mccMoveNfa_0(2, 0);
      case 125:
         mccmatchedKind = 17;
         return mccMoveNfa_0(2, 0);
      default :
         return mccMoveNfa_0(2, 0);
   }
}
private int mccMoveStringLiteralDfa1_0(long active0)
{
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 0);
   }
   switch((int)curChar) {
      case 10:
         if ((active0 & 32L) != 0L)
         {
            mccmatchedKind = 5;
            mccmatchedPos = 1;
         }
         break;
      case 78:
         return mccMoveStringLiteralDfa2_0(active0, 1024L);
      case 79:
         return mccMoveStringLiteralDfa2_0(active0, 256L);
      case 83:
         return mccMoveStringLiteralDfa2_0(active0, 2176L);
      case 88:
         return mccMoveStringLiteralDfa2_0(active0, 512L);
      case 110:
         return mccMoveStringLiteralDfa2_0(active0, 1024L);
      case 111:
         return mccMoveStringLiteralDfa2_0(active0, 256L);
      case 115:
         return mccMoveStringLiteralDfa2_0(active0, 2176L);
      case 120:
         return mccMoveStringLiteralDfa2_0(active0, 512L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 1);
}
private int mccMoveStringLiteralDfa2_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 1);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 1);
   }
   switch((int)curChar) {
      case 67:
         return mccMoveStringLiteralDfa3_0(active0, 512L);
      case 73:
         return mccMoveStringLiteralDfa3_0(active0, 1024L);
      case 77:
         return mccMoveStringLiteralDfa3_0(active0, 2176L);
      case 78:
         return mccMoveStringLiteralDfa3_0(active0, 256L);
      case 99:
         return mccMoveStringLiteralDfa3_0(active0, 512L);
      case 105:
         return mccMoveStringLiteralDfa3_0(active0, 1024L);
      case 109:
         return mccMoveStringLiteralDfa3_0(active0, 2176L);
      case 110:
         return mccMoveStringLiteralDfa3_0(active0, 256L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 2);
}
private int mccMoveStringLiteralDfa3_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 2);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 2);
   }
   switch((int)curChar) {
      case 69:
         return mccMoveStringLiteralDfa4_0(active0, 512L);
      case 71:
         return mccMoveStringLiteralDfa4_0(active0, 2048L);
      case 78:
         return mccMoveStringLiteralDfa4_0(active0, 128L);
      case 84:
         return mccMoveStringLiteralDfa4_0(active0, 1280L);
      case 101:
         return mccMoveStringLiteralDfa4_0(active0, 512L);
      case 103:
         return mccMoveStringLiteralDfa4_0(active0, 2048L);
      case 110:
         return mccMoveStringLiteralDfa4_0(active0, 128L);
      case 116:
         return mccMoveStringLiteralDfa4_0(active0, 1280L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 3);
}
private int mccMoveStringLiteralDfa4_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 3);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 3);
   }
   switch((int)curChar) {
      case 65:
         return mccMoveStringLiteralDfa5_0(active0, 128L);
      case 69:
         return mccMoveStringLiteralDfa5_0(active0, 2304L);
      case 73:
         return mccMoveStringLiteralDfa5_0(active0, 1024L);
      case 80:
         return mccMoveStringLiteralDfa5_0(active0, 512L);
      case 97:
         return mccMoveStringLiteralDfa5_0(active0, 128L);
      case 101:
         return mccMoveStringLiteralDfa5_0(active0, 2304L);
      case 105:
         return mccMoveStringLiteralDfa5_0(active0, 1024L);
      case 112:
         return mccMoveStringLiteralDfa5_0(active0, 512L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 4);
}
private int mccMoveStringLiteralDfa5_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 4);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 4);
   }
   switch((int)curChar) {
      case 65:
         return mccMoveStringLiteralDfa6_0(active0, 1024L);
      case 77:
         return mccMoveStringLiteralDfa6_0(active0, 128L);
      case 78:
         return mccMoveStringLiteralDfa6_0(active0, 2048L);
      case 84:
         return mccMoveStringLiteralDfa6_0(active0, 512L);
      case 88:
         return mccMoveStringLiteralDfa6_0(active0, 256L);
      case 97:
         return mccMoveStringLiteralDfa6_0(active0, 1024L);
      case 109:
         return mccMoveStringLiteralDfa6_0(active0, 128L);
      case 110:
         return mccMoveStringLiteralDfa6_0(active0, 2048L);
      case 116:
         return mccMoveStringLiteralDfa6_0(active0, 512L);
      case 120:
         return mccMoveStringLiteralDfa6_0(active0, 256L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 5);
}
private int mccMoveStringLiteralDfa6_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 5);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 5);
   }
   switch((int)curChar) {
      case 69:
         if ((active0 & 128L) != 0L)
         {
            mccmatchedKind = 7;
            mccmatchedPos = 6;
         }
         return mccMoveStringLiteralDfa7_0(active0, 2048L);
      case 73:
         return mccMoveStringLiteralDfa7_0(active0, 512L);
      case 76:
         if ((active0 & 1024L) != 0L)
         {
            mccmatchedKind = 10;
            mccmatchedPos = 6;
         }
         break;
      case 84:
         if ((active0 & 256L) != 0L)
         {
            mccmatchedKind = 8;
            mccmatchedPos = 6;
         }
         break;
      case 101:
         if ((active0 & 128L) != 0L)
         {
            mccmatchedKind = 7;
            mccmatchedPos = 6;
         }
         return mccMoveStringLiteralDfa7_0(active0, 2048L);
      case 105:
         return mccMoveStringLiteralDfa7_0(active0, 512L);
      case 108:
         if ((active0 & 1024L) != 0L)
         {
            mccmatchedKind = 10;
            mccmatchedPos = 6;
         }
         break;
      case 116:
         if ((active0 & 256L) != 0L)
         {
            mccmatchedKind = 8;
            mccmatchedPos = 6;
         }
         break;
      default :
         break;
   }
   return mccMoveNfa_0(2, 6);
}
private int mccMoveStringLiteralDfa7_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 6);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 6);
   }
   switch((int)curChar) {
      case 79:
         return mccMoveStringLiteralDfa8_0(active0, 512L);
      case 82:
         return mccMoveStringLiteralDfa8_0(active0, 2048L);
      case 111:
         return mccMoveStringLiteralDfa8_0(active0, 512L);
      case 114:
         return mccMoveStringLiteralDfa8_0(active0, 2048L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 7);
}
private int mccMoveStringLiteralDfa8_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 7);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 7);
   }
   switch((int)curChar) {
      case 65:
         return mccMoveStringLiteralDfa9_0(active0, 2048L);
      case 78:
         if ((active0 & 512L) != 0L)
         {
            mccmatchedKind = 9;
            mccmatchedPos = 8;
         }
         break;
      case 97:
         return mccMoveStringLiteralDfa9_0(active0, 2048L);
      case 110:
         if ((active0 & 512L) != 0L)
         {
            mccmatchedKind = 9;
            mccmatchedPos = 8;
         }
         break;
      default :
         break;
   }
   return mccMoveNfa_0(2, 8);
}
private int mccMoveStringLiteralDfa9_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 8);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 8);
   }
   switch((int)curChar) {
      case 84:
         return mccMoveStringLiteralDfa10_0(active0, 2048L);
      case 116:
         return mccMoveStringLiteralDfa10_0(active0, 2048L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 9);
}
private int mccMoveStringLiteralDfa10_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 9);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 9);
   }
   switch((int)curChar) {
      case 79:
         return mccMoveStringLiteralDfa11_0(active0, 2048L);
      case 111:
         return mccMoveStringLiteralDfa11_0(active0, 2048L);
      default :
         break;
   }
   return mccMoveNfa_0(2, 10);
}
private int mccMoveStringLiteralDfa11_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return mccMoveNfa_0(2, 10);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) {
   return mccMoveNfa_0(2, 10);
   }
   switch((int)curChar) {
      case 82:
         if ((active0 & 2048L) != 0L)
         {
            mccmatchedKind = 11;
            mccmatchedPos = 11;
         }
         break;
      case 114:
         if ((active0 & 2048L) != 0L)
         {
            mccmatchedKind = 11;
            mccmatchedPos = 11;
         }
         break;
      default :
         break;
   }
   return mccMoveNfa_0(2, 11);
}
private void mccCheckNAdd(int state)
{
   if (mccrounds[state] != mccround)
   {
      mccstateSet[mccnewStateCnt++] = state;
      mccrounds[state] = mccround;
   }
}
private void mccAddStates(int start, int end)
{
   do {
      mccstateSet[mccnewStateCnt++] = mccnextStates[start];
   } while (start++ != end);
}
private void mccCheckNAddTwoStates(int state1, int state2)
{
   mccCheckNAdd(state1);
   mccCheckNAdd(state2);
}
private void mccCheckNAddStates(int start, int end)
{
   do {
      mccCheckNAdd(mccnextStates[start]);
   } while (start++ != end);
}
private void mccCheckNAddStates(int start)
{
   mccCheckNAdd(mccnextStates[start]);
   mccCheckNAdd(mccnextStates[start + 1]);
}
static readonly long[] mccbitVec0 = {
   0L, 0L, -1L, -1L
};
private int mccMoveNfa_0(int startState, int curPos)
{
   int strKind = mccmatchedKind;
   int strPos = mccmatchedPos;
   int seenUpto = curPos + 1;
   input_stream.Backup(seenUpto);
   try { curChar = input_stream.ReadChar(); }
   catch(System.IO.IOException) { throw new Exception("Internal Error"); }
   curPos = 0;
   int[] nextStates;
   int startsAt = 0;
   mccnewStateCnt = 27;
   int i = 1;
   mccstateSet[0] = startState;
   int j, kind = Int32.MaxValue;
   for (;;)
   {
      if (++mccround == Int32.MaxValue)
         ReInitRounds();
      if (curChar < 64)
      {
         long l = 1L << curChar;
         do
         {
            switch(mccstateSet[--i])
            {
               case 2:
                  if (curChar == 47)
                     mccstateSet[mccnewStateCnt++] = 0;
                  break;
               case 0:
                  if (curChar != 47)
                     break;
                  if (kind > 6)
                     kind = 6;
                  mccCheckNAdd(1);
                  break;
               case 1:
                  if ((-9217 & l) == 0L)
                     break;
                  if (kind > 6)
                     kind = 6;
                  mccCheckNAdd(1);
                  break;
               case 4:
                  if ((-9217 & l) != 0L)
                     mccAddStates(0, 2);
                  break;
               case 5:
                  if ((9216 & l) != 0L && kind > 12)
                     kind = 12;
                  break;
               case 6:
                  if (curChar == 10 && kind > 12)
                     kind = 12;
                  break;
               case 7:
                  if (curChar == 13)
                     mccstateSet[mccnewStateCnt++] = 6;
                  break;
               case 14:
                  if ((-9217 & l) != 0L)
                     mccAddStates(3, 5);
                  break;
               case 15:
                  if ((9216 & l) != 0L && kind > 13)
                     kind = 13;
                  break;
               case 16:
                  if (curChar == 10 && kind > 13)
                     kind = 13;
                  break;
               case 17:
                  if (curChar == 13)
                     mccstateSet[mccnewStateCnt++] = 16;
                  break;
               case 25:
                  if ((287948901175001088 & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  mccstateSet[mccnewStateCnt++] = 25;
                  break;
               case 26:
                  if ((288019269919178752 & l) == 0L)
                     break;
                  if (kind > 15)
                     kind = 15;
                  mccstateSet[mccnewStateCnt++] = 26;
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      else if (curChar < 128)
      {
         long l = 1L << (curChar & 63);
         do
         {
            switch(mccstateSet[--i])
            {
               case 2:
                  if ((576460743847706622 & l) != 0L)
                  {
                     if (kind > 14)
                        kind = 14;
                     mccCheckNAddTwoStates(25, 26);
                  }
                  if ((18014398513676288 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 22;
                  else if ((281474976776192 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 11;
                  break;
               case 1:
                  if (kind > 6)
                     kind = 6;
                  mccstateSet[mccnewStateCnt++] = 1;
                  break;
               case 3:
                  if ((8589934594 & l) != 0L)
                     mccCheckNAddStates(0, 2);
                  break;
               case 4:
                  mccCheckNAddStates(0, 2);
                  break;
               case 8:
                  if ((35184372097024 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 3;
                  break;
               case 9:
                  if ((549755814016 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 8;
                  break;
               case 10:
                  if ((8589934594 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 9;
                  break;
               case 11:
                  if ((1125899907104768 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 10;
                  break;
               case 12:
                  if ((281474976776192 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 11;
                  break;
               case 13:
                  if ((70368744194048 & l) != 0L)
                     mccCheckNAddStates(3, 5);
                  break;
               case 14:
                  mccCheckNAddStates(3, 5);
                  break;
               case 18:
                  if ((140737488388096 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 13;
                  break;
               case 19:
                  if ((2199023256064 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 18;
                  break;
               case 20:
                  if ((2251799814209536 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 19;
                  break;
               case 21:
                  if ((1125899907104768 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 20;
                  break;
               case 22:
                  if ((137438953504 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 21;
                  break;
               case 23:
                  if ((18014398513676288 & l) != 0L)
                     mccstateSet[mccnewStateCnt++] = 22;
                  break;
               case 24:
                  if ((576460743847706622 & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  mccCheckNAddTwoStates(25, 26);
                  break;
               case 25:
                  if ((576460745995190270 & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  mccCheckNAdd(25);
                  break;
               case 26:
                  if ((576460745995190270 & l) == 0L)
                     break;
                  if (kind > 15)
                     kind = 15;
                  mccCheckNAdd(26);
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      else
      {
         int i2 = (curChar & 0xff) >> 6;
         long l2 = 1L << (curChar & 63);
         do
         {
            switch(mccstateSet[--i])
            {
               case 1:
                  if ((mccbitVec0[i2] & l2) == 0L)
                     break;
                  if (kind > 6)
                     kind = 6;
                  mccstateSet[mccnewStateCnt++] = 1;
                  break;
               case 4:
                  if ((mccbitVec0[i2] & l2) != 0L)
                     mccAddStates(0, 2);
                  break;
               case 14:
                  if ((mccbitVec0[i2] & l2) != 0L)
                     mccAddStates(3, 5);
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      if (kind != Int32.MaxValue)
      {
         mccmatchedKind = kind;
         mccmatchedPos = curPos;
         kind = Int32.MaxValue;
      }
      ++curPos;
      if ((i = mccnewStateCnt) == (startsAt = 27 - (mccnewStateCnt = startsAt)))
         break;
      try { curChar = input_stream.ReadChar(); }
      catch(System.IO.IOException) { break; }
   }
   if (mccmatchedPos > strPos)
      return curPos;

   int toRet = Math.Max(curPos, seenUpto);

   if (curPos < toRet)
      for (i = toRet - Math.Min(curPos, seenUpto); i-- > 0; )
         try { curChar = input_stream.ReadChar(); }
         catch(System.IO.IOException) { throw new Exception("Internal Error : Please send a bug report."); }

   if (mccmatchedPos < strPos)
   {
      mccmatchedKind = strKind;
      mccmatchedPos = strPos;
   }
   else if (mccmatchedPos == strPos && mccmatchedKind > strKind)
      mccmatchedKind = strKind;

   return toRet;
}
static readonly int[] mccnextStates = {
   4, 5, 7, 14, 15, 17, 
};
public static readonly string[] mccstrLiteralImages = {
"", null, null, null, null, null, null, null, null, null, null, null, null, 
null, null, null, "{", "}", "(", ")", ":", "*", "<", ">", };
public static readonly string[] lexStateNames = {
   "DEFAULT", 
};
static readonly long[] mcctoToken = {
   16777089, 
};
static readonly long[] mcctoSkip = {
   126, 
};
protected SimpleCharStream input_stream;
private readonly int[] mccrounds = new int[27];
private readonly int[] mccstateSet = new int[54];
protected char curChar;
public SMParserTokenManager(SimpleCharStream stream) {
   if (input_stream != null)
      throw new TokenMgrError("ERROR: Second call to constructor of static lexer. You must use ReInit() to initialize the static variables.", TokenMgrError.StaticLexerError);
   input_stream = stream;
}
public SMParserTokenManager(SimpleCharStream stream, int lexState)
   : this(stream) {
   SwitchTo(lexState);
}
public void ReInit(SimpleCharStream stream) {
   mccmatchedPos = mccnewStateCnt = 0;
   curLexState = defaultLexState;
   input_stream = stream;
   ReInitRounds();
}
private void ReInitRounds()
{
   int i;
   mccround = -2147483647;
   for (i = 27; i-- > 0;)
      mccrounds[i] = Int32.MinValue;
}
public void ReInit(SimpleCharStream stream, int lexState) {
   ReInit(stream);
   SwitchTo(lexState);
}
public void SwitchTo(int lexState) {
   if (lexState >= 1 || lexState < 0)
      throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.InvalidLexicalState);
   else
      curLexState = lexState;
}

protected Token mccFillToken()
{
   Token t = Token.NewToken(mccmatchedKind);
   t.kind = mccmatchedKind;
   string im = mccstrLiteralImages[mccmatchedKind];
   t.image = (im == null) ? input_stream.GetImage() : im;
   t.beginLine = input_stream.BeginLine;
   t.beginColumn = input_stream.BeginColumn;
   t.endLine = input_stream.EndLine;
   t.endColumn = input_stream.EndColumn;
   return t;
}

int curLexState = 0;
int defaultLexState = 0;
int mccnewStateCnt;
int mccround;
int mccmatchedPos;
int mccmatchedKind;

public Token GetNextToken() {
  int kind;
  Token specialToken = null;
  Token matchedToken;
  int curPos = 0;

for (;;) {
   try {
      curChar = input_stream.BeginToken();
   } catch(System.IO.IOException) {
      mccmatchedKind = 0;
      matchedToken = mccFillToken();
      return matchedToken;
   }

   mccmatchedKind = Int32.MaxValue;
   mccmatchedPos = 0;
   curPos = mccMoveStringLiteralDfa0_0();
   if (mccmatchedKind != Int32.MaxValue) {
      if (mccmatchedPos + 1 < curPos)
         input_stream.Backup(curPos - mccmatchedPos - 1);
      if ((mcctoToken[mccmatchedKind >> 6] & (1L << (mccmatchedKind & 63))) != 0L) {
         matchedToken = mccFillToken();
         return matchedToken;
      }
      else
      {
         goto EOFLoop;
      }
   }
   int error_line = input_stream.EndLine;
   int error_column = input_stream.EndColumn;
   string error_after = null;
   bool EOFSeen = false;
   try { input_stream.ReadChar(); input_stream.Backup(1); }
   catch (System.IO.IOException) {
      EOFSeen = true;
      error_after = curPos <= 1 ? "" : input_stream.GetImage();
      if (curChar == '\n' || curChar == '\r') {
         error_line++;
         error_column = 0;
      } else
         error_column++;
   }
   if (!EOFSeen) {
      input_stream.Backup(1);
      error_after = curPos <= 1 ? "" : input_stream.GetImage();
   }
   throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LexicalError);
EOFLoop: ;
  }
}

}
