using QuorumTest.Model;

namespace QuorumTest.Mappers
{
    public sealed class PersonMap : BaseMap<Person>
    {
        public PersonMap() 
        {
            Map(x => x.Id).Index(0).Name("id");
            Map(x => x.Name).Index(1).Name("name");
        }  
    }
}
