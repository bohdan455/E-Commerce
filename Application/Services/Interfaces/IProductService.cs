﻿using Application.Dto;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductBriefInformation>> GetAll();
        Task<ProductFullInfromation> GetById(int id);
    }
}