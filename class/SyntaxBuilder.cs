using System.Linq;
using System.Collections.Generic;

namespace CS.ArgumentSyntaxBuilder
{
    public class ArgumentSyntaxBuilder
    {
        private string _Build = "";
        private void _Append(string Input)
        {
            if (String.IsNullOrEmpty(this._Build))
            {
                this._Build = Input;
            }
            else
            {
                this._Build = $"{this._Build} {Input}";
            }
        }

        public ArgumentSyntaxBuilder makeRegular(string Input, ArgumentRegularOptions options)
        {
            string title = options.Title;
            bool titleExists = !String.IsNullOrEmpty(title);

            bool inf = options.Infinite;
            bool opt = options.Optional;

            if (!opt)
            {
                this._Append($"<{(titleExists ? $"{title}: " : "")}{Input}{(inf ? "..." : "")}>");
            }
            else
            {
                this._Append($"<?{(titleExists ? $"{title}: " : "")}{Input}{(inf ? "..." : "")}>");
            }

            return this;
        }

        public ArgumentSyntaxBuilder makeChoice(string[] Inputs, ArgumentChoiceOptions options)
        {
            string title = options.Title;
            bool titleExists = !String.IsNullOrEmpty(title);

            string def = options.Default;
            bool defExists = !String.IsNullOrEmpty(def);

            bool inf = options.Infinite;
            bool opt = options.Optional;
            bool exactify = options.Exactify;

            if (defExists && Array.IndexOf(Inputs, def) == -1)
            {
                throw new ArgumentException("Default Input not found in array");
            }

            if (exactify)
            {
                Inputs = (Inputs.Select((el) => $"\"{el}\"")).ToArray();
            }

            string Joined = String.Join(" | ", Inputs);

            if (!opt)
            {
                this._Append($"<{(titleExists ? $"{title}: " : "")}{Joined}{(defExists ? $" def = \"{def}\"" : "")}{(inf ? "..." : "")}>");
            }
            else
            {
                this._Append($"<?{(titleExists ? $"{title}: " : "")}{Joined}{(defExists ? $" def = \"{def}\"" : "")}{(inf ? "..." : "")}>");
            }

            return this;
        }

        public string endBuild()
        {
            if (String.IsNullOrEmpty(this._Build))
            {
                throw new InvalidOperationException("No build started");
            }

            string oldBuild = this._Build;
            this._Build = "";
            return oldBuild;
        }
    }
}