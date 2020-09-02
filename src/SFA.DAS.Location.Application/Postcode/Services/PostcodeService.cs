﻿using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.Postcode.Services
{
    public class PostcodeService : IPostcodeService
    {
        private IPostcodeRepository _repository;

        public PostcodeService(IPostcodeRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SuggestedLocation>> GetLocationsByOuterPostcode(string query, int resultCount)
        {
            // var result = await _repository.GetAllStartingWithOutcode(query, resultCount);
            throw new System.NotImplementedException();
        }
    }
}
