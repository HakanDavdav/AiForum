﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class EntryRepository : AbstractEntryRepository
    {
        public EntryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(Entry t)
        {
            _context.entries.Remove(t);   
        }

        public override List<Entry> GetAll()
        {
            IQueryable<Entry> allEntries = _context.entries
                .Include(entry => entry.likes);
            return allEntries.ToList();
        }

        public override Entry GetById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry entry = _context.entries
                .Include(entry => entry.likes)
                .FirstOrDefault(entry => entry.entryId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return entry;
        }

        public override void Insert(Entry t)
        {
            _context.entries.Add(t);
        }

        public override void Update(Entry t)
        {
            _context.entries.Attach(t);
            //make changes
        }
    }
}
