using static System.Console;
using CS.ArgumentSyntaxBuilder;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            ArgumentSyntaxBuilder SyntaxBuilder = new ArgumentSyntaxBuilder();
            string[] DMUserChoices = { "yes", "no" };

            string Syntax = SyntaxBuilder.MakeRegular("User", new ArgumentRegularOptions { })
                .MakeChoice(DMUserChoices, new ArgumentChoiceOptions { Optional = true, Exactify = true, Default = "yes" })
                .MakeRegular("reason", new ArgumentRegularOptions { Optional = true, Infinite = true })
                .EndBuild();

            WriteLine(Syntax);
        }
    }
}