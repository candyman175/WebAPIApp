using System;

namespace WebApiSimpleParms.Models
{
    public class Person: IEquatable<Person>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Equals (Person other)
        {
            return (Id == other.Id);
        }
    }
}