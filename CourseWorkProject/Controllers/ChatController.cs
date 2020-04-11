using CourseWorkProject.DAL;
using CourseWorkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWorkProject.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        CWContext db = new CWContext();
        public ActionResult Index()
        {
           var CurrentName = HttpContext.User.Identity.Name;
            var CurrentUser = db.Users.FirstOrDefault(h => h.UserName == CurrentName);
            if (CurrentUser.Role.id == 2)
            {
                var GroupList = db.Groups.Where(h => h.id == CurrentUser.Group.id).FirstOrDefault();
                var myList = db.Users.Where(h => h.UserName != CurrentName && h.GroupMember.Group.id == GroupList.id).ToList();
                return View(myList);
            }
            var GroupMem = db.GroupMembers.Where(h => h.Group.id == CurrentUser.GroupMember.Group.id).FirstOrDefault();
            var list = db.Users.Where(h => h.UserName != CurrentName && h.GroupMember.Group.id == GroupMem.Group.id || h.UserName != CurrentName && h.Group.id == GroupMem.Group.id).ToList();
            return View(list);
        }
    }
}