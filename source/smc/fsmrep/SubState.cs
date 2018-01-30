namespace smc.fsmrep
{
    public interface SubState : State
    {
        SuperState getSuperState();
    }
}