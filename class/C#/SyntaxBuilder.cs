/*
    This is the C# version of the library. There is also a JavaScript verison.
    Just messing around for now. Library isn't finished.
    The implementation and usage for this library is in Program.cs
*/

using System.Collections.Generic;

namespace CSVersion.SyntaxBuilder
{
    public class ArgumentSyntaxBuilder
    {
        // To be done later..
        private string build = "";
        private void append(string input)
        {
            if (String.IsNullOrEmpty(this.build))
            {
                this.build = input;
            }
            else
            {
                this.build = $"{this.build} {input}";
            }
        }

        public ArgumentSyntaxBuilder makeRegular(string input, Dictionary<string, dynamic> options)
        {
            return this;
        }
    }
}