This Fork was created with the intent of moving the code into C#, in order to use it with T4 templates in VS projects.

It is not intended to add features or even improve the existing functionality (for the time being.)

---

# The Care and Feeding of the State Map Compiler

by Robert Martin R.C.M. Consulting Inc. 4 June, 1993

* Java Version created 11 June, 1998 by Bhama Rao
* updated 17 July, 2001 by Micah Martin Object Mentor, Inc.
* updated 10 Aug, 2004 by Paul Pagel and Jake Scruggs Object Mentor, Inc.

This paper describes 'smc', a program which compiles state transition tables into C++ or Java classes. Finite State Machines are an important part of computer application analysis and design. However, their expression in "computer code" is often muddied by artifacts of the language and the application. The state map compiler allows the control elements of an application to be clearly and succinctly described in isolation from the program code.

Finite state machines are an important part of the analysis and design of computer applications. A great many applications can be described by using one or more FSMs. When the control model of a program is expressed as an FSM, it makes it easier to search for error cases and alternatives.

Finite State Machines are described by tables called "State Maps" or "State Transition Tables". A typical State map looks something like this:

Subway Turnstyle State Transition Table

|Current State | Transition | Next State |  Actions |
|--------------|------------|------------|----------|
|Locked        | Coin       | Unlocked   |  Unlock  |
|              | Pass       | Locked     |   Alarm  |
|Unlocked      | Pass       | Locked     |   Lock   |
|              | Coin       | Unlocked   | ThankYou |

This State Map examines the control model of a Subway Turnstyle. The machine can have two states (`Locked` and `Unlocked`) and accepts two events. The `Coin` event indicates that someone has deposited a coin. The `Pass` event indicates that someone has "passed through" the turnstyle. There are also four actions or behaviors that the machine invokes. It can lock the turnstyle, unlock the turnstyle, sound an alarm, and thank the user for extra money.

The State map is interpreted in the following manner. When the turnstyle is in the `Locked` state, and a `Coin` event is received, the machine transitions to the `Unlocked` state and invokes the `Unlock` behavior. On the other hand, if the `Pass` event is received, it indicates that someone has forced their way through the turnstyle, so the machine rings an alarm.

When the turnstyle is in the unlocked state and it receives a `Pass` event, then the machine transitions back to the Locked state, and invokes the `Lock` action. However, if the Coin event is received in the Unlocked state, then someone has deposited extra money into the turnstyle, and the machine, politely, says "Thank You".

Now imagine a C++ class which implements the behaviors of the turnstyle:

```cpp
// ----------------------------tscontext.h------------------------------
class TurnStyleContext
{
    public:
        void Lock();
        void Unlock();
        void Alarm();
        void ThankYou();
};
```

This is a class that you write in order to capture all the behavior
demanded by the state machine. When you call the `Lock` member function, it
locks the turnstyle. When you call the `Alarm` member function, it sounds an
alarm. Every action of the state machine is present as a member function
of this class.

Our state machine can be implemented in by feeding the following text file
into smc...

```
// ---------------------------turnstyle.sm--------------------------------
Context TurnStyleContext  // the name of the context class
FSMName TurnStyle         // the name of the FSM to create
Initial Locked            // the name of the initial state
                          // for C++ output
pragma Header tscontext.h // the header file name for the context class
{
    Locked
    {
        Coin Unlocked Unlock
        Pass Locked   Alarm
    }
    Unlocked
    {
        Coin Unlocked Thankyou
        Pass Locked   Lock
    }
}
```

If C++ output is generated, SMC outputs two files. `turnstyle.cpp` and `turnstyle.h`. These implement a class whose name is: `TurnStyle`, from the `FSMName` above. The definition of this class, from `turnStyle.h`, is:

```cpp
// -------------------------turnStyle.h (abbreviated)----------------------
class TurnStyle : public TurnStyleContext
{
    public:
        static TurnStyleUnlockedState UnlockedState;
        static TurnStyleLockedState LockedState;
        void Pass() {itsState->Pass(*this);}
        void Coin() {itsState->Coin(*this);}
        void SetState(TurnStyleState& theState) {itsState=&theState;}
        TurnStyleState& GetState() const {return *itsState;};
        private:
        TurnStyleState* itsState;
};
```

Notice, first, that the class `TurnStyle` inherits from the `TurnStyleContext`. So it has all the behaviors that we need. Also notice that it declares member functions for the event codes. There is a member function for `Pass` and one for `Coin`. Finally notice that two static members have been declared, one for each state that the machine can be in. These are derived from the common base state: `TurnStyleState`. The state that the machine is in is determined by which of these static members the 'itsState' member points at.

Using the class `TurnStyle`, we can write our turnstyle application as follows.

