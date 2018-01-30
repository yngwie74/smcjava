namespace smc.builder
{
    public interface FSMBuilderErrorManager
    {

        void error(string str);

        void error(SyntaxLocation sl, string str);

    }
}