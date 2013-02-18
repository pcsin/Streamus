﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FluentValidation;
using Streamus.Backend.Domain.Validators;

namespace Streamus.Backend.Domain
{        
    //  TODO: Currently there is only the ability to have a single PlaylistCollection.
    //  Should create PlaylistCollection objects as a LinkedList so that adding and removing is possible.
    [DataContract]
    public class PlaylistCollection
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "userId")]
        public Guid UserId
        {
            get { return User.Id; }
            set { User.Id = value; }
        }

        public User User { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        //  Use collection interfaces so NHibernate can inject with its own collection implementation.
        [DataMember(Name = "playlists")]
        public IList<Playlist> Playlists { get; set; }

        public PlaylistCollection()
        {
            Id = Guid.Empty;
            Title = string.Empty;
            Playlists = new List<Playlist>();

            //  A collection should always have at least one Playlist.
            CreatePlaylist();
        }

        public PlaylistCollection(string title) 
            : this()
        {
            Title = title;
        }

        public Playlist CreatePlaylist()
        {
            string title = string.Format("New Playlist {0:D4}", Playlists.Count);
            var playlist = new Playlist(title);
            return AddPlaylist(playlist);
        }

        public Playlist AddPlaylist(Playlist playlist)
        {
            playlist.Collection = this;
            //playlist.CollectionId = Id;
            playlist.Position = Playlists.Count;

            Playlists.Add(playlist);
            return playlist;
        }

        public void RemovePlaylist(Playlist playlist)
        {
            Playlists.Remove(playlist);
        }

        public void ValidateAndThrow()
        {
            var validator = new PlaylistCollectionValidator();
            validator.ValidateAndThrow(this);
        }

        private int? _oldHashCode;
        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;

            bool thisIsTransient = Equals(Id, Guid.Empty);

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can NEVER change its hash code.
            if (thisIsTransient)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PlaylistCollection other = obj as PlaylistCollection;
            if (other == null)
                return false;

            // handle the case of comparing two NEW objects
            bool otherIsTransient = Equals(other.Id, Guid.Empty);
            bool thisIsTransient = Equals(Id, Guid.Empty);
            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }

    }
}