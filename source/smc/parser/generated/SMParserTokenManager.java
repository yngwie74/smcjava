package smc.parser.generated;
/* Generated By:JavaCC: Do not edit this line. SMParserTokenManager.java */
import smc.parser.iface.SMParserInterface;

public class SMParserTokenManager implements SMParserConstants
{
static private final int jjStopAtPos(int pos, int kind)
{
   jjmatchedKind = kind;
   jjmatchedPos = pos;
   return pos + 1;
}
static private final int jjMoveStringLiteralDfa0_0()
{
   switch(curChar)
   {
      case 9:
         jjmatchedKind = 2;
         return jjMoveNfa_0(2, 0);
      case 10:
         jjmatchedKind = 3;
         return jjMoveNfa_0(2, 0);
      case 13:
         jjmatchedKind = 4;
         return jjMoveStringLiteralDfa1_0(0x20L);
      case 32:
         jjmatchedKind = 1;
         return jjMoveNfa_0(2, 0);
      case 40:
         jjmatchedKind = 18;
         return jjMoveNfa_0(2, 0);
      case 41:
         jjmatchedKind = 19;
         return jjMoveNfa_0(2, 0);
      case 42:
         jjmatchedKind = 21;
         return jjMoveNfa_0(2, 0);
      case 58:
         jjmatchedKind = 20;
         return jjMoveNfa_0(2, 0);
      case 60:
         jjmatchedKind = 22;
         return jjMoveNfa_0(2, 0);
      case 62:
         jjmatchedKind = 23;
         return jjMoveNfa_0(2, 0);
      case 67:
         return jjMoveStringLiteralDfa1_0(0x100L);
      case 69:
         return jjMoveStringLiteralDfa1_0(0x200L);
      case 70:
         return jjMoveStringLiteralDfa1_0(0x880L);
      case 73:
         return jjMoveStringLiteralDfa1_0(0x400L);
      case 99:
         return jjMoveStringLiteralDfa1_0(0x100L);
      case 101:
         return jjMoveStringLiteralDfa1_0(0x200L);
      case 102:
         return jjMoveStringLiteralDfa1_0(0x880L);
      case 105:
         return jjMoveStringLiteralDfa1_0(0x400L);
      case 123:
         jjmatchedKind = 16;
         return jjMoveNfa_0(2, 0);
      case 125:
         jjmatchedKind = 17;
         return jjMoveNfa_0(2, 0);
      default :
         return jjMoveNfa_0(2, 0);
   }
}
static private final int jjMoveStringLiteralDfa1_0(long active0)
{
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 0);
   }
   switch(curChar)
   {
      case 10:
         if ((active0 & 0x20L) != 0L)
         {
            jjmatchedKind = 5;
            jjmatchedPos = 1;
         }
         break;
      case 78:
         return jjMoveStringLiteralDfa2_0(active0, 0x400L);
      case 79:
         return jjMoveStringLiteralDfa2_0(active0, 0x100L);
      case 83:
         return jjMoveStringLiteralDfa2_0(active0, 0x880L);
      case 88:
         return jjMoveStringLiteralDfa2_0(active0, 0x200L);
      case 110:
         return jjMoveStringLiteralDfa2_0(active0, 0x400L);
      case 111:
         return jjMoveStringLiteralDfa2_0(active0, 0x100L);
      case 115:
         return jjMoveStringLiteralDfa2_0(active0, 0x880L);
      case 120:
         return jjMoveStringLiteralDfa2_0(active0, 0x200L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 1);
}
static private final int jjMoveStringLiteralDfa2_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 1);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 1);
   }
   switch(curChar)
   {
      case 67:
         return jjMoveStringLiteralDfa3_0(active0, 0x200L);
      case 73:
         return jjMoveStringLiteralDfa3_0(active0, 0x400L);
      case 77:
         return jjMoveStringLiteralDfa3_0(active0, 0x880L);
      case 78:
         return jjMoveStringLiteralDfa3_0(active0, 0x100L);
      case 99:
         return jjMoveStringLiteralDfa3_0(active0, 0x200L);
      case 105:
         return jjMoveStringLiteralDfa3_0(active0, 0x400L);
      case 109:
         return jjMoveStringLiteralDfa3_0(active0, 0x880L);
      case 110:
         return jjMoveStringLiteralDfa3_0(active0, 0x100L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 2);
}
static private final int jjMoveStringLiteralDfa3_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 2);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 2);
   }
   switch(curChar)
   {
      case 69:
         return jjMoveStringLiteralDfa4_0(active0, 0x200L);
      case 71:
         return jjMoveStringLiteralDfa4_0(active0, 0x800L);
      case 78:
         return jjMoveStringLiteralDfa4_0(active0, 0x80L);
      case 84:
         return jjMoveStringLiteralDfa4_0(active0, 0x500L);
      case 101:
         return jjMoveStringLiteralDfa4_0(active0, 0x200L);
      case 103:
         return jjMoveStringLiteralDfa4_0(active0, 0x800L);
      case 110:
         return jjMoveStringLiteralDfa4_0(active0, 0x80L);
      case 116:
         return jjMoveStringLiteralDfa4_0(active0, 0x500L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 3);
}
static private final int jjMoveStringLiteralDfa4_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 3);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 3);
   }
   switch(curChar)
   {
      case 65:
         return jjMoveStringLiteralDfa5_0(active0, 0x80L);
      case 69:
         return jjMoveStringLiteralDfa5_0(active0, 0x900L);
      case 73:
         return jjMoveStringLiteralDfa5_0(active0, 0x400L);
      case 80:
         return jjMoveStringLiteralDfa5_0(active0, 0x200L);
      case 97:
         return jjMoveStringLiteralDfa5_0(active0, 0x80L);
      case 101:
         return jjMoveStringLiteralDfa5_0(active0, 0x900L);
      case 105:
         return jjMoveStringLiteralDfa5_0(active0, 0x400L);
      case 112:
         return jjMoveStringLiteralDfa5_0(active0, 0x200L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 4);
}
static private final int jjMoveStringLiteralDfa5_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 4);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 4);
   }
   switch(curChar)
   {
      case 65:
         return jjMoveStringLiteralDfa6_0(active0, 0x400L);
      case 77:
         return jjMoveStringLiteralDfa6_0(active0, 0x80L);
      case 78:
         return jjMoveStringLiteralDfa6_0(active0, 0x800L);
      case 84:
         return jjMoveStringLiteralDfa6_0(active0, 0x200L);
      case 88:
         return jjMoveStringLiteralDfa6_0(active0, 0x100L);
      case 97:
         return jjMoveStringLiteralDfa6_0(active0, 0x400L);
      case 109:
         return jjMoveStringLiteralDfa6_0(active0, 0x80L);
      case 110:
         return jjMoveStringLiteralDfa6_0(active0, 0x800L);
      case 116:
         return jjMoveStringLiteralDfa6_0(active0, 0x200L);
      case 120:
         return jjMoveStringLiteralDfa6_0(active0, 0x100L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 5);
}
static private final int jjMoveStringLiteralDfa6_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 5);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 5);
   }
   switch(curChar)
   {
      case 69:
         if ((active0 & 0x80L) != 0L)
         {
            jjmatchedKind = 7;
            jjmatchedPos = 6;
         }
         return jjMoveStringLiteralDfa7_0(active0, 0x800L);
      case 73:
         return jjMoveStringLiteralDfa7_0(active0, 0x200L);
      case 76:
         if ((active0 & 0x400L) != 0L)
         {
            jjmatchedKind = 10;
            jjmatchedPos = 6;
         }
         break;
      case 84:
         if ((active0 & 0x100L) != 0L)
         {
            jjmatchedKind = 8;
            jjmatchedPos = 6;
         }
         break;
      case 101:
         if ((active0 & 0x80L) != 0L)
         {
            jjmatchedKind = 7;
            jjmatchedPos = 6;
         }
         return jjMoveStringLiteralDfa7_0(active0, 0x800L);
      case 105:
         return jjMoveStringLiteralDfa7_0(active0, 0x200L);
      case 108:
         if ((active0 & 0x400L) != 0L)
         {
            jjmatchedKind = 10;
            jjmatchedPos = 6;
         }
         break;
      case 116:
         if ((active0 & 0x100L) != 0L)
         {
            jjmatchedKind = 8;
            jjmatchedPos = 6;
         }
         break;
      default :
         break;
   }
   return jjMoveNfa_0(2, 6);
}
static private final int jjMoveStringLiteralDfa7_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 6);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 6);
   }
   switch(curChar)
   {
      case 79:
         return jjMoveStringLiteralDfa8_0(active0, 0x200L);
      case 82:
         return jjMoveStringLiteralDfa8_0(active0, 0x800L);
      case 111:
         return jjMoveStringLiteralDfa8_0(active0, 0x200L);
      case 114:
         return jjMoveStringLiteralDfa8_0(active0, 0x800L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 7);
}
static private final int jjMoveStringLiteralDfa8_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 7);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 7);
   }
   switch(curChar)
   {
      case 65:
         return jjMoveStringLiteralDfa9_0(active0, 0x800L);
      case 78:
         if ((active0 & 0x200L) != 0L)
         {
            jjmatchedKind = 9;
            jjmatchedPos = 8;
         }
         break;
      case 97:
         return jjMoveStringLiteralDfa9_0(active0, 0x800L);
      case 110:
         if ((active0 & 0x200L) != 0L)
         {
            jjmatchedKind = 9;
            jjmatchedPos = 8;
         }
         break;
      default :
         break;
   }
   return jjMoveNfa_0(2, 8);
}
static private final int jjMoveStringLiteralDfa9_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 8);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 8);
   }
   switch(curChar)
   {
      case 84:
         return jjMoveStringLiteralDfa10_0(active0, 0x800L);
      case 116:
         return jjMoveStringLiteralDfa10_0(active0, 0x800L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 9);
}
static private final int jjMoveStringLiteralDfa10_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 9);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 9);
   }
   switch(curChar)
   {
      case 79:
         return jjMoveStringLiteralDfa11_0(active0, 0x800L);
      case 111:
         return jjMoveStringLiteralDfa11_0(active0, 0x800L);
      default :
         break;
   }
   return jjMoveNfa_0(2, 10);
}
static private final int jjMoveStringLiteralDfa11_0(long old0, long active0)
{
   if (((active0 &= old0)) == 0L)
      return jjMoveNfa_0(2, 10);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) {
   return jjMoveNfa_0(2, 10);
   }
   switch(curChar)
   {
      case 82:
         if ((active0 & 0x800L) != 0L)
         {
            jjmatchedKind = 11;
            jjmatchedPos = 11;
         }
         break;
      case 114:
         if ((active0 & 0x800L) != 0L)
         {
            jjmatchedKind = 11;
            jjmatchedPos = 11;
         }
         break;
      default :
         break;
   }
   return jjMoveNfa_0(2, 11);
}
static private final void jjCheckNAdd(int state)
{
   if (jjrounds[state] != jjround)
   {
      jjstateSet[jjnewStateCnt++] = state;
      jjrounds[state] = jjround;
   }
}
static private final void jjAddStates(int start, int end)
{
   do {
      jjstateSet[jjnewStateCnt++] = jjnextStates[start];
   } while (start++ != end);
}
static private final void jjCheckNAddTwoStates(int state1, int state2)
{
   jjCheckNAdd(state1);
   jjCheckNAdd(state2);
}
static private final void jjCheckNAddStates(int start, int end)
{
   do {
      jjCheckNAdd(jjnextStates[start]);
   } while (start++ != end);
}
static private final void jjCheckNAddStates(int start)
{
   jjCheckNAdd(jjnextStates[start]);
   jjCheckNAdd(jjnextStates[start + 1]);
}
static final long[] jjbitVec0 = {
   0x0L, 0x0L, 0xffffffffffffffffL, 0xffffffffffffffffL
};
static private final int jjMoveNfa_0(int startState, int curPos)
{
   int strKind = jjmatchedKind;
   int strPos = jjmatchedPos;
   int seenUpto;
   input_stream.backup(seenUpto = curPos + 1);
   try { curChar = input_stream.readChar(); }
   catch(java.io.IOException e) { throw new Error("Internal Error"); }
   curPos = 0;
   int[] nextStates;
   int startsAt = 0;
   jjnewStateCnt = 27;
   int i = 1;
   jjstateSet[0] = startState;
   int j, kind = 0x7fffffff;
   for (;;)
   {
      if (++jjround == 0x7fffffff)
         ReInitRounds();
      if (curChar < 64)
      {
         long l = 1L << curChar;
         MatchLoop: do
         {
            switch(jjstateSet[--i])
            {
               case 2:
                  if (curChar == 47)
                     jjstateSet[jjnewStateCnt++] = 0;
                  break;
               case 0:
                  if (curChar != 47)
                     break;
                  if (kind > 6)
                     kind = 6;
                  jjCheckNAdd(1);
                  break;
               case 1:
                  if ((0xffffffffffffdbffL & l) == 0L)
                     break;
                  if (kind > 6)
                     kind = 6;
                  jjCheckNAdd(1);
                  break;
               case 4:
                  if ((0xffffffffffffdbffL & l) != 0L)
                     jjAddStates(0, 2);
                  break;
               case 5:
                  if ((0x2400L & l) != 0L && kind > 12)
                     kind = 12;
                  break;
               case 6:
                  if (curChar == 10 && kind > 12)
                     kind = 12;
                  break;
               case 7:
                  if (curChar == 13)
                     jjstateSet[jjnewStateCnt++] = 6;
                  break;
               case 14:
                  if ((0xffffffffffffdbffL & l) != 0L)
                     jjAddStates(3, 5);
                  break;
               case 15:
                  if ((0x2400L & l) != 0L && kind > 13)
                     kind = 13;
                  break;
               case 16:
                  if (curChar == 10 && kind > 13)
                     kind = 13;
                  break;
               case 17:
                  if (curChar == 13)
                     jjstateSet[jjnewStateCnt++] = 16;
                  break;
               case 25:
                  if ((0x3ff000000000000L & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  jjstateSet[jjnewStateCnt++] = 25;
                  break;
               case 26:
                  if ((0x3ff400000000000L & l) == 0L)
                     break;
                  if (kind > 15)
                     kind = 15;
                  jjstateSet[jjnewStateCnt++] = 26;
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      else if (curChar < 128)
      {
         long l = 1L << (curChar & 077);
         MatchLoop: do
         {
            switch(jjstateSet[--i])
            {
               case 2:
                  if ((0x7fffffe07fffffeL & l) != 0L)
                  {
                     if (kind > 14)
                        kind = 14;
                     jjCheckNAddTwoStates(25, 26);
                  }
                  if ((0x40000000400000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 22;
                  else if ((0x1000000010000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 11;
                  break;
               case 1:
                  if (kind > 6)
                     kind = 6;
                  jjstateSet[jjnewStateCnt++] = 1;
                  break;
               case 3:
                  if ((0x200000002L & l) != 0L)
                     jjCheckNAddStates(0, 2);
                  break;
               case 4:
                  jjCheckNAddStates(0, 2);
                  break;
               case 8:
                  if ((0x200000002000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 3;
                  break;
               case 9:
                  if ((0x8000000080L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 8;
                  break;
               case 10:
                  if ((0x200000002L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 9;
                  break;
               case 11:
                  if ((0x4000000040000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 10;
                  break;
               case 12:
                  if ((0x1000000010000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 11;
                  break;
               case 13:
                  if ((0x400000004000L & l) != 0L)
                     jjCheckNAddStates(3, 5);
                  break;
               case 14:
                  jjCheckNAddStates(3, 5);
                  break;
               case 18:
                  if ((0x800000008000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 13;
                  break;
               case 19:
                  if ((0x20000000200L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 18;
                  break;
               case 20:
                  if ((0x8000000080000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 19;
                  break;
               case 21:
                  if ((0x4000000040000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 20;
                  break;
               case 22:
                  if ((0x2000000020L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 21;
                  break;
               case 23:
                  if ((0x40000000400000L & l) != 0L)
                     jjstateSet[jjnewStateCnt++] = 22;
                  break;
               case 24:
                  if ((0x7fffffe07fffffeL & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  jjCheckNAddTwoStates(25, 26);
                  break;
               case 25:
                  if ((0x7fffffe87fffffeL & l) == 0L)
                     break;
                  if (kind > 14)
                     kind = 14;
                  jjCheckNAdd(25);
                  break;
               case 26:
                  if ((0x7fffffe87fffffeL & l) == 0L)
                     break;
                  if (kind > 15)
                     kind = 15;
                  jjCheckNAdd(26);
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      else
      {
         int i2 = (curChar & 0xff) >> 6;
         long l2 = 1L << (curChar & 077);
         MatchLoop: do
         {
            switch(jjstateSet[--i])
            {
               case 1:
                  if ((jjbitVec0[i2] & l2) == 0L)
                     break;
                  if (kind > 6)
                     kind = 6;
                  jjstateSet[jjnewStateCnt++] = 1;
                  break;
               case 4:
                  if ((jjbitVec0[i2] & l2) != 0L)
                     jjAddStates(0, 2);
                  break;
               case 14:
                  if ((jjbitVec0[i2] & l2) != 0L)
                     jjAddStates(3, 5);
                  break;
               default : break;
            }
         } while(i != startsAt);
      }
      if (kind != 0x7fffffff)
      {
         jjmatchedKind = kind;
         jjmatchedPos = curPos;
         kind = 0x7fffffff;
      }
      ++curPos;
      if ((i = jjnewStateCnt) == (startsAt = 27 - (jjnewStateCnt = startsAt)))
         break;
      try { curChar = input_stream.readChar(); }
      catch(java.io.IOException e) { break; }
   }
   if (jjmatchedPos > strPos)
      return curPos;

   int toRet = Math.max(curPos, seenUpto);

   if (curPos < toRet)
      for (i = toRet - Math.min(curPos, seenUpto); i-- > 0; )
         try { curChar = input_stream.readChar(); }
         catch(java.io.IOException e) { throw new Error("Internal Error : Please send a bug report."); }

   if (jjmatchedPos < strPos)
   {
      jjmatchedKind = strKind;
      jjmatchedPos = strPos;
   }
   else if (jjmatchedPos == strPos && jjmatchedKind > strKind)
      jjmatchedKind = strKind;

   return toRet;
}
static final int[] jjnextStates = {
   4, 5, 7, 14, 15, 17, 
};
public static final String[] jjstrLiteralImages = {
"", null, null, null, null, null, null, null, null, null, null, null, null, 
null, null, null, "\173", "\175", "\50", "\51", "\72", "\52", "\74", "\76", };
public static final String[] lexStateNames = {
   "DEFAULT", 
};
static final long[] jjtoToken = {
   0xffff81L, 
};
static final long[] jjtoSkip = {
   0x7eL, 
};
static private ASCII_CharStream input_stream;
static private final int[] jjrounds = new int[27];
static private final int[] jjstateSet = new int[54];
static protected char curChar;
public SMParserTokenManager(ASCII_CharStream stream)
{
   if (input_stream != null)
      throw new TokenMgrError("ERROR: Second call to constructor of static lexer. You must use ReInit() to initialize the static variables.", TokenMgrError.STATIC_LEXER_ERROR);
   input_stream = stream;
}
public SMParserTokenManager(ASCII_CharStream stream, int lexState)
{
   this(stream);
   SwitchTo(lexState);
}
static public void ReInit(ASCII_CharStream stream)
{
   jjmatchedPos = jjnewStateCnt = 0;
   curLexState = defaultLexState;
   input_stream = stream;
   ReInitRounds();
}
static private final void ReInitRounds()
{
   int i;
   jjround = 0x80000001;
   for (i = 27; i-- > 0;)
      jjrounds[i] = 0x80000000;
}
static public void ReInit(ASCII_CharStream stream, int lexState)
{
   ReInit(stream);
   SwitchTo(lexState);
}
static public void SwitchTo(int lexState)
{
   if (lexState >= 1 || lexState < 0)
      throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.INVALID_LEXICAL_STATE);
   else
      curLexState = lexState;
}

static private final Token jjFillToken()
{
   Token t = Token.newToken(jjmatchedKind);
   t.kind = jjmatchedKind;
   String im = jjstrLiteralImages[jjmatchedKind];
   t.image = (im == null) ? input_stream.GetImage() : im;
   t.beginLine = input_stream.getBeginLine();
   t.beginColumn = input_stream.getBeginColumn();
   t.endLine = input_stream.getEndLine();
   t.endColumn = input_stream.getEndColumn();
   return t;
}

static int curLexState = 0;
static int defaultLexState = 0;
static int jjnewStateCnt;
static int jjround;
static int jjmatchedPos;
static int jjmatchedKind;

public static final Token getNextToken() 
{
  int kind;
  Token specialToken = null;
  Token matchedToken;
  int curPos = 0;

  EOFLoop :
  for (;;)
  {   
   try   
   {     
      curChar = input_stream.BeginToken();
   }     
   catch(java.io.IOException e)
   {        
      jjmatchedKind = 0;
      matchedToken = jjFillToken();
      return matchedToken;
   }

   jjmatchedKind = 0x7fffffff;
   jjmatchedPos = 0;
   curPos = jjMoveStringLiteralDfa0_0();
   if (jjmatchedKind != 0x7fffffff)
   {
      if (jjmatchedPos + 1 < curPos)
         input_stream.backup(curPos - jjmatchedPos - 1);
      if ((jjtoToken[jjmatchedKind >> 6] & (1L << (jjmatchedKind & 077))) != 0L)
      {
         matchedToken = jjFillToken();
         return matchedToken;
      }
      else
      {
         continue EOFLoop;
      }
   }
   int error_line = input_stream.getEndLine();
   int error_column = input_stream.getEndColumn();
   String error_after = null;
   boolean EOFSeen = false;
   try { input_stream.readChar(); input_stream.backup(1); }
   catch (java.io.IOException e1) {
      EOFSeen = true;
      error_after = curPos <= 1 ? "" : input_stream.GetImage();
      if (curChar == '\n' || curChar == '\r') {
         error_line++;
         error_column = 0;
      }
      else
         error_column++;
   }
   if (!EOFSeen) {
      input_stream.backup(1);
      error_after = curPos <= 1 ? "" : input_stream.GetImage();
   }
   throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LEXICAL_ERROR);
  }
}

}