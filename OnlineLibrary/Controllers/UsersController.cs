﻿using OnlineLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Domain;
using Omu.ValueInjecter;
using OnlineLibrary.Models;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Controllers
{
  public class UsersController : Controller
  {
    private IUserService service;

    public UsersController(IUserService service)
    {
      this.service = service;
    }

    // GET: Users
    public ActionResult Index(int? page, string sort="", string sortdir="")
    {
        const int pageSize = 2;
        var pageNumber = page.HasValue ? page.Value : 1;

        ViewBag.Count = service.Count();
        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = ViewBag.Count / pageSize;
        ViewBag.PageSize = pageSize;


      List<UserViewModel> userList = new List<UserViewModel>();
      var userDbList = service.GetList(pageNumber, pageSize, "FirstName","Asc");
      userList.InjectFrom(userDbList);

      return View(userList);
    }

    // GET: Users/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: Users/Create
    public ActionResult Create()
    {
      CreateUserViewModel model=new CreateUserViewModel();
      
      return View(model);
        }


    // POST: Users/Create
    [HttpPost]
    public ActionResult Create(CreateUserViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //use the service to insert in db
                    User newUser = new User();
                    newUser.Salt = Guid.NewGuid().ToString();
                    string hashedPassword = Security.HashSHA1(newUser.Password + newUser.Salt);
                    newUser.Password = hashedPassword;

                    newUser.InjectFrom(model);

                    service.CreateUser(newUser);

                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Users/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add update logic here

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    // GET: Users/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Users/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }
  }
}
