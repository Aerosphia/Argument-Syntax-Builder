using System.Linq;
using System.Collections.Generic;
using CSVersion.SyntaxBuilder;

namespace CSVersion.SyntaxBuilder
{
    public class ArgumentSyntaxBuilder
    {
        // To be done later..
        private string build = "";
        private void _append(string input)
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

        public ArgumentSyntaxBuilder makeRegular(string input, ArgumentRegularOptions options)
        {
            string title = options.Title;
            bool titleExists = !String.IsNullOrEmpty(title);

            bool inf = options.Infinite;
            bool opt = options.Optional;

            if (!opt)
            {
                this._append($"<{(titleExists ? $"{title}: " : "")}{input}{(inf ? "..." : "")}>");
            }
            else
            {
                this._append($"<?{(titleExists ? $"{title}: " : "")}{input}{(inf ? "..." : "")}>");
            }

            return this;
        }

        public ArgumentSyntaxBuilder makeChoice(string[] inputs, ArgumentChoiceOptions options)
        {
            string title = options.Title;
            bool titleExists = !String.IsNullOrEmpty(title);

            string def = options.Default;
            bool defExists = !String.IsNullOrEmpty(def);

            bool inf = options.Infinite;
            bool opt = options.Optional;
            bool exactify = options.Exactify;

            if (defExists && Array.IndexOf(inputs, def) == -1)
            {
                throw new ArgumentException("Default input not found in array");
            }

            if (exactify)
            {
                inputs = (inputs.Select((el) => $"\"{el}\"")).ToArray();
            }

            string Joined = String.Join(" | ", inputs);

            if (!opt)
            {
                this._append($"<{(titleExists ? $"{title}: " : "")}{Joined}{(defExists ? $" def = \"{def}\"" : "")}{(inf ? "..." : "")}>");
            }
            else
            {
                this._append($"<?{(titleExists ? $"{title}: " : "")}{Joined}{(defExists ? $" def = \"{def}\"" : "")}{(inf ? "..." : "")}>");
            }

            return this;
        }

        public string endBuild()
        {
            if (String.IsNullOrEmpty(build))
            {
                throw new InvalidOperationException("No build started");
            }

            string oldBuild = this.build;
            this.build = "";
            return oldBuild;
        }
    }
}