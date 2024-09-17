using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sermo.UI.Contracts;


// Changes for Sprint 1 --  I want to view a list of rooms that represent conversations -- Max Lasitsa
// Pseudo-code: Display a list of rooms and their details

namespace Sermo.UI.ViewModels
{
    public class MessageListViewModel
    {
        public MessageListViewModel(IEnumerable<MessageViewModel> messages)
        {
            Messages = new List<MessageViewModel>(messages);
        }

        public List<MessageViewModel> Messages
        {
            get;
            private set;
        }
    }
}