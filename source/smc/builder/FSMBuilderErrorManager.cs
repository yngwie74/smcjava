namespace SMC.Builder
{
    public interface FSMBuilderErrorManager
    {
        void Error(SyntaxLocation loc, string s);
        void Error(string s);
    }
}