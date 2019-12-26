﻿using System.Collections.Generic;
using System.Linq;

namespace Api
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookshelfContext _context;

        public CategoryRepository(BookshelfContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories
                .ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories
                .Single(c => c.Id == id);
        }

        public int Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category.Id;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories
                .Single(c => c.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}