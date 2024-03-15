namespace Management_System.Infrustructure.MappingProfile
{
    public class MiladiToShamsiConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return "DateTime";
        }

    }

    public class ShamsiToMiladiConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return DateTime.Now;
        }
    }
}

