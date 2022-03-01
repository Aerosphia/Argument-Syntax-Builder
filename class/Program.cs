using static System.Console;
using CS.ArgumentSyntaxBuilder;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            ArgumentSyntaxBuilder SyntaxBuilder = new ArgumentSyntaxBuilder();
            string[] dmUserChoices = { "yes", "no" };

            string Syntax = SyntaxBuilder.makeRegular("User", new ArgumentRegularOptions { })
                .makeChoice(dmUserChoices, new ArgumentChoiceOptions { Optional = true, Exactify = true, Default = "yes" })
                .makeRegular("reason", new ArgumentRegularOptions { Optional = true, Infinite = true })
                .endBuild();

            WriteLine(Syntax);
        }
    }
}