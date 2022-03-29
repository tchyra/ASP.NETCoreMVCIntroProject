using IntroProject.Models;
using IntroProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroProject.Controllers
{
    public class PeopleController : Controller
    {
        // the variable that will hold our context
        private readonly IntroDbContext _ctx;

        // request a context object from the framework
        public PeopleController(IntroDbContext ctx)
        {
            // we got the context, store it for use in actions
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            // load an array of people sorted by first and last name
            var people = _ctx.People
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToArray();

            // pass the people we got to the view
            return View(people);
        }

        // custom route - will match /People/9efd-...
        [Route("/{controller}/{personId:guid}")]
        public IActionResult Details(Guid personId)
        {
            // load a specific person by id
            var person = _ctx.People
                .SingleOrDefault(p => p.Id == personId);

            // alternatively, we could use this to find the enity
            // this might even be the better way to do it,
            // I'm just used to SingleOrDefault() and old habits die hard
            //var person = _ctx.People.Find(personId);

            // check if we found the person
            if (person == null)
            {
                // no person, return a redirect
                // can't show the details of someone who doesn't exist
                return PersonNotFoundRedirect();
            }

            // pass the person we got to the view
            return View(person);
        }

        // this action will run when the user types /People/Create into his/hers browser
        [HttpGet]
        public IActionResult Create()
        {
            // show a form for adding people
            return View();
        }

        // this action will run when the user submits a form with method="post" to /People/Create
        [HttpPost]
        public IActionResult Create(string firstName, string lastName, DateTime dateOfBirth)
        {
            // 1. create entity object
            var newPerson = Person.CreateNew(firstName, lastName, dateOfBirth);

            // 2. attach created entity object to DbContext
            _ctx.People.Add(newPerson);

            // 3. save changes in the context to the database
            _ctx.SaveChanges();

            // inform the user that everything went ok
            TempData["Message"] = "Person successfully created.";

            // redirect the user to show the person that was created
            return RedirectToAction(nameof(Details), new { personId = newPerson.Id });
        }


        // helper function to redirect the user in case the person they wanted was not found in the database
        // it's better to have it in one place than to type it over and over again,
        // especially with complex URLs or if you want to add an error message to TempData every time.
        [NonAction]
        private IActionResult PersonNotFoundRedirect()
        {
            TempData["Message"] = "Nie znaleziono żądanej osoby.";
            return RedirectToAction(nameof(Index)); // nameof() will make sure the name updates, even if we rename the action to something else.
        }
    }
}
