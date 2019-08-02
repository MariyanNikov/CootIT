﻿namespace CookIt.Web.ViewModels.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CookIt.Data.Models;
    using CookIt.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CheckoutInputModel : IMapTo<Order>, IHaveCustomMappings, IValidatableObject
    {
        private const string ErrorMessageDeliveryDate = "Delivery Date must be at least one day after the day you are placing the order.";

        [Required]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow.AddDays(1);

        [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(61, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        public int AddressId { get; set; }

        [BindNever]
        public string IssuerId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CheckoutInputModel, Order>()
                 .ForMember(x => x.CommentIssuer, opt => opt.MapFrom(c => c.Comment))
                 .ForMember(x => x.FullNameIssuer, opt => opt.MapFrom(c => c.FullName));
        }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.DeliveryDate.Day <= DateTime.UtcNow.Day)
            {
                yield return new ValidationResult(ErrorMessageDeliveryDate);
            }
        }
    }
}
