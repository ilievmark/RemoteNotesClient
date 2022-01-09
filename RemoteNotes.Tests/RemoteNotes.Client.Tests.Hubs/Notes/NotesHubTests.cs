using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Models;
using Xunit;

namespace RemoteNotes.Client.Tests.Hubs.Notes
{
    public class NotesHubTests
    {
        [Fact]
        public async Task GetNotes_Check()
        {
            //Arrange
            var hub = NotesHubFactory.GetHub();
            
            //Act
            var notes = await hub.GetNotesAsync();
            
            //Assert
            notes.Count().Should().Be(2);
        }
        
        [Fact]
        public async Task PutNote_Check()
        {
            //Arrange
            var hub = NotesHubFactory.GetHub();
            
            //Act
            await hub.PutNoteAsync(new NoteModel {Id = 3} );
            var notes = await hub.GetNotesAsync();
            
            //Assert
            notes.Count().Should().Be(3);
        }
        
        [Fact]
        public async Task DeleteNote_Check()
        {
            //Arrange
            var hub = NotesHubFactory.GetHub();
            
            //Act
            var notes = await hub.GetNotesAsync();
            await hub.DeleteNoteAsync(notes.First()); 
            notes = await hub.GetNotesAsync();
            
            //Assert
            notes.Count().Should().Be(1);
        }
        
        [Fact]
        public async Task UpdatedNote_Check()
        {
            //Arrange
            var hub = NotesHubFactory.GetHub();
            var count = 0;
            hub.NoteStorageUpdate += (change, model) => count++;
            
            //Act
            var notes = await hub.GetNotesAsync();
            var json = notes.First().ToJson();
            await hub.HandleMessageAsync(json);
            
            //Assert
            count.Should().Be(1);
        }
    }
}