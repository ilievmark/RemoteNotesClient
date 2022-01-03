using System;
using RemoteNotes.Domain.Models;
using RemoteNotes.Domain.Services.ViewModel;

namespace RemoteNotes.UI.ViewModel.Notes
{
    public class NoteViewModel : BindableBase
    {
        public NoteViewModel(NoteModel note)
        {
            UpdateFieldsFromModel(note);
        }
        
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _authorUserId;
        public int AuthorUserId
        {
            get => _authorUserId;
            set => SetProperty(ref _authorUserId, value);
        }

        private string _topic;
        public string Topic
        {
            get => _topic;
            set => SetProperty(ref _topic, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get => _photoUrl;
            set => SetProperty(ref _photoUrl, value);
        }

        private DateTime _publishTime;
        public DateTime PublishTime
        {
            get => _publishTime;
            set => SetProperty(ref _publishTime, value);
        }

        private DateTime? _lastModifyTime;
        public DateTime? LastModifyTime
        {
            get => _lastModifyTime;
            set => SetProperty(ref _lastModifyTime, value);
        }
        
        public NoteModel Note
        {
            get => MakeNoteModel();
            set => UpdateFieldsFromModel(value);
        }

        private NoteModel MakeNoteModel()
        {
            return new NoteModel
            {
                Id = Id,
                AuthorUserId = AuthorUserId,
                Topic = Topic,
                Text = Text,
                PhotoUrl = PhotoUrl,
                PublishTime = PublishTime,
                LastModifyTime = LastModifyTime
            };
        }

        private void UpdateFieldsFromModel(NoteModel note)
        {
            Id = note.Id;
            AuthorUserId = note.AuthorUserId;
            Topic = note.Topic;
            Text = note.Text;
            PhotoUrl = note.PhotoUrl;
            PublishTime = note.PublishTime;
            LastModifyTime = note.LastModifyTime;
        }
    }
}