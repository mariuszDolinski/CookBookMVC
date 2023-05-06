using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient
{
    public class EditRecipeIngridientCommand : RecipeIngridientDto, IRequest
    {
        public int RecipeId { get; set; } = default!;
    }
}
