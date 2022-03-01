namespace CS.ArgumentSyntaxBuilder
{
    public class ArgumentRegularOptions
    {
        public bool Optional { get; set; } = false;
        public bool Infinite { get; set; } = false;
        public string Title { get; set; } = "";

        public void SetDefaults()
        {
            Optional = false;
            Infinite = false;
            Title = "";
        }
    }
}