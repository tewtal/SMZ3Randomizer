using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Randomizer.Shared.Models
{
    public enum SessionEventType
    {
        ItemFound,          // Item found, can be used for both single and multiworld
        ItemRequest,        // Request what an item at given location is
        ChatMessage,        // Chat Message
        SystemMessage,      // Chat Message (from system)
        Forfeit,            // A player wants to forfeit for themselves
        ForfeitVote,        // Initializes a forfeit vote (at least 2 players need to do this unless the seed is 2 player only)
        Status,             // Generic status update (can be used for tracking purposes)
        Other               // Undefined other message
    }

    [Index(nameof(ToWorldId), nameof(EventType))]
    public class SessionEvent
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SequenceNum { get; set; }
        public int FromWorldId { get; set; }
        public int ToWorldId { get; set; }
        public int ItemId { get; set; }
        public int ItemLocation { get; set; }
        public SessionEventType EventType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public bool Confirmed { get; set; }
    }
}
