using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Models.Reviews
{
    public class SingleReviewViewModel
    {
        public SingleReviewViewModel()
        {
            
        }

        public SingleReviewViewModel(Guid? placeId, string userId, string username)
        {
            PlaceId = placeId;
            UserId = userId;
            Username = username;
        }

        [Required]
        public Guid? PlaceId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Message { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}