```cpp
// ----------------------turnstylemain.cpp-------------------------------

#include "turnStyle.h"

main()
{
    TurnStyle fsm;
    fsm.Lock(); // Make sure the gate is locked.

    for(;;)
    {
        if (a coin has been dropped) fsm.Coin();
        if (the user passes) fsm.Pass();
    }
}
```

Notice that all the control has been externalized. All the application does is look for events and feed them into the FSM. The FSM takes the events invokes the correct behaviors inherited from `TurnStyleContext`.

How does all this magic work? Lets look at some more of `turnStyle.h`

```cpp
// ------------------------ excerpts from turnstyle.h ----------------
class TurnStyleState {
    public:
        virtual const char* StateName() const = 0;
        virtual void Pass(TurnStyle& s);
        virtual void Coin(TurnStyle& s);
};
```

This class represents the base class for all states. Notice that it has virtual functions for each event; i.e. a `Pass` and `Coin` function. The `StateName` function is there as a debugging and error handling tool.

Next look at the definition of the two states.

```cpp
class TurnStyleUnlockedState : public TurnStyleState {
    public:
        virtual const char* StateName() const
        {return("Unlocked");};
        virtual void Pass(TurnStyle&);
        virtual void Coin(TurnStyle&);
};

class TurnStyleLockedState : public TurnStyleState {
    public:
        virtual const char* StateName() const
        {return("Locked");};
        virtual void Pass(TurnStyle&);
        virtual void Coin(TurnStyle&);
};
```

These two classes declare the virtual functions again, and implement the `StateName` function. Notice that a reference to `TurnStyle` is passed into each event. What do the virtual functions do? Here's an example.

```cpp
// --------------------- excerpt from turnstyle.cpp --------------------
void TurnStyleLockedState::Coin(TurnStyle& s) {
    s.SetState(TurnStyle::UnlockedState);
    s.Unlock();
}
```

When the `Coin` function of the `TurnStyleLockedState` class is invoked, it will change the state of the FSM to `UnlockedState` and will invoke the `Unlock` function of the `TurnStyle` object.

You see? The _virtual_ functions of each state object change the state and invoke the appropriate behavior.

Now lets trace this from the beginning. Lets say that the FSM is in the Locked state. Then the itsState member of the `TurnStyle` class will point to the `LockedState` object which is a static instance of the `TurnStyleLockedState` class. When the application detects that a coin has been deposited, it calls the `Coin` function of the `TurnStyle` object. This function in turn invokes: itsState->Coin(this), which calls the `Coin` function of the `TurnStyleLockedState` class. This function changes the state and invokes the appropriate behavior as previously described.

The rest of the implemenations of the virtual functions are described below.

```cpp
// -------------------- more excerpts from turnstyle.cpp ------------------
void TurnStyleUnlockedState::Pass(TurnStyle& s) {
    s.SetState(TurnStyle::LockedState);
    s.Lock();
}
void TurnStyleUnlockedState::Coin(TurnStyle& s) {
    s.SetState(TurnStyle::UnlockedState);
    s.Thankyou();
}
void TurnStyleLockedState::Pass(TurnStyle& s) {
    s.SetState(TurnStyle::LockedState);
    s.Alarm();
}
```

The class `TurnStyleState` also implements the virtual event functions. These implemenations are there in the unlikely case that the application declares an event that the current state cannot understand. In this case the default implementation from `TurnStyleState` will be invoked.

```cpp
// ------------------ final exerpt from turnStyle.cpp --------------------
void TurnStyleState::Pass(TurnStyle& s)
 {s.FSMError("Pass", s.GetState().StateName());}

void TurnStyleState::Coin(TurnStyle& s)
 {s.FSMError("Coin", s.GetState().StateName());}
```

Notice that these functions expect that the `TurnStyle` class has a member function entitled "FSMError" which takes two `char*` arguments. This member function is not written by SMC. You must supply it in your base context class. Thus we should rewrite the base context class as follows.

```cpp
------------------------tscontext.h---------------------------------
class TurnStyleContext
{
    public:
        void Lock();
        void Unlock();
        void Alarm();
        void ThankYou();
        void FSMError(char*, char*);
};
```

The first argument will be the name of the event. The second argument will be the name of the current state. Detecting such an error is a serious thing, and should probably result in an abort.

## SMC SYNTAX

The State Map source file is a straight ascii file. It is meant to be typed by a human. Comments can appear enclosed in `/*` and `*/`, or after a `//` to the end of a line.

```/* this is a comment */```

```// so is this```

States, events and actions are given names. These names must begin with an alphabetic character, and every subsequent character must be alphabetic, numeric or a `_` (underscore).

At the top of the State map source file is a section containing keywords. This is followed by the left brace `{`, then the actual state machine definition and at the end the closing right brace `}`.

