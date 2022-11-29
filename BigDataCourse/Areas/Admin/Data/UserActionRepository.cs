﻿using BigDataCourse.Areas.Admin.Data.Interface;
using BigDataCourse.Areas.Admin.Models;
using BigDataCourse.Areas.Admin.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BigDataCourse.Areas.Admin.Data
{
    public class UserActionRepository: IUserActionRepository
    {
        private readonly DBContext _context = null;

        public UserActionRepository(IOptions<DbSettings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task<IEnumerable<UserActions>> GetAll()
        {
            try
            {
                List<UserActions> lst = await _context.UserActions.Find(_ => true)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<UserActions> Create(UserActions item)
        {
            try
            {
                await _context.UserActions.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
            return item;
        }
    }
}
