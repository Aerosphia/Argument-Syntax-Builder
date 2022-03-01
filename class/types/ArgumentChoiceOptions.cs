namespace CS.ArgumentSyntaxBuilder
{
    public class ArgumentChoiceOptions
    {
        public bool Optional { get; set; } = false;
        public bool Infinite { get; set; } = false;
        public string Title { get; set; } = "";
        public bool Exactify { get; set; } = false;
        public string Default { get; set; } = "";

        public void SetDefaults()
        {
            Optional = false;
            Infinite = false;
            Title = "";
            Exactify = false;
            Default = "";
        }
    }
}