e.g.

```
Keyword1
Keyword2
...
{
    State1
    State2
}
```

## KEYWORDS and HEADER INFORMATION

The header information is specified by using the keywords. They specify contextual information that the parser needs to properly build the C++ or Java source files. The keywords can appear in any order, but must be the first things in the file, other than comments.

The required keywords are: `FSMName`, `Context` and `Initial`. Optional keywords are `Version`, `Generator`, `Exception` and `Pragma`.

### FSMName

The word following FSMName specifies the name of the statemap. This name is used to create the name of the Finite State Machine class. If the source file `mystatemap.sm` contains :

```FSMName MyStateMap```

The class for the Finite State Machine will be named "MyStateMap". If C++ output is generated, the two files created will be called `mystatemap.cpp` and `mystatemap.h` (from `mystatemap.sm`). If Java output is generated, the file created will be called `MyStateMap.java` (from the name of the State Machine class).

### Context

The name following this keyword specifies the class name of the context data structure. This is the data structure that encapsulates all the Finite State Machine's behaviors, and from which the Finite State Machine will be derived. Remember that this class needs member functions for each of the Actions in the FSM, and must also have an `FSMError(char*, char*)` function, if the `Exception` keyword is not used.

### Initial

The name following this keyword is taken to be the initial state of the finite state machine. The default constructor of the FSMName class is generated to set the initial state accordingly.

### Version

Takes all the text following the keyword and puts it in a static char array in the C++ output file. This can be used for SCCS id strings which will be compiled into the object files and therefore accessible via "sccs what"

e.g. ```Version 3.4 TurnStyle 6/4/93 by rcm```

This will put the following line into the .cpp file generated by smc:

```static char _versID[] = "Version 3.4 TurnStyle 6/4/93 by rcm";```

In addition, there will be a `GetVersion` function generated for the State Machine class that returns this string.

### Generator

The name following this keyword is the fully qualified Java class name of the code generator to be used to generate the output. e.g.

* `Generator smc.generator.java.SMJavaGenerator` or
* `Generator smc.generator.cpp.SMCPPGenerator`

The first statement will run the Java code generator which results in the creation of .java files. The second statement results in the creation of the C++ files .cpp and .h.

This keyword can be overriden on the command line, so, it is optional in the State Machine source file.

### Exception

The name of an exception class follows this keyword. Instead of calling the `FSMError` function as described above, this exception will be thrown. The exception class must be implemented - ie. it is not generated by smc.

This keyword is optional. If it is not specified, `FSMError` is called. In this case, `FSMError` must be defined in the Context class.

### Pragma

This keyword precedes additional keywords that are specific to the output generators. Any of these keywords maybe specified in the source file. If a generator does not use a particular Pragma keyword, it is simply ignored.

#### Pragma Header (for the C++ code generator)

The keyword `Header` is followed by the name of a file. You may have many of these statements in your statemap file. Each one specifies a header file that will be `#include`'d into the .h output file. One, at least, is necessary. It must specify the header file which contains the definition of the class named by the Context keyword. If the `Exception` keyword is used, the header file associated with that class should also be specified.

#### Pragma Using (for the C# code generator)
Simililar to `Header`, it is the name of the namespace(s) which are being used. This keyword is optional.

#### Pragma Import (for the Java code generator)

This is similar to the `Header` keyword. The `Import` keyword is followed by the name of a Java class to be imported in the State machine class. You may have many of these statements in your statemap file. At least the Context class must be imported. If the `Exception` keyword is used, the class specified after the `Exception` keyword must be imported as well. This keyword is optional.

#### Pragma Namespace (for the C++ and C# code generators)

The keyword `Namespace` is followed by a name which will be the namespace that the State Machine class will belong to. This keyword is optional.

#### Pragma Package (for the Java code generator)

The keyword `Package` is followed by a name which is the Java package that the State Machine class will belong to. This keyword is optional.

## TRANSITION ENTRIES

Following the initial header in the source file, there must be an open brace `{`, followed by the state definitions, followed by a final closing brace `}`.

State definitions take the form:

```
currentState
{
    Transition1
    Transition2
    ...
}
```

There may be 0 or more Transition entries. If there are no Transition entries, this is a final state.

Transition entries take the form:

`event` `nextState` `action`

Actions may be grouped so that a single transition will cause several actions to be performed. This form is as follows:

`event` `nextState` `{ action action ... }`

Sometimes a transition will elicit no action. This can be specified in the following manner:

`event` `nextState` `{ }`

Sometimes the transition is internal and there is no state change. This can be specified as follows:

`event` `*` `action`

or event

`CurrentState action`

A state may have _Entry_ and _Exit actions_. The _Entry action_ is executed every time a transition is made into that state. Entry actions are specifed as follows:

