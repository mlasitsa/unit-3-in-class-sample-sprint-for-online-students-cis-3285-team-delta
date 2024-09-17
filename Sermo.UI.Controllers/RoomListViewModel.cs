using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sermo.UI.Contracts;

// Changes for Sprint 2 -- I want to filter message content so that it is appropriate -- Max Lasitsa
// Pseudo-code: Implement filtering logic during message creation

namespace Sermo.UI.ViewModels
{
    public class RoomListViewModel
    {
        public RoomListViewModel(IEnumerable<RoomViewModel> rooms)
        {
            Rooms = new List<RoomViewModel>(rooms);
        }

        public List<RoomViewModel> Rooms
        {
            get;
            private set;
        }
    }
}