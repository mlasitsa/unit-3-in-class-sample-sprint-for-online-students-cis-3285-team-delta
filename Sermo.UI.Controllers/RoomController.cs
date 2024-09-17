using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

using Sermo.UI.Contracts;
using Sermo.UI.ViewModels;
using System.Net;

namespace Sermo.UI.Controllers
{
    public class RoomController : Controller
    {
        public RoomController(IRoomViewModelReader reader, IRoomViewModelWriter writer)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(writer != null);

            this.reader = reader;
            this.writer = writer;
        }
        
        [HttpGet]
        public ActionResult List()
        {
            var roomListViewModel = new RoomListViewModel(reader.GetAllRooms());
            
            return View(roomListViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new RoomViewModel());
            // Changes Sprint 1 --  I want to view a list of rooms that represent 
            //conversations. -- Trish Valeri

            // Changes for Sprint 1 -- I want to view a list of rooms that represent 
            // conversations -- Max Lasitsa
            // Pseudo-code: Implement logic to create a new room for conversations
            // Example code:
            /*
            [HttpPost]
            public ActionResult Create(Room room)
            {
                if (ModelState.IsValid)
                {
                    // Logic to save room to the database
                    return RedirectToAction("Index");
                }
                return View(room);
            }
            */
        }

        [HttpPost]
        public ActionResult Create(RoomViewModel model)
        {
            ActionResult result;
 
            if(ModelState.IsValid)
            {
                writer.CreateRoom(model);

                result = RedirectToAction("List");
            }
            else
            {
                result = View("Create", model);
            }

            return result;
        }

        [HttpGet]
        public ActionResult Messages(int roomID)
        {
            var messageListViewModel = new MessageListViewModel(reader.GetRoomMessages(roomID));

            return View(messageListViewModel);
        }

        // Changes for Sprint 2 -- I want to send markdown text that will be correctly formatted -- Max Lasitsa
        // Pseudo-code: Modify constructor to handle markdown formatted text

        [HttpPost]
        public ActionResult AddMessage(MessageViewModel messageViewModel)
        {
            ActionResult result;

            if(ModelState.IsValid)
            {
                writer.AddMessage(messageViewModel);

                result = Json(messageViewModel);
            }
            else
            {
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return result;
        }

        private readonly IRoomViewModelReader reader;
        private readonly IRoomViewModelWriter writer;
    }
}