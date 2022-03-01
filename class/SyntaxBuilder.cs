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

        public ArgumentSyntaxBuilder MakeRegular(string Input, ArgumentRegularOptions Options)
        {
            string Title = Options.Title;
            bool TitleExists = !String.IsNullOrEmpty(Title);

            bool Inf = Options.Infinite;
            bool Opt = Options.Optional;

            if (!Opt)
            {
                this._Append($"<{(TitleExists ? $"{Title}: " : "")}{Input}{(Inf ? "..." : "")}>");
            }
            else
            {
                this._Append($"<?{(TitleExists ? $"{Title}: " : "")}{Input}{(Inf ? "..." : "")}>");
            }

            return this;
        }

        public ArgumentSyntaxBuilder MakeChoice(string[] Inputs, ArgumentChoiceOptions Options)
        {
            string Title = Options.Title;
            bool TitleExists = !String.IsNullOrEmpty(Title);

            string Def = Options.Default;
            bool DefExists = !String.IsNullOrEmpty(Def);

            bool Inf = Options.Infinite;
            bool Opt = Options.Optional;
            bool Exactify = Options.Exactify;

            if (DefExists && Array.IndexOf(Inputs, Def) == -1)
            {
                throw new ArgumentException("Default Input not found in array");
            }

            if (Exactify)
            {
                Inputs = (Inputs.Select((el) => $"\"{el}\"")).ToArray();
            }

            string Joined = String.Join(" | ", Inputs);

            if (!Opt)
            {
                this._Append($"<{(TitleExists ? $"{Title}: " : "")}{Joined}{(DefExists ? $" def = \"{Def}\"" : "")}{(Inf ? "..." : "")}>");
            }
            else
            {
                this._Append($"<?{(TitleExists ? $"{Title}: " : "")}{Joined}{(DefExists ? $" def = \"{Def}\"" : "")}{(Inf ? "..." : "")}>");
            }

            return this;
        }

        public string EndBuild()
        {
            if (String.IsNullOrEmpty(this._Build))
            {
                throw new InvalidOperationException("No build started");
            }

            string OldBuild = this._Build;
            this._Build = "";
            return OldBuild;
        }
    }
}