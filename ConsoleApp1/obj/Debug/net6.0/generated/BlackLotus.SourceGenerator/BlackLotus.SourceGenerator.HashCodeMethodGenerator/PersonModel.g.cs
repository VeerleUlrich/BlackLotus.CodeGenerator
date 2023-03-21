namespace ConsoleApp1
            {

                public partial class PersonModel
                {
                    public override int GetHashCode()
                    {
                        return HashCode.Combine(Name, Age, BirthDate);
                    }
                }
            }