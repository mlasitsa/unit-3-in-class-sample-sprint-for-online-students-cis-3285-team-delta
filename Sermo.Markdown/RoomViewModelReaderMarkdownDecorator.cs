using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sermo.UI.Contracts;

// Changes for Sprint 2 -- As a system administrator, I want to serve hundreds of users concurrently. -- Max Lasitsa
// Pseudo-code: Handle concurrent requests when creating messages

namespace Sermo.Markdown
{
    using Markdown=MarkdownDeep.Markdown;

    public class RoomViewModelReaderMarkdownDecorator : IRoomViewModelReader
    {
        public RoomViewModelReaderMarkdownDecorator(IRoomViewModelReader @delegate, Markdown markdown)
        {
            this.@delegate = @delegate;
            this.markdown = markdown;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            return @delegate.GetAllRooms();
        }

        public IEnumerable<MessageViewModel> GetRoomMessages(int roomID)
        {
            var roomMessages = @delegate.GetRoomMessages(roomID);

            foreach(var viewModel in roomMessages)
            {
                viewModel.Text = markdown.Transform(viewModel.Text);
            }

            return roomMessages;
        }

        private readonly IRoomViewModelReader @delegate;
        private readonly Markdown markdown;
    }
}
