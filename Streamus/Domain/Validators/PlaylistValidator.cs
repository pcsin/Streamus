﻿using System;
using FluentValidation;

namespace Streamus.Domain.Validators
{
    public class PlaylistValidator : AbstractValidator<Playlist>
    {
        public PlaylistValidator()
        {
            RuleFor(playlist => playlist.Title).Length(0, 255);

            RuleFor(playlist => playlist.NextPlaylist).NotNull();
            RuleFor(playlist => playlist.PreviousPlaylist).NotNull();

            //RuleFor(playlist => playlist.NextListId)
            //    .NotEqual(Guid.Empty)
            //    .When(playlist => playlist.Id != Guid.Empty);

            //RuleFor(playlist => playlist.PreviousListId)
            //    .NotEqual(Guid.Empty)
            //    .When(playlist => playlist.Id != Guid.Empty);
        }
    }
}