```
currentState <entryAction
{
    ...
}
```

or

```
currentState <{entryAction1 entryAction2}
{
    ...
}
```

The _Exit action_ is executed every time a transition is made out of that state. Exit actions are specified as follows:

```
currentState >exitAction
{
    ...
}
```

or

```
currentState >{exitAction1 exitAction2}
{
    ...
}
```

You may specify both Entry and Exit actions for a given State.


## SUBSTATES

Sometimes you will find that certain states are nearly identical in terms of the way that they process events. For example:

```
Angry
{
    Ouch   Angry   Cry
    Tickle Annoyed Laugh
    Stroke Annoyed Withdraw
}
Sad
{
    Ouch   Angry   Cry
    Tickle Annoyed Laugh
    Stroke Pleased StrokeBack
}
```

These two states are identical except for the way they process strokes. It seems a shame to have to recode the Ouch and Tickle transitions for both states. A substate is a state which inherits the behavior from a super state. In SMC we can code this as:

```
(Emotional)
{
    Ouch   Angry   Cry
    Tickle Annoyed Laugh
}

Angry : Emotional
{
    Stroke Annoyed Withdraw
}

Sad : Emotional
{
    Stroke Pleased StrokeBack
}
```

The parentheses denote a super state, and the colon denotes state inheritance. `Angry` is a sub-state of `Emotional`. `Emotional` is a super state.

Super states cannot be used as the target state of a transition. i.e.

```
Happy
{
    Hit Emotional Pout
}
```

Does not contain a valid transition, because `Emotional` is a superstate being used as the target of a transition.

Super states can also be substates:

```
(X) : Y {...}
```

Thus you can create a huge tree of states and their substates.

### ISSUING EVENTS FROM ACTIONS

Sometimes, it is nice to be able to issue an event from within an action function. For example: You have an action function named `Open`. It opens a file. If the file opens correctly, you would like to issue the "OK" event. But if the file fails to open you would like to issue the "Fail" event.

Unfortunately, the action functions are members of the context class, from which the FSM is derived. The context class does not know anything about the Events. So if you tried to call `OK` or `Fail`, the compiler would complain.

To solve this problem, make the `OK` and `Fail` members of the context class virtual. Then derive a new class from the `FSMName` class and reimplement them there. Since this new class is derived from the finite state machine, it will have knowledge of the Event functions.

EXAMPLE:

```cpp
class FileContext
{
    public:
        virtual void Open() = 0;
};
```
----------------------------
```
Context FileContext
FSMName FileFSM
{
....transitions.
}
```
----------------------------
```cpp
class FileMachine : public FileFSM
{
    public:
        virtual void Open() {
            if (it works) OK();
            else Fail();
        }
}
```

By using this method, you keep all the code that knows about the FSM in the classes derived from the context. The context knows nothing of the FSM (except the `FSMError` function).

## A COMPLETE EXAMPLE

The `Stripper` program example strips the comments out of C, C++ or Java programs. The source files for both the C++ and the Java code for this example should be included in the distribution.

The files required are:

* `stripfsm.sm` - the Finite State Machine source
* `stContext.h` - context class for C++ code
* `stripper.cpp` - the main program for C++ code
* `StripperContext.java` - context class for Java code
* `Stripper.java` - the main class for Java code
* `FSMException.java` - example Java Exception class

## RUNNING SMC

This version of Smc is written in java. To run it, you will need to have JDK 1.1.5 with the Collections Package installed. The `CLASSPATH` should include the JDK, the Collections package and the directory where Smc is installed.

Smc has the following command line arguments:

```
java smc.Smc [-o outputdir] [-f] [-g generator] stripFSM.sm
```

where:

* `-o` is optional (defaults to `.`), it specifies the output directory.
* `-f` is optional, forces overwrite of existing files, if not specified user will be queried.
* `-g` is optional, overrides the generator in the source file.

> **Note**: `-g` uses the class name. If you are creating Java code, don't
> worry about it; Java is the default.
>
> * To generate C++ code use `smc.generator.cpp.SMCppGenerator`
> * To generate C# code use `smc.generator.csharp.SMCSharpGenerator`


## INSTALLATION

This version of Smc is written in Java. It was compiled with JDK 1.5


## LEGALESE

This software is free. Use if for whatever you like. I provide no warranty or guarantee of any kind. Use the software at our own risk. If you have any ideas for improvements... "just leave a message, maybe I'll call."

----
Robert C. Martin (Uncle Bob) | email: unclebob@objectmentor.com  
Object Mentor Inc. | blog: www.butunclebob.com  
The Agile Transition Experts | web: www.objectmentor.com  
800-338-6716 |  
Questions please send to smc@objectmentor.com
