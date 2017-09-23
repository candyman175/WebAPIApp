using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiSimpleParms.Models;

namespace WebApiSimpleParms.Controllers
{
    //NOTE: Always had to return a http status code of "OK" (or "Created") to get the controller to return a value to the javascript on the HTML page.
    //In some cases, passed a null.
    public class DefaultController : ApiController
    {
        List<Person> _people = new List<Person>();
        private readonly HttpContext _context = HttpContext.Current;

        /// <summary>
        /// Normally you would use a data layer to retrieve the information from a database
        /// </summary>
        private void Repository()
        {
            if (_context.Session["people"] == null)
            {
                _people.Add(new Person { Id = 1, Name = "John", Age = 24 });
                _people.Add(new Person { Id = 2, Name = "Jane", Age = 26 });
                _people.Add(new Person { Id = 3, Name = "Eric", Age = 4 });
                _people.Add(new Person { Id = 4, Name = "Bob", Age = 12 });
                _context.Session["people"] = _people;
            }
        }

        private void RetrievePeople()
        {
            Repository();
            _people = _context.Session["people"] as List<Person>;
        }  
      
        /// <summary>
        /// Create an in-memory record using Session
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddPerson(Person person)
        {
            RetrievePeople();
            if (_people != null)
            {
                _people.Add(person);
                _context.Session["people"] = _people;
            }
            return Request.CreateResponse(HttpStatusCode.Created, person);
        }
        /// <summary>
        /// Update an in-memory record: delete and add
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage UpdatePerson(Person person)
        {
            RetrievePeople();
            if (_people != null)
            {
                var idx = _people.IndexOf(person);
                if (idx >= 0)
                {                   
                    _people.RemoveAt(idx);
                    _people.Insert(idx, person);
                    _context.Session["people"] = _people;                    
                }
                else
                {
                    person = null;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
        /// <summary>
        /// Delete the specified person (only required the Id of the person)
        /// Note: Requires the implementation of a comparer in the Person class
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public HttpResponseMessage DeletePerson(Person person)
        {
            RetrievePeople();
            if (_people != null)
            {
                var idx = _people.IndexOf(person);
                if (idx >= 0)
                {
                    _people.RemoveAt(idx);
                    _context.Session["people"] = _people;                    
                }
                else
                {
                    person = null;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }        

        ///
        //GET: api/Default/5
        /// <summary>
        /// Retrieve a specific person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetPerson(int id)
        {
            RetrievePeople();
            Person selectedPerson = null;
            if (_people != null)
            {
                selectedPerson = _people.FirstOrDefault(p => p.Id == id);                
            }
            return Request.CreateResponse(HttpStatusCode.OK, selectedPerson);
        }

        /// <summary>
        /// Retrieve all records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetPeople()
        {
            RetrievePeople();            
            return Request.CreateResponse(HttpStatusCode.OK, _people);
        }
    }
}
