﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Bookshelf
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryValidator _validator;

        public CategoriesController(ICategoryRepository categoryRepository, CategoryValidator validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        // GET api/categories
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return _categoryRepository.GetAll().Result;
        }

        // POST api/categories
        [HttpPost]
        public ActionResult<Category> Post([FromBody] Category category)
        {
            var validation = _validator.Validate(category);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToString());
            }
            int id = _categoryRepository.Add(category).Result;
            return _categoryRepository.Get(id).Result;
        }

        // PUT api/categories
        [HttpPut]
        public ActionResult<Category> Put([FromBody] Category category)
        {
            var validation = _validator.Validate(category);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToString());
            }
            _categoryRepository.Update(category);
            return _categoryRepository.Get(category.Id).Result;
        }

        // DELETE api/categories
        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.Get(id).Result;
            if (category.Id == default)
            {
                return BadRequest($"Category with id {id} not found.");
            }
            _categoryRepository.Delete(category);
            return category;
        }
    }
}