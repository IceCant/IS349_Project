using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS349_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IS349_Library.Controllers
{
    public class BooktypeController : Controller
    {
        private readonly Dbcontext _context;
        public BooktypeController()
        {
            _context = new Dbcontext();
        }
        // GET: Booktype
        public ActionResult Index()
        {
            var booktype = _context.ReadData("SELECT * FROM Table_booktype");
            var booktypes = new List<Booktype>();
            while (booktype.Read())
            {
                booktypes.Add(new Booktype
                {
                    BooktypeId = int.Parse(booktype[0].ToString()),
                    BooktypeName = booktype[1].ToString()
                });
            }
            booktype.Close();
            return View(booktypes);
        }

        public JsonResult Search(string q)
        {
            q = q.ToLower();
            var booktype = _context.GetType(q);
            var types = new List<Booktype>();
            while (booktype.Read())
            {
                types.Add(new Booktype
                {
                    BooktypeId = int.Parse(booktype[0].ToString()),
                    BooktypeName = booktype[1].ToString()
                });
            }
            booktype.Close();
            return Json(types);
        }

        // GET: Booktype/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booktype/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booktype/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booktype booktype)
        {
            try
            {
                if (string.IsNullOrEmpty(booktype.BooktypeName))
                {
                    ViewData["Exist"] = "Booktype name field is required";
                    return View(booktype);
                }
                bool exs = _context.Is_exist("SELECT * FROM Table_booktype WHERE booktype_name='" + booktype.BooktypeName + "'");
                if (!exs)
                {
                    var sql = "INSERT INTO Table_booktype(booktype_name) VALUES('" + booktype.BooktypeName + "')";
                    if (_context.ExecuteQuery(sql))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ViewData["Exist"] = "The Booktype ("+ booktype.BooktypeName +") is already Exist";
                }
                return View(booktype);
            }
            catch
            {
                return View();
            }
        }

        // GET: Booktype/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booktype/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Booktype/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booktype/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}