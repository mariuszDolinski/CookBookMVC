﻿using AutoMapper;
using CookBook.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.RecipeUtils.Queries.GetRecipeById
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeByIdQueryHandler(IMapper mapper, IRecipeRepository recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<RecipeDto> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetRecipeById(request.Id);
            var dto = _mapper.Map<RecipeDto>(recipe);

            return dto;
        }
    }
}
