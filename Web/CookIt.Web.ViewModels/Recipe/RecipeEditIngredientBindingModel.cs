﻿namespace CookIt.Web.ViewModels.Recipe
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CookIt.Data.Models;
    using CookIt.Services.Mapping;

    public class RecipeEditIngredientBindingModel : IMapFrom<RecipeIngredient>, IMapTo<RecipeIngredient>, IValidatableObject
    {
        [Range(1, 9999999, ErrorMessage = "You should choose a valid ingredient.")]
        public int IngredientId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The ingredient count must be a positive number.")]
        public int? Count { get; set; }

        [Range(1.0, double.MaxValue, ErrorMessage = "The ingredient weight must be a positive number.")]
        public double? Weight { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.Count == null && this.Weight == null)
            {
                yield return new ValidationResult("One of the fields Count or Weight must be filled.");
            }

            if (this.Count != null && this.Weight != null)
            {
                yield return new ValidationResult("Only one of the fields Count or Weight must be filled.");
            }
        }
    }